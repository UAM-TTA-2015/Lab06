namespace UamTTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BudgetTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionTime = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        ReceivingAccount_Id = c.Int(),
                        SendingAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.ReceivingAccount_Id)
                .ForeignKey("dbo.Accounts", t => t.SendingAccount_Id)
                .Index(t => t.ReceivingAccount_Id)
                .Index(t => t.SendingAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "SendingAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "ReceivingAccount_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "SendingAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "ReceivingAccount_Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.BudgetTemplates");
            DropTable("dbo.Budgets");
        }
    }
}
