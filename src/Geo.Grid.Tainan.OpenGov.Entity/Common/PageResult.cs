using System;
using System.Collections.Generic;
using Geo.Grid.Tainan.OpenGov.Entity.Interface;

namespace Geo.Grid.Tainan.OpenGov.Entity.Common
{
    public class PageResult<T> : IResult where T : class
    {
        public PageResult() : this(false)
        {
        }

        public PageResult(bool success)
        {
            PageSize = 10;
            CurrentPage = 1;
            ID = Guid.NewGuid();
            Success = success;
            Data = null;
        }

        public int Total { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        public int CurrentPage { get; set; }

        public Exception Exception { get; set; }

        public Guid ID { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}