using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.API.Application.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.API.Controllers
{
	public class ProductController : BaseController
	{
		public ProductController(IMediator mediator) : base(mediator)
		{

		}

		

		/// <summary>
		/// Gets a product by ID
		/// </summary>
		/// <param name="productId">ID of Product</param>
		/// <returns></returns>
		[HttpGet("product")]
		public async Task<GetProducts.Response> GetProductById([FromQuery] Guid productId)
		{
			GetProducts.Request request = new GetProducts.Request { ProductId = productId };

			return await Handle(request);
		}

		

	}
}
