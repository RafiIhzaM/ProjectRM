using Microsoft.AspNetCore.Mvc;
using ProjectRM.datamodels;
using ProjectRM.Service;
using ProjectRM.Services;
using ProjectRM.viewmodels;

namespace ProjectRM.Controllers
{
    public class VariantController : Controller
    {
        private CategoryService categoryService;
        private VariantService variantService;
        private int IdUser = 1;

        public VariantController(CategoryService _categoryService, VariantService _variantService)
        {
            categoryService = _categoryService;
            this.variantService = _variantService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
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

            List<VMTblVariant> data = await variantService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.NameVariant.ToLower().Contains(searchString.ToLower())
                    || a.NameCategory.ToLower().Contains(searchString.ToLower())
                    || a.Description.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.NameVariant).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.NameVariant).ToList();
                    break;
            }

            return View(PaginatedList<VMTblVariant>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));

        }
        public async Task<IActionResult> Create()
        {
            VMTblVariant data = new VMTblVariant();

            List<TblCategory> listCategory = await categoryService.GetAllData();
            ViewBag.ListCategory = listCategory;

            return PartialView(data);
        }

        public async Task<JsonResult> CheckNameIsExist(string name, int id, int idCategory)
        {
            bool isExist = await variantService.CheckByName(name, id, idCategory);
            return Json(isExist);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMTblVariant dataParam)
        {
            dataParam.CreateBy = IdUser;
            VMResponse respon = await variantService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }


        public async Task<IActionResult> Edit(int id)
        {
            VMTblVariant data = await variantService.GetDataById(id);

            List<TblCategory> listCategory = await categoryService.GetAllData();
            ViewBag.ListCategory = listCategory;

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMTblVariant dataParam)
        {
            VMResponse respon = await variantService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }

        public async Task<IActionResult> Detail(int id)
        {
            VMTblVariant data = await variantService.GetDataById(id);
            return PartialView(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VMTblVariant data = await variantService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            VMResponse respon = await variantService.Delete(id);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }





    }
}

