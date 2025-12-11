using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class Organismo
    {
        public String IdOrganismo { get; set; }
        public String NOrganismo { get; set; }

        public object this[string empty]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
