using SiteCatering.Domain.Repositories.Abstract;

namespace SiteCatering.Domain
{
    public class DataManager
    {
        public virtual IDishRepository DishRepository { get; set; }

        public DataManager(IDishRepository dishRepository)
        {
            DishRepository = dishRepository;
        }
    }
}
