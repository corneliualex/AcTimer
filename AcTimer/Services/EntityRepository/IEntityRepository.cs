

using System.Collections.Generic;
using AcTimer.Models;

namespace AcTimer.Services.EntityRepository
{
    //used by Activites and Categories controllers to avoid duplicate code
    public interface IEntityRepository<T> where T : class
    {
        T GetById(int? id);
        T GetDetails(int? id);
        IEnumerable<T> GetAll();
        bool Delete(int? id);
        void NewOrUpdate(T T);
    }
}