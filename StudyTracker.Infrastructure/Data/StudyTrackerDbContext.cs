using Microsoft.EntityFrameworkCore;
using StudyTracker.Core.Models;

namespace StudyTracker.Infrastructure.Data;

public class StudyTrackerDbContext : DbContext
{
    public StudyTrackerDbContext(DbContextOptions<StudyTrackerDbContext> options) : base(options)
    {
    }

    public DbSet<SessaoEstudo> SessoesEstudo { get; set; }
}