using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;
using RepairCompanyApi.Services;
using RepairCompanyApi.Services.Implementations;
using Xunit.Sdk;


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


    [Theory]
    [InlineData("", "Athens" )]
    [InlineData(null, "Athens" )]
    [InlineData("Georgiou", "Athens")]
    public async Task CreateOwnerAsync_ShouldReturnOwner_WhenAllowedAddress (string lastNane, string city )
    {
        // Arrange
        var ownerId = 1;
        var owner = new PropertyOwner {   LastName = lastNane, Address = city };
        var ownerDto = new PropertyOwnerDtoRequest { LastName = lastNane, Address = city };
        var expectedOwnerId = 1L;

        // Setup the mock to return the returns ownerId  when AddAsync is called
        _ownerRepositoryMock.Setup(repo => repo.AddAsync(owner)).ReturnsAsync(1L);
        _mapperMock.Setup(service => service.Map<PropertyOwner>(ownerDto)).Returns(owner);

        // Act
        var result = await _ownerService.CreatePropertyOwner(ownerDto);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedOwnerId, result.Result);
    }

    [Theory]
    [InlineData("", "Lamia")]
    [InlineData(null, "Lamia")]
    [InlineData("Georgiou", "Lamia")]
    public async Task CreateOwnerAsync_ShouldNotReturnOwner_WhenAllowedAddress(string lastNane, string city)
    {
        // Arrange
        var ownerId = 1;
        var owner = new PropertyOwner { LastName = lastNane, Address = city };
        var ownerDto = new PropertyOwnerDtoRequest { LastName = lastNane, Address = city };
        var expectedOwnerId = 0L;

        // Setup the mock to return the returns ownerId  when AddAsync is called
        _ownerRepositoryMock.Setup(repo => repo.AddAsync(owner)).ReturnsAsync(1L);
        _mapperMock.Setup(service => service.Map<PropertyOwner>(ownerDto)).Returns(owner);

        // Act
        var result = await _ownerService.CreatePropertyOwner(ownerDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedOwnerId, result.Result);
    }

}
