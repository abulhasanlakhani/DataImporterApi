using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Domain
{
    public class Response
    {
        public Response() { }

        public bool Success { get; set; }

    }

    public class Response<T> : Response
    {
        public Response() { }

        public T Payload { get; set; }
    }
}
