using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Negocio.Sipro;

namespace Sipro.Controllers
{
    public class BaseController : Controller
    {
        #region Constructore
        public BaseController()
        {

        }
        #endregion

        // GET: Base
        public async Task TipoResponsabilidad(string _idResponsable)
        {
            GestionTipoResponsable gestionTipoResponsable = new GestionTipoResponsable();

            await gestionTipoResponsable.ObtenerTipoResponsabilidadResponsableProyecto(_idResponsable);

            string tipoReponsabilidadUnido = string.Empty;
            foreach (var tipoResponsabilidad in gestionTipoResponsable.LstTipoResponsabilidades)
            {
                tipoReponsabilidadUnido += $"{tipoResponsabilidad.Descripcion},";
            }
            if (string.IsNullOrEmpty(tipoReponsabilidadUnido))
                ViewBag.TipoResponsabilid = string.Empty;
            else
                ViewBag.TipoResponsabilid = tipoReponsabilidadUnido;
        }
    }
}