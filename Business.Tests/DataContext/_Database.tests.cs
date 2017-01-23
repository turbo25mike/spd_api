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
    public class DatabaseTests
    {
        [TestInitialize]
        public void Setup()
        {
            _someMessage = "testing: " + Guid.NewGuid();
            IConfiguration _someConfig = Mock.Create<IConfiguration>();
            Mock.Arrange(() => _someConfig.DBConnectionString).Returns(() => "SERVER=localhost;DATABASE=automap;UID=root;PASSWORD=admin;");
            _someDatabase = new Database {config = _someConfig};
        }

        [TestMethod]
        public void ShouldGetMediaData()
        {
            ThenRequest.MediaID.Should().Be(_someMediaID);
        }

        [TestMethod]
        public void ShouldUpdateMediaData()
        {
            GivenAMessage();
            ThenRequest.Message.Should().Be(_someMessage);
        }

        private void GivenAMessage()
        {
            _someDatabase.Update(DBTable.media, new Dictionary<string, string>{{ "Message",_someMessage}}, new KeyValuePair<string, string>("MediaID", _someMediaID.ToString()));
        }

        private MediaRequest ThenRequest
        {
            get
            {
                List<MediaRequest> result = _someDatabase.Select<MediaRequest>(DBTable.media, "MediaID = " + _someMediaID);
                return result[0];
            }
        }

        private int _someMediaID = 1;
        private string _someMessage;
        private Database _someDatabase;
    }
}
