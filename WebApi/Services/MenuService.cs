using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class MenuService
    {
        private readonly IMongoCollection<Menu> _menus;


        public MenuService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var database = client.GetDatabase("BookstoreDb");
            _menus = database.GetCollection<Menu>("Menus");
        }

        public List<Menu> Get()
        {
            return _menus.Find(book => true).ToList();
        }

        public Menu Get(string id)
        {
            return _menus.Find<Menu>(menu => menu.Id == id).FirstOrDefault();
        }

        public Menu Create(Menu menu)
        {
            _menus.InsertOne(menu);
            return menu;
        }

        public void Update(string id, Menu menuIn)
        {
            _menus.ReplaceOne(menu => menu.Id == id, menuIn);
        }

        public void Remove(Menu menuIn)
        {
            _menus.DeleteOne(menu => menu.Id == menuIn.Id);
        }

        public void Remove(string id)
        {
            _menus.DeleteOne(menu => menu.Id == id);
        }
    }
}
