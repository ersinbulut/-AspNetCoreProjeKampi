using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("admin")]
    [AllowAnonymous]
    public class BlogController : Controller
    {
        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workbook=new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog ADI";

                int BlogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using(var stream =new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Calisma1.xlsx");
                }
            };
           
        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> bm = new List<BlogModel>()
            {
                new BlogModel{ID=1,BlogName="c# programlamaya giriş"},
                new BlogModel{ID=2,BlogName="tesla firmasının araçları"},
                new BlogModel{ID=2,BlogName="2022 olimpiyatları"},
            };
            return bm;
        }
        public IActionResult BlogListExcel()
        {
            return View();
        }


        
        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog ADI";

                int BlogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Calisma1.xlsx");
                }
            };
        }

        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> bm2 = new List<BlogModel2>();
            using(var c = new Context())
            {
                bm2 = c.Blogs.Select(x => new BlogModel2
                {
                    ID = x.BlogID,
                    BlogName = x.BlogTitle
                }).ToList();
            }
            return bm2;
        }

        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }
}
