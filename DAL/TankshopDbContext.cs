using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nettbutikk.Model
{
    public class TankshopDbContext : DbContext
    {
        public TankshopDbContext()
            : base("name=TankshopDbContext")
        {
            try
            {
                Database.CreateIfNotExists();
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.ToString());
                throw;
            }
        }


        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }

        public virtual DbSet<Postal> Postals { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderline> Orderlines { get; set; }
        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<OldCategory> OldCategories { get; set; }
        public virtual DbSet<OldImage> OldImages { get; set; }
        public virtual DbSet<OldProduct> OldProducts { get; set; }
        public virtual DbSet<OldOrderline> OldOrderLines { get; set; }

        // FAQ
        public virtual DbSet<FAQCategory> FAQCategories { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<UserQuestion> UserQuestions { get; set; }

    }

    public class Customer
    {
        public Customer()
        {
            this.Orders = new List<Order>();
        }
        [Key]
        public int CustomerId { get; set; }
        public string Email { get; set; }

        public virtual List<Order> Orders { get; set; }
    }

    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string Email { get; set; }
    }

    public class Person
    {

        [Key]
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }

        public virtual Postal Postal { get; set; }

    }

    // PersonId / password combination
    public class Credential
    {
        [Key]
        public string Email { get; set; }
        public byte[] Password { get; set; }
    }

    // Postaladdress
    public class Postal
    {
        public Postal()
        {
            this.People = new List<Person>();
        }
        [Key]
        public string Zipcode { get; set; }
        public string City { get; set; }

        public virtual List<Person> People { get; set; }
    }

    // Item-category
    public class Category
    {
        public Category()
        {
            this.Products = new List<Product>();
        }
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }

    }

    // Product
    public class Product
    {
        public Product()
        {
            this.Images = new List<Image>();
        }
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }

    // Complete order
    public class Order
    {
        public Order()
        {
            this.Orderlines = new List<Orderline>();
        }
        [Key]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual List<Orderline> Orderlines { get; set; }
    }

    // Individual orderlines
    public class Orderline
    {
        [Key]
        public int OrderlineId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }


    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public virtual Product Product { get; set; }
    }

    public class OldImage
    {
        [Key]
        public int OldImageId { get; set; }

        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

        public DateTime Changed { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
    }

    public class OldCategory
    {

        [Key]
        public int OldCategoryId { get; set; }

        public string Name { get; set; }

        public DateTime Changed { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
    }

    public class OldProduct
    {

        [Key]
        public int OldProductId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public DateTime Changed { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }

    }

    public class OldOrderline{

        [Key]
        public int Id { get; set; }
        public int OrderlineId { get; set; }
        public int OrderId { get; set; }
        public int ProductId_From { get; set; }
        public int ProductId_To { get; set; }
        public int Count_From { get; set; }
        public int Count_To{ get; set; }
        public int AdminId { get; set; }

        public DateTime Changed { get; set; }

        //public virtual Orderline Orderline{ get; set; }
        //public virtual Product Product { get; set; }
        //public virtual Order Order{ get; set; }
        public virtual Admin Admin { get; set; }
    }


    //  FAQ

    public class FAQCategory
    {
        public FAQCategory()
        {
            this.Questions = new List<FAQ>();
        }
        [Key]
        public int FAQCategoryId { get; set; }
        public string Name { get; set; }

        public virtual List<FAQ> Questions { get; set;}
    }

    public class FAQ
    {
        [Key]
        public int FAQId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public virtual FAQCategory FAQCategory { get; set; }
    }

    public class UserQuestion
    {
        [Key]
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

}
