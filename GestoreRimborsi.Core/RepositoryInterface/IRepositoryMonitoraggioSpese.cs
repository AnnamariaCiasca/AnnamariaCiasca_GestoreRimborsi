using GestoreRimborsi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreRimborsi.Core.RepositoryInterface
{
    public interface IRepositoryMonitoraggioSpese : IRepository<MonitoraggioSpesa>
    {
        List<MonitoraggioSpesa> GetItemsWithOutApproval();
        List<MonitoraggioSpesa> GetByName(string nome);
    }
}
