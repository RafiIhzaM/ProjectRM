using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectRM.datamodels;
using ProjectRM.viewmodels;

namespace ProjectRM.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiCategoryController : ControllerBase
    {
        private readonly BLQ_ProjectContext db;
        private VMResponse respon = new VMResponse();

        public apiCategoryController(BLQ_ProjectContext _db)
        {
            db = _db;
        }

        [HttpGet("GetAllData")]
        public List<TblCategory> GetAllData()
        {
            List<TblCategory> data = db.TblCategories.Where(a => a.IsDelete == false).ToList();
            return data;
        }

        [HttpGet("GetDataById/{Id}")]
        public TblCategory DataById(int id)
        {
            TblCategory result = db.TblCategories.Where(a => a.Id == id).FirstOrDefault()!;
            return result;
        }

        [HttpGet("CheckCategoryByName/{name}/{id}")]
        public bool CheckName(string name, int id)
        {
            TblCategory data = new TblCategory();
            if (id == 0)
            {
                data = db.TblCategories.Where(a => a.NameCategory == name && a.IsDelete == false).FirstOrDefault()!;
            }
            else
            {
                data = db.TblCategories.Where(a => a.NameCategory == name && a.IsDelete == false && a.Id != id).FirstOrDefault()!;
            }

            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost("Save")]
        public VMResponse Save(TblCategory data)
        {
            data.CreateDate = DateTime.Now;
            data.CreateBy = 1;
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
                respon.Message = "Failed saved : " + ex.Message;
            }

            return respon;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(TblCategory data)
        {
            TblCategory dt = db.TblCategories.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.NameCategory = data.NameCategory;
                dt.UpdateBy = data.UpdateBy;
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
                    respon.Message = "Failed saved : " + ex.Message;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data not found";
            }

            return respon;

        }

        [HttpDelete("Delete/{Id}")]
        public VMResponse Delete(int Id)
        {
            TblCategory dt = db.TblCategories.Where(a => a.Id == Id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.IsDelete = true;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = "Data success delete";
                }
                catch (Exception ex)
                {
                    respon.Success = false;
                    respon.Message = "Failed delete : " + ex.Message;
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
