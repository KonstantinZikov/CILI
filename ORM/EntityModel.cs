namespace ORM
{
    using System.Data.Entity;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("DbConnection"){ Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityModel>()); }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }
    }
}
