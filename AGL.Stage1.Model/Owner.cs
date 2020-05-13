using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.Stage1.Model
{
    /// <summary>
    /// Represents the pet owner model returned by the External People Service 
    /// </summary>
    public class Owner
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public IEnumerable<Pet> pets { get; set; }
    }
}
