using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.API.DataAccess;
using Products.API.DataAccess.Entities;
using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Products.API.Configuration.Mediatr
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
				where TRequest : IRequest<TResponse>
	{
		ProductsDbContext _dbContext;
		protected ProductsDbContext _ef => _dbContext;
		public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);



		[SetterProperty]
		public ProductsDbContext DbContext
		{
			set
			{
				if (_dbContext != null)
					throw new Exception("DbContext has already beeen initialized!");
				else
					_dbContext = value;
			}
		}


        public IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity => _ef.Set<TEntity>().AsNoTracking();

        public async Task SaveChanges()
		{
			await _dbContext.SaveChangesAsync();
		}

		protected void Update(BaseEntity entity, Guid personId)
		{

			if (entity.CreatedBy == Guid.Empty)
			{
				entity.CreatedBy = personId;
				entity.CreatedOn = DateTime.UtcNow;
			}
			entity.ModifiedOn = DateTime.UtcNow;
			entity.ModifiedBy = personId;

			_ef.Update(entity);
		}

		protected void UpdateRange(IEnumerable<BaseEntity> entities, Guid personId)
		{
			entities = entities.Select(x =>
			{
				x.ModifiedOn = DateTime.UtcNow;
				x.ModifiedBy = personId;
				return x;
			});
			_ef.UpdateRange(entities);
		}

		protected Guid Add(BaseEntity entity, Guid personId)
		{
			entity.CreatedOn = DateTime.UtcNow;
			entity.CreatedBy = personId;
			entity.ModifiedOn = entity.CreatedOn;
			entity.ModifiedBy = entity.CreatedBy;


			return _ef.Add(entity).Entity.Id;
		}

		protected void AddRange(IEnumerable<BaseEntity> entities, Guid personId)
		{
			entities = entities.Select(entity =>
			{
				entity.CreatedOn = DateTime.UtcNow;
				entity.CreatedBy = personId;
				entity.ModifiedOn = entity.CreatedOn;
				entity.ModifiedBy = entity.CreatedBy;
				return entity;
			});

			_ef.AddRange(entities);
		}

		protected void Remove(BaseEntity entity, Guid personId)
		{
			entity.ModifiedOn = DateTime.UtcNow;
			entity.ModifiedBy = personId;

			_ef.Remove(entity);
		}

		protected async Task<EntityType> GetById<EntityType>(Guid id)
			where EntityType : BaseEntity
		{
			return await _ef.FindAsync<EntityType>(id);
		}

	}
}
