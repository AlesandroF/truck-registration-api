using AutoMapper;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Truck.Registration.Application.Exceptions;
using Truck.Registration.Application.Profiles;
using Truck.Registration.Application.UseCases.Put;
using Truck.Registration.Application.UseCases.Put.Handler;
using Truck.Registration.Domain.Enums;
using Truck.Registration.Domain.Ports;
using Xunit;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Tests.TruckTypes.Commands
{
    [ExcludeFromCodeCoverage]
    public class PutCommandHandlerTests
    {
        private readonly IMapper _mapper;

        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITruckRepository> _truckRepositoryMock;
        private readonly PutHandler _handler;
        private readonly PutCommand _truckRequest;

        public PutCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _truckRepositoryMock = new Mock<ITruckRepository>();
            
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<PutProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            
            _handler = new PutHandler(_unitOfWork.Object, _mapper);

            _truckRequest = new PutCommand
            {
                Model = TruckModelEnum.FM,
                ModelYear = 2030,
                YearManufacture = 2029
            };
        }

        [Fact]
        public async Task Valid_Truck_Put_ReturnCorrectType()
        {
            var truck = new Entity.Truck
            {
                Id = 1,
                Active = true,
                Model = TruckModelEnum.FM,
                ModelYear = 2020,
                YearManufacture = 2019
            };

            _truckRepositoryMock.Setup(e => e.UpdateAsync(It.IsAny<Entity.Truck>()));
            _truckRepositoryMock.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(truck);

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            var result = await _handler.Handle(_truckRequest, CancellationToken.None);

            result.ShouldBeOfType<PutResponse>();
            _unitOfWork.Verify(e => e.CommitAsync(), Times.Once());

            result.Id.ShouldBe(truck.Id);
            result.Model.ShouldBe(truck.Model);
            result.ModelYear.ShouldBe(truck.ModelYear);
            result.YearManufacture.ShouldBe(truck.YearManufacture);
        }

        [Fact]
        public async Task Valid_Truck_Put_ReturnNotFound()
        {
            _truckRepositoryMock.Setup(e => e.UpdateAsync(It.IsAny<Entity.Truck>()));

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            Should.Throw<CustomValidationException>(() => _handler.Handle(new PutCommand() { Model = TruckModelEnum.FM, ModelYear = 2020, YearManufacture = 2020 }, CancellationToken.None)).ErrosMessage.ShouldBe(new List<string> { "Truck not exist!" });

            _unitOfWork.Verify(e => e.CommitAsync(), Times.Never);
        }
    }
}