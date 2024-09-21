
using LinkDev.IKEA.DAL.Entities.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Employees
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("Varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("Varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8 , 2)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            //builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");

            builder.Property(E => E.Gender)
                .HasConversion(

                (gender) => gender.ToString(),
                (gender) =>(Gender) Enum.Parse(typeof(Gender) , gender)

                );


            builder.Property(E => E.EmplyeeType)
               .HasConversion(

               (emplyeeType) => emplyeeType.ToString(),
               (emplyeeType) => (EmplyeeType)Enum.Parse(typeof(EmplyeeType), emplyeeType)

               );
        }
    }
}
