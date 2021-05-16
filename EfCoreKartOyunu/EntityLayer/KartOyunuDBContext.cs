using EntityLayer.GameCart;
using EntityLayer.Result;
using EntityLayer.UserConfig;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer
{
    public class KartOyunuDBContext:DbContext
    {
        public KartOyunuDBContext(DbContextOptions<KartOyunuDBContext> options) : base(options) { }
        public KartOyunuDBContext() { }
        ///Kullanıcı Yönetimi
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        ///Kullanıcı Yönetimi
        
        ///Oyun Sonuçları
        public DbSet<ScoreTable> ScoreTables { get; set; }
        public DbSet<GameVariant> GameVariants { get; set; }
        ///Oyun Sonuçları

        ///OyunKartları
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FileRepo> FirstFileRepos { get; set; }
        ///OyunKartları
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q7EGB25; Initial Catalog=KartOyunuDB;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ScoreTableConfiguration());
            modelBuilder.ApplyConfiguration(new GameVariantConfiguration());
            modelBuilder.ApplyConfiguration(new FirstFileRepoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
