using Microsoft.EntityFrameworkCore;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Infrastructure.Contexts;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
