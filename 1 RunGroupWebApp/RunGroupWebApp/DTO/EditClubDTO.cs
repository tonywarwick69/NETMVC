using RunGroupWebApp.Data.Enum;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.DTO
{
    public class EditClubDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public String ImageURL { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
