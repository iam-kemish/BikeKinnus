using BikeKinnus.Models.Models;

namespace BikeKinnus.Repositary
{
    public interface ICategory:IRepositary<Category>
    {
        void Update(Category category);
        void Save();
    }
}
