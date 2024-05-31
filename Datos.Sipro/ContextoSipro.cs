namespace Datos.Sipro
{
    using System.Data.Entity;

    public class ContextoSipro : DbContext
    {
        #region Constructores
        public ContextoSipro() : base("ContextoSipro")
        {
            Database.SetInitializer<ContextoSipro>(null);
        }
        #endregion

        #region Propiedades
        public virtual IDbSet<SiproBitacora> SiproBitacora { get; set; }
        public virtual IDbSet<SiproBitacoResponsables> SiproBitacoResponsables { get; set; }
        public virtual IDbSet<SiproEstados> SiproEstados { get; set; }
        public virtual IDbSet<SiproEvidencia> SiproEvidencia { get; set; }
        public virtual IDbSet<SiproFases> SiproFases { get; set; }
        public virtual IDbSet<SiproObservaciones> SiproObservaciones { get; set; }
        public virtual IDbSet<SiproProyecto> SiproProyecto { get; set; }
        public virtual IDbSet<SiproRecurso> SiproRecurso { get; set; }
        public virtual IDbSet<SiproResponsable> SiproResponsable { get; set; }
        public virtual IDbSet<SiproTipoRecurso> SiproTipoRecurso { get; set; }
        public virtual IDbSet<SiproTipoResponsabilidad> SiproTipoResponsabilidad { get; set; }
        public virtual IDbSet<VmRehuPersonal> VmRehuPersonal { get; set; }
        public virtual IDbSet<PortalUnidadesSiglas> PortalUnidadesSiglas { get; set; }
        public virtual IDbSet<SiproUsuarios> SiproUsuarios { get; set; }
        public virtual IDbSet<SiproRoles> SiproRoles { get; set; }
        public virtual IDbSet<SiproUsuarioRol> SiproUsuarioRol { get; set; }
        public virtual IDbSet<SiproComentario> SiproComentario { get; set; }
        public virtual IDbSet<SiproCtrlDominios> SiproCtrlDominios { get; set; }
        public virtual IDbSet<SiproEstadoComentario> SiproEstadoComentario { get; set; }


        #endregion
    }
}
