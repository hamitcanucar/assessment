using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleProject.DataAccess.Entities
{
    public enum ReportType { GettingReady, Complete }
    public class Report : AEntity
    {
        public DateTime ReportCreateTime { get; set; }
        public ReportType ReportType{ get; set; }
        public string Location { get; set; }



        public class UserInformationEntityConfiguration : EntityConfiguration<Report>
        {
            public UserInformationEntityConfiguration() : base("report")
            {
            }

            public override void Configure(EntityTypeBuilder<Report> builder)
            {
                base.Configure(builder);

                builder.HasIndex(u => u.ReportType)
                .HasMethod("hash");

                builder.Property(u => u.ReportCreateTime)
                    .HasColumnName("report_create_date")
                    .HasColumnType("varchar(32)");
                
                builder.Property(u => u.Location)
                .HasColumnName("location")
                .HasColumnType("varchar(64)");
            }
        }
    }
}