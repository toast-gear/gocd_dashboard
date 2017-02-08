using api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace api.Repository
{
    public class HttpRepository
    {
        HttpClient Client = HttpClientSingleton.Instance();
        public PipelineStatus HttpGetRequest(string PipelineName)
        {
            Task<string> Response = Client.GetStringAsync(string.Format("http://localhost:8153/go/api/pipelines/{0}/status", PipelineName));
            PipelineStatus PipelineStatusObject = JsonConvert.DeserializeObject<PipelineStatus>(Response.Result);
            return PipelineStatusObject;
        }

        public PipelineStatus HttpGetRequestAsync(string PipelineName)
        {
            HttpResponseMessage Response = Client.GetAsync(string.Format("http://localhost:8153/go/api/pipelines/{0}/status", PipelineName)).Result;
            string Json = Response.Content.ReadAsStringAsync().Result;
            PipelineStatus PipelineStatusObject = JsonConvert.DeserializeObject<PipelineStatus>(Json);
            return PipelineStatusObject;
        }
        public async Task<HttpResponseMessage> HttpPausePipelinePostRequestAsync(string PipelineName, PausePipeline PausePipeline)
        {
            string Json = JsonConvert.SerializeObject(PausePipeline);
            StringContent StringContent = new StringContent(Json, Encoding.UTF8, "application/vnd.go.cd.v1+json");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("Confirm", "true");
            return await Client.PostAsJsonAsync(string.Format("http://localhost:8153/go/api/pipelines/{0}/pause", PipelineName), StringContent);
            //return await Client.PostAsync(string.Format("http://localhost:8153/go/api/pipelines/{0}/pause", PipelineName), StringContent);
        }
    }
}
