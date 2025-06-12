using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ImageRepository(IAppDbContext dbContext) : IImageRepository
{
    private readonly DbSet<AppImage> _dbSet = dbContext.Images;

    public async Task AddAsync(AppImage item, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }
}
