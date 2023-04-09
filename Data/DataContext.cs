using Microsoft.EntityFrameworkCore;
using PharmaKursWork.Models;

public class DataContext : DbContext
{
    public DbSet<Challenge> Challenges { get; set; } = null!;
    public DbSet<Challenger> Challengers { get; set; } = null!;
    public DbSet<CommodityGroup> CommodityGroups { get; set; } = null!;
    public DbSet<Laboratory> Laboratories { get; set; } = null!;
    public DbSet<LaboratoryEmployee> LaboratoryEmployees { get; set; } = null!;
    public DbSet<Med> Meds { get; set; } = null!;
    public DbSet<UnitMeasure> UnitMeasures { get; set; } = null!;
    public DbSet<PharmaKursWork.Models.Scientist> Scientists { get; set; } = null!;
    public DbSet<TechStaff> TechStaffs { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
        
    }
}