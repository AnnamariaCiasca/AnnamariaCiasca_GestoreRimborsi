using GestoreRimborsi.AdoRepository;
using GestoreRimborsi.Core.BusinessLayer;
using GestoreRimborsi.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace GestoreRimborsi.CreatoreFile
{
   public class Program
    {
        private static readonly IBusinessLayer bl = new BusinessLayer(new RepositoryMonitoraggioSpesa(), new RepositoryDipendente());
        static void Main(string[] args)
        {
            Evento evento = new Evento();
           
            evento.FaiPartireIlProgramma += LanciaIlProgramma;
            evento.SeInseritoNomeGiusto();

        }

        private static void LanciaIlProgramma(Evento evento)
        {
            try
            {
                bl.ScriviSuFile(evento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }
    }


}
