using Car.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserChatsManager
    {
        List<Chat> GetUserChats(int userId);
    }
}
