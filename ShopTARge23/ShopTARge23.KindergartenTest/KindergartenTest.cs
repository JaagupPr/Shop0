using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopTARge23.Core.Domain;
using ShopTARge23.ApplicationServices.Services;
using Xunit;
using System;
using System.Threading.Tasks;

namespace ShopTARge23.KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyKindergarten_WhenReturnResult()
        {
            // Arrange
            KindergartenDto dto = MockKindergartenDto();

            // Act
            var result = await Svc<IKindergartensServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdKindergarten_WhenReturnsNotEqual()
        {
            // Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");

            // Act
            await Svc<IKindergartensServices>().DetailAsync(guid);

            // Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdKindergarten_WhenReturnsEqual()
        {
            // Arrange
            Guid databaseGuid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");
            Guid guid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");

            // Act
            await Svc<IKindergartensServices>().DetailAsync(guid);

            // Assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdKindergarten_WhenDeleteKindergarten()
        {
            // Arrange
            KindergartenDto kindergarten = MockKindergartenDto();

            var addKindergarten = await Svc<IKindergartensServices>().Create(kindergarten);
            var result = await Svc<IKindergartensServices>().Delete((Guid)addKindergarten.Id);

            // Assert
            Assert.Equal(result, addKindergarten);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdKindergarten_WhenDidNotDeleteKindergarten()
        {
            // Arrange
            KindergartenDto kindergarten = MockKindergartenDto();

            var kindergarten1 = await Svc<IKindergartensServices>().Create(kindergarten);
            var kindergarten2 = await Svc<IKindergartensServices>().Create(kindergarten);

            // Act
            var result = await Svc<IKindergartensServices>().Delete((Guid)kindergarten2.Id);

            // Assert
            Assert.NotEqual(result.Id, kindergarten1.Id);
        }

        private KindergartenDto MockKindergartenDto()
        {
            return new KindergartenDto
            {
                Id = Guid.NewGuid(),
                GroupName = "Sunshine Group",
                ChildrenCount = 20,
                KindergartenName = "Happy Kids",
                Teacher = "Mrs. Smith",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
