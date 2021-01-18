using System;
using ExampleProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleProject.DataAccess.Entities
{
    public enum UserType { User, Admin }

    public class Role
    {
        public const string USER = "User";
        public const string ADMIN = "Admin";
    }

    public class JWTUser
    {
        public const string ID = "id";
    }

    public class User : AEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set;}
        public UserType UserType { get; set; }

        public UserInformation Information { get; set; }
        public Report Report{ get; set; }
    }

    public class UserEntityConfiguration : EntityConfiguration<User>
    {
        public UserEntityConfiguration() : base("user")
        {
        }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserType)
                .HasMethod("hash");
            
            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(64)")
                .IsRequired();
            builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(128)")
                .IsRequired();
            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("varchar(64)");
            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar(64)");
            

            builder.Property(u => u.UserType)
                .HasColumnName("user_type")
                .HasColumnType("varchar(16)")
                .HasDefaultValue(UserType.User)
                .HasConversion(
                    ut => ut.ToString(),
                    ut => (UserType)Enum.Parse(typeof(UserType), ut)
                );

            
        }
    }
}