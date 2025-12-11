using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DA_SIFCOS.Entidades
{
    public  class Producto
    {
        public String IdProducto { get; set; }
        public String NProducto  { get; set; }

        public String IdRubro { get; set; }
        public String UrlCodigoQr { get; set; }
        public byte[] ImgBmp { get; set; }

        public object this[string empty]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
