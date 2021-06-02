﻿using Car.Data.Enums;

namespace Car.Domain.Dto
{
    public class StopDto
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public StopType Type { get; set; }

        public AddressDto Address { get; set; }

        public int UserId { get; set; }
    }
}
