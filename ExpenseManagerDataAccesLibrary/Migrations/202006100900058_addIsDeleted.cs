namespace ExpenseManagerDataAccesLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Incomes", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incomes", "IsDeleted");
            DropColumn("dbo.Expenses", "IsDeleted");
        }
    }
}
