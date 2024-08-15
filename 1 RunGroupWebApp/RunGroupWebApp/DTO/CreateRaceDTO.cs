using RunGroupWebApp.Data.Enum;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.DTO
{
    public class CreateRaceDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public Address Address { get; set; }    
    }
}
