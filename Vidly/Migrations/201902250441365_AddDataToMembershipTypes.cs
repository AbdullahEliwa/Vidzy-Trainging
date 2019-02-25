namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataToMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("insert into MembershipTypes (Name, SignUpFee, DurationInMonth, DiscountRate ) values ('Pay as you go', 0, 0, 0)");
            Sql("insert into MembershipTypes (Name, SignUpFee, DurationInMonth, DiscountRate ) values ('Monthly', 25, 1, 10)");
            Sql("insert into MembershipTypes (Name, SignUpFee, DurationInMonth, DiscountRate ) values ('Quarterly', 60, 3, 15)");
            Sql("insert into MembershipTypes (Name, SignUpFee, DurationInMonth, DiscountRate ) values ('Annual', 250, 12, 20)");
        }
        
        public override void Down()
        {
        }
    }
}
