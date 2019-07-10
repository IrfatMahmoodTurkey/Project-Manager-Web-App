using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Utility
{
    public static class Alert
    {
        public static string SuccessAlert { get; set; }

        public static string FaliedAlert { get; set; }
        public static string WarningAlert { get; set; }

        public static string AlertGenerate(string type, string message, string description)
        {
            if (type.Equals("Success"))
            {
                SuccessAlert = string.Format(
                    "<div class=\"alert alert-success alert-dismissible fadein \"><a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>\"<strong>"+message+"!</strong> "+description+ "</div>");

                return SuccessAlert;
            }
            else if (type.Equals("Falied"))
            {
                FaliedAlert =
                    string.Format(
                        "<div class=\"alert alert-danger alert-dismissible fadein \"><a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a><strong>"+message+"!</strong> "+description+"</div>");
                return FaliedAlert;
            }
            else
            {
                WarningAlert = string.Format(
                    "<div class=\"alert alert-warning alert-dismissible fadein \"><a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a><strong>"+message+"!</strong> "+description+"</div>");
                return WarningAlert;
            }
        }
    }
}
