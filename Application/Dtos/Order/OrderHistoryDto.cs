namespace Application.Dtos.Order;

public record OrderHistoryDto(decimal TotalPrice, DateTime CreatedAt, IEnumerable<OrderProductHistoryDto> OrderProducts);