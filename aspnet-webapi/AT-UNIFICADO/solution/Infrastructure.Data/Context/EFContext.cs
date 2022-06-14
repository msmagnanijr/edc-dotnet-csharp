using Domain.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;
public class EFContext : IdentityDbContext<IdentityUser>
{
    public string connectionString;
    public EFContext(DbContextOptions<EFContext> options) : base(options)
    {
    }

    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
