using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Repostories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        void Add(T entity);
        T GetById(int id);
        List<T> GetAll();
        void Delete(T entity);
        int Commit();
    }
}