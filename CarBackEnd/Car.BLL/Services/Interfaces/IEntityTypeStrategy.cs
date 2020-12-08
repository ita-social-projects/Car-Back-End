using Car.DAL.Entities;

namespace Car.BLL.Services.Interfaces
{
    public interface IEntityTypeStrategy<TEntity>
        where TEntity : IEntityWithImage
    {
        string GetFolderId();

        string GetCredentialFilePath();

        string GetFileName(TEntity entity);
    }
}
