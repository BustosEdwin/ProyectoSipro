namespace Servicio.Sipro
{
    using Comun.Sipro.Utilidades;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Threading.Tasks;
    public class Token
    {
        public string ObtenerToken()
        {
            //RestClient client = new RestClient("https://catalogoservicioweb.policia.gov.co/sw/token");
            RestClient client = new RestClient("http://192.168.2.173:80/token");
            RestRequest request = new RestRequest(Method.POST);
            string user = System.Web.Configuration.WebConfigurationManager.AppSettings["UsuarioSW"];
            string password = System.Web.Configuration.WebConfigurationManager.AppSettings["PasswordSW"];
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("grant_type", "password");
            request.AddParameter("undefined", $"grant_type=password&username={user}&password={password}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var tokenDeserealizado = JsonConvert.DeserializeObject<TokenDTO>(response.Content);

            return tokenDeserealizado.access_token;
        }
    }
}
