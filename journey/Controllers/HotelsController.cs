using System.Security.Claims;
using journey.Data;
using journey.Data.Dto;
using journey.Models;
using journey.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace journey.Controllers;

[Authorize]
public class HotelsController : CoreController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly PhotoService _photoService;
    public HotelsController(ApplicationDbContext dbContext, UserManager<User> userManager, PhotoService photoService)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _photoService = photoService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetHotels([FromQuery] string? location = null)
    {
        var hotels = await _dbContext.Hotels
            .Where(h => location == null || h.Location.ToLower().Contains(location.ToLower()))
            .ToListAsync();
        var hotelDtos = hotels.Select(h => HotelToDto(h));
        return Ok(hotelDtos);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetHotel(Guid id)
    {
        var hotel = await _dbContext.Hotels
            .Include(h => h.Rooms)
            .ThenInclude(r => r.Photos)
            .Include(h => h.Ratings.OrderByDescending(r => r.CreatedAt))
            .AsSplitQuery()
            .FirstOrDefaultAsync(h => h.Id == id);
        if (hotel is null) return BadRequest(new { error = "Hotel does not exist" });
        return Ok(HotelToDto(hotel));

    }
    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public async Task<ActionResult> AddHotel(HotelDto hotelDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return BadRequest("User does not exist");

        var hotel = new Hotel
        {
            Title = hotelDto.Title,
            Location = hotelDto.Location,
            Description = hotelDto.Description,
            LocationOnMap = hotelDto.LocationOnMap,
            Email = hotelDto.Email,
            Phone = hotelDto.Phone,
            AddedBy = user.Name,
        };

        _dbContext.Hotels.Add(hotel);
        if (await _dbContext.SaveChangesAsync() > 0)
            return Ok(HotelToDto(hotel));
        return BadRequest(new { error = "Could not Add Hotel" });
    }
    [HttpPost("add-room")]
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public async Task<ActionResult> AddRoom([FromForm] RoomDto roomDto)
    {
        // Must be a valid user
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return BadRequest("User does not exist");

        // Get the hotel
        var hotel = await _dbContext.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == roomDto.HotelId);
        if (hotel is null) return BadRequest(new { error = "Hotel does not exist" });

        // Photo upload
        if (roomDto.UploadPhotos.Count > 3)
            return BadRequest("Can't add more than 3 photos");
        var uploadedPhotos = new List<Photo>();
        foreach (var photo in roomDto.UploadPhotos)
        {
            var photoResult = await _photoService.AddPhotoAsync(photo);
            if (photoResult.Error != null)
                return BadRequest(photoResult.Error.Message);
            var newPhoto = new Photo
            {
                ImageUrl = photoResult.SecureUrl.AbsoluteUri,
                PublicId = photoResult.PublicId
            };
            uploadedPhotos.Add(newPhoto);
        }

        // set the thumbnail to the room
        if (uploadedPhotos.Count > 0)
            hotel.ThumbnailUrl = uploadedPhotos[0].ImageUrl;

        // Create the room.
        var room = new Room
        {
            Price = roomDto.Price,
            Category = roomDto.Category,
            Photos = uploadedPhotos,
            AC = roomDto.AC,
            Description = roomDto.Description
        };
        hotel.Rooms.Add(room);

        if (await _dbContext.SaveChangesAsync() > 0)
        {
            return Ok(RoomToDto(room));
        }
        return BadRequest(new { error = "Could not Add Room" });
    }
    [HttpPost("rate")]
    public async Task<ActionResult> RateHotel(RatingDto ratingDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound();
        var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == ratingDto.HotelId);
        if (hotel is null) return NotFound();
        var rating = new Rating
        {
            Text = ratingDto.Text.Trim(),
            Stars = ratingDto.Stars,
            User = user,
            UserName = user.Name
        };
        hotel.Ratings.Add(rating);
        if (await _dbContext.SaveChangesAsync() > 0)
            return Ok(RatingToDto(rating));
        return BadRequest("Rating failed");
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public async Task<ActionResult> DeleteHotels()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound();

        var hotels = await _dbContext.Hotels
            .Include(h => h.Rooms)
            .ThenInclude(r => r.Photos)
            .ToListAsync();

        foreach (var hotel in hotels)
        {
            foreach (var room in hotel.Rooms)
            {
                foreach (var photo in room.Photos)
                {
                    await _photoService.DeletePhotoAsync(photo.PublicId);
                }
            }
        }

        _dbContext.RemoveRange(hotels);

        if (await _dbContext.SaveChangesAsync() > 0)
            return Ok();
        return BadRequest("Failed to delete hotels");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public async Task<ActionResult> DeleteHotel(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound();

        var hotel = await _dbContext.Hotels
            .Include(h => h.Ratings)
            .Include(h => h.Rooms)
            .ThenInclude(r => r.Photos)
            .FirstOrDefaultAsync(h => h.Id == id);
        if (hotel is null) return BadRequest("Invalid Hotel ID");

        foreach (var room in hotel.Rooms)
        {
            foreach (var photo in room.Photos)
            {
                await _photoService.DeletePhotoAsync(photo.PublicId);
            }
        }

        _dbContext.Hotels.Remove(hotel);

        if (await _dbContext.SaveChangesAsync() > 0)
            return Ok();
        return BadRequest("Failed to delete hotel");
    }
    private static RoomDto RoomToDto(Room room)
    {
        var roomDto = new RoomDto
        {
            Id = room.Id,
            Category = room.Category,
            Price = room.Price,
            HotelId = room.HotelId,
            Description = room.Description
        };
        if (room.Photos is not null)
            roomDto.Photos = room.Photos.Select(PhotoToDto).ToList();
        return roomDto;
    }
    private static PhotoDto PhotoToDto(Photo photo)
    {
        return new PhotoDto
        {
            Id = photo.Id,
            ImageUrl = photo.ImageUrl,
            PublicId = photo.PublicId
        };
    }
    private static RatingDto RatingToDto(Rating rating)
    {
        return new RatingDto
        {
            Id = rating.Id,
            UserId = rating.UserId,
            UserName = rating.UserName,
            Text = rating.Text,
            Stars = rating.Stars,
            HotelId = rating.HotelId
        };
    }
    private static HotelDto HotelToDto(Hotel hotel)
    {
        var hotelDto = new HotelDto
        {
            Id = hotel.Id,
            Title = hotel.Title,
            Location = hotel.Location,
            Description = hotel.Description,
            LocationOnMap = hotel.LocationOnMap,
            Phone = hotel.Phone,
            Email = hotel.Email,
            ThumbnailUrl = hotel.ThumbnailUrl,
            AddedBy = hotel.AddedBy
        };
        if (hotel.Rooms is not null)
            hotelDto.Rooms = hotel.Rooms.Select(RoomToDto).ToList();
        if (hotel.Ratings is not null)
            hotelDto.Ratings = hotel.Ratings.Select(RatingToDto).ToList();
        return hotelDto;
    }

}
