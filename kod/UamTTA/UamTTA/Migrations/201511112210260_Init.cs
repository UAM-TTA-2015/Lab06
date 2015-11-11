namespace UamTTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RelatedBankAccount = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpectedIncomes = c.Decimal(precision: 18, scale: 2),
                        TargetBalance = c.Decimal(precision: 18, scale: 2),
                        RequiresClearing = c.Boolean(nullable: false),
                        ClearingAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.ClearingAccount_Id)
                .Index(t => t.ClearingAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "ClearingAccount_Id", "dbo.Accounts");
            DropIndex("dbo.Accounts", new[] { "ClearingAccount_Id" });
            DropTable("dbo.Accounts");
        }
    }
}
