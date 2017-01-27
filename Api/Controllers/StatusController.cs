using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Amazon.EC2.Model;
using Microsoft.AspNet.Identity;
using Amazon.EC2;

namespace Api.Controllers
{
    [RoutePrefix("api/status")]
    public class StatusController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse("Looking Good!");
        }

        [Route("environment")]
        public HttpResponseMessage GetEnvironment()
        {
            return Request.CreateResponse(ConfigurationManager.AppSettings["ENVIRONMENT"]);
        }

        [Route("exception")]
        public HttpResponseMessage GetException()
        {
            throw new Exception();
        }

        [Authorize]
        [Route("secure")]
        public HttpResponseMessage GetSecured()
        {
            var userId = ClaimsPrincipal.Current.Identity.GetUserId();
            return Request.CreateResponse($"Hello, {userId}! You are currently authenticated.");
        }

        [Route("tags")]
        public HttpResponseMessage GetTags()
        {
            var client = new AmazonEC2Client();
            var tags = new List<string>();
            var request = new DescribeInstancesRequest();
            var response = client.DescribeInstances(request);
            foreach (var ec2 in response.Reservations)
            {
                foreach (var instance in ec2.Instances)
                {
                    tags.AddRange(instance.Tags.Select(t => t.Key));
                }
            }
            return Request.CreateResponse(string.Join(",", tags));
        }
    }
}
