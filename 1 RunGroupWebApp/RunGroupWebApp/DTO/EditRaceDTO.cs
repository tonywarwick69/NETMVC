﻿using RunGroupWebApp.Data.Enum;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.DTO
{
    public class EditRaceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public int AddressId { get; set; }  
        public Address Address { get; set; }
    }
}
