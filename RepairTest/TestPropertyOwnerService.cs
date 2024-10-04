using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;
using RepairCompanyApi.Services;
using RepairCompanyApi.Services.Implementations;


namespace RepairTest;

public class TestPropertyOwnerService
{
    private readonly IPropertyOwnerService _ownerService;
    private readonly Mock<IPropertyOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<PropertyOwnerServiceUsingRepository>> _loggerMock;
    private readonly Mock<IDistributedCache> _cacheMock;

    public TestPropertyOwnerService()
    {
        // Create a mock repository
        _ownerRepositoryMock = new Mock<IPropertyOwnerRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<PropertyOwnerServiceUsingRepository>>();
        _cacheMock = new Mock<IDistributedCache>();

        // Inject the mock into the service
        _ownerService = new PropertyOwnerServiceUsingRepository(
             _mapperMock.Object, _ownerRepositoryMock.Object,
          _loggerMock.Object, _cacheMock.Object);
    }


    [Fact]
    public async Task CreateOwnerAsync_ShouldReturnOwner_WhenAllowedAddress ()
    {
        // Arrange
        var ownerId = 1;
        var owner = new PropertyOwner {   LastName = "Georgiou", Address = "Athens"  };
        var expectedOwnerId = 1;

        // Setup the mock to return the order when GetOrderById is called
        _ownerRepositoryMock.Setup(repo => repo.AddAsync(owner)).ReturnsAsync(true);

        // Act
        var result = await _ownerService.PostPropertyOwner(owner);
        

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(expectedOwnerId, result.Value.Id);
      
    }

     

}
