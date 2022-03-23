using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Utility;

namespace Taste.Areas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.FoodType.GetAll() });
        }

        [Authorize(Roles = SD.ManagerRole)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
                return BadRequest();

            var objFromDb = _unitOfWork.FoodType.GetFirstOrDefault(n => n.Id == id);

            if (objFromDb == null)
                return Json(new { success = false, message = "Something wrong while deleting!" });

            _unitOfWork.FoodType.Remove(objFromDb);

            _unitOfWork.save();

            return Json(new { success = true, message = "Successfuly Deleted." });
        }
    }
}
