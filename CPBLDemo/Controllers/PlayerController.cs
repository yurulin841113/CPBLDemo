using CPBLDemo.Data;
using CPBLDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public IActionResult Index()
        {
            List<PlayerList> objPlayerList = _db.PlayerList.ToList();

            return View(objPlayerList);
        }

        [HttpGet]
        public IActionResult Index(int? id, string name, int? number)
        {
            ViewData["id"] = id;
            ViewData["name"] = name;
            ViewData["number"] = number;

            IQueryable<PlayerList> query = _db.PlayerList;


            if (id.HasValue && id > 0)
            {
                // 確認 id.Value 不是 null
                query = query.Where(c => c.Id.ToString().Contains(id.Value.ToString()));
            }

            if (number.HasValue && number > 0)
            {
                // 確認 number.Value 不是 null
                query = query.Where(c => c.Number.ToString().Contains(number.Value.ToString()));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            List<PlayerList> objPlayerList = query.ToList();

            return View(objPlayerList);
        }
        #endregion

        #region 新增    
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PlayerList objPlayerlist)
        {
            if (ModelState.IsValid)
            {

                objPlayerlist.CreatedTime = DateTime.Now;

                _db.PlayerList.Add(objPlayerlist);

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion
        #region 刪除
        [HttpPost]
        public IActionResult Delete(int id)
        {
            PlayerList? query = _db.PlayerList.Find(id);


            if (query != null)
            {
                _db.PlayerList.Remove(query); // 刪除操作
                _db.SaveChanges(); // 保存變更
                return RedirectToAction("Index");
            }

            return View();

        }
        #endregion
    }
}
