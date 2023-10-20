using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public IEnumerable<ValidationFailure>? ValidationErrors { get; set; }

        public IEnumerable<string>? ApplicationErrors { get; set; }
    }
}
