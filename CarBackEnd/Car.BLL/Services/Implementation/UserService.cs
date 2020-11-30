using Car.BLL.Services.Interfaces;
using Car.DAL.Interfaces;
using Google.Apis.Drive.v3.Data;

namespace Car.BLL.Services.Implementation
{
    class UserService : IUserService<File>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICreatorDrive<File> userDrive;

        public UserService(IUnitOfWork unitOfWork, ICreatorDrive<File> userDrive)
        {
            this.unitOfWork = unitOfWork;
            this.userDrive = userDrive;
        }
    }
}
