using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProjectRM.datamodels;
using ProjectRM.Services;
using ProjectRM.viewmodels;

namespace ProjectRM.Controllers
{
    public class CategoryController : Controller
    {

        private CategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;


        public CategoryController(CategoryService _categoryService, IWebHostEnvironment _webHostEnvironment)
        {
            categoryService = _categoryService;
            webHostEnvironment = _webHostEnvironment;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.currentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<TblCategory> data = await categoryService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.NameCategory.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.NameCategory).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.NameCategory).ToList();
                    break;
            }

            return View(PaginatedList<TblCategory>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            TblCategory data = new TblCategory();
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMTblCategory dataParam)
        {
            if (dataParam.ImageFile != null)
            {
                dataParam.Image = Upload(dataParam.ImageFile);
            }

            VMResponse respon = await categoryService.Create(dataParam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

        public async Task<JsonResult> CheckNameIsExist(string nameCategory, int id)
        {
            bool isExist = await categoryService.CheckCategoryByName(nameCategory, id);
            return Json(isExist);
        }

        public string Upload(IFormFile ImageFile)
        {
            string uniqueFileName = "";

            if (ImageFile != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string FilePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
            }


            return uniqueFileName;
        }

        public async Task<IActionResult> Edit(int id)
        {
            TblCategory data = await categoryService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory dataParam)
        {
            dataParam.UpdateBy = 1;
            dataParam.UpdateDate = DateTime.Now;
            VMResponse respon = await categoryService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

        public async Task<IActionResult> Detail(int id)
        {
            TblCategory data = await categoryService.GetDataById(id);
            return PartialView(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TblCategory data = await categoryService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = 1;
            VMResponse respon = await categoryService.Delete(id);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }


    }
}
