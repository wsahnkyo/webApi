using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly MenuService _menuService;
        private readonly BookService _bookService;
        public MenusController(MenuService menuService,BookService bookService)
        {
            _menuService = menuService;
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, object>> Get()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("menuList", _menuService.Get());
            dic.Add("bookList", _bookService.Get());
            dic.Add("boolean", true);
            dic.Add("number", 11);
            dic.Add("String", "aaa");
            return dic;
           
        }


        [HttpGet("list")]
        public ActionResult<Dictionary<string, object>> list()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<Menu> menus = new List<Menu>();
            for (int i = 0; i <= 3; i++)
            {
                Menu menu = new Menu();
                menu.Id = i.ToString();
                menu.MenuName = "目录名称" + i;
                menu.Pid = i.ToString();
                menus.Add(menu);
            }
            List<Book> books = new List<Book>();
            for (int i = 0; i <= 3; i++)
            {
                Book book = new Book();
                book.Id = i.ToString();
                book.Price = new Random().Next(1, 1000);
                book.Author = i.ToString();
                book.BookName = "图书名称" + i;
                book.Category = "图书类别" + i;
                books.Add(book);
            }

            dic.Add("menuList", menus);
            dic.Add("bookList", books);
            dic.Add("boolean", true);
            dic.Add("number", 11);
            dic.Add("String", "aaa");
            return dic;
        }

        [HttpGet("{id:length(24)}", Name = "GetMenu")]
        public ActionResult<Menu> Get(string id)
        {
            var menu = _menuService.Get(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        [HttpPost]
        public ActionResult<Menu> Create([FromForm] Menu menu)
        {
            _menuService.Create(menu);

            return CreatedAtRoute("GetMenu", new { id = menu.Id.ToString() }, menu);
        }
    }
}