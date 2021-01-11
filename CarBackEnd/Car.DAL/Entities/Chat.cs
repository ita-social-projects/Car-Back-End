using System;
using System.Collections.Generic;
using System.Text;

namespace Car.DAL.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string ChatName { get; set; }

        public IEnumerable<UserChat> Users { get; set; }
    }
}
