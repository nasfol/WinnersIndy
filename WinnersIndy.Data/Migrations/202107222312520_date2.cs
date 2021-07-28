namespace WinnersIndy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Member", "DateOfBirth", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Member", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
