using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Gateway.IRepositories
{
    public interface IProjectRepository:IRepository<Project>
    {
        int RowCount();
        Project GetProjectByProjectId(int projectId);

        List<int> GetProjectCountByMonth();

        List<int> NumberOfThisAndPrevYearProject();
    }
}
