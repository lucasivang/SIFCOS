using System;

namespace DA_SIFCOS.Entidades
{
    public class daException : Exception
    {
        public daException(String mensaje)
            : base(mensaje)
        {
        }
    }
}
