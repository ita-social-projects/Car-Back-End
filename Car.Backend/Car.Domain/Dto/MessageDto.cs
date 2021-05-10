﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int SenderId { get; set; }

        public string SenderName { get; set; }

        public string SenderSurname { get; set; }

        public string ImageId { get; set; }
    }
}