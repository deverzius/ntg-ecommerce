using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data;

public class CommerceCoreDbContext(DbContextOptions<CommerceCoreDbContext> options) : DbContext(options)
{
}