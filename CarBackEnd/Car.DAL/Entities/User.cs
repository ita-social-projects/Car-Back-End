using System;
using System.Collections.Generic;
using System.Text;

namespace Car.DAL.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public DateTime HireDate { get; set; }
        public string Email { get; set; }
    }
}
