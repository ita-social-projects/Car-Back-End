using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string ChatName { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
