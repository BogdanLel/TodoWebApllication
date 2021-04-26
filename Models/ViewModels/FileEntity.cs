using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebApllication.Models.ViewModels
{
    public class FileEntity
    {
        public string FileBase64 { get; set; }
        public string FileName { get; set; }
        public int TodoId { get; set; }
    }
}