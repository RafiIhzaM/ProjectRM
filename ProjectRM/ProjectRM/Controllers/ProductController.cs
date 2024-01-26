using Microsoft.AspNetCore.Mvc;
using ProjectRM.datamodels;
using ProjectRM.Service;
using ProjectRM.Services;
using ProjectRM.viewmodels;

namespace ProjectRM.Controllers
{
    public class ProductController : Controller
    {
        private CategoryService categoryService;
        private VariantService variantService;
        private ProductService productService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private int IdUser = 1;

        public ProductController(CategoryService _categoryService, VariantService _variantService, ProductService _productService, IWebHostEnvironment _webHostEnvironment)
        {
            categoryService = _categoryService;
            productService = _productService;
            variantService = _variantService;
            webHostEnvironment = _webHostEnvironment;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSort = sortOrder == "Price" ? "price_desc" : "price";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            List<VMTblProduct> data = await productService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.NameVariant.ToLower().Contains(searchString.ToLower())
                    || a.NameCategory.ToLower().Contains(searchString.ToLower())
                    || a.NameProduct.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.NameProduct).ToList();
                    break;
                case "price_desc":
                    data = data.OrderByDescending(a => a.Price).ToList();
                    break;
                case "price":
                    data = data.OrderBy(a => a.Price).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.NameProduct).ToList();
                    break;
            }

            return View(PaginatedList<VMTblProduct>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }


        public async Task<IActionResult> Create()
        {
            VMTblProduct data = new VMTblProduct();
            List<TblCategory> listCategory = await categoryService.GetAllData();
            ViewBag.ListCategory = listCategory;

            List<VMTblVariant> listVariant = await variantService.GetAllData();
            ViewBag.ListVariant = listVariant;


            return PartialView(data);
        }

        public async Task<JsonResult> GetDataByIdCategory(int id)
        {
            List<VMTblVariant> data = await variantService.GetDataByIdCategory(id);
            return Json(data);

        }


        public async Task<JsonResult> CheckNameIsExist(string name, int id, int idVariant)
        {
            bool isExist = await productService.CheckByName(name, id, idVariant);
            return Json(isExist);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMTblProduct dataParam)
        {

            if (dataParam.ImageFile != null)
            {
                dataParam.Image = Upload(dataParam.ImageFile);
            }
            VMResponse respon = await productService.Create(dataParam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }

        public string Upload(IFormFile ImageFile)
        {
            string uniqueFileName = "";
            if (ImageFile != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        public async Task<IActionResult> Edit(int id)
        {
            VMTblProduct data = await productService.GetDataById(id);

            //untuk drop down List
            List<TblCategory> listCategory = await categoryService.GetAllData();
            ViewBag.ListCategory = listCategory;

            //untuk drop down List
            List<VMTblVariant> listVariant = await variantService.GetAllData();
            ViewBag.ListVariant = listVariant;


            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMTblProduct dataParam)
        {

            if (dataParam.ImageFile != null)
            {
                dataParam.Image = Upload(dataParam.ImageFile);
            }
            VMResponse respon = await productService.Edit(dataParam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }



        public async Task<IActionResult> Detail(int id)
        {
            VMTblProduct data = await productService.GetDataById(id);


            return PartialView(data);
        }


        public async Task<IActionResult> Delete(int id)
        {
            VMTblProduct data = await productService.GetDataById(id);
            return PartialView(data);
        }
        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            VMResponse respon = await productService.Delete(id);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }


    }
}

