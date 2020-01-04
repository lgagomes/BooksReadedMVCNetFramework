﻿using BooksReadedMVC.Models;
using System.Web.Mvc;

namespace BooksReadedMVC.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Authors
        public ActionResult Index()
        {
            using(AuthorModel model = new AuthorModel())
            {
                return View(model.GetList());
            }            
        }
    }
}