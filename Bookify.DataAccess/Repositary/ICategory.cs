using Bookify.Models;

namespace Bookify.Repositary
{
    public interface ICategory:IRepositary<Category>
    {
        void update(Category category);
        void Save();
    }
}
