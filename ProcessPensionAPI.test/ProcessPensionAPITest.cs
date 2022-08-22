using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProcessPensionAPI.Controllers;
using ProcessPensionAPI.Model;
using ProcessPensionAPI.Provider;
using ProcessPensionAPI.Repository;

namespace ProcessPensionAPI.test
{
    public class ProcessPensionAPITest
    {
        
        private IRequestRepository repo;
        private IRequestProvider provider;
        private ProcessPensionController _controller;
        ProcessPensionInput input1;
        ProcessPensionInput input2;
        string token = "dummytoken";


        [SetUp]
        public void Setup()
        {
            repo = new RequestRepository();
            provider = new RequestProvider(repo);
            _controller = new ProcessPensionController(provider);
            input1 = new ProcessPensionInput()
            {
                Id = 1,
                AadhaarNumber = "111111111111"
            };
            input2 = new ProcessPensionInput()
            {
                Id = 12,
                AadhaarNumber = "111111111112"
            };
        }
        [Test]
        public void ProcessPensionControllerPositiveTest()
        {
            var result = _controller.Post(input1);
            var response = result as ObjectResult;
            Assert.IsNull(response);
        }

        [Test]
        public void ProcessPensionControllerNegativeTest()
        {

            try
            {
                var result = _controller.Post(input2);
                var response = result as ObjectResult;
                Assert.AreEqual(404, response.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
        [Test]
        public void ProcessPensionProviderNegativeTest()
        {
            var result = provider.ProcessPension(input2, null);
            Assert.IsNull(result);
        }

        [Test]
        public void ProcessPensionRepositoryNegativeTest()
        {
            token = null;
            var result = repo.ProcessPension(input2, token);
            Assert.IsNull(result);
        }
    }
}
