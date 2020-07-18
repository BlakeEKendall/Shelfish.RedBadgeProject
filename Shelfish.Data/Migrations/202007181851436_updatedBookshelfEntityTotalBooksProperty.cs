namespace Shelfish.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedBookshelfEntityTotalBooksProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookshelf", "TotalBooks", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookshelf", "TotalBooks");
        }
    }
}
