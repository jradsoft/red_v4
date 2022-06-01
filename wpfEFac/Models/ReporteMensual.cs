using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wpfEFac.Models
{
    public class ReporteMensual
    {
        private eFacDBEntities db;

        public ReporteMensual()
        {
            db = new eFacDBEntities();
        }

        public List<Factura> GetReporteData(DateTime date,DateTime dateFin, int idEmpresa, int tipoComp)
        {

            string strStatus = "A";

            if (tipoComp == 1) {

                strStatus = "C";
            
            }



            var query = from r in db.Factura
                        where
                        r.intID_Empresa == idEmpresa &&
                        (r.dtmFecha.Year >= date.Year &&
                        r.dtmFecha.Month >= date.Month &&
                        r.dtmFecha.Day >= date.Day &&
                        r.dtmFecha.Year <= dateFin.Year &&
                        r.dtmFecha.Month <= dateFin.Month &&
                        r.dtmFecha.Day <= dateFin.Day
                        ) &&
                        r.chrStatus == strStatus &&
                        r.chrStatus != "P" ||
                        (r.dtmFechaCancelacion.HasValue ? //Si tiene fecha de cancelacion
                        r.dtmFechaCancelacion.Value.Year == date.Year && //parte true
                        r.dtmFechaCancelacion.Value.Month == date.Month
                        : false) //parte false

                        select r;



            return query.ToList();
        }

        public List<Factura> GetReporteDataVentas(DateTime date, DateTime dateFin, int idEmpresa, int typeComp)
        {
            /*
            var query = from r in db.Factura
                        where r.intID_Empresa == idEmpresa &&
                        (r.dtmFecha.Year == date.Year &&
                        r.dtmFecha.Month == date.Month) 
                         &&
                        r.chrStatus != "P"
                        select r;
            */


            string strStatus = "A";

            if (typeComp == 1)
            {

                strStatus = "C";

            }



            var query = from r in db.Factura
                        where
                        r.intID_Empresa == idEmpresa &&
                        (r.dtmFecha.Year >= date.Year &&
                        r.dtmFecha.Month >= date.Month &&
                        r.dtmFecha.Day >= date.Day &&
                        r.dtmFecha.Year <= dateFin.Year &&
                        r.dtmFecha.Month <= dateFin.Month &&
                        r.dtmFecha.Day <= dateFin.Day
                        ) &&
                        r.chrStatus == strStatus &&
                        r.chrStatus != "P" ||
                        (r.dtmFechaCancelacion.HasValue ? //Si tiene fecha de cancelacion
                        r.dtmFechaCancelacion.Value.Year == date.Year && //parte true
                        r.dtmFechaCancelacion.Value.Month == date.Month
                        : false) //parte false

                        select r;

            return query.ToList();
        }

        public String GenerateMonthReport(List<Factura> reporte, int tipoReporte, bool enviarMail)
        {
            string resultado = null; ;

            try
            {
                var empresa = reporte.First().Empresa;
                var fecha = reporte.First().dtmFecha;

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "1" + empresa.strRFC + fecha.ToString("MM") + fecha.Year; // Default file name

                if (tipoReporte == 0)
                {
                    dlg.DefaultExt = ".txt"; // Default file extension
                    dlg.Filter = "Documentos PDF (.txt)|*.txt"; // Filter files by extension
                }
                else
                {
                    dlg.DefaultExt = ".csv"; // Default file extension
                    dlg.Filter = "Excel CSV (.csv)|*.csv"; // Filter files by extension
                }

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;

                    System.IO.FileStream txtFile = new System.IO.FileStream(filename, System.IO.FileMode.Create);

                    txtFile.Close();

                    TextWriter tw = new StreamWriter(filename);

                    if (tipoReporte == 0)
                        WriteReportToFile(tw, reporte);
                    // else
                    //     WriteReportToFileVentas(tw, reporte);

                    tw.Close();
                    if ((tipoReporte == 0) || ((tipoReporte == 1) && (!enviarMail)))
                        System.Diagnostics.Process.Start(filename);

                    resultado = filename;
                }
            }
            catch (Exception e)
            {
                resultado = null;
                throw new Exception(e.Message);

            }

            return resultado;
        }



        public String GenerateMonthReportVentas(List<Factura> reporte, int tipoReporte, bool enviarMail)
        {
            string resultado = null; ;

            try
            {

                var empresa = reporte.First().Empresa;
                var fecha = reporte.First().dtmFecha;

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "1" + empresa.strRFC + fecha.ToString("MM") + fecha.Year; // Default file name

                if (tipoReporte == 0)
                {
                    dlg.DefaultExt = ".txt"; // Default file extension
                    dlg.Filter = "Documentos PDF (.txt)|*.txt"; // Filter files by extension
                }
                else
                {
                    dlg.DefaultExt = ".csv"; // Default file extension
                    dlg.Filter = "Excel CSV (.csv)|*.csv"; // Filter files by extension
                }

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;

                    System.IO.FileStream txtFile = new System.IO.FileStream(filename, System.IO.FileMode.Create);

                    txtFile.Close();

                    TextWriter tw = new StreamWriter(filename);

                    //  if (tipoReporte == 0)
                    //      WriteReportToFile(tw, reporte);
                    //  else
                    WriteReportToFileVentas(tw, reporte);

                    tw.Close();
                    if ((tipoReporte == 0) || ((tipoReporte == 1) && (!enviarMail)))
                        System.Diagnostics.Process.Start(filename);

                    resultado = filename;
                }
            }
            catch (Exception e)
            {
                resultado = null;
                throw new Exception(e.Message);

            }

            return resultado;
        }


        private void WriteReportToFileVentas(TextWriter tw, List<Factura> reporte)
        {
            string pipe = ",";



            tw.WriteLine(
           "serie" + pipe +
           "folio" + pipe +
           "NombreComercial " + pipe +
           "RazonSocial" + pipe +
            "RFC" + pipe +
                //   "Fecha Aprobacion" + pipe +
            "Fecha Captura" + pipe +
            "SubTotal" + pipe +
            "Descuento" + pipe +
            "IVA" + pipe +
            "Total" + pipe +
            "Estado" + pipe +
            "Comentario" + pipe +
            "UUID" + pipe
           // "Status"+pipe+
           // "Fecha Pago" + pipe +
           // "Forma Pago" + pipe 

            //"Ingreso/Egreso" + pipe

            );

            foreach (var item in reporte)
            {
                try
                {
                    WriteReportVentas(tw, pipe, item);
                }
                catch (Exception e)
                {

                }

            }
        }


        private void WriteReportToFile(TextWriter tw, List<Factura> reporte)
        {
            string pipe = "|";

            foreach (var item in reporte)
            {
                if (item.chrStatus == "C")
                {
                    if (item.dtmFecha.Year == item.dtmFechaCancelacion.Value.Year &&
                        item.dtmFecha.Month == item.dtmFechaCancelacion.Value.Month)
                    {
                        tw.WriteLine(pipe + item.Clientes.strRFC +
                        pipe + item.strSerie + pipe + item.strFolio +
                        pipe + item.Empresa.Folios.First(f => f.chrStatus == "A").strAño_Aprovacion + item.Empresa.Folios.First(f => f.chrStatus == "A").intNumero_Aprovacion +
                        pipe + item.dtmFecha.ToString("dd/MM/yyyy")
                        + " " + item.dtmFecha.ToString("HH:mm:ss") +
                        pipe + item.dcmTotal.ToString("F") +
                        pipe + item.dcmIVA.ToString("F") +
                        pipe + GetEstadoCFD("A") +
                        pipe + GetEfectoCFD(item.CFD.strTipoCFD) +
                        pipe + pipe + pipe + pipe
                        );

                        tw.WriteLine(pipe + item.Clientes.strRFC +
                        pipe + item.strSerie + pipe + item.strFolio +
                        pipe + item.Empresa.Folios.First(f => f.chrStatus == "A").strAño_Aprovacion + item.Empresa.Folios.First(f => f.chrStatus == "A").intNumero_Aprovacion +
                        pipe + item.dtmFecha.ToString("dd/MM/yyyy")
                        + " " + item.dtmFecha.ToString("HH:mm:ss") +
                        pipe + item.dcmTotal.ToString("F") +
                        pipe + item.dcmIVA.ToString("F") +
                        pipe + GetEstadoCFD("C") +
                        pipe + GetEfectoCFD(item.CFD.strTipoCFD) +
                        pipe + pipe + pipe + pipe
                        );
                    }
                    else
                    {
                        WriteReport(tw, pipe, item);
                    }
                }
                else
                {
                    WriteReport(tw, pipe, item);
                }
            }
        }

        private void WriteReport(TextWriter tw, string pipe, Factura item)
        {
            tw.WriteLine(pipe + item.Clientes.strRFC +
            pipe + item.strSerie + pipe + item.strFolio +
            pipe + item.Empresa.Folios.First(f => f.chrStatus == "A").strAño_Aprovacion + item.Empresa.Folios.First(f => f.chrStatus == "A").intNumero_Aprovacion +
            pipe + item.dtmFecha.ToString("dd/MM/yyyy")
            + " " + item.dtmFecha.ToString("HH:mm:ss") +
            pipe + item.dcmTotal.ToString("F") +
            pipe + item.dcmIVA.ToString("F") +
            pipe + GetEstadoCFD(item.chrStatus) +
            pipe + GetEfectoCFD(item.CFD.strTipoCFD) +
            pipe + pipe + pipe + pipe
            );
        }


        private void WriteReportVentas(TextWriter tw, string pipe, Factura item)
        {
            decimal subtotal = 0;
            decimal descuento = 0;
            decimal total = 0;
            subtotal = item.dcmSubTotal;
            descuento = item.Detalle_Factura.Sum(p => p.dcmDescuento);
            string strEstado = item.chrStatus;

            total = item.dcmTotal;
            if (descuento > 0) total -= descuento;
            

            tw.WriteLine(
            item.strSerie + pipe +
            item.strFolio + pipe +
            item.Clientes.strNombreComercial.Replace(",", "") + pipe +
            item.Clientes.strRazonSocial.Replace(",", "") + pipe +
            item.Clientes.strRFC + pipe +
                // item.dtmFechaAprovacion.Value.ToString("dd/MM/yyyy") + pipe +
             item.dtmFecha.ToString("dd/MM/yyyy") + pipe +
             subtotal.ToString("F").Replace(",", ".") + pipe +
             descuento.ToString("F").Replace(",", ".") + pipe +


             item.dcmIVA.ToString("F").Replace(",", ".") + pipe +

             total.ToString("F").Replace(",", ".") + pipe +

             item.chrStatus + pipe +

             item.strNumero + pipe +

             item.strSelloDigital + pipe +
             

           //  item.strNumero + pipe +

           //  item.dtmFechaEnvio + pipe +

        //     item.strNumeroContrato + pipe +

             GetEfectoCFD(item.CFD.strTipoCFD)

            );
        }

        private string GetEfectoCFD(string p)
        {
            switch (p)
            {
                case "ingreso":
                    {
                        return "I";
                    }
                case "egreso":
                    {
                        return "E";
                    }
                case "traslado":
                    {
                        return "T";
                    }
                default:
                    return "E";

            }
        }

        private string GetEstadoCFD(string p)
        {
            if (p == "C")
            {
                return "0";
            }

            return "1";
        }

    }
}