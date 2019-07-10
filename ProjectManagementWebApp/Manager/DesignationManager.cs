using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementWebApp.Gateway.UnitOfWork;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Utility;

namespace ProjectManagementWebApp.Manager
{
    public class DesignationManager
    {
        private UnitOfWork unitOfWork;

        public DesignationManager()
        {
            unitOfWork = new UnitOfWork();
        }

        // save designation
        public string Save(Designation designation)
        {
            if (unitOfWork.Designation.IsExists(x => x.DesignationName == designation.DesignationName && x.State != 0))
            {
                return Alert.AlertGenerate("Warning","Already Exists","Designation already exists");
            }
            else
            {
                unitOfWork.Designation.Add(designation);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return Alert.AlertGenerate("Success", "Success", "Designation Saved Successfully");
                }
                else
                {
                    return Alert.AlertGenerate("Failed", "Failed", "Failed to Save Designation");
                }
            }
        }

        // get all designation
        public IEnumerable<Designation> GetAll()
        {
            return unitOfWork.Designation.Get(x => x.State == 1);
        }

        // get by id
        public Designation GetById(int id)
        {
            return unitOfWork.Designation.Find(x=>x.Id == id && x.State == 1);
        }

        // update
        public string Update(Designation designation)
        {
            if (unitOfWork.Designation.IsExists(x =>
                x.Id != designation.Id && x.DesignationName == designation.DesignationName && x.State == 1))
            {
                return Alert.AlertGenerate("Warning", "Already Exists", "Designation already exists");
            }
            else
            {
                unitOfWork.Designation.Update(designation);
                int rowsAffected = unitOfWork.Complete();

                if (rowsAffected > 0)
                {
                    return "1";
                }
                else
                {
                    return Alert.AlertGenerate("Failed", "Failed", "Failed to Update Designation");
                }
            }
        }

        // get designations for dropdown
        public List<SelectListItem> GetDesignationsForDropDown()
        {
            List<Designation> designations = (List<Designation>) GetAll();
            List<SelectListItem> selectListItems = designations.ConvertAll(x => new SelectListItem()
                {Text = x.DesignationName, Value = x.Id.ToString()});
            return selectListItems;
        }

        // check designation is exists or not
        public bool IsDesignationExists(int designationId)
        {
            return unitOfWork.Designation.IsExists(x => x.Id == designationId && x.State == 1);
        }
    }
}
