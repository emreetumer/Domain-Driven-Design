using DomainDrivenDesign.Domain.Orders;
using MediatR;

namespace DomainDrivenDesign.Application.Features.Orders.CreateOrder;

public sealed record CreateOrderCommand(List<CreateOrderDto> createOrderDtos) : IRequest;
