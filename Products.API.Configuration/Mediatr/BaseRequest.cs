using MediatR;

namespace Products.API.Configuration.Mediatr
{
    public abstract class BaseRequest<TResponse> : IRequest<TResponse>
	  where TResponse : BaseResponse
	{
	}
}
