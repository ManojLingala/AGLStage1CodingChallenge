using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.Stage1.Model.ViewModel
{
    public class CatListByOwnerGenderViewModel
    {
        public string OwnerGender { get; set; }
        public IEnumerable<string> CatNames { get; set; }
    }
}
