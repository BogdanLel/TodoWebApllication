using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoWebApllication.Models;
using TodoWebAplication.BussinesLogic;
using TodoWebApllication.Models.ViewModels;

namespace TodoWebApllication.Controllers
{
    [RoutePrefix("api/FileApi")]
    public class FileApiController : ApiController
    {
        private Entities _db;

        public FileApiController()
        {
            _db = new Entities();
        }

        [Route("UploadFile")]
        [HttpPost]
        public IHttpActionResult UploadFile(FileEntity fileEntity)
        {
            try
            {
                FileLogic fileLogic = new FileLogic(_db);
                fileLogic.WriteFileToDisk(fileEntity.FileBase64, fileEntity.FileName, fileEntity.TodoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [Route("DownloadFile")]
        [HttpGet]
        public IHttpActionResult DownloadFile(int fileId)
        {
            try
            {
                FileLogic fileLogic = new FileLogic(_db);
                var result = fileLogic.ReadFileBytesFromDisk(fileId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
