namespace EventSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lehgooo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "Order_ID", "dbo.Orders");
            DropIndex("dbo.Tickets", new[] { "Order_ID" });
            CreateTable(
                "dbo.TicketOrders",
                c => new
                    {
                        Ticket_ID = c.Int(nullable: false),
                        Order_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ticket_ID, t.Order_ID })
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID, cascadeDelete: true)
                .Index(t => t.Ticket_ID)
                .Index(t => t.Order_ID);
            
            AddColumn("dbo.Orders", "TimeCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tickets", "Order_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "Order_ID", c => c.Int());
            DropForeignKey("dbo.TicketOrders", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.TicketOrders", "Ticket_ID", "dbo.Tickets");
            DropIndex("dbo.TicketOrders", new[] { "Order_ID" });
            DropIndex("dbo.TicketOrders", new[] { "Ticket_ID" });
            DropColumn("dbo.Orders", "TimeCreated");
            DropTable("dbo.TicketOrders");
            CreateIndex("dbo.Tickets", "Order_ID");
            AddForeignKey("dbo.Tickets", "Order_ID", "dbo.Orders", "ID");
        }
    }
}
