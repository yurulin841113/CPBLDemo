using CPBLDemo.DataAccess.Repository.IRepository;
using CPBLDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPBLDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<Team> objTeam = _unitOfWork.Team.GetAll().ToList(); // 包含團隊資料

            return View(objTeam);
        }

        #region 新增    
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team obj)
        {
            if (obj.TeamId.ToString() == obj.TeamName.ToString())
            {
                ModelState.AddModelError("TeamId", "球隊編號不能跟球隊名稱一致。");
            }
            var existingTeamId = _unitOfWork.Team.Get(t => t.TeamId == obj.TeamId);
            if (existingTeamId != null)
            {
                ModelState.AddModelError("TeamId", "該球隊編號已存在，請使用其他編號。");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Team.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "球隊新增成功！";
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region 編輯
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team? teamFromDb = _unitOfWork.Team.Get(u => u.TeamId == id);
            if (teamFromDb == null)
            {
                return NotFound();
            }
            return View(teamFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Team obj)
        {
            if (obj.TeamId.ToString() == obj.TeamName.ToString())
            {
                ModelState.AddModelError("TeamId", "球隊編號不能跟球隊名稱一致。");
            }
            

            if (ModelState.IsValid)
            {
                _unitOfWork.Team.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "球隊編輯成功！";
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region 刪除
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team teamFromDb = _unitOfWork.Team.Get(u => u.TeamId == id);
            if (teamFromDb == null)
            {
                return NotFound();
            }
            return View(teamFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Team? obj = _unitOfWork.Team.Get(u => u.TeamId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Team.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "球隊刪除成功！";
            return RedirectToAction("Index");
        }
        #endregion

    }
}
