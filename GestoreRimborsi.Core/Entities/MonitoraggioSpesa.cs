using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreRimborsi.Core.Entities
{
  public class MonitoraggioSpesa
    {
      
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Spesa { get; set; }
        public CategoriaEnum Categoria { get; set; }
        public string Descrizione { get; set; } 
        public Dipendente Dipendente { get; set; }
        public bool? Approvata { get; set; }
        public double? Rimborso { get; set; }
        public ApprovatoreEnum? Approvatore{ get; set; }

      

    }

    public enum CategoriaEnum
    {
        Vitto = 1,
        Alloggio = 2,
        Trasferta = 3,
        Altro = 4,
    }

    public enum ApprovatoreEnum
    {
      Manager = 1,
      OperationManager = 2,
      CEO = 3,
    
    }
}
