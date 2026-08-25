namespace Application.Interfaces;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    IProductRepository Product { get; }
    IOrderRepository Order { get; }
    IOrderProductRepository OrderProduct { get; }

    void Save();
    Task SaveAsync();
    
}