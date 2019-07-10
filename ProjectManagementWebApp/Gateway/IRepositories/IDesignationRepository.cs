using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Gateway.IRepositories
{
    public interface IDesignationRepository:IRepository<Designation>
    {
        int RowCount();
    }
}
