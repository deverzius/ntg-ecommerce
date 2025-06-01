using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ImageRepository(IApplicationDbContext dbContext) : IImageRepository
{
    private readonly DbSet<Image> _dbSet = dbContext.Images;

    public async Task AddAsync(Image item, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }
}
