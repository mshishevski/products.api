namespace Products.API.Configuration.Mediatr
{
    public class BaseValidator<TRequest, TResponse> : FluentValidation.AbstractValidator<TRequest>
			   where TRequest : BaseRequest<TResponse>
			   where TResponse : BaseResponse
	{
		public virtual void Setup()
		{

		}
	}
}
