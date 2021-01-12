using System.Threading.Tasks;
using Car.DAL.Entities;

namespace Car.BLL.Services.Interfaces
{
    public interface IJourneyService
    {
        Journey GetJourneyById(int id);
    }
}
