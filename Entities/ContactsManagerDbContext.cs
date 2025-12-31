using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;



namespace Entities
{
    public class ContactsManagerDbContext:DbContext

    {


        // making constructor to pass the connection string to the base class(DbContext)

        public ContactsManagerDbContext(DbContextOptions options):base(options)
        {

        }







        // here in dbContext we will define dbsets for each entity class which is used to map db tables with entity classes

        public DbSet<Country> Countries { get; set; }


        // here dbset<entityclass> table_name {get;set}
        public DbSet<Person> Persons { get; set; }


        //binding dbsets to corresponding tables

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // in different version ToTable() may not work so download 8.0.7 version of microsoft.entityframeworkcore.sqlserver
            // package from nuget package manager

            modelBuilder.Entity<Country>().ToTable("Countries");


            // note:: ToTable("Countries") this is the exact table name where entity<country> will be bind and its
            // name is not necessary to be same as  of above dbset name

            modelBuilder.Entity<Person>().ToTable("Persons");



            // adding a seed data to the db

            //modelBuilder.Entity<Country>().HasData(

            //     new Country() { Id = Guid.Parse("B7078C84-62DD-4551-BA74-DAD88E597492"), CountryName = "Nepal" },
            //        new Country() { Id = Guid.Parse("A055FA40-94C0-43AE-83B0-86138B267297"), CountryName = "India" },
            //        new Country() { Id = Guid.Parse("C85C09FF-9DA2-44F8-AA86-59502FC3737F"), CountryName = "Bhutan" },
            //        new Country() { Id = Guid.Parse("07481067-C679-4A48-9823-4B04F7E353E0"), CountryName = "Pakistan" },
            //        new Country() { Id = Guid.Parse("FEFC9D80-FB10-4114-AC9C-3F0F9E999974"), CountryName = "Bangladesh" },
            //        new Country() { Id = Guid.Parse("7DA058F2-DFBE-446B-BD9F-C68338399AED"), CountryName = "Srilanka" },
            //        new Country() { Id = Guid.Parse("57643CCC-1371-4445-B92C-C74A6E428ED1"), CountryName = "Maldives" }

            //    );

            // but instead of passsing like this we can create a json file and read from it and add seed data

            string countriesJsonData = File.ReadAllText("E:\\code5thSem\\SimpleCRUDProject\\Entities\\/JSON/countries.json");
            // now convertings this json to list of country objects

            List<Country> allCountries=System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJsonData);

            foreach(var country in allCountries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }


            // similarly reading all the persons from json file and adding seed data

            
            string personsJsonData=File.ReadAllText("E:\\code5thSem\\SimpleCRUDProject\\Entities\\JSON/persons.json");
            List<Person> allPersons=System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJsonData);

            foreach(Person person in allPersons)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }
        }
    }
}
