using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Manager
{
    public class FileManager
    {
        private UnitOfWork unitOfWork;

        public FileManager()
        {
            unitOfWork = new UnitOfWork();
        }

        // get project file name
        public IEnumerable<File> GetFile(int projectId)
        {
            return unitOfWork.File.Get(x => x.ProjectId == projectId && x.State == 1);
        }

        // get file name for dropdown
        public List<SelectListItem> GetFileForDropDown(int projectId)
        {
            List<File> files = (List<File>) GetFile(projectId);
            List<SelectListItem> selectListItems = files.ConvertAll(x => new SelectListItem()
                {Value = x.Id.ToString(), Text = x.FileName});
            return selectListItems;
        }

        // get file by project id
        public File GetFileById(int id)
        {
            return unitOfWork.File.Find(x => x.Id == id && x.State == 1);
        }
    }
}
