using AutoMapper;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Truck.Registration.Application.Helper;
using Truck.Registration.Application.Profiles;
using Truck.Registration.Application.UseCases.Get;
using Truck.Registration.Application.UseCases.Get.Handler;
using Truck.Registration.Domain.Enums;
using Truck.Registration.Domain.Ports;
using Xunit;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Tests.TruckTypes.Commands
{
    [ExcludeFromCodeCoverage]
    public class GetCommandHandlerTests
    {
        private readonly IMapper _mapper;

        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITruckRepository> _truckRepositoryMock;
        private readonly GetHandler _handler;
        private readonly GetCommand _truckRequest;

        public GetCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<GetProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _unitOfWork = new Mock<IUnitOfWork>();
            _truckRepositoryMock = new Mock<ITruckRepository>();

            _handler = new GetHandler(_unitOfWork.Object, _mapper);

            _truckRequest = new GetCommand();
        }

        [Fact]
        public async Task Valid_Truck_Get_ReturnCorrectType()
        {
            var truck = new Entity.Truck
            {
                Id = 1,
                Active = true,
                Model = TruckModelEnum.FM,
                ModelYear = 2020,
                YearManufacture = 2019
            };

            _truckRepositoryMock.Setup(e => e.GetAllAsync()).ReturnsAsync(new List<Entity.Truck> { truck });

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            var result = await _handler.Handle(_truckRequest, CancellationToken.None);

            result.ShouldBeOfType<List<GetResponse>>();

            foreach (var item in result)
            {
                item.Id.ShouldBe(truck.Id);
                item.Model.ShouldBe(truck.Model.GetDescription());
                item.ModelYear.ShouldBe(truck.ModelYear);
                item.YearManufacture.ShouldBe(truck.YearManufacture);
            }
        }
    }
}