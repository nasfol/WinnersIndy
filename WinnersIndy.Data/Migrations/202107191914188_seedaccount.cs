﻿namespace WinnersIndy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedaccount : DbMigration
    {
        public override void Up()
        {
            Sql(@" 
        
                    INSERT INTO [dbo].[ApplicationUser] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7eafd9ed-df1f-4471-b820-1f2c3a7674e1', N'guest@winners.com', 0, N'ALtiAoRP5BJzzTKIVyLN8Cm6MxyjsvklPHPjL1nkNp9CnpXAIQnDVGnPTeAVlRvbZQ==', N'292c4aaf-2a69-42e4-b514-46fb2f247d03', NULL, 0, 0, NULL, 1, 0, N'guest@winners.com')
                    INSERT INTO [dbo].[ApplicationUser] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'846462d3-99b4-4c28-88b9-00ae8899cef2', N'pastor@winners.com', 0, N'AFXFUMC8xadHH00gQsAghn0TIeX4om1G2QcIKSpF4OXm3BuZBtTsN+R7eREOCr1grw==', N'cbb8cfb2-8b6e-41c9-ab17-8bef5d7f20e4', NULL, 0, 0, NULL, 1, 0, N'pastor@winners.com')

            
                    INSERT INTO [dbo].[IdentityRole] ([Id], [Name]) VALUES (N'31cb77f0-5f99-44c5-933f-dd46c1e83427', N'Admin')
INSERT INTO [dbo].[IdentityUserRole] ([UserId], [RoleId], [IdentityRole_Id], [ApplicationUser_Id]) VALUES (N'846462d3-99b4-4c28-88b9-00ae8899cef2', N'31cb77f0-5f99-44c5-933f-dd46c1e83427', NULL, NULL)


            ");
        }
        
        public override void Down()
        {
        }
    }
}