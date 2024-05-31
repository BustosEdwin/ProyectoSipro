namespace Comun.Sipro.Utilidades
{
    using System;
    using System.Threading.Tasks;

    public static class Validaciones
    {
        #region Metodos Externos
        public static async Task<EstadoRespuesta> LimiteCadenaAsync(string _contenido, long _limiteCaracteres)
        {
            return await Task<EstadoRespuesta>.Factory.StartNew(() =>
            {
                if (_contenido.Length > _limiteCaracteres)
                    return new EstadoRespuesta
                    {
                        Codigo = 0,
                        Estado = false,
                        Mensaje = "La cantidad de caracteres supera el umbral deseado."
                    };

                return new EstadoRespuesta
                {
                    Codigo = 1,
                    Estado = true,
                    Mensaje = "La cantidad de caracteres es la deseada."
                };
            });
        }

        public static async Task<EstadoRespuesta> ValidarUnidades(Nullable<decimal> _unidadUno, Nullable<decimal> _unidadDos, params string[] _etiquetas)
        {
            return await Task<EstadoRespuesta>.Factory.StartNew(() =>
            {
                if (_unidadUno == null || _unidadUno.ToString().Length == 0)
                    return new EstadoRespuesta
                    {
                        Codigo = 0,
                        Estado = false,
                        Mensaje = $"El campo {_etiquetas[0]}, debe ser elegido."
                    };

                if (_unidadDos == null || _unidadDos.ToString().Length == 0)
                    return new EstadoRespuesta
                    {
                        Codigo = 0,
                        Estado = false,
                        Mensaje = $"El campo {_etiquetas[1]}, debe ser elegido."
                    };

                return new EstadoRespuesta
                {
                    Codigo = 1,
                    Estado = true,
                    Mensaje = "La validaciòn es correcta."
                };
            });

        }
        #endregion
    }
}
