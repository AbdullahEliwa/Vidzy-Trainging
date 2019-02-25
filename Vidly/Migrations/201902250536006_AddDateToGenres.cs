namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToGenres : DbMigration
    {
        public override void Up()
        {
            Sql("insert into genres (Name) values ('Comedy')");
            Sql("insert into genres (Name) values ('Action')");
            Sql("insert into genres (Name) values ('Family')");
            Sql("insert into genres (Name) values ('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
