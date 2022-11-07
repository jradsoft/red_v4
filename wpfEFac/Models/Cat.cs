using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
   public class Cat
    {

        public string clave { set; get; }
        public string descripcion { set; get; }
    }


   public class ImpuestosVarios
   {
       public decimal porcImp { set; get; }
       public Boolean TypeIva { set; get; }
       public Boolean TypeRetIva { set; get; }
       public Boolean TypeRetIsr { set; get; }
       public Boolean TypeRetIeps { set; get; }
       public decimal sumaBaseImp { set; get; }
       public decimal sumaImporteImp { set; get; }


   }

   

   public static class FactCat {

       public static List<Cat> getListRegimen()
       {
           List<Cat> myList = new List<Cat>();
           myList.Add(new Cat() { clave = "601", descripcion = "General de Ley Personas Morales" });
           myList.Add(new Cat() { clave = "603", descripcion = "Personas Morales con Fines no Lucrativos" });
           myList.Add(new Cat() { clave = "605", descripcion = "Sueldos y Salarios e Ingresos Asimilados a Salarios" });
           myList.Add(new Cat() { clave = "606", descripcion = "Arrendamiento" });
           myList.Add(new Cat() { clave = "608", descripcion = "Demas ingresos" });
           myList.Add(new Cat() { clave = "609", descripcion = "Consolidación" });
           myList.Add(new Cat() { clave = "610", descripcion = "Residentes en el Extranjero sin Establecimiento Permanente en México" });
           myList.Add(new Cat() { clave = "611", descripcion = "Ingresos por Dividendos (socios y accionistas)" });
           myList.Add(new Cat() { clave = "612", descripcion = "Personas Físicas con Actividades Empresariales y Profesionales" });
           myList.Add(new Cat() { clave = "614", descripcion = "Ingresos por interes" });
           myList.Add(new Cat() { clave = "615", descripcion = "Régimen de los ingresos por obtención de premios" });
           myList.Add(new Cat() { clave = "616", descripcion = "Sin obligaciones fiscales" });
           myList.Add(new Cat() { clave = "620", descripcion = "Sociedades Cooperativas de Producción que optan por diferir sus ingresos" });
           myList.Add(new Cat() { clave = "621", descripcion = "Incorporación Fiscal" });
           myList.Add(new Cat() { clave = "622", descripcion = "Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras" });
           myList.Add(new Cat() { clave = "623", descripcion = "Opcional para Grupos de Sociedades" });
           myList.Add(new Cat() { clave = "624", descripcion = "Coordinados" });
           myList.Add(new Cat() { clave = "625", descripcion = "Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas" });
           myList.Add(new Cat() { clave = "626", descripcion = "Régimen Simplificado de Confianza" });
           
           return myList;
       }


       public static List<Cat> getListBancos()
       {
           List<Cat> myList = new List<Cat>();
           myList.Add(new Cat() { clave = "BNM840515VB1", descripcion = "BANAMEX" });
           myList.Add(new Cat() { clave = "BNC8507311M4", descripcion = "BANCOMEXT" });
           myList.Add(new Cat() { clave = "BNO670315CDO", descripcion = "BANOBRAS" });
           myList.Add(new Cat() { clave = "BBA830831LJ2", descripcion = "BBVA BANCOMER" });
           myList.Add(new Cat() { clave = "BSM970519DU8", descripcion = "SANTANDER" });
           myList.Add(new Cat() { clave = "BNE820901682", descripcion = "BANJERCITO" });
           myList.Add(new Cat() { clave = "HMI950125KG8", descripcion = "HSBC" });
           myList.Add(new Cat() { clave = "BBA940707IE1", descripcion = "BAJIO" });
           myList.Add(new Cat() { clave = "IBA950503GTA", descripcion = "IXE" });
           myList.Add(new Cat() { clave = "BII931004P61", descripcion = "INBURSA" });
           myList.Add(new Cat() { clave = "BIN931011519", descripcion = "INTERACCIONES" });
           myList.Add(new Cat() { clave = "BMI9312038R3", descripcion = "MIFEL" });
           myList.Add(new Cat() { clave = "SIN9412025I4", descripcion = "SCOTIABANK" });
           myList.Add(new Cat() { clave = "BRM940216EQ6", descripcion = "BANREGIO" });
           myList.Add(new Cat() { clave = "BIN940223KE0", descripcion = "INVEX" });
           myList.Add(new Cat() { clave = "BAN950525MD6", descripcion = "BANSI" });
           myList.Add(new Cat() { clave = "BAF950102JP5", descripcion = "AFIRME" });
           myList.Add(new Cat() { clave = "BMN930209927", descripcion = "BANORTE" });
           myList.Add(new Cat() { clave = "AEB960223JP7", descripcion = "AMERICAN EXPRESS" });
           myList.Add(new Cat() { clave = "BTM960401DV7", descripcion = "TOKYO" });
           myList.Add(new Cat() { clave = "BJP950104LJ5", descripcion = "JP MORGAN" });
           myList.Add(new Cat() { clave = "BMI9704113PA", descripcion = "BMONEX" });
           myList.Add(new Cat() { clave = "BVM951002LX0", descripcion = "VE POR MAS" });
           myList.Add(new Cat() { clave = "IBM951129P29", descripcion = "ING" });
           myList.Add(new Cat() { clave = "DBM000228J35", descripcion = "DEUTSCHE" });
           myList.Add(new Cat() { clave = "BAI0205236Y8", descripcion = "AZTECA" });
           myList.Add(new Cat() { clave = "BAM0511076B3", descripcion = "AUTOFIN" });
           myList.Add(new Cat() { clave = "BCI001030ECA", descripcion = "COMPARTAMOS" });
           myList.Add(new Cat() { clave = "BAF060524EV6", descripcion = "BANCO FAMSA" });
           myList.Add(new Cat() { clave = "BMI061005NY5", descripcion = "BMULTIVA" });
           myList.Add(new Cat() { clave = "PBI061115SC6", descripcion = "ACTINVER" });
           myList.Add(new Cat() { clave = "BWM0611132A9", descripcion = "WAL-MART" });
           myList.Add(new Cat() { clave = "NFI3406305T0", descripcion = "NAFIN" });
           myList.Add(new Cat() { clave = "ICB061106GB0", descripcion = "INTERBANCO" });
           myList.Add(new Cat() { clave = "BSI061110963", descripcion = "BANCOPPEL" });
           myList.Add(new Cat() { clave = "CIB850918BN8", descripcion = "CIBANCO" });
           myList.Add(new Cat() { clave = "BBS110906HD3", descripcion = "BBASE" });
           myList.Add(new Cat() { clave = "BPS121217FS6", descripcion = "PAGATODO" });
           myList.Add(new Cat() { clave = "HCM010608EG1", descripcion = "INMOBILIARIO" });
           myList.Add(new Cat() { clave = "BBA130722BR7", descripcion = "BANCREA" });
           myList.Add(new Cat() { clave = "DCS150115331", descripcion = "SABADELL" });
           myList.Add(new Cat() { clave = "BAN500901167", descripcion = "BANSEFI" });
           myList.Add(new Cat() { clave = "HFE011011HH1", descripcion = "HIPOTECARIA FEDERAL" });
           myList.Add(new Cat() { clave = "MCB860313CD6", descripcion = "MONEXCB" });
           myList.Add(new Cat() { clave = "GGB080116EZ0", descripcion = "GBM" });
           myList.Add(new Cat() { clave = "MCC860715J45", descripcion = "MASARI" });
           myList.Add(new Cat() { clave = "VCC920820FQ4", descripcion = "VALUE" });
           myList.Add(new Cat() { clave = "VCB870729PH6", descripcion = "VECTOR" });
           myList.Add(new Cat() { clave = "CBF920629BV0", descripcion = "FINAMEX" });
           myList.Add(new Cat() { clave = "ACB7609076M2", descripcion = "CB ACTINVER" });
           myList.Add(new Cat() { clave = "DSC950118SX2", descripcion = "CBDEUTSCHE" });
           myList.Add(new Cat() { clave = "ICB061106G80", descripcion = "CB INTERCAM" });
           myList.Add(new Cat() { clave = "VCB070518M88", descripcion = "CI BOLSA" });
           myList.Add(new Cat() { clave = "AKA060427QP2", descripcion = "AKALA" });
           myList.Add(new Cat() { clave = "BJP950104LJ5", descripcion = "CB JPMORGAN" });
           myList.Add(new Cat() { clave = "ORR920530QL5", descripcion = "REFORMA" });
           myList.Add(new Cat() { clave = "STP081030FE6", descripcion = "STP" });
           myList.Add(new Cat() { clave = "PCB050107CP2", descripcion = "EVERCORE" });
           myList.Add(new Cat() { clave = "SMN930802FN9", descripcion = "SEGMTY" });
           myList.Add(new Cat() { clave = "OEN040430H19", descripcion = "OPCIONES EMPRESARIALES DEL NOROESTE" });
           myList.Add(new Cat() { clave = "LSF970101N35", descripcion = "LIBERTAD" });

           return myList;
       }
   
   }
}
