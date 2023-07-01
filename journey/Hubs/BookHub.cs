using System.Security.Claims;
using journey.Data;
using journey.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace journey.Hubs;

[Authorize]
public class BookHub : Hub
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    public BookHub(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async override Task OnConnectedAsync()
    {
        var userId = Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await Clients.User(userId).SendAsync("Connected", userId);
    }
    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await Clients.User(userId).SendAsync("Disconnected", userId);
        await base.OnDisconnectedAsync(exception);
    }

}
