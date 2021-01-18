using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleProject.DataAccess.Entities
{
    public class UserInformation : AEntity
    {
        public string PersonalId { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }



        public class UserInformationEntityConfiguration : EntityConfiguration<UserInformation>
        {
            public UserInformationEntityConfiguration() : base("user_information")
            {
            }

            public override void Configure(EntityTypeBuilder<UserInformation> builder)
            {
                base.Configure(builder);

                builder.Property(u => u.PersonalId)
                    .HasColumnName("personal_id")
                    .HasColumnType("varchar(64)")
                    .IsRequired();
                builder.Property(u => u.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(32)");

                builder.HasOne<User>(uc => uc.User)
                    .WithMany(u => u.Information)
                    .HasForeignKey(uc => uc.UserId);

                builder.Property(uc => uc.UserId);
            }
        }
    }
}