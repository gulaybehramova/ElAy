using FrontToBack103.DAL;
using FrontToBack103.Models;
using FrontToBack103.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontToBack103.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class ProductController : Controller
    {
        private AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int take = 7, int pageSize=1)
        {
            ViewBag.Number = (pageSize-1)*take;
            List<Product> products = _context.Products
                .Include(p => p.Category)
                .Skip((pageSize-1)*take)
                .Take(take)
                .ToList();

            Pagination<ProductVM> pagination = new Pagination<ProductVM>(
                ReturnPageCount(take),
                pageSize,
                MapProductToProductVM(products)
                );

            return View(pagination);
        }

        private List<ProductVM> MapProductToProductVM(List<Product> products)
        {
            //List<ProductVM> productVMs = new List<ProductVM>();

            //foreach (var item in products)
            //{
            //    ProductVM product = new ProductVM
            //    {
            //        Id=item.Id,
            //        Name=item.Name,
            //        ImageUrl=item.ImageUrl,
            //        Count=item.Count,
            //        Price=item.Price,
            //        CategoryName=item.Category.Name
            //    };
            //    productVMs.Add(product);
            //}
            //return productVMs;


            List<ProductVM> productVMs = products.Select(p => new ProductVM
            {
                Id=p.Id,
                Name=p.Name,
                ImageUrl=p.ImageUrl,
                Count=p.Count,
                Price=p.Price,
                CategoryName=p.Category.Name
            }).ToList();
            return productVMs;
        }

        private int ReturnPageCount(int take)
        {
            int productCount = _context.Products.Count();

            return (int)Math.Ceiling(((decimal)productCount/take));
        }
    }
}
