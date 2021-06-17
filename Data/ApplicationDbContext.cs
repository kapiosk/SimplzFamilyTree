using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplzFamilyTree.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Add-Migration M12 -v
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonEvent> PersonEvents { get; set; }
        public virtual DbSet<PersonRelation> PersonRelations { get; set; }
        public virtual DbSet<PersonImage> PersonImages { get; set; }
    }

    public enum Relation
    {
        ParentX = 1,
        ParentY = 2,
        Guardian = 3,
        Spouse = 4
    }

    public class Person
    {
        public int PersonId { get; set; }

        public string FullName { get; set; }

        public string Nickname { get; set; }

        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DoD { get; set; }

        public string EMail { get; set; }

        public ICollection<PersonImage> PersonImages { get; set; }

        public ICollection<PersonEvent> PersonEvents { get; set; }

        [ForeignKey("PersonId")]
        public ICollection<PersonRelation> Children { get; set; }

        //[ForeignKey("RelatedPersonId")]
        //public ICollection<PersonRelation> Parents { get; set; }
    }

    public class PersonImage
    {
        public int PersonImageId { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "image")]
        public byte[] ProductImage { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public Person Person { get; set; }
    }

    public class PersonEvent
    {
        public int PersonEventId { get; set; }

        public DateTime Timestamp { get; set; }

        public string Description { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public Person Person { get; set; }
    }

    public class PersonRelation
    {
        public int PersonRelationId { get; set; }

        public Relation Relation { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        [ForeignKey("Person")]
        public int RelatedPersonId { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}
