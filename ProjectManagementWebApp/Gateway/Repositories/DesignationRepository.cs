using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Gateway.IRepositories;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.Context;

namespace ProjectManagementWebApp.Gateway.Repositories
{
    public class DesignationRepository:Repository<Designation>, IDesignationRepository
    {
        private ApplicationDbContext context;

        public DesignationRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public int RowCount()
        {
            return context.Designations.ToList().Count;
        }
    }
}
