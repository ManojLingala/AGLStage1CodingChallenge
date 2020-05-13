using AGL.Stage1.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using AGL.Stage1Challenge.Common;
using AGL.Stage1.Model.ViewModel;

namespace AGL.Stage1.Services.Interfaces
{
    public interface IDataService
    {
        Task<Result<IEnumerable<Owner>>> GetPeople();
        Task<IEnumerable<CatListByOwnerGenderViewModel>> GetOwnerGenderWithCats();
    }
}
