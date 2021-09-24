using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreRimborsi.Core.BusinessLayer
{
    public class Evento
    {
        public delegate void ScriviSuFile(Evento evento);
        public delegate void PuntatorePerFarPartiIlProgramma(Evento evento);

        public event ScriviSuFile MandaLaNotifica;
        public event PuntatorePerFarPartiIlProgramma FaiPartireIlProgramma;

        
        public void SeInseritoNomeGiusto()
        {
            if (FaiPartireIlProgramma != null)
            {
                FaiPartireIlProgramma(this);
            }
        }
    }
}
    

