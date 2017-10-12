using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Client.DAO;
using Newtonsoft.Json;
namespace Client.Controllers
{
    public class NhaXuatBansController : Controller
    {
       static  string Baseurl = "http://localhost:37817";
        private DBConnection _db = new DBConnection();
        // GET: NhaXuatBans
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<NhaXuaBan> lisNxb = new List<NhaXuaBan>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage res = await client.GetAsync("api/NhaXuaBans/GetNxb");

                //Checking the response is successful or not which is sent using HttpClient  
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var empResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    lisNxb = JsonConvert.DeserializeObject<List<NhaXuaBan>>(empResponse);

                }
                //returning the employee list to view  
                return View(lisNxb);
            }
        }

        //DELETE :NhaXuatBans/Delete/5
        public async Task<ActionResult> Delete  (int id)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage res = await client.DeleteAsync("api/NhaXuaBans/DeleteNhaXuaBan/" + id);
            
                if (res.IsSuccessStatusCode)
                    {

                    Response.StatusCode = 404;
                 }
                }

                return RedirectToAction("Index");
            }
        // GET: NhaXuatBans/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            NhaXuaBan nhaxuatban  = new NhaXuaBan();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage res = await client.GetAsync("api/NhaXuaBans/GetNhaXuaBan/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var empResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    nhaxuatban = JsonConvert.DeserializeObject<NhaXuaBan>(empResponse);

                }
                //returning the employee list to view  
                return View(nhaxuatban);
            }
        }
  
        // GET: NhaXuatBans/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhaXuatBans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NhaXuaBan nhaxuatban)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                StringContent content = new StringContent(JsonConvert.SerializeObject(nhaxuatban), Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("api/NhaXuaBans/PostNhaXuaBan", content);

                //Checking the response is successful or not which is sent using HttpClient  
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(nhaxuatban);

            }
        }

        // GET: NhaXuatBans/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            NhaXuaBan nhaxuatban = new NhaXuaBan();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage res = await client.GetAsync("api/NhaXuaBans/GetNhaXuaBan/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var empResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    nhaxuatban = JsonConvert.DeserializeObject<NhaXuaBan>(empResponse);

                }
                //returning the employee list to view  
                return View(nhaxuatban);
            }
        }
  
        [ValidateAntiForgeryToken]
        public  ActionResult Edit(NhaXuaBan nhaXuaBan)
        {
           //string json = JsonConvert.SerializeObject(nhaXuaBan);
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri("http://localhost:37817/api/NhaXuaBans/PutNhaXuaBan/");

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                var putTask = client.PutAsJsonAsync<NhaXuaBan>("nhaXuaBan", nhaXuaBan);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(nhaXuaBan);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
