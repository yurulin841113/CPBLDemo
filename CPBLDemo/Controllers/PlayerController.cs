using CPBLDemo.Data;
using CPBLDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace CPBLDemo.Controllers
{
    public class PlayerController : Controller
    {
        private readonly dbCpblBaseBallContext _db;
        public PlayerController(dbCpblBaseBallContext db)
        {
            _db = db;
        }

        #region 取得所有球隊做下拉選單功能
        private List<Team> GetAllTeams()
        {
            return _db.Team.ToList();
        }
        #endregion

        #region 查詢     
        public IActionResult Index()
        {
            var objPlayerList = _db.PlayerList.Include(p => p.Team).ToList(); // 包含團隊資料

            return View(objPlayerList);
        }
        [HttpGet]
        public IActionResult Index(int? id, string? name, int? number, int? teamid)
        {
            ViewData["id"] = id;
            ViewData["name"] = name;
            ViewData["number"] = number;
            ViewData["teamid"] = teamid; 
            ViewBag.Teams = GetAllTeams();

            IQueryable<PlayerList> query = _db.PlayerList.Include(p => p.Team); // 提前包含 Team 以提高效能

            if (id.HasValue && id > 0)
            {
                query = query.Where(c => c.Id.ToString().Contains(id.Value.ToString()));
            }

            if (number.HasValue && number > 0)
            {
                query = query.Where(c => c.Number.ToString().Contains(number.Value.ToString()));
            }

            if (teamid.HasValue)
            {
                query = query.Where(c => c.Team.TeamId == teamid.Value);
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
            ViewBag.Teams = GetAllTeams();

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

                TempData["success"] = "新增成功!!";

                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region 編輯
        public IActionResult Edit(int? id)
        {
            ViewBag.Teams = GetAllTeams();

            if (id == null || id == 0)
            {
                return NotFound();
            }

            PlayerList? playerlistFromDb =  _db.PlayerList.Include(p => p.Team).FirstOrDefault(p => p.Id == id);

            if (playerlistFromDb == null)
            {
                return NotFound();
            }

            return View(playerlistFromDb);
        }
        [HttpPost]
        public IActionResult Edit(PlayerList objPlayerlist)
        {
            if (ModelState.IsValid)
            {
                _db.PlayerList.Update(objPlayerlist);

                _db.SaveChanges();

                TempData["success"] = "編輯成功!!";

                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region 確認後刪除
        public IActionResult Delete(int? id)
        {
            ViewBag.Teams = GetAllTeams();

            if (id == null || id == 0)
            {
                return NotFound();
            }

            PlayerList? playerlistFromDb = _db.PlayerList.Find(id);

            if (playerlistFromDb == null)
            {
                return NotFound();
            }

            return View(playerlistFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            PlayerList? objPlayerlist = _db.PlayerList.Find(id);

            if (objPlayerlist == null)
            {
                return NotFound();
            }

            _db.PlayerList.Remove(objPlayerlist); // 刪除操作
            _db.SaveChanges(); // 保存變更
            TempData["success"] = "刪除成功!!";
            return RedirectToAction("Index");
        }

        #endregion

        #region 直接刪除  
        //[HttpPost]
        //public IActionResult Delete(int? id)
        //{

        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    PlayerList? query = _db.PlayerList.Find(id);

        //    if (query != null)
        //    {
        //        _db.PlayerList.Remove(query); // 刪除操作
        //        _db.SaveChanges(); // 保存變更
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        #endregion
    }
}
