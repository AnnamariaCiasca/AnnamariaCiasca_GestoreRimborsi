using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreRimborsi.Core.BusinessLayer
{
    public interface IBusinessLayer
    {
        void Update();

        void ScriviSuFile(Evento evento);
    }
}
