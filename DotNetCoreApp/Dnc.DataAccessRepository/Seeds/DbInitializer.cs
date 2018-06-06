using Dnc.DataAccessRepository.Context;
using Dnc.Entities.Application;
using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Seeds
{
    public static class DbInitializer
    {
        public static void Initialze(EntityDbContext context)
        {
            //如果没有数据就创建数据库,数据存在就跳出执行
            try
            {
                if (context.ApplicationUsers.Any())
                    return;
            }
            catch 
            {
                context.Database.EnsureCreated();
            }
            var appGroups = new List<ApplicationGroup> {
                new ApplicationGroup {Name="系统管理员组", Decription="具备使用系统全部权限的用户组。" },
                new ApplicationGroup {Name="授权用户组", Decription="具备指定权限的用户组" },
                new ApplicationGroup {Name="普通访客组", Decription="仅仅注册资料的，具有常规的公开业务模块使用权限的用户组" }
            };

            if (!context.ApplicationGroups.Any())
            {
                
                foreach (var item in appGroups)
                {
                    context.ApplicationGroups.Add(item);
                }
                context.SaveChanges();
            }
            if (!context.ApplicationUsers.Any())
            {

                var appUsers = new List<ApplicationUser>
            {
                new ApplicationUser {Name="tiger",Password="123@abc", Group=appGroups[0], ApplicationGroupID=appGroups[0].ID },
                new ApplicationUser {Name="lion",Password="123@abc", Group=appGroups[1], ApplicationGroupID=appGroups[0].ID },
                new ApplicationUser {Name="rabbit",Password="123@abc", Group=appGroups[2], ApplicationGroupID=appGroups[0].ID }
            };
                foreach (var item in appUsers)
                {
                    context.ApplicationUsers.Add(item);
                }
                context.SaveChanges();
            }
            var articleTypes = new List<ArticleType>
            {
                new ArticleType {Name="行业要闻", Decription=0, SortCode=0 },
                new ArticleType {Name="意见领袖", Decription=0, SortCode=0 },
                new ArticleType {Name="观点专题", Decription=0, SortCode=0 },
                new ArticleType {Name="海外来风",Decription=0, SortCode=0 },
                new ArticleType {Name="科技前沿", Decription=0, SortCode=0 },
            };
            if (!context.ArticleTypes.Any())
            {
              
                foreach (var item in articleTypes)
                    context.ArticleTypes.Add(item);
                context.SaveChanges();
            }
            if (!context.Label.Any())
            {
                var LabelAdd = new List<Label> {
                new Label { Name = "dotnetCore" },
                new Label { Name = "ASP.NET" },
            new Label { Name = "前端" },
            new Label { Name = "后台" },
            new Label { Name = "大数据" },
            new Label { Name = "微服务" }};
                foreach (var item in LabelAdd)
                    context.Label.Add(item);
                context.SaveChanges();

            }
            if (!context.Articles.Any())
            {
                var articles = new List<NewsArticle>
            {
                new NewsArticle {Label="dotnetCore,ASP.NET,前端,后台", Name="关于行业要闻", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[0], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
                new NewsArticle {Label="微服务,ASP.NET,前端", Name="关于意见领袖", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[1], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
                new NewsArticle {Label="大数据,ASP.NET,前端" ,Name="关于观点专题", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[2], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
                new NewsArticle {Label="dotnetCore,ASP.NET" , Name="关于海外来风", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[3], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
                new NewsArticle {Label="dotnetCore,ASP.NET,前端,后台" ,Name="关于科技前沿", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[4], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
            };
                foreach (var item in articles)
                    context.Articles.Add(item);
                context.SaveChanges();
            }
           


           

           
            

        }
    }
}
