using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class Concepto
    {
        public string Cantidad { get; set; }
        public string UnidadMedida{ get; set; }
        public string NoIdentificacion { get; set; }
        public string Descripcion { get; set; }
        public string ValorUnitario { get; set; }
        public string Importe { get; set; }
        public string claveSat { get; set; }

        public List<InformacionAduanera> InformacionAduanera { get; set; }

        /// <summary>
        /// Atributo requerido para precisar el número de
        /// la cuenta predial del inmueble cubierto por el
        /// presente concepto en caso de recibos de arrendamiento.
        /// </summary>
        public string NumeroCuentaPredial { get; set; }

        /// <summary>
        /// Nodo opcional donde se incluirán los nodos 
        /// complementarios de extensión al concepto, 
        /// definidos por el SAT, de acuerdo a disposiciones
        /// particulares a un sector o actividad especifica.
        /// </summary>
        public ComplementoConcepto complemento { get; set; }

        public Parte parte { get; set; }

        public string Descuento { get; set; }
    }
}
