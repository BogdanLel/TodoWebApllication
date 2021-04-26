using System;
using System.Data.Entity;
using System.IO;
using System.Web;
using TodoWebApllication.Models;
using TodoWebApllication.Models.Services;
using TodoWebApllication.Models.ViewModels;

namespace TodoWebAplication.BussinesLogic
{
    public class FileLogic
    {
        private DbContext _db;
        private FileService _fileService;

        public FileLogic(DbContext db)
        {
            _db = db;
            _fileService = new FileService(_db);
        }


        public FileWriteResponse WriteFileToDisk(string fileBase64, string fileName, int todoId)
        {
            FileWriteResponse response = new FileWriteResponse();
            try
            {
                if (!FileExist(fileName))
                {
                    var pathLocation = HttpContext.Current.Server.MapPath("/AttachmentDirectory");
                    string fileLocation = Path.Combine(pathLocation, fileName);
                    fileBase64 = fileBase64.Substring(fileBase64.IndexOf(",") + 1);
                    File.WriteAllBytes(fileLocation, Convert.FromBase64String(fileBase64));
                    var fileTable = new FilesTable()
                    {
                        Name = fileName,
                        TodoId = todoId,
                    };
                    _fileService.Add(fileTable);
                    response.Text = "File uploaded successfull!";
                }
                else
                {
                    throw new Exception("File already  exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File can not be uploaded! " + ex.Message);
            }
            return response;
        }

        public byte[] ReadFileBytesFromDisk(int fileId)
        {
            byte[] fileBytes = null;
            try
            {
                var currentFile = _fileService.Get(fileId);
                var pathLocation = HttpContext.Current.Server.MapPath("/AttachmentDirectory");
                string fileLocation = Path.Combine(pathLocation, currentFile.Name);
                fileBytes = File.ReadAllBytes(fileLocation);
            }
            catch (Exception ex)
            {
                throw new Exception("cannot read file" + ex.Message);
            }

            return fileBytes;
        }

        public string RemoveFile(string fileName)
        {
            try
            {
                if (FileExist(fileName))
                {
                    var path = HttpContext.Current.Server.MapPath("/AttachmentDirectory/" + fileName);
                    File.Delete(path);
                    return "OK!";
                }

                throw new Exception("File not exist!");

            }
            catch (Exception ex)
            {
                throw new Exception("File can not be deleted! " + ex.Message);
            }

        }

        public bool FileExist(string fileName)
        {
            try
            {
                var path = HttpContext.Current.Server.MapPath("AttachmentDirectory/" + fileName);
                if (File.Exists(path))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File not exist!" + ex.Message);
            }
        }

    }
}