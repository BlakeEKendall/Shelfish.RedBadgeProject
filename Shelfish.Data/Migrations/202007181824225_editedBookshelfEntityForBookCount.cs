namespace Shelfish.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedBookshelfEntityForBookCount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookshelf", "TotalBooks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookshelf", "TotalBooks", c => c.Int(nullable: false));
        }
    }
}
