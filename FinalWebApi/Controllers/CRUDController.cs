using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using System.Net.Http;
namespace FinalWebApi.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<Emp> e = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64250/api/Employee");
            var consumeApi = hc.GetAsync("Employee");
            consumeApi.Wait();
            var readData = consumeApi.Result;
            if(readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<Emp>>();
                displayData.Wait();
                e = displayData.Result;
            }
            return View(e);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Emp e)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64250/api/Employee");
            var insertRecord = hc.PostAsJsonAsync<Emp>("Employee", e);
            insertRecord.Wait();
            var saveData = insertRecord.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
                return View("Create");
        }
    }
}