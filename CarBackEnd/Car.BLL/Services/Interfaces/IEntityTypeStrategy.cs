using Car.DAL.Entities;

namespace Car.BLL.Services.Interfaces
{
    public interface IEntityTypeStrategy<in TEntity>
        where TEntity : IEntityWithImage
    {
        string GetFolderId();

        string GetCredentialFilePath();

        string GetFileName(TEntity entity);
    }
}
