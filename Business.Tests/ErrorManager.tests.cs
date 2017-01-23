using System;
using FluentAssertions;
using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace ImageServices.Tests
{
    [TestClass]
    public class ErrorManagerTests
    {
        private IDatabase _someDatabase;
        private ErrorManager _someErrorManager;

        [TestInitialize]
        public void Setup()
        {
            _someDatabase = Mock.Create<IDatabase>();
            _someErrorManager = new ErrorManager() {_db = _someDatabase};
        }
        [TestMethod]
        public void ShouldReturnMessage()
        {
            WhenHandleIsCalledWithException(new Exception());
            ThenResponse.Should().Be(Constants.ServerError);
        }

        private void WhenHandleIsCalledWithException(Exception ex)
        {
            ThenResponse = _someErrorManager.Handle(ex);
        }

        private string ThenResponse;
    }
}
