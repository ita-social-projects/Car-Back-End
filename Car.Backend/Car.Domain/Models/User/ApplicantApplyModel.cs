using System.Collections.Generic;
using Car.Domain.Models.Stops;

namespace Car.Domain.Models.User
{
    public class ApplicantApplyModel
    {
        public JourneyUserModel? JourneyUser { get; set; }

        public ICollection<StopModel> ApplicantStops { get; set; } = new List<StopModel>();
    }
}
