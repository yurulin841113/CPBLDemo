using CPBLDemo.DataAccess.Data;
using CPBLDemo.DataAccess.Repository.IRepository;
using CPBLDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPBLDemo.DataAccess.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly dbCpblBaseBallContext _db;

        public TeamRepository(dbCpblBaseBallContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Team obj)
        {
            _db.Team.Update(obj);
        }
    }
}
