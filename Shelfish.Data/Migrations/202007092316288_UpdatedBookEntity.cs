namespace Shelfish.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedBookEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "Isbn", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "Isbn", c => c.Int(nullable: false));
        }
    }
}
