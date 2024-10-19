using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPBLDemo.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPlayerListRepository PlayerList { get; }
        ITeamRepository  Team { get; }
        void Save();
    }
}
