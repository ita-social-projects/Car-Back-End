using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;

namespace Car.Domain.Services.Interfaces
{
    public interface IFirebaseService
    {
        public Task<string> SendAsync(Message message);
    }
}
