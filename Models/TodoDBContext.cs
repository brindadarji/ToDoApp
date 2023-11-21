using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models
{
    public partial class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options)
        :base(options)
        {

        }

        public virtual DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasKey(e => e.Id).HasName("PK_dbo.Todos");

                entity.ToTable("Todos");

                entity.Property(e => e.Details).HasMaxLength(150);
            });
            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
