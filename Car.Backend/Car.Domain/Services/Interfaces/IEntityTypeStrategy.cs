using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IEntityTypeStrategy<in TEntity>
        where TEntity : IEntityWithImage
    {
        string GetFolderId();

        string GetCredentialFilePath();

        string GetFileName(TEntity entity);
    }
}
