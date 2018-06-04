

using System.Collections.Generic;
using AcTimer.Models;

namespace AcTimer.Services.EntityRepository
{
    //used by Activites and Categories controllers to avoid duplicate code
    public interface IEntityRepository<T> where T : class
    {
        T GetById(int? id);
        IEnumerable<T> GetAll();
        bool IsDeleted(int? id);
        bool IsNewOrUpdate(T T);
    }
}