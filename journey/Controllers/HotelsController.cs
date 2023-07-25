using journey.Data;
using journey.Data.Dto;
using journey.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace journey.Controllers;

[Authorize]
public class HotelsController : CoreController
{
    private readonly ApplicationDbContext _dbContext;
    public HotelsController(ApplicationDbContext dbContext)
    {
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
    public async Task<ActionResult> GetHotels(Guid id)
    {
        var hotel = await _dbContext.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == id);
        if (hotel is null) return BadRequest(new { error = "Hotel does not exist" });
        return Ok(HotelToDto(hotel));

    }
    [HttpPost]
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
        return hotelDto;
    }

}
