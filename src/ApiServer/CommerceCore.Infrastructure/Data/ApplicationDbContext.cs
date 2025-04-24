using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
}