﻿using RunGroupWebApp.Data.Enum;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.DTO
{
    public class CreateClubDTO
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image {  get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
