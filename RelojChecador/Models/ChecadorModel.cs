using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelojChecador.Models
{
    public class ChecadorModel
    {
        public String hora { get; set; }
        public List<ClaseModel> clases { get; set; }

        public ChecadorModel()
        {

        }
    }
}
