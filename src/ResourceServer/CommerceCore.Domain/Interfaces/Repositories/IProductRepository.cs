using CommerceCore.Domain.Common;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Domain.Interfaces.Repositories;

public class GetProductsOptions : IGetAllOptions;

public interface IProductRepository : ICrudRepository<Product, Guid, GetProductsOptions>;
