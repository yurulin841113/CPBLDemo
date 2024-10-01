using CPBLDemo.Data;
using CPBLDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPBLDemo.Controllers
{
    public class PlayerController : Controller
    {
        private readonly dbCpblBaseBallContext _db;
        public PlayerController(dbCpblBaseBallContext db)
        {
            _db = db;
        }

        #region 首頁     
        public IActionResult Index(int? id, string name)
        {

            IQueryable<PlayerList> query = _db.PlayerList;


            if (id.HasValue)
            {

                query = query.Where(c => c.Id == id.Value);

            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            List<PlayerList> objPlayerList = query.ToList();

            ViewData["id"] = id;
            ViewData["name"] = name;

            return View(objPlayerList);
        }
        #endregion

    }
}
