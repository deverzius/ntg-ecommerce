using CommerceCore.Domain.Common;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Domain.Interfaces.Repositories;

public interface IProductRepository : ICrudRepository<Product, Guid>;
