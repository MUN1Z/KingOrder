namespace KingOrder.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        void Commit();

        void Dispose();
    }
}
