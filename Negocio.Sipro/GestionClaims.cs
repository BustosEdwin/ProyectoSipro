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

        public async Task<object> ObtenerClaimAsync(string _identificador)
        {
            return await Task<object>.Factory.StartNew(() =>
            {
                return (from claim in ClaimsIdentity.Claims
                        where claim.Type == _identificador
                        select claim).FirstOrDefault().Value;
            });
        }

        public object ObtenerClaim(string _identificador)
        {
            return (from claim in ClaimsIdentity.Claims
                    where claim.Type == _identificador
                    select claim).FirstOrDefault().Value;
        }

        public string[] ObtenerClaimRoles(string _identificador) {
            return (from claim in ClaimsIdentity.Claims
                    where claim.Type == _identificador
                    select claim.Value.ToString()).ToArray();
        }

        public decimal ConvertirAdecimal()
        {
            return Convert.ToDecimal(this);
        }
    }
}
