using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DeliveryNet.Data.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export]
    public class DeliveryNetContext : DbContext
    {
        public DeliveryNetContext()
            : base("name=DefaultConnection")
        {
        }

        static DeliveryNetContext()
        {
            // static constructors are guaranteed to only fire once per application.
            // I do this here instead of App_Start so I can avoid including EF
            // in my MVC project (I use UnitOfWork/Repository pattern instead)
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        public DbSet<News> News { get; set; }
        public DbSet<Backend> Backends { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Customer> Users { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Tickets> Tickets { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var result = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    result += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        result += string.Format("- Property: \"{0}\", Error: \"{1}\"; ",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                var exception = new Exception("[custom] Validation failed for one or more entities. " + result);
                throw exception;
            }
        }
    }
}