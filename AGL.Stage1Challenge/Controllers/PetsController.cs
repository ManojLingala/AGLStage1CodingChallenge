using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AGL.Stage1.Model.ViewModel;
using AGL.Stage1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using AGL.Stage1Challenge.Common;
using AGL.Stage1.Services.Interfaces;

namespace AGL.Stage1Challenge.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IDataService _dataService;
      
        public PetsController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet(Name = "getCats")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<CatListByOwnerGenderViewModel>> GetCats()
        {

            return await _dataService.GetOwnerGenderWithCats();

        } 
    }
}
