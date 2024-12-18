using BookStoreAPI.Models;

namespace BookStoreAPI.Repository
{
    public class GenericRepository<T> where T : class
    {
        BookStoreContext db;
        public GenericRepository(BookStoreContext context)
        {
            db = context;
        }

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            db.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            T entity = db.Set<T>().Find(id);
            db.Set<T>().Remove(entity);
        }
    }
}
