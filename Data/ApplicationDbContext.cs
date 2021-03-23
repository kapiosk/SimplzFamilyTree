using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            modelBuilder.Entity<PersonRelation>()
                .HasKey(c => new { c.PersonId, c.RelatedPersonId });
            modelBuilder.Entity<PersonRelation>()
                .HasIndex(c => new { c.PersonId, c.RelatedPersonId });
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonEvent> PersonEvents { get; set; }
        public virtual DbSet<PersonRelation> PersonRelations { get; set; }
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
        public DateTime DoB { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
    }

    public class PersonEvent
    {
        public int PersonEventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
    }

    public class PersonRelation
    {
        public Relation Relation { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}
