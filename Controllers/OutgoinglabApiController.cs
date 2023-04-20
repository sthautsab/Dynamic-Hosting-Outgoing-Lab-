using OutGoingLab.DAL;
using OutGoingLab.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OutGoingLab.Controllers
{
    [Authorize]
    public class OutgoinglabApiController : ApiController
    {
        Dal ud = new Dal();
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<Outgoinglab> outgoinglabData = new List<Outgoinglab>();
            try
            {
                outgoinglabData = ud.GetData();
                if (outgoinglabData != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, outgoinglabData);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in table");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage Create(Outgoinglab rf)
        {
            try
            {
                var x = ud.CreateData(rf);
                if (x != 0)
                {
                    rf.Id = x;
                    return Request.CreateResponse(HttpStatusCode.OK, rf);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage Update(int id, Outgoinglab re)
        {
            Outgoinglab rf = new Outgoinglab();
            rf = ud.GetData().Find(o => o.Id == id);
            try
            {
                if (rf != null)
                {
                    int x = ud.Edit(id, re);
                    re.Id = x;
                    return Request.CreateResponse(HttpStatusCode.OK, re);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Record not found to edit");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
