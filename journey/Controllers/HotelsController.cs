using System.Security.Claims;
using journey.Data;
using journey.Data.Dto;
using journey.Models;
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
    public HotelsController(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
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
            .Include(h => h.Ratings.OrderByDescending(r => r.CreatedAt))
            .FirstOrDefaultAsync(h => h.Id == id);
        if (hotel is null) return BadRequest(new { error = "Hotel does not exist" });
        return Ok(HotelToDto(hotel));

    }
    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public async Task<ActionResult> AddHotel(HotelDto hotelDto)
    {
        var hotel = hotelDto.ToHotel();
        _dbContext.Hotels.Add(hotel);
        if (await _dbContext.SaveChangesAsync() > 0)
            return Ok(HotelToDto(hotel));
        return BadRequest(new { error = "Could not Add Hotel" });
    }
    [HttpPost("add-room")]
    public async Task<ActionResult> AddRoom(RoomDto roomDto)
    {
        var room = new Room
        {
            Price = roomDto.Price,
            Catrgory = roomDto.Catrgory
        };
        var hotel = await _dbContext.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == roomDto.HotelId);
        if (hotel is null) return BadRequest(new { error = "Hotel does not exist" });
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
    private RoomDto RoomToDto(Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Catrgory = room.Catrgory,
            Price = room.Price,
            HotelId = room.HotelId
        };
    }
    private RatingDto RatingToDto(Rating rating)
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
    private HotelDto HotelToDto(Hotel hotel)
    {
        var hotelDto = new HotelDto
        {
            Id = hotel.Id,
            Title = hotel.Title,
            Location = hotel.Location,
            LocationOnMap = hotel.LocationOnMap,
            Description = hotel.Description,
            Phone = hotel.Phone,
            Email = hotel.Email
        };
        if (hotel.Rooms is not null)
            hotelDto.Rooms = hotel.Rooms.Select(r => RoomToDto(r)).ToList();
        if (hotel.Ratings is not null)
            hotelDto.Ratings = hotel.Ratings.Select(r => RatingToDto(r)).ToList();
        return hotelDto;
    }

}
