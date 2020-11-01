using Products.API.Configuration.Mediatr;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Products.API.Application.Features
{
    public class GetProducts
    {
        public class Request : BaseRequest<Response>
        {
            public Guid ProductId { get; set; }
        }

        public class Response : BaseResponse
        {
            
        }


        public class Handler : BaseHandler<Request, Response>
        {
            public Handler()
            {
                
            }

            public override async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                Response response = new Response();

                return response;
            }
        }
    }
}
