using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectManagementWebApp.Gateway.IRepositories;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Models.Context;

namespace ProjectManagementWebApp.Gateway.Repositories
{
    public class ProjectRepository:Repository<Project>, IProjectRepository
    {
        private ApplicationDbContext context;

        public ProjectRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public int RowCount()
        {
            return context.Projects.Where(x => x.State == 1).ToList().Count;
        }

        public Project GetProjectByProjectId(int projectId)
        {
            Project project = context.Projects.Include(x => x.Files).Where(x=>x.Id == projectId).FirstOrDefault();

            return project;
        }

        public List<int> GetProjectCountByMonth()
        {
            List<int> totals = new List<int>();

            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                {
                    string month = "0" + i;
                    int total = context.Projects.Where(x=>x.StartDate.StartsWith(month) && x.StartDate.EndsWith(DateTime.Now.Year.ToString())).ToList().Count;
                    totals.Add(total);
                }
                else
                {
                    int total = context.Projects.Where(x => x.StartDate.StartsWith(i.ToString()) && x.StartDate.EndsWith(DateTime.Now.Year.ToString())).ToList().Count;
                    totals.Add(total);
                }
            }

            return totals;
        }

        public List<int> NumberOfThisAndPrevYearProject()
        {
            List<int> projectCount = new List<int>();
            int year = DateTime.Now.Year;

            for (int i = 0; i < 3; i++)
            {
                int count = context.Projects.Where(x => x.StartDate.Contains(year.ToString())).ToList().Count;
                year--;
                projectCount.Add(count);
            }

            return projectCount;
        }
    }
}
