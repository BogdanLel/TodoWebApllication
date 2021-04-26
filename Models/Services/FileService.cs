using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoWebAplication.Models.Services;
using TodoWebApllication.Models;

namespace TodoWebApllication.Models.Services
{
    public class FileService : BaseService<FilesTable>
    {
        public FileService(DbContext context) : base(context)
        {

        }
    }
}