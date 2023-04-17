using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Db.SqlServer.Ef.Entities.App
{
    public class UserDbEntity
    {
        public string Id { get; set; }
        public int ReferenceCode { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public int Active { get; set; }
        public string Pin { get; set; }
        public string Card1 { get; set; }
        public string Card2 { get; set; }
        public DateTime? AccessValidityDate { get; set; }
        public int Organization { get; set; }
        public int Branch { get; set; }
        public int Department { get; set; }
        public int Designation { get; set; }
        public int Section { get; set; }
        public int Category { get; set; }
        public int Grade { get; set; }
        public int LeaveGroup { get; set; }
        public int AccessLevel { get; set; }
        public int enrolled_fingers { get; set; }
        public int enrolled_faces { get; set; }
    }

    public class UserDbEntityConfig : IEntityTypeConfiguration<UserDbEntity>
    {
        public void Configure(EntityTypeBuilder<UserDbEntity> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(i => i.Id);
        }
    }
}
