using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uranus.Caching;
using Uranus.GlobalSetting;
using Uranus.GlobalSetting.Configuration;
using Uranus.Email;
using Microsoft.Extensions.DependencyInjection;

namespace XUnit.Uranus.Caching.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly ICacheManager cacheManager;
        private readonly IGlobalSettingManager globalSettingManager;
        private readonly IGlobalSettingConfiguration globalSettingConfiguration;
        private readonly IServiceProvider serviceProvider;
        public ValuesController(IServiceProvider serviceProvider, ICacheManager cacheManager, IGlobalSettingManager globalSettingManager, IGlobalSettingConfiguration globalSettingConfiguration)
        {
            this.cacheManager = cacheManager;
            this.globalSettingManager = globalSettingManager;
            this.globalSettingConfiguration = globalSettingConfiguration;

            this.serviceProvider = serviceProvider;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            cacheManager.Set("A", "A");
            var a = cacheManager.Get<string>("A");
            var b = cacheManager.Retrive("B", () =>
            {
                return "B";
            });

            cacheManager.Set("C", "C");
            cacheManager.Remove("C");

            var c = cacheManager.Get<string>("C");

            var email = serviceProvider.GetService(typeof(EmailGlobalSettingProvider));
            
            var settings = await globalSettingManager.GetAsync();

            return new string[] { a, b, c };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
