using CPBLDemo.DataAccess.Data;
using CPBLDemo.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPBLDemo.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private dbCpblBaseBallContext _db;
        public IPlayerListRepository PlayerList { get; private set; }
        public ITeamRepository Team { get; private set; }

        public UnitOfWork(dbCpblBaseBallContext db)
        {
            _db = db;
            PlayerList = new PlayerListRepository(_db);
            Team = new TeamRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
