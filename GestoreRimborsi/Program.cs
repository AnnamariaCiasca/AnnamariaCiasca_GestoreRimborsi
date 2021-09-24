using GestoreRimborsi.AdoRepository;
using GestoreRimborsi.Core.BusinessLayer;
using System;

namespace GestoreRimborsi
{
    class Program
    {
        private static readonly IBusinessLayer bl = new BusinessLayer(new RepositoryMonitoraggioSpesa(), new RepositoryDipendente());
        static void Main(string[] args)
        {
            bl.Update();
            

        }
    }
}
