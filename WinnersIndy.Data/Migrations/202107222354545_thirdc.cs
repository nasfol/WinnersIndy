namespace WinnersIndy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendance", "ChildrenClassId", "dbo.ChildrenClass");
            AddColumn("dbo.Attendance", "ChildrenClass_ChildrenClassId", c => c.Int());
            CreateIndex("dbo.Attendance", "ChildrenClass_ChildrenClassId");
            AddForeignKey("dbo.Attendance", "ChildrenClass_ChildrenClassId", "dbo.ChildrenClass", "ChildrenClassId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendance", "ChildrenClass_ChildrenClassId", "dbo.ChildrenClass");
            DropIndex("dbo.Attendance", new[] { "ChildrenClass_ChildrenClassId" });
            DropColumn("dbo.Attendance", "ChildrenClass_ChildrenClassId");
            AddForeignKey("dbo.Attendance", "ChildrenClassId", "dbo.ChildrenClass", "ChildrenClassId");
        }
    }
}
