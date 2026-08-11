using EShop.Console.Entities;
using EShop.Console.Notifications;

var parentCategory = new Category(Guid.NewGuid(), "Electronics", null);
var phones = new Category(Guid.NewGuid(), "Phones", parentCategory.Id);
parentCategory.AddSubCategory(phones);

var product = new Product(Guid.NewGuid(), "Iphone", "Iphone 17 pro max", 50000m, 10, phones);
phones.AddProduct(product);

var customer = new Customer(Guid.NewGuid(), "Baselyosry", "baselyosry@gmail.com", "password");
var cart = new Cart(Guid.NewGuid(), customer.Id);
cart.AddItem(product, 1);

var order = new Order(Guid.NewGuid(), "Pending", 0m, customer);
order.AddItem(product, 1);
customer.AddOrder(order);

Console.WriteLine(product.Summarize());
Console.WriteLine(cart.Summarize());
Console.WriteLine(order.Summarize());
Console.WriteLine(customer.Summarize());

Notification email = new EmailNotification();
Notification sms = new SmsNotification();
email.SendConfirmation();
sms.SendConfirmation();
