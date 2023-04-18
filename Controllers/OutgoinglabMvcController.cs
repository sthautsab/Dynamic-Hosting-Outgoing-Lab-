using OutGoingLab.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            client.BaseAddress = new Uri("http://localhost:57305/api/");
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
                client.BaseAddress = new Uri("http://localhost:57305/api/");
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
            client.BaseAddress = new Uri("http://localhost:57305/api/");
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
                client.BaseAddress = new Uri("http://localhost:57305/api/");
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