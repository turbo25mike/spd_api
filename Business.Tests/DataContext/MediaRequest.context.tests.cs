using System;
using System.Collections.Generic;
using System.Net.Http;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using Telerik.JustMock;

namespace ImageServices.Tests
{
    [TestClass]
    public class MediaRequestContextTests
    {
        [TestInitialize]
        public void Setup()
        {
            _someDatabase = Mock.Create<IDatabase>();
            _someRequest = new MediaRequest { MediaID = _someID };
            Mock.Arrange(() => _someDatabase.Update(DBTable.media, Arg.IsAny<Dictionary<string, string>>(),Arg.IsAny<KeyValuePair<string, string>>()));
            _someRequestContext = new MediaRequestContext() { DB = _someDatabase };
        }

        [TestMethod]
        public void ShouldReturnNullWhenGetByID()
        {
            GivenDBReturnsNull();
            WhenGetByID();
            ThenSomeRequest.Should().Be(null);
        }

        [TestMethod]
        public void ShouldReturnOneWhenGetByID()
        {
            GivenDBReturnsOne();
            WhenGetByID();
            ThenSomeRequest.Should().Be(_someRequest);
        }

        [TestMethod]
        public void ShouldReturnNullWhenNoMatchingRecordsForGetMultipleByName()
        {
            GivenDBReturnsNull();
            WhenGetByUserName();
            ThenSomeListOfRequests.Should().BeNull();
        }

        [TestMethod]
        public void ShouldReturnMultipleMatchingRecordsForGetMultipleByName()
        {
            GivenDBReturnsMultiple();
            WhenGetByUserName();
            ThenSomeListOfRequests.Count.Should().Be(2);
        }

        [TestMethod]
        public void ShouldSave()
        {
            WhenSaveIsCalled();
            ThenResponse.Should().Be(true);
        }

        private void WhenSaveIsCalled()
        {
            ThenResponse = _someRequestContext.Save(_someRequest);
        }

        private void WhenGetByID()
        {
            _someRequest = _someRequestContext.GetByID(_someID);
        }

        private void WhenGetByUserName()
        {
            ThenSomeListOfRequests = _someRequestContext.GetMultipleByUserName(_someUsername);
        }

        private void GivenDBReturnsMultiple()
        {
            Mock.Arrange(() => _someDatabase.Select<MediaRequest>(DBTable.media, Arg.AnyString, Arg.AnyInt)).Returns(() => new List<MediaRequest> { _someRequest, _someRequest });
        }

        private void GivenDBReturnsOne()
        {
            Mock.Arrange(() => _someDatabase.Select<MediaRequest>(DBTable.media, Arg.AnyString, Arg.AnyInt)).Returns(() => new List<MediaRequest> { _someRequest });
        }

        private void GivenDBReturnsNull()
        {
            Mock.Arrange(() => _someDatabase.Select<MediaRequest>(DBTable.media, Arg.AnyString, Arg.AnyInt)).Returns(() => null);
        }

        private MediaRequest ThenSomeRequest => _someRequest;

        private MediaRequestContext _someRequestContext;
        private IDatabase _someDatabase;
        private int _someID = 1;
        private string _someUsername = "bob.villa@home.com";
        private bool ThenResponse;
        private List<MediaRequest> ThenSomeListOfRequests;
        private MediaRequest _someRequest;
    }
}
