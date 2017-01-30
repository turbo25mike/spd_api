using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using Telerik.JustMock;

namespace Business.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestInitialize]
        public void Setup()
        {
            _someMessage = "testing: " + Guid.NewGuid();
            _someDatabase = new Database();
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
            _someDatabase.Update(DBTable.Media, new Dictionary<string, string>{{ "Message",_someMessage}}, new KeyValuePair<string, string>("MediaID", _someMediaID.ToString()));
        }

        private MediaRequest ThenRequest
        {
            get
            {
                var result = _someDatabase.Select<MediaRequest>(DBTable.Media, "MediaID = " + _someMediaID);
                return result[0];
            }
        }

        private readonly int _someMediaID = 1;
        private string _someMessage;
        private Database _someDatabase;
    }
}
