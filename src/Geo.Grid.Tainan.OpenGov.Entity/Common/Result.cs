using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity.Interface;

namespace Geo.Grid.Tainan.OpenGov.Entity.Common
{
    public class Result<T> : IResult
         where T : class
    {
        public Result()
            : this(false)
        {
        }

        public Result(bool success)
        {
            ID = Guid.NewGuid();
            Success = success;
            Data = null;
        }

        public Result(string message)
        {
            ID = Guid.NewGuid();
            Success = false;
            Message = message;
        }

        public T Data
        {
            get;
            set;
        }

        #region IResult 成員

        public Exception Exception
        {
            get;
            set;
        }

        public Guid ID
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }

        #endregion IResult 成員
    }
}