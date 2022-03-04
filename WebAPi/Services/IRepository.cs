using System.Collections.Generic;

namespace WebAPi.Services
{
    public interface IRepository<T>
    {
        // create entity new object.
        void AddEntity(T entity);

        // return Entity if found if not return null.
        T GetEntity(int id, bool includeRelatedEntity);

        // Return list of entity if found.
        IEnumerable<T> GetEntities();

        //Save entity data in database.
        void Save();

        // check if entity exist.
        bool EntityExist(int id);

    }
}
