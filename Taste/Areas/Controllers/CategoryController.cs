using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Utility;

namespace Taste.Areas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Category.GetAll() });
        }


        [Authorize(Roles = SD.ManagerRole)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var CategoryFromDb = _unitOfWork.Category.GetFirstOrDefault(n => n.Id == id);
            if (CategoryFromDb == null)
                return Json(new { success = false, message = "Error Happened While Deleting!" });

            _unitOfWork.Category.Remove(CategoryFromDb);

            _unitOfWork.save();

            return Json(new { success = true, message = "Deleted Successfuly." });

        }
    }
}
