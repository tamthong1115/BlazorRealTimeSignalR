using BlazorRealTimeSignalR.Repos;
using ChatModels;
using ChatModels.DTOs;
using ChatModels.Models;
using Microsoft.AspNetCore.SignalR;
using System;

namespace BlazorRealTimeSignalR.ChatHubs
{
    public class ChatHub(ChatRepo chatRepo) : Hub
    {
        public async Task SendMessageToGroup(GroupChat chat)
        {
            var saveChatDTO = await chatRepo.AddChatToGroupAsync(chat);
            await Clients.All.SendAsync("ReceiveGroupMessages", saveChatDTO);
        }

        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            var availableUsers = await chatRepo.AddAvailableUser(availableUser);
            await Clients.All.SendAsync("NotifyAllClients", availableUsers);
        }


        public async Task RemoveUser(string userId)
        {
            var availableUsers = await chatRepo.RemoveUserAsync(userId);
            await Clients.All.SendAsync("NotifyAllClients", availableUsers);
        }

        public async Task AddIndividualChat(IndividualChat individualChat)
        {
            await chatRepo.AddIndividualChatAsync(individualChat);
            var requestDTO = new RequestChatDTO()
            {
                SenderId = individualChat.SenderId,
                ReceiverId = individualChat.ReceiverId,
            };

            var getChats = await chatRepo.GetIndividualChatsAsync(requestDTO);
            var prepareIndividualChat  = new IndividualChatDTO()
            {
                SenderId = individualChat.SenderId,
                ReceiverId = individualChat.ReceiverId,
                Message = individualChat.Message,
                Date = individualChat.Date,
                SenderName = getChats.Where(x => x.SenderId == individualChat.SenderId).FirstOrDefault()!.SenderName,
                ReceiverName = getChats.Where(x => x.ReceiverId == individualChat.ReceiverId).FirstOrDefault()!.ReceiverName
            };

            await Clients.User(individualChat.ReceiverId!).SendAsync("ReceiveIndividualMessage", prepareIndividualChat);
            // Save the chat to the database or perform other actions
            
        }
    }
}
