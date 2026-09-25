using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.CreateSellerOffer;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Tests.Application.Features.Catalog.ProductVariants.SellerOffers;

public sealed class SellerOfferHandlerTests
{
    [Test]
    public async Task CreateSellerOffer_should_create_offer_and_return_id()
    {
        var sellerId = Guid.NewGuid();
        var productVariantId = Guid.NewGuid();

        var userRepository = new FakeUserRepository
        {
            ExistsResult = true
        };

        var productVariantRepository = new FakeProductVariantRepository
        {
            ExistsResult = true
        };

        var sellerOfferRepository = new FakeSellerOfferRepository();
        var unitOfWork = new FakeUnitOfWork();

        var handler = new CreateSellerOfferCommandHandler(
            userRepository,
            productVariantRepository,
            sellerOfferRepository,
            unitOfWork);

        var command = new CreateSellerOfferCommand(
            sellerId,
            productVariantId,
            100_000,
            10);

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.EqualTo(Guid.Empty));
            Assert.That(
                sellerOfferRepository.AddedOffer,
                Is.Not.Null);

            Assert.That(
                sellerOfferRepository.AddedOffer!.SellerId,
                Is.EqualTo(sellerId));

            Assert.That(
                sellerOfferRepository.AddedOffer.ProductVariantId,
                Is.EqualTo(productVariantId));

            Assert.That(
                sellerOfferRepository.AddedOffer.Price,
                Is.EqualTo(100_000));

            Assert.That(
                sellerOfferRepository.AddedOffer.Stock,
                Is.EqualTo(10));

            Assert.That(
                result,
                Is.EqualTo(sellerOfferRepository.AddedOffer.Id));

            Assert.That(
                unitOfWork.SaveChangesCalled,
                Is.True);
        });
    }

    [Test]
    public async Task CreateSellerOffer_should_throw_not_found_when_seller_does_not_exist()
    {
        var userRepository = new FakeUserRepository
        {
            ExistsResult = false
        };

        var productVariantRepository = new FakeProductVariantRepository
        {
            ExistsResult = true
        };

        var sellerOfferRepository = new FakeSellerOfferRepository();
        var unitOfWork = new FakeUnitOfWork();

        var handler = new CreateSellerOfferCommandHandler(
            userRepository,
            productVariantRepository,
            sellerOfferRepository,
            unitOfWork);

        var command = new CreateSellerOfferCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            100_000,
            10);

        var exception = Assert.ThrowsAsync<NotFoundException>(
            async () => await handler.Handle(
                command,
                CancellationToken.None));

        Assert.That(
            exception!.Message,
            Does.Contain("Seller"));

        Assert.That(
            sellerOfferRepository.AddedOffer,
            Is.Null);

        Assert.That(
            unitOfWork.SaveChangesCalled,
            Is.False);
    }

    [Test]
    public async Task CreateSellerOffer_should_throw_not_found_when_product_variant_does_not_exist()
    {
        var userRepository = new FakeUserRepository
        {
            ExistsResult = true
        };

        var productVariantRepository = new FakeProductVariantRepository
        {
            ExistsResult = false
        };

        var sellerOfferRepository = new FakeSellerOfferRepository();
        var unitOfWork = new FakeUnitOfWork();

        var handler = new CreateSellerOfferCommandHandler(
            userRepository,
            productVariantRepository,
            sellerOfferRepository,
            unitOfWork);

        var command = new CreateSellerOfferCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            100_000,
            10);

        var exception = Assert.ThrowsAsync<NotFoundException>(
            async () => await handler.Handle(
                command,
                CancellationToken.None));

        Assert.That(
            exception!.Message,
            Does.Contain("Product variant"));

        Assert.That(
            sellerOfferRepository.AddedOffer,
            Is.Null);

        Assert.That(
            unitOfWork.SaveChangesCalled,
            Is.False);
    }

    private sealed class FakeUserRepository : IUserRepository
    {
        public bool ExistsResult { get; set; }

        public Task<bool> ExistsAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ExistsResult);
        }

        public Task SetSellerStatusAsync(
            Guid userId,
            SellerStatus status,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    private sealed class FakeProductVariantRepository : IProductVariantRepository
    {
        public bool ExistsResult { get; set; }

        public Task<bool> ExistsAsync(
            Guid productVariantId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ExistsResult);
        }

        public Task AddAsync(
            ProductVariant productVariant,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<ProductVariant?> GetByIdWithDetailsAsync(
            Guid productVariantId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<ProductVariant?>(null);
        }
    }

    private sealed class FakeSellerOfferRepository : ISellerOfferRepository
    {
        public SellerOffer? AddedOffer { get; private set; }

        public Task AddAsync(
            SellerOffer sellerOffer,
            CancellationToken cancellationToken = default)
        {
            AddedOffer = sellerOffer;
            return Task.CompletedTask;
        }

        public Task<SellerOffer?> GetByIdAsync(
            Guid sellerOfferId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<SellerOffer?>(null);
        }

        public Task<SellerOffer?> GetByIdForUpdateAsync(
            Guid sellerOfferId,
            uint expectedRowVersion,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<SellerOffer?>(null);
        }
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public bool SaveChangesCalled { get; private set; }

        public Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            SaveChangesCalled = true;
            return Task.FromResult(1);
        }
    }
}