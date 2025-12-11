using System;

namespace BL_SIFCOS
{
    public class BlException : Exception
    {
        public BlException(String mensaje)
            : base(mensaje)
        {
        }

        protected BlException()
        {
            
        }
    }
}
