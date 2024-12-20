using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.RepositaryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    public class CompanyClass :  Repositary<Company>,  ICompany
    {
        private ApplicationDbContext _Db;
        public CompanyClass(ApplicationDbContext Db) : base(Db)
        {
            _Db = Db;
        }

        public void Save()
        {
           _Db.SaveChanges();
        }

        public void Update(Company company)
        {
            _Db.companies.Update(company);
        }
    }
}
