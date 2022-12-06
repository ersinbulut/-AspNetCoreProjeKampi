using CoreDemo.Areas.Admin.Models;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreDemo.Areas.Admin.Controllers
{
	[Area("admin")]
	[AllowAnonymous]
	public class ChartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult CategoryChart()
		{
			List<CategoryClass> list = new List<CategoryClass>();

			list.Add(new CategoryClass
			{
				categoryname="tekmoloji",
				categorycount=10
			});
			list.Add(new CategoryClass
			{
				categoryname = "yazılım",
				categorycount = 14
			});
			list.Add(new CategoryClass
			{
				categoryname = "spor",
				categorycount = 5
			});
			list.Add(new CategoryClass
			{
				categoryname = "Sinema",
				categorycount = 2
			});
			return Json(new {jsonlist = list});
		}
	}
}
