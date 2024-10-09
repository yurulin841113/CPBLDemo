using CPBLDemo.DataAccess.Data;
using CPBLDemo.DataAccess.Repository.IRepository;
using CPBLDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPBLDemo.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerListRepository _playerListRepo;
        private readonly ITeamRepository _teamRepo;
        public PlayerController(IPlayerListRepository playerListRepo, ITeamRepository teamRepo)
        {
            _playerListRepo = playerListRepo;
            _teamRepo = teamRepo;
        }

        #region 查詢     
        public IActionResult Index()
        {
            List<PlayerList> objPlayerList = _playerListRepo.GetAll().OrderBy(c => c.TeamId).ToList(); // 包含團隊資料

            return View(objPlayerList);
        }
        [HttpGet]
        public IActionResult Index(int? id, string? name, int? number, int? teamid)
        {
            ViewData["id"] = id;
            ViewData["name"] = name;
            ViewData["number"] = number;
            ViewData["teamid"] = teamid;
            ViewBag.Teams = _teamRepo.GetAll().ToList();// 取得所有球隊做下拉選單功能

            IQueryable<PlayerList> query = _playerListRepo.GetAll().AsQueryable();

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

            List<PlayerList> objPlayerList = query.OrderBy(c => c.TeamId).ToList();

            return View(objPlayerList);
        }
        #endregion

        #region 新增    
        public IActionResult Create()
        {
            ViewBag.Teams = _teamRepo.GetAll().ToList();// 取得所有球隊做下拉選單功能

            return View();
        }
        [HttpPost]
        public IActionResult Create(PlayerList objPlayerlist)
        {
            if (ModelState.IsValid)
            {
                objPlayerlist.CreatedTime = DateTime.Now;

                _playerListRepo.Add(objPlayerlist);

                _playerListRepo.Save();

                TempData["success"] = "新增成功!!";

                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region 編輯
        public IActionResult Edit(int? id)
        {
            ViewBag.Teams = _teamRepo.GetAll().ToList();// 取得所有球隊做下拉選單功能

            if (id == null || id == 0)
            {
                return NotFound();
            }

            PlayerList? playerlistFromDb = _playerListRepo.Get(c => c.Id == id);

            if (playerlistFromDb == null)
            {
                return NotFound();
            }

            playerlistFromDb.Height = Math.Floor(playerlistFromDb.Height);
            playerlistFromDb.Weight = Math.Floor(playerlistFromDb.Weight);

            return View(playerlistFromDb);
        }
        [HttpPost]
        public IActionResult Edit(PlayerList objPlayerlist)
        {
            if (ModelState.IsValid)
            {
                _playerListRepo.Update(objPlayerlist);

                _playerListRepo.Save();

                TempData["success"] = "編輯成功!!";

                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region 確認後刪除
        public IActionResult Delete(int? id)
        {
            ViewBag.Teams = _teamRepo.GetAll().ToList();// 取得所有球隊做下拉選單功能

            if (id == null || id == 0)
            {
                return NotFound();
            }

            PlayerList? playerlistFromDb = _playerListRepo.Get(c => c.Id == id);

            if (playerlistFromDb == null)
            {
                return NotFound();
            }

            playerlistFromDb.Height = Math.Floor(playerlistFromDb.Height);
            playerlistFromDb.Weight = Math.Floor(playerlistFromDb.Weight);

            return View(playerlistFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            PlayerList? objPlayerlist = _playerListRepo.Get(c => c.Id == id);

            if (objPlayerlist == null)
            {
                return NotFound();
            }

            _playerListRepo.Remove(objPlayerlist); // 刪除操作
            _playerListRepo.Save(); // 保存變更
            TempData["success"] = "刪除成功!!";
            return RedirectToAction("Index");
        }
        #endregion

        //#region 直接刪除  
        ////[HttpPost]
        ////public IActionResult Delete(int? id)
        ////{

        ////    if (id == null || id == 0)
        ////    {
        ////        return NotFound();
        ////    }

        ////    PlayerList? query = _db.PlayerList.Find(id);

        ////    if (query != null)
        ////    {
        ////        _db.PlayerList.Remove(query); // 刪除操作
        ////        _db.SaveChanges(); // 保存變更
        ////        return RedirectToAction("Index");
        ////    }
        ////    else
        ////    {
        ////        return NotFound();
        ////    }
        ////}
        //#endregion
    }
}
