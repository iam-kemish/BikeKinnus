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
    public class AppUserClass : Repositary<AppUser>, IAppUser
    {
        private readonly ApplicationDbContext _Db;
        public AppUserClass(ApplicationDbContext Db) : base(Db)
        {
            _Db = Db;
        }

     
    }
}
