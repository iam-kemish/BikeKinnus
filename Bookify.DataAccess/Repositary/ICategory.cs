using Bookify.Models.Models;

namespace Bookify.Repositary
{
    public interface ICategory:IRepositary<Category>
    {
        void Update(Category category);
        void Save();
    }
}
