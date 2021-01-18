using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleProject.DataAccess.Entities
{
    public enum UserReportType { GettingReady, Complete }
    public class UserReport : AEntity
    {
        public DateTime ReportCreateTime { get; set; }
        public UserReportType UserReportType{ get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }



        public class UserInformationEntityConfiguration : EntityConfiguration<UserReport>
        {
            public UserInformationEntityConfiguration() : base("user_report")
            {
            }

            public override void Configure(EntityTypeBuilder<UserReport> builder)
            {
                base.Configure(builder);

                builder.HasIndex(u => u.UserReportType)
                .HasMethod("hash");

                builder.Property(u => u.ReportCreateTime)
                    .HasColumnType("varchar(32)");

                builder.HasOne<User>(uc => uc.User)
                    .WithOne(u => u.Report)
                    .HasForeignKey<UserReport>(uc => uc.UserId);

                builder.Property(uc => uc.UserId);
            }
        }
    }
}