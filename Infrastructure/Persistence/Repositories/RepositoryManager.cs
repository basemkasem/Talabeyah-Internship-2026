using Application.Interfaces;

namespace Infrastructure.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _context;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<IOrderRepository> _orderRepository;
    private readonly Lazy<IOrderProductRepository> _orderProductRepository;

    public RepositoryManager(AppDbContext context)
    {
        _context = context;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
        _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(context));
        _orderProductRepository = new Lazy<IOrderProductRepository>(() => new OrderProductRepository(context));
    }

    public IUserRepository User => _userRepository.Value;
    public IProductRepository Product => _productRepository.Value;
    public IOrderRepository Order => _orderRepository.Value;
    public IOrderProductRepository OrderProduct => _orderProductRepository.Value;

    public void Save()
    {
        _context.SaveChanges();
    }

    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}