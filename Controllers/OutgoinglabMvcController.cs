using OutGoingLab.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;



namespace OutGoingLab.Controllers
{
    public class OutgoinglabMvcController : Controller
    {

        HttpClient client = new HttpClient();

        [HttpGet]
        public ActionResult Index()
        {
            List<Outgoinglab> outgoingLabList = new List<Outgoinglab>();
            //adding authentication header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
           "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("Utsab:shrestha")));

            var dynamicuri = Request.Url.ToString();
            string trimmedUrl = dynamicuri.Replace("/OutgoinglabMvc", "/api/");
            client.BaseAddress = new Uri(trimmedUrl);

            //client.BaseAddress = new Uri("http://localhost:57305/api/");
            var response = client.GetAsync("outgoinglabapi");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var display = result.Content.ReadAsAsync<List<Outgoinglab>>();
                display.Wait();
                outgoingLabList = display.Result;
            }
            return View(outgoingLabList);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Outgoinglab ol)
        {
            if (ModelState.IsValid)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
           "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("Utsab:shrestha")));

                var dynamicuri = Request.Url.ToString();
                string trimmedUrl = dynamicuri.Replace("/OutgoinglabMvc", "/api/");
                client.BaseAddress = new Uri(trimmedUrl);
                var request = client.PostAsJsonAsync<Outgoinglab>("outgoinglabapi", ol);
                request.Wait();
                var test = request.Result;
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            List<Outgoinglab> outgoingLabList = new List<Outgoinglab>();
            // client.BaseAddress = new Uri("http://localhost:57305/api/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
           "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("Utsab:shrestha")));

            var dynamicuri = Request.Url.ToString();
            string trimmedUrl = dynamicuri.Replace($"/OutgoinglabMvc/Edit/{id}", "/api/");
            client.BaseAddress = new Uri(trimmedUrl);
            var response = client.GetAsync("outgoinglabapi");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var display = result.Content.ReadAsAsync<List<Outgoinglab>>();
                display.Wait();
                outgoingLabList = display.Result;
            }
            var model = outgoingLabList.Find(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, Outgoinglab e)
        {
            if (ModelState.IsValid)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
           "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("Utsab:shrestha")));

                var dynamicuri = Request.Url.ToString();
                string trimmedUrl = dynamicuri.Replace($"/OutgoinglabMvc/Edit/{id}", "/api/");
                client.BaseAddress = new Uri(trimmedUrl);
                //client.BaseAddress = new Uri("http://localhost:57305/api/");
                var response = client.PutAsJsonAsync<Outgoinglab>($"outgoinglabapi?Id={id}", e);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Edit");

        }




    }
}