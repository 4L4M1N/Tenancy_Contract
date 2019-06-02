using System;
using System.Collections.Generic;
using System.Text;
using TenancyContract.Entities;
using Xunit;

namespace UnitTests
{
    public class TenantTests
    {
        [Fact]
        public void CanChangeTenantName()
        {
            //Arrange
            var tenant = new Tenant { Name = "Test", Email = "test@gmail.com" };
            //Act
            tenant.Name = "New Name";
            //Assert
            Assert.Equal("New Name", tenant.Name);
        }

    }
}
