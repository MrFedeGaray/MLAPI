using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using isMutantApi.Services;


namespace isMutantApi.Controllers
{
    //extern alias Alias1;
    //extern alias Alias2;
    //using namespace1 = Alias1::System.Net.Http.http HttpRequestMessageExtensions;
    //using namespace2 = Alias2::System.Web.Http.HttpRequestMessageExtensions;

    public class DefaultController : ApiController
    {

        [Route("mutant/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] object _adn)
        {
            try
            {
                var a = Newtonsoft.Json.JsonConvert.DeserializeObject<Dna>(_adn.ToString());
                var mutantSubject = new IsMutant(a.dna);
                if (mutantSubject.Checkable && mutantSubject.CheckDna())
                {
                    //is Mutant
                    var ok = new HttpResponseMessage(HttpStatusCode.OK);
                    return ok;
                }
                else
                {
                    //is not Mutant
                    var forbidden = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return forbidden;

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                var forbidden = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return forbidden;
            }
        }
    }
}
