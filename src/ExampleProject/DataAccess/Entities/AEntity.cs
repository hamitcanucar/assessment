using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleProject.DataAccess.Entities
{
    public class AEntity
    {
        public AEntity()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : AEntity
    {
        private readonly string tableName;

        public EntityConfiguration(string tableName)
        {
            this.tableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(e => e.ID);

            builder.Property(e => e.ID)
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.CreateTime)
                .HasColumnName("create_time")
                .HasDefaultValueSql("now()");
        }
    }
}