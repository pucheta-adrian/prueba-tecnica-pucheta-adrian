namespace Unilink.TechTest.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        BalanceId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BalanceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Balances");
        }
    }
}
