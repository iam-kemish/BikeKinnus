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
            Company company = new Company();

            if (id == null)
            {
                // Create mode
                return View(company);
            }
            else
            {
                // Update mode
                company = _ICompany.Get(u => u.Id == id);
                if (company == null)
                {
                    TempData["error"] = "Company not found.";
                    return NotFound();
                }
                return View(company);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(Company company, int? id)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid data. Please check the form and try again.";
                return View(company);
            }

            if (id == null || id == 0)
            {
                // Create new company
                _ICompany.Add(company);
                _ICompany.Save();
                TempData["success"] = "Company created successfully.";
            }
            else
            {
                // Update existing company
                var existingCompany = _ICompany.Get(u => u.Id == id);
                if (existingCompany != null)
                {
                    existingCompany.State = company.State;
                    existingCompany.StreetAddress = company.StreetAddress;
                    existingCompany.PostalCode = company.PostalCode;
                    existingCompany.Name = company.Name;
                    existingCompany.City = company.City;

                    _ICompany.Update(existingCompany);
                    _ICompany.Save();
                    TempData["success"] = "Company updated successfully.";
                }
                else
                {
                    TempData["error"] = "Company not found for update.";
                    return NotFound();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Invalid company ID.";
                return NotFound();
            }

            Company company = _ICompany.Get(u => u.Id == id);
            if (company == null)
            {
                TempData["error"] = "Company not found.";
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var companyToDelete = _ICompany.Get(u => u.Id == id);
            if (companyToDelete == null)
            {
                TempData["error"] = "Company not found.";
                return NotFound();
            }

            _ICompany.Remove(companyToDelete);
            _ICompany.Save();
            TempData["success"] = "Company deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
