using AutoMapper;
using Moq;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Truck.Registration.Application.Helper;
using Truck.Registration.Application.Profiles;
using Truck.Registration.Application.UseCases.GetById;
using Truck.Registration.Application.UseCases.GetById.Handler;
using Truck.Registration.Domain.Enums;
using Truck.Registration.Domain.Ports;
using Xunit;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Tests.TruckTypes.Commands
{
    [ExcludeFromCodeCoverage]
    public class GetByIdCommandHandlerTests
    {
        private readonly IMapper _mapper;

        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITruckRepository> _truckRepositoryMock;
        private readonly GetByIdHandler _handler;
        private readonly GetByIdCommand _truckRequest;

        public GetByIdCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<GetProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _unitOfWork = new Mock<IUnitOfWork>();
            _truckRepositoryMock = new Mock<ITruckRepository>();

            _handler = new GetByIdHandler(_unitOfWork.Object, _mapper);

            _truckRequest = new GetByIdCommand
            {
                Id = 1
            };
        }

        [Fact]
        public async Task Valid_Truck_GetById_ReturnCorrectType()
        {
            var truck = new Entity.Truck
            {
                Id = 1,
                Active = true,
                Model = TruckModelEnum.FM,
                ModelYear = 2020,
                YearManufacture = 2019
            };

            _truckRepositoryMock.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(truck);

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            var result = await _handler.Handle(_truckRequest, CancellationToken.None);

            result.ShouldBeOfType<GetByIdResponse>();

            result.Id.ShouldBe(truck.Id);
            result.Model.ShouldBe(truck.Model.GetDescription());
            result.ModelYear.ShouldBe(truck.ModelYear);
            result.YearManufacture.ShouldBe(truck.YearManufacture);
        }
    }
}