using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public class Calles
    {
        public String IdCalle { get; set; }
        public String NCalle { get; set; }

        public object this[string empty]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
