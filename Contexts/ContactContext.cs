using Crito.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crito.Contexts;

public class ContactContext : DbContext

{
    protected readonly IConfiguration Configuration;


    public ContactContext(DbContextOptions<ContactContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public required DbSet<ContactEntity> Contacts { get; set; }
    public required DbSet<SubscriptionEntity> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ContactEntity>(entity =>
        {
            entity.ToTable("ContactUs");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactUmbracoKey).HasColumnName("contactUmbracoKey");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Description).HasColumnName("description");
        });


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlite(Configuration.GetConnectionString("UmbracoDatabase"));
    }
}
