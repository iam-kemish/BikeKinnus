using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeKinnus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly ICompany _ICompany;
        public CompanyController(ApplicationDbContext db, ICompany company)
        {
            _Db = db;   
            _ICompany = company;
        }
        public IActionResult Index()
        {
           List<Company> companies = _ICompany.GetAll().ToList();
            return View(companies);
        }
       
        public IActionResult CreateUpdate(int? id)
        {
            Company Company = new Company();
           if(id== null)
            {
                return View(Company);
            }else
            {
                Company = _ICompany.Get(u => u.Id == id);
                return View(Company);
            }
        }
        [HttpPost]
        public IActionResult CreateUpdate(Company company, int? id) 
        {
            if(id == null || id == 0)
            {
                _ICompany.Add(company);
                _ICompany.Save();
                TempData["success"] = "Company created successfully.";
            }
            var existingCompany = _ICompany.Get(u => u.Id == id);
            if(existingCompany != null)
            {
                existingCompany.State = company.State;
                existingCompany.StreetAddress = company.StreetAddress;
                existingCompany.PostalCode = company.PostalCode;
                existingCompany.Name = company.Name;
                existingCompany.City = company.City;

                _ICompany.Update(existingCompany);
                _ICompany.Save();
            }


            return (RedirectToAction("Index"));
        }
    public IActionResult Delete( int? id)
    {
           if(id==0 || id == null)
            {
                return NotFound();
            }
           
                Company company = _ICompany.Get(u => u.Id == id);
                if (company != null)
                {
                    return View(company);
                }
            return View(company);
          
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var CompanyToDelete = _ICompany.Get(u => u.Id == id);
            if(CompanyToDelete == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _ICompany.Remove(CompanyToDelete);
                _ICompany.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
  

   

}
