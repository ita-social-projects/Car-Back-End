using Car.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Car.BLL.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public DateTime HireDate { get; set; }

        public string Email { get; set; }

        public string ImageId { get; set; }

        public string Token { get; set; }
    }
}
