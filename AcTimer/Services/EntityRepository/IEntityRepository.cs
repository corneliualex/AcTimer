

namespace AcTimer.Services.EntityRepository
{
    public interface IEntityRepository<T> where T : class
    {
        T getById(int? id);
    }
}