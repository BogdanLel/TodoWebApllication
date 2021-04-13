using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebAplication.Models.ViewModels
{
    public class GenericViewModel
    {

    }

    public class Filter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string FilterText { get; set; }
        public string OrderBy { get; set; }
    }

    public class PagingListViewModel<T>
    {
        public int NumberOfPages { get; set; }
        public int Count { get; set; }
        public List<T> Items { get; set; }
    }
}