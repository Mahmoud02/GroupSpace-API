using GroupSpace.BLL;
using GroupSpace.BLL.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace GroupSpaceApi.Hubs
{
    [AllowAnonymous]
    public class GroupChat : Hub
    {
        private readonly IUserService userService;
        public GroupChat(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task NewMessage(ChatDto msg)
        {
            await Clients.All.SendAsync("MessageReceived", msg);
        }

        public async Task SendMessage(string group, ChatDto msg)
        {
            var user = userService.Find(msg.Sub);
            msg.UserName = user.UserName;
            msg.PhotoUrl = user.PersonalImageUrl;
            msg.Date = DateTime.Now;
            await Clients.Group(group).SendAsync("MessageReceived", msg);
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            string msg = $"{Context.ConnectionId} has joined the group {groupName}.";
            ChatDto chat = new() { Message = msg, Type ="sent", Date = DateTime.Now };
            await Clients.Group(groupName).SendAsync("MessageReceived", chat);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
