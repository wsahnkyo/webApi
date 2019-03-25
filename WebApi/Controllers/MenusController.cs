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