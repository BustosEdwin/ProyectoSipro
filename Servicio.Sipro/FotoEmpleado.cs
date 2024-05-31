namespace Servicio.Sipro
{
    using Newtonsoft.Json;
    using RestSharp;
    using System.Threading.Tasks;
    public class FotoEmpleado
    {
        public string ObtenerFotoBase64(string _identificacion)
        {
            //var client = new RestClient($"https://catalogoservicioweb.policia.gov.co/sw/api/DiversidadPonal/ConsultarImagenFuncionario?_numeroDocumento= {_identificacion}");
            var client = new RestClient($"http://192.168.2.173:80/api/DiversidadPonal/ConsultarImagenFuncionario?_numeroDocumento= {_identificacion}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"bearer {new Token().ObtenerToken()}");
            IRestResponse response = client.Execute(request);

            var foto = JsonConvert.DeserializeObject<string>(response.Content);
            return foto;
        }
    }
}
