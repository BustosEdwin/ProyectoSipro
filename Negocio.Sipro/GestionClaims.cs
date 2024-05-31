namespace Negocio.Sipro
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class GestionClaims
    {

        private ClaimsIdentity ClaimsIdentity;

        public GestionClaims(ClaimsIdentity _claimsIdentity)
        {
            ClaimsIdentity = _claimsIdentity;
        }

        public GestionClaims() { }

        /// <summary>
        /// Método para obtener claim por identificador Asincrono
        /// </summary>
        /// <param name="_identificador"></param>
        /// <returns></returns>
        public async Task<object> ObtenerClaimAsync(string _identificador)
        {
            return await Task<object>.Factory.StartNew(() =>
            {
                return (from claim in ClaimsIdentity.Claims
                        where claim.Type == _identificador
                        select claim).FirstOrDefault().Value;
            });
        }

        /// <summary>
        /// Método para obtener claim por identificador
        /// </summary>
        /// <param name="_identificador"></param>
        /// <returns></returns>
        public object ObtenerClaim(string _identificador)
        {
            return (from claim in ClaimsIdentity.Claims
                    where claim.Type == _identificador
                    select claim).FirstOrDefault().Value;
        }

        /// <summary>
        /// Método Obtener Claim por identificador 
        /// </summary>
        /// <param name="_identificador"></param>
        /// <returns></returns>
        public string[] ObtenerClaimRoles(string _identificador) {
            return (from claim in ClaimsIdentity.Claims
                    where claim.Type == _identificador
                    select claim.Value.ToString()).ToArray();
        }

        /// <summary>
        /// Método para convertir a decimal algunos valores
        /// </summary>
        /// <returns></returns>
        public decimal ConvertirAdecimal()
        {
            return Convert.ToDecimal(this);
        }
    }
}
