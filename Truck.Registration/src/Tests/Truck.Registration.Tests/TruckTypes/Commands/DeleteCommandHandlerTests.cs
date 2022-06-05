using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Truck.Registration.Application.Exceptions;
using Truck.Registration.Application.UseCases.Delete;
using Truck.Registration.Application.UseCases.Delete.Handler;
using Truck.Registration.Domain.Enums;
using Truck.Registration.Domain.Ports;
using Xunit;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Tests.TruckTypes.Commands
{
    [ExcludeFromCodeCoverage]
    public class DeleteCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITruckRepository> _truckRepositoryMock;
        private readonly DeleteHandler _handler;
        private readonly DeleteCommand _truckRequest;

        public DeleteCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _truckRepositoryMock = new Mock<ITruckRepository>();

            _handler = new DeleteHandler(_unitOfWork.Object);

            _truckRequest = new DeleteCommand
            {
                Id = 1,
            };
        }

        [Fact]
        public async Task Valid_Truck_Deleted_ReturnCorrectType()
        {
            var truck = new Entity.Truck
            {
                Id = 1,
                Active = true,
                Model = TruckModelEnum.FM,
                ModelYear = 2020,
                YearManufacture = 2019
            };

            _truckRepositoryMock.Setup(e => e.DeleteAsync(It.IsAny<int>()));
            _truckRepositoryMock.Setup(e => e.Query(It.IsAny<Expression<Func<Entity.Truck, bool>>>(), null)).Returns(new List<Entity.Truck> { truck }.AsQueryable());

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            var result = await _handler.Handle(_truckRequest, CancellationToken.None);

            result.ShouldBeOfType<DeleteResponse>();
            _unitOfWork.Verify(e => e.CommitAsync(), Times.Once());

            result.Id.ShouldBe(truck.Id);
        }

        [Fact]
        public async Task Valid_Truck_Delete_ReturnNotFound()
        {
            _truckRepositoryMock.Setup(e => e.DeleteAsync(It.IsAny<int>()));

            _unitOfWork.Setup(r => r.TruckRepository).Returns(_truckRepositoryMock.Object);

            Should.Throw<CustomValidationException>(() => _handler.Handle(_truckRequest, CancellationToken.None)).ErrosMessage.ShouldBe(new List<string> { "Truck not exist!" });

            _unitOfWork.Verify(e => e.CommitAsync(), Times.Never);
        }
    }
}