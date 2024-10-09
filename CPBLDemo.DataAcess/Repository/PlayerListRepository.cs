using CPBLDemo.DataAccess.Data;
using CPBLDemo.DataAccess.Repository.IRepository;
using CPBLDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPBLDemo.DataAccess.Repository
{
    public class PlayerListRepository : Repository<PlayerList>, IPlayerListRepository
    {
        private readonly dbCpblBaseBallContext _db;
 
        public PlayerListRepository(dbCpblBaseBallContext db):base(db) 
        {  
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(PlayerList obj)
        {
            _db.PlayerList.Update(obj);
        }

    }
}
