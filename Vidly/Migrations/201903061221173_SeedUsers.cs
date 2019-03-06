namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1a875838-b968-46f3-9793-8ad2276c4ae5', N'Admin@vidly.com', 0, N'AMuQqJ4jqwHzloJFgzcPDfsZQ43b/14+QXhe9p5o7U7FFHjF4yYegjLw+6+mrB5Hhg==', N'2fed6eb2-e2af-4d44-8347-aca7974993ca', NULL, 0, 0, NULL, 1, 0, N'Admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cb749e32-95c5-488a-8d44-3be0650fc5f0', N'guest@vidly.com', 0, N'AJm2bHKONMEeQa223pmBWQwHd2JtFsXXHH8eB3kbDRhL3u6z2Ti7xTlFZ4KMCy6KOg==', N'ed50801a-cdf0-4465-8999-6e086fc7e560', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c4bfaa6c-c083-4be4-954e-6e626d9946e9', N'CanManageMovie')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1a875838-b968-46f3-9793-8ad2276c4ae5', N'c4bfaa6c-c083-4be4-954e-6e626d9946e9')
            ");

        }

        public override void Down()
        {
        }
    }
}
