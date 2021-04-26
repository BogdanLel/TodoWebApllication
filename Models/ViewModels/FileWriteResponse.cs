using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebApllication.Models.ViewModels
{
    public class FileWriteResponse
    {
        public string Text { get; set; }
        public bool IsError { get; set; }
    }
}