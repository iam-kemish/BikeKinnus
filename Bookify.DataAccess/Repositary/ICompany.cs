using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeKinnus.DataAccess.Repositary
{
    public interface ICompany:IRepositary<Company>
    {
        void Update(Company company);
        void Save();
       
    }
}
