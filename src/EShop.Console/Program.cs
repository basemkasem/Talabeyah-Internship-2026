using EShop.Console.Entities;
using EShop.Console.Notifications;
using EShop.Console.Repositories;
using EShop.Console.Services;

var parentCategory = new Category(Guid.NewGuid(), "Electronics", null);
var phones = new Category(Guid.NewGuid(), "Phones", parentCategory.Id);
parentCategory.AddSubCategory(phones);

var product = new Product(Guid.NewGuid(), "Iphone", "Iphone 17 pro max", 50000m, 10, phones);
phones.AddProduct(product);

var customer = new Customer(Guid.NewGuid(), "Baselyosry", "baselyosry@gmail.com", "password");
var cart = new Cart(Guid.NewGuid(), customer.Id);
cart.AddItem(product, 1);

IStockValidator stockValidator = new StockValidator();
IDiscountService discountService = new PercentageDiscount(10);
IOrderRepository orderRepository = new InMemoryOrderRepository();
Notification notification = new EmailNotification();

var orderProcessor = new OrderProcessor(
    stockValidator,
    discountService,
    orderRepository,
    notification);

var order = orderProcessor.PlaceOrder(cart);

Console.WriteLine(product.Summarize());
Console.WriteLine(cart.Summarize());
Console.WriteLine(order.Summarize());
Console.WriteLine(customer.Summarize());
