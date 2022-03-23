using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Utility;

namespace Taste.Areas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType") });
        }

        [Authorize(Roles = SD.ManagerRole)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(n => n.Id == id);

                if (objFromDb == null)
                    return Json(new { success = false, message = "Something wrong while deleting!" });

                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);

                _unitOfWork.MenuItem.Remove(objFromDb);

                _unitOfWork.save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Something wrong while deleting!" });
            }

            return Json(new { success = true, message = "Successfuly Deleted." });
        }
}
}
