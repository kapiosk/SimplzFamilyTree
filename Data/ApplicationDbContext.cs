using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplzFamilyTree.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Add-Migration M5 -v
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
        Guardian
    }

    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? DoB { get; set; }
        public string EMail { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<PersonImage> PersonImages { get; set; }
        public ICollection<PersonEvent> PersonEvents { get; set; }
    }

    public class PersonImage
    {
        public int PersonImageId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "image")]
        public byte[] ProductImage { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }

    public class PersonEvent
    {
        public int PersonEventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }

    public class PersonRelation
    {
        public int PersonRelationId { get; set; }
        public Relation Relation { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [ForeignKey("RelatedPersonId")]
        public Person RelatedPerson { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}
