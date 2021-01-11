using Car.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Car.BLL.Services.Interfaces
{
    public interface IUserChatsManager
    {
        IQueryable<Chat> GetUserChats(int userId);
    }
}
