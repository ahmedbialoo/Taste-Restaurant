using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Utility;

namespace Taste.Pages.Admin.FoodType
{
    [Authorize(Roles = SD.ManagerRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.FoodType foodTypeObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            foodTypeObj = new Models.FoodType();

            if (id != null)
            {
                foodTypeObj = _unitOfWork.FoodType.GetFirstOrDefault(f => f.Id == id);
                if (foodTypeObj == null)
                    return NotFound();
            }
            return Page();
        }
        [HttpPost]
        public IActionResult OnPost()
        {
            if (foodTypeObj == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return Page();
            if (foodTypeObj.Id == 0)
                _unitOfWork.FoodType.Add(foodTypeObj);
            else
                _unitOfWork.FoodType.Update(foodTypeObj);


            _unitOfWork.save();
            return RedirectToPage("./Index");
        }


    }
}
