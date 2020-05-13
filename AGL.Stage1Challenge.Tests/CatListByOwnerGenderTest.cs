using AGL.Stage1.Model;
using AGL.Stage1.Services;
using AGL.Stage1.Services.Interfaces;
using AGL.Stage1Challenge.Common.Settings;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AGL.Stage1Challenge.Tests
{
    public class CatListByOwnerGenderTest
    {

        private DataService _sut;
        private readonly IOptions<AppSettings> _appSettings;

        public CatListByOwnerGenderTest()
        {
            _appSettings = Options.Create(new AppSettings() { ServiceUrl = "http://ServiceUrl" });
        }
        [Fact]
        public async Task ShouldBeSuccess_GetPeople()
        {
            // Arrange
            const string responseJSON = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]}]";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(_appSettings.Value.ServiceUrl)
                    .Respond("application/json", responseJSON);

            var client = mockHttp.ToHttpClient();

            _sut = new DataService(
                client,
                _appSettings,
            Substitute.For<ILogger<DataService>>());

            // Act
            var peopleResponse = await _sut.GetPeople();
            var owners = peopleResponse.Value.ToList();

            // Assert
            Assert.True(peopleResponse.IsSuccess);
            Assert.True(owners.Any());
            Assert.Equal("Bob", owners.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task ShouldFail_GetPeople()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(_appSettings.Value.ServiceUrl)
                .Respond(req => new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            var client = mockHttp.ToHttpClient();

            _sut = new DataService(
                client,
                _appSettings,
                Substitute.For<ILogger<DataService>>());

            // Act
            var peopleResponse = await _sut.GetPeople();

            // Assert
            Assert.True(peopleResponse.IsFailed);
        }


        [Fact]
        public async Task Should_GetCountries_SortedByLabel()
        {
            // Arrange
            const string responseJSON = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]}]";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(_appSettings.Value.ServiceUrl)
                    .Respond("application/json", responseJSON);

            var client = mockHttp.ToHttpClient();

            _sut = new DataService(
                client,
                _appSettings,
                Substitute.For<ILogger<DataService>>());

           var response= await _sut.GetOwnerGenderWithCats();

            // Assert           

            Assert.Single(response);
            Assert.Equal("Male", response.FirstOrDefault()?.OwnerGender);
            Assert.Equal(1, response.FirstOrDefault()?.CatNames.Count());

        }
    }
}
