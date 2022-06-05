using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Truck.Registration.Application.Exceptions;
using Truck.Registration.Application.Helper;
using Truck.Registration.Application.UseCases.Add;
using Truck.Registration.Application.UseCases.Add.Handler;
using Truck.Registration.Domain.Enums;
using Truck.Registration.Domain.Ports;
using Xunit;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Tests.TruckTypes.Commands
{
    [ExcludeFromCodeCoverage]
    public class AddCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITruckRepository> _truckRepositoryMock;
        private readonly AddHandler _handler;
        private readonly AddCommand _truckRequest;

        public AddCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _truckRepositoryMock = new Mock<ITruckRepository>();

            _handler = new AddHandler(_unitOfWork.Object);

            _truckRequest = new AddCommand
            {
                Model = TruckModelEnum.FM,
                ModelYear = 2030,
                YearManufacture = 2019
            };
        }

        [Fact]
        public async Task Valid_Truck_Added_ReturnCorrectType()
        {
            var truck = new Entity.Truck
            {
                Id = 1,
                Active = true,
                Model = TruckModelEnum.FM,
                ModelYear = 2020,
                YearManufacture = 2019
            };

            _truckRepositoryMock.Setup(e => e.Add(It.IsAny<Entity.Truck>())).ReturnsAsync(truck);
            _truckRepositoryMock.Setup(r => r.GetWithFilter(It.IsAny<Expression<Func<Entity.Truck, bool>>>())).ReturnsAsync(new List<Entity.Truck>());

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            var result = await _handler.Handle(_truckRequest, CancellationToken.None);

            result.ShouldBeOfType<AddResponse>();
            _unitOfWork.Verify(e => e.CommitAsync(), Times.Once());

            result.Id.ShouldBe(truck.Id);
            result.Model.ShouldBe(truck.Model.GetDescription());
            result.ModelYear.ShouldBe(truck.ModelYear);
            result.YearManufacture.ShouldBe(truck.YearManufacture);
        }

        [Fact]
        public void Valid_Truck_Added_Validate_ThrowsException()
        {
            var trucks = new List<Entity.Truck>
            {
                new Entity.Truck
                {
                    Id = 1,
                    Active = true,
                },
                new Entity.Truck
                {
                    Id = 2,
                    Active = true,
                },
                new Entity.Truck
                {
                    Id = 3,
                    Active = true,
                }
            };

            _truckRepositoryMock.Setup(r => r.GetWithFilter(It.IsAny<Expression<Func<Entity.Truck, bool>>>())).ReturnsAsync(trucks);

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            Should.Throw<CustomValidationException>(() => _handler.AddValidate(new AddCommand() { Model = TruckModelEnum.FM, ModelYear = 2020, YearManufacture = 2020 })).ErrosMessage.ShouldBe(new List<string> { "Truck already exists!" });
        }

        [Fact]
        public void Valid_Truck_Added_Validate_ReturnOk()
        {
            var trucks = new List<Entity.Truck>();

            _truckRepositoryMock.Setup(r => r.GetWithFilter(It.IsAny<Expression<Func<Entity.Truck, bool>>>())).ReturnsAsync(trucks);

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            Should.NotThrow(() => _handler.AddValidate(new AddCommand() { Model = TruckModelEnum.FM, ModelYear = 2020, YearManufacture = 2020 }));
        }
    }
}