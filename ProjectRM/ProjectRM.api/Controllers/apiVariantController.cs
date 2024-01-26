using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectRM.datamodels;
using ProjectRM.viewmodels;
using System.Data;

namespace ProjectRM.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiVariantController : ControllerBase
    {
        private readonly BLQ_ProjectContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiVariantController(BLQ_ProjectContext _db)
        {
            db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMTblVariant> GetAllData()
        {
            List<VMTblVariant> data = (from v in db.TblVariants
                                       join c in db.TblCategories on v.IdCategory equals c.Id
                                       where v.IsDelete == false
                                       select new VMTblVariant
                                       {
                                           Id = v.Id,
                                           NameVariant = v.NameVariant,
                                           Description = v.Description,

                                           IdCategory = v.IdCategory,
                                           NameCategory = c.NameCategory,

                                           IsDelete = v.IsDelete,
                                           CreateDate = v.CreateDate,
                                       }).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMTblVariant GetDataById(int id)
        {
            VMTblVariant data = (from v in db.TblVariants
                                 join c in db.TblCategories on v.IdCategory equals c.Id
                                 where v.IsDelete == false && v.Id == id
                                 select new VMTblVariant
                                 {
                                     Id = v.Id,
                                     NameVariant = v.NameVariant,
                                     Description = v.Description,

                                     IdCategory = v.IdCategory,
                                     NameCategory = c.NameCategory,

                                     IsDelete = v.IsDelete
                                 }).FirstOrDefault()!;
            return data;
        }
        [HttpGet("CheckByName/{name}/{id}/{idCategory}")]
        public bool CheckName(string name, int id, int idCategory)
        {
            TblVariant data = new TblVariant();
            if (id == 0)
            {
                data = db.TblVariants.Where(a => a.NameVariant == name && a.IsDelete == false && a.IdCategory == idCategory).FirstOrDefault()!;
            }
            else
            {
                data = db.TblVariants.Where(a => a.NameVariant == name && a.IsDelete == false && a.IdCategory == idCategory
                && a.Id != id).FirstOrDefault()!;
            }

            if (data != null)//untuk saat edit di front end
            {
                return true;
            }
            return false;
        }

        [HttpGet("GetDataByIdCategory/{id}")]
        public List<VMTblVariant> GetDataByIdCategory(int id)
        {
            List<VMTblVariant> data = (from v in db.TblVariants
                                       join c in db.TblCategories on v.IdCategory equals c.Id
                                       where v.IsDelete == false && v.IdCategory == id
                                       select new VMTblVariant
                                       {
                                           Id = v.Id,
                                           NameVariant = v.NameVariant,
                                           Description = v.Description,

                                           IdCategory = v.IdCategory,
                                           NameCategory = c.NameCategory,
                                       }).ToList();
            return data;
        }

        [HttpPost("Save")]
        public VMResponse Save(TblVariant data)
        {
            data.Description = data.Description ?? "";
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
        public VMResponse Edit(TblVariant data)
        {
            TblVariant dt = db.TblVariants.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.NameVariant = data.NameVariant;
                dt.Description = data.Description;
                dt.IdCategory = data.IdCategory;
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
            TblVariant dt = db.TblVariants.Where(a => a.Id == id).FirstOrDefault()!;

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

