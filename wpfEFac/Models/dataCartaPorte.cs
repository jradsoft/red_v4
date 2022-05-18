using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    class dataCartaPorte
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Origen
        {
            public string IDUbicacion { get; set; }
            public string TipoUbicacion { get; set; }
            public string RFCRemitenteDestinatario { get; set; }
            public string NombreRemitenteDestinatario { get; set; }
            public DateTime FechaHoraSalidaLlegada { get; set; }
            
            
            
        }

        public class DomicilioOrigen
        {
            public string Calle { get; set; }
            public string NumeroExterior { get; set; }
            public string Colonia { get; set; }
            public string Localidad { get; set; }
            public string Referencia { get; set; }
            public string Municipio { get; set; }
            public string Estado { get; set; }
            public string Pais { get; set; }
            public string CodigoPostal { get; set; }
             
            
        }


        public class DomicilioDestino
        {
            public string Calle { get; set; }
            public string NumeroExterior { get; set; }
            public string Colonia { get; set; }
            public string Localidad { get; set; }
            public string Referencia { get; set; }
            public string Municipio { get; set; }
            public string Estado { get; set; }
            public string Pais { get; set; }
            public string CodigoPostal { get; set; }
        }

        public class DomicilioOperador
        {
            public string Calle { get; set; }
            public string CodigoPostal { get; set; }
            public string Colonia { get; set; }
            public string Estado { get; set; }
            public string Localidad { get; set; }
            public string Municipio { get; set; }
            public string NumeroExterior { get; set; }
            public string Pais { get; set; }
            public string Referencia { get; set; }
        }

        public class Destino
        {
            public string IDUbicacion { get; set; }
            public string TipoUbicacion { get; set; }
            public string RFCRemitenteDestinatario { get; set; }
            public string NombreRemitenteDestinatario { get; set; }
            public DateTime FechaHoraSalidaLlegada { get; set; }
            public decimal DistanciaRecorrida { get; set; }

            
        }

        public class UbicacionOrigen
        {
           
            public Origen Origen { get; set; }
            public DomicilioOrigen DomicilioOri { get; set; }
            
        }

        public class UbicacionDestino
        {

            public Destino Destino { get; set; }
            public DomicilioDestino DomicilioDest { get; set; }
           
        }

        public class Mercancia
        {
            public string BienesTransp { get; set; }
            public string Descripcion { get; set; }
            public decimal Cantidad { get; set; }
            public string ClaveUnidad { get; set; }
            public string MaterialPeligroso { get; set; }
            public string ClaveMatPel { get; set; }
            public string Embalaje { get; set; }
            public decimal PesoEnKg { get; set; }
            public CantidadTransporta CantidadTransporta { get; set; }
        }

        public class CantidadTransporta
        {
            public decimal Cantidad { get; set; }
            public string IDOrigen { get; set; }
            public string IDDestino { get; set; }
 
        
        }

        public class Mercancancias
        {
            public int NumTotalMercancias { get; set; }
            public decimal PesoBrutoTotal { get; set; }
            public string UnidadPeso { get; set; }
            public List<Mercancia> Mercancia { get; set; }
            public Autotransporte AutoTransporte { get; set; }
            
        
        }

        public class IdentificacionVehicular
        {
            public string ConfigVehicular { get; set; }
            public string PlacaVM { get; set; }
            public int AnioModeloVM { get; set; }
           
        }

        public class Seguros
        {
            public string AseguraRespCivil { get; set; }
            public string PolizaRespCivil { get; set; }
            public string AseguraCarga { get; set; }

        }

        public class Remolque
        {
            public string SubTipoRem { get; set; }
            public string Placa { get; set; }
           
        }

        public class Autotransporte
        {
            public string PermSCT { get; set; }
            public string NumPermisoSCT { get; set; }
            public IdentificacionVehicular IdentificacionVehicular { get; set; }
            public Seguros Seguros { get; set; }
            public List<Remolque> Remolques { get; set; }

        }

        public class TiposFigura 
        {
            public string TipoFigura { get; set; }
            public string RFCFigura { get; set; }
            public string NumLicencia { get; set; }
            public string NombreFigura { get; set; }
            //public string ResidenciaFiscalOperador { get; set; }
            //public DomicilioOperador Domicilio { get; set; }
        }

        //public class Operadores
        //{
        //    public Operador Operador { get; set; }
        //}

        public class FiguraTransporte
        {

            public List<TiposFigura> TiposFigura { get; set; }
        }

        public class Root
        {
            public string TranspInternac { get; set; }
            public decimal TotalDistRec { get; set; }
            public List<UbicacionOrigen> UbicacionOr { get; set; }
            public List<UbicacionDestino> UbicacionDes { get; set; }
            public Mercancancias Mecancancias { get; set; }
            public Autotransporte Autotransporte { get; set; }
            public FiguraTransporte FiguraTransporte { get; set; }
        }



    }
}
