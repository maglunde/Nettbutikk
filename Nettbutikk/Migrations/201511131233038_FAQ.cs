namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FAQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQCategories",
                c => new
                    {
                        FAQCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FAQCategoryId);
            
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        FAQId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        FAQCategory_FAQCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.FAQId)
                .ForeignKey("dbo.FAQCategories", t => t.FAQCategory_FAQCategoryId)
                .Index(t => t.FAQCategory_FAQCategoryId);
            
            CreateTable(
                "dbo.UserQuestions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        Email = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FAQs", "FAQCategory_FAQCategoryId", "dbo.FAQCategories");
            DropIndex("dbo.FAQs", new[] { "FAQCategory_FAQCategoryId" });
            DropTable("dbo.UserQuestions");
            DropTable("dbo.FAQs");
            DropTable("dbo.FAQCategories");
        }
    }
}
