using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products;
using MediatR;

namespace DomainDrivenDesign.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    int Quantity,
    decimal Amount,
    string Curency,
    Guid categoryId) : IRequest;

internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.CreateAsync(
            request.Name,
            request.Quantity,
            request.Amount,
            request.Curency,
            request.categoryId,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}