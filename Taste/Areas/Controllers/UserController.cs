using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Utility;

namespace Taste.Areas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Json(new { data = _userManager.Users.ToList() });
        }

        [Authorize(Roles = SD.ManagerRole)]
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
                return Json(new { success = false, message = "Somthing wrong happened while locking/unlocking" });

            //LockUnlock(objFromDb);

            _unitOfWork.save();
            return Json(new { success = true, message = " Operation successful" });
        }

    }
}
