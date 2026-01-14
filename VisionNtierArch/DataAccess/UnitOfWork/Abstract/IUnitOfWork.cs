
namespace DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get;}
        Task SaveAsync();
    }
}
