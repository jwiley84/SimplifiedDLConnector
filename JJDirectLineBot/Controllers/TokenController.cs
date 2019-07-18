using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JJDirectLineBot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace JJDirectLineBot.Controllers
{

    public class TokenController : ControllerBase
    {
        private readonly IOptions<DLSModel> dlSecret;

        public TokenController(IOptions<DLSModel> dls)
        {
            dlSecret = dls;
        }

        [Route("/directline/token")]
        [HttpPost]
        public async Task<string> TokenRequest()
        {
            var secret = dlSecret.Value.DirectLineSecret;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://directline.botframework.com/v3/directline/tokens/generate");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", secret);
            var userId = $"dl_{Guid.NewGuid()}";
            request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new { User = new { Id = userId } }),
                Encoding.UTF8,
                "application/json");
            var response = await client.SendAsync(request);
            string token = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                return body;
            }
            else
            {
                //Error();
                return token;
            }

        }
    }
}