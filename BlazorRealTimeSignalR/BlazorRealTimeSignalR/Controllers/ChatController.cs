using BlazorRealTimeSignalR.Repos;
using ChatModels;
using ChatModels.DTOs;
using ChatModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorRealTimeSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ChatRepo chatRepo) : ControllerBase
    {
        [HttpGet("group-chats")]
        public async Task<IActionResult> GetGroupChatsAsync() => Ok(await chatRepo.GetGroupChatsAsync());
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync() => Ok(await chatRepo.GetAvailableUsersAsync());
        [HttpPost("individual")]
        public async Task<IActionResult> GetIndividualChatsAsync(RequestChatDTO requestChatDTO) 
        {
            return Ok(await chatRepo.GetIndividualChatsAsync(requestChatDTO));
        }
    }
}
