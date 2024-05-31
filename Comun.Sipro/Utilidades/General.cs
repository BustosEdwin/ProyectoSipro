namespace Comun.Sipro.Utilidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.DirectoryServices.Protocols;
    using System.Net;

    public class General
    {
        /// <summary>
        /// Este metodo realiza la validación con el OID de la Policia Nacional de Colombia
        /// </summary>
        /// <param name="_usuario"></param>
        /// <param name="_clave"></param>
        /// <returns>Retorna verdadero en caso de que exista el usuario empresarial o falso en caso de que no exista dicho usuario</returns>
        public static bool LoginOud(string _usuario, string _clave)
        {

            try
            {
                string servidor = "oud.policia.gov.co:389";
                string dn = "cn=" + _usuario + ",cn=users,dc=policia,dc=gov,dc=co";

                LdapConnection conexionOid = new LdapConnection(servidor);
                conexionOid.AuthType = AuthType.Basic;
                conexionOid.Timeout = new TimeSpan(0, 0, 15);
                NetworkCredential credentiales = new NetworkCredential(dn, _clave);
                conexionOid.Bind(credentiales);
                conexionOid.Dispose();
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
