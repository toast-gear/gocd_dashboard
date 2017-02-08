using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Newtonsoft.Json;
using System.Net.Http;
using api.Repository;
using System.Net;
using System.Web.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PipelinesController : Controller
    {
        private readonly HttpRepository HttpRepo;
        public PipelinesController()
        {
            HttpRepo = new HttpRepository();
        }

        [HttpGet]
        [Route("{PipelineName}/status")]
        public PipelineStatus GetPipelineStatus(string PipelineName)
        {
            try
            {
                return HttpRepo.HttpGetRequest(PipelineName.ToLower());
            }
            catch (Exception ex)
            {
                var message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(message);
            }
        }

        [HttpPost]
        [Route("{PipelineName}/pause")]
        public async Task<HttpResponseMessage> PausePipeline([FromBody] PausePipeline PausePipeline, string PipelineName)
        {
            try
            {
                return await HttpRepo.HttpPausePipelinePostRequestAsync(PipelineName.ToLower(), PausePipeline);
            }
            catch (Exception ex)
            {
                var message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(message);
            }
        }
    }
}
