namespace SSE_Reporting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class levelidaddedtopositionclass : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Positions", name: "Level_Id", newName: "LevelId");
            RenameIndex(table: "dbo.Positions", name: "IX_Level_Id", newName: "IX_LevelId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Positions", name: "IX_LevelId", newName: "IX_Level_Id");
            RenameColumn(table: "dbo.Positions", name: "LevelId", newName: "Level_Id");
        }
    }
}
