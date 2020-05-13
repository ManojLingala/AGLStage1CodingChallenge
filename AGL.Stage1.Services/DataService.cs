using AGL.Stage1.Model;
using AGL.Stage1.Services.Interfaces;
using AGL.Stage1Challenge.Common;
using FluentResults;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using AGL.Stage1.Model.ViewModel;
using System.Net.Http;
using AGL.Stage1Challenge.Common.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace AGL.Stage1.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _serviceUrl;
        private readonly ILogger<DataService> _logger;

        public DataService(HttpClient httpClient, IOptions<AppSettings> settings , ILogger<DataService> logger)
        {
            _httpClient = httpClient;
            _serviceUrl= settings?.Value?.ServiceUrl ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger;
        }
       public async Task<IEnumerable<CatListByOwnerGenderViewModel>> GetOwnerGenderWithCats()
        {
            var peopleResult = await GetPeople();
            // Convert To List
            var ownerList = peopleResult.Value.ToList();

            //Core logic which filters out the pet category and groupby with owner gender.
            var genderWithCats = ownerList
                .Where(owner => owner.pets != null && owner.pets.Any())
                .GroupBy(owner => owner.Gender)
                .Select(
                    group => new CatListByOwnerGenderViewModel
                    {
                        OwnerGender = group.Key,
                        CatNames = group
                            .SelectMany(p => p.pets)
                            .Where(p => p.Type.Equals("Cat", StringComparison.OrdinalIgnoreCase))
                            .Select(pet => pet.Name)
                            .OrderBy(name => name)
                    });

            //new ValueResponse<IEnumerable<CatListByOwnerGenderViewModel>>(HttpStatusCode.OK, genderWithCats)

            return genderWithCats;
        }

      public  async Task<Result<IEnumerable<Owner>>> GetPeople()
        {
            try
            {
                var content = await _httpClient.GetAsync(_serviceUrl);
                var jsonContent = await content.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<IEnumerable<Owner>>(jsonContent);
                return Results.Ok(response);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,"Error occured in the API");

                return Results.Fail<IEnumerable<Owner>>(new Error(exception.Message));
            }
        }
    }
}
