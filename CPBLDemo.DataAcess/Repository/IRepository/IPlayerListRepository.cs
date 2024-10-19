using CPBLDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPBLDemo.DataAccess.Repository.IRepository
{
    public interface IPlayerListRepository :IRepository<PlayerList>
    {
        void Update(PlayerList obj);

    }
}
