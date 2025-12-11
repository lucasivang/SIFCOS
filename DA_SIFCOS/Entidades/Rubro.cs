using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class Rubro
    {
        public String IdRubro { get; set; }
        public String NRubro { get; set; }

        public object this[string empty]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
