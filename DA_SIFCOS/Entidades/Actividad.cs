using System;


namespace DA_SIFCOS.Entidades
{
    public class Actividad
    {
        public String Id_Actividad { get; set; }
        public String N_Actividad { get; set; }

        public object this[string empty]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
