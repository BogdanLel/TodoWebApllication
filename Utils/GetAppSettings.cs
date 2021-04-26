using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TodoWebApllication.Utils
{
    namespace Timesheet.Utils
    {
        public static class GetAppSettings
        {
            public static string GetAttachmentDirectoryPath()
            {
                return Convert.ToString(ConfigurationManager.AppSettings["AttachmentDirectory"]);
            }
        }
    }
}