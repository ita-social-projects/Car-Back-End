namespace Car.BLL.Services.Interfaces
{
    public interface ICreatorDrive<TFile>
    {
        IDriveService<TFile> CarDriveService { get; }

        IDriveService<TFile> AvatarDriveService { get; }
    }
}
