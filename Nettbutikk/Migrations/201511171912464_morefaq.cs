namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morefaq : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FAQs", "FAQCategory_FAQCategoryId", "dbo.FAQCategories");
            DropIndex("dbo.FAQs", new[] { "FAQCategory_FAQCategoryId" });
            RenameColumn(table: "dbo.FAQs", name: "FAQCategory_FAQCategoryId", newName: "FAQCategoryId");
            AddColumn("dbo.FAQs", "Score", c => c.Int(nullable: false));
            AlterColumn("dbo.FAQs", "FAQCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.FAQs", "FAQCategoryId");
            AddForeignKey("dbo.FAQs", "FAQCategoryId", "dbo.FAQCategories", "FAQCategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FAQs", "FAQCategoryId", "dbo.FAQCategories");
            DropIndex("dbo.FAQs", new[] { "FAQCategoryId" });
            AlterColumn("dbo.FAQs", "FAQCategoryId", c => c.Int());
            DropColumn("dbo.FAQs", "Score");
            RenameColumn(table: "dbo.FAQs", name: "FAQCategoryId", newName: "FAQCategory_FAQCategoryId");
            CreateIndex("dbo.FAQs", "FAQCategory_FAQCategoryId");
            AddForeignKey("dbo.FAQs", "FAQCategory_FAQCategoryId", "dbo.FAQCategories", "FAQCategoryId");
        }
    }
}
