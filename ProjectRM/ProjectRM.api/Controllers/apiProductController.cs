using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectRM.datamodels;
using ProjectRM.viewmodels;

namespace ProjectRM.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiProductController : ControllerBase
    {
        private readonly BLQ_ProjectContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiProductController(BLQ_ProjectContext _db)
        {
            db = _db;
        }
        [HttpGet("GetAllData")]
        public List<VMTblProduct> GetAllData()
        {
            List<VMTblProduct> data = (from p in db.TblProducts
                                       join v in db.TblVariants on p.IdVariant equals v.Id
                                       join c in db.TblCategories on v.IdCategory equals c.Id
                                       where p.IsDelete == false
                                       select new VMTblProduct
                                       {
                                           Id = p.Id,
                                           NameProduct = p.NameProduct,
                                           Price = p.Price,
                                           Stock = p.Stock,
                                           Image = p.Image,

                                           IdVariant = p.IdVariant,
                                           NameVariant = v.NameVariant,

                                           IdCategory = v.IdCategory,
                                           NameCategory = c.NameCategory,

                                           CreateDate = p.CreateDate
                                       }).ToList();

            return data;
        }


        //LEFT JOIN

        [HttpGet("GetAllData_LeftJoin")]

        public List<VMTblProduct> GetAllData_LeftJoin()
        {
            List<VMTblProduct> data = (from p in db.TblProducts
                                       join v in db.TblVariants on p.IdVariant equals v.Id
                                       join c in db.TblCategories on v.IdCategory equals c.Id into tc
                                       from tcategoty in tc.DefaultIfEmpty()
                                       where p.IsDelete == false //&& tcategoty != null
                                       select new VMTblProduct
                                       {
                                           Id = p.Id,
                                           NameProduct = p.NameProduct,
                                           Price = p.Price,
                                           Stock = p.Stock,
                                           Image = p.Image,

                                           IdVariant = p.IdVariant,
                                           NameVariant = v.NameVariant,

                                           IdCategory = v.IdCategory,
                                           NameCategory = tcategoty.NameCategory ?? "",//jika null maka menjadi empty(fungsi ternary) dan null akan muncul jika where diatas tidak divalidasi

                                           CreateDate = p.CreateDate
                                       }).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMTblProduct GetDataById(int id)
        {
            VMTblProduct data = (from p in db.TblProducts
                                 join v in db.TblVariants on p.IdVariant equals v.Id
                                 join c in db.TblCategories on v.IdCategory equals c.Id
                                 where p.IsDelete == false && p.Id == id
                                 select new VMTblProduct
                                 {
                                     Id = p.Id,
                                     NameProduct = p.NameProduct,
                                     Price = p.Price,
                                     Stock = p.Stock,
                                     Image = p.Image,

                                     IdVariant = p.IdVariant,
                                     NameVariant = v.NameVariant,

                                     IdCategory = v.IdCategory,
                                     NameCategory = c.NameCategory,

                                     CreateDate = p.CreateDate
                                 }).FirstOrDefault()!;
            return data;
        }

        [HttpGet("CheckByName/{name}/{id}/{idVariant}")]
        public bool CheckName(string name, int id, int idVariant)
        {
            TblProduct data = new TblProduct();
            if (id == 0)//create
            {
                data = db.TblProducts.Where(a => a.NameProduct == name && a.IsDelete == false && a.IdVariant == idVariant).FirstOrDefault()!;
            }
            else//edit
            {
                data = db.TblProducts.Where(a => a.NameProduct == name && a.IsDelete == false && a.IdVariant == idVariant
                && a.Id != id).FirstOrDefault()!;
            }

            if (data != null)//untuk saat edit di front end
            {
                return true;
            }
            return false;
        }

        [HttpPost("Save")]
        public VMResponse Save(TblProduct data)
        {
            data.CreateBy = IdUser;
            data.CreateDate = DateTime.Now;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();

                respon.Message = "Data success saved";
            }
            catch (Exception ex)
            {
                respon.Success = false;
                respon.Message = "Failed Saved : " + ex.Message;
            }

            return respon;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(TblProduct data)
        {
            TblProduct dt = db.TblProducts.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.NameProduct = data.NameProduct;
                dt.IdVariant = data.IdVariant;
                dt.Price = data.Price;
                dt.Stock = data.Stock;
                if (data.Image != null)
                {
                    dt.Image = data.Image;
                }
                dt.UpdateBy = IdUser;
                dt.UpdateDate = DateTime.Now;


                try
                {
                    db.Update(dt);
                    db.SaveChanges();


                    respon.Message = "Data success saved";
                }
                catch (Exception ex)
                {
                    respon.Success = false;
                    respon.Message = "Failed saved" + ex.Message;
                }

            }
            else
            {
                respon.Success = false;
                respon.Message = "Data not found";
            }
            return respon;
        }

        [HttpDelete("Delete/{id}")]

        public VMResponse Delete(int id)
        {
            TblProduct dt = db.TblProducts.Where(a => a.Id == id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.UpdateBy = IdUser;
                dt.UpdateDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();


                    respon.Message = "Data success deleted";
                }
                catch (Exception ex)
                {
                    respon.Success = false;
                    respon.Message = "Failed deleted" + ex.Message;
                }

            }
            else
            {
                respon.Success = false;
                respon.Message = "Data not found";
            }
            return respon;
        }
    }
}
