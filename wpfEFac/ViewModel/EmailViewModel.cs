using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Text.RegularExpressions;
using wpfEFac.Models;
using wpfEFac.Helpers;
using System.Windows;

namespace wpfEFac.ViewModel
{
    public class EmailViewModel : INotifyPropertyChanged
    {
        private wpfEFac.Models.eFacDBEntities db;
        
        private ObservableCollection<string> _destinationEmails;
        private bool _sendEmail;
        private string _toEmail;
        private string _fromEmail;
        private string _subject = "Comprobante fiscal digital";
        private string _message = "Mensaje enviado por la aplicacion <MyFactura - E>" ;
        private string _pdfAttachement;
        private string _xmlAttachement;
        private bool _ssl;

        private string _mailpattern = @"^(([^<>()[\]\\.,;:\s@\""]+" +
            @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" +
            @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" +
            @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" +
            @"[a-zA-Z]{2,}))$";
        private Regex mailRegex;

        public const string DestinationEmailsPropertyName = "DestinationEmails";
        public const string SendingEmailPropertyName = "SendingEmail";
        public const string ToEmailPropertyName = "ToEmail";
        public const string FromEmailPropertyName = "FromEmail";
        public const string SubjectPropertyName = "Subject";
        public const string MessagePropertyName = "Message";
        public const string PDFAttachementPropertyName = "PDFAttachement";
        public const string XMLAttachementPropertyName = "XMLAttachement";
        public const string SslPropertyName = "Ssl";

        public EmailViewModel()
        {
            if (!(bool) DesignerProperties.IsInDesignModeProperty
                .GetMetadata(typeof(DependencyObject)).DefaultValue)
            {
                db = new Models.eFacDBEntities();
            }
            else
            {
                this.FromEmail = "Ska_p264@homail.com";
                this.ToEmail = "adsoft@live.com.mx";
                this.XMLAttachement = "XML";
                this.PDFAttachement = "PDF";
            }

            _destinationEmails = new ObservableCollection<string>();

            SendEmailCommand = new RelayCommand(() => SendEmail());

            mailRegex = new Regex(_mailpattern);
        }

        public RelayCommand SendEmailCommand
        {
            get;
            private set;
        }

        public string ToEmail
        {
            get
            {
                return _toEmail;
            }
            set
            {
                if (_toEmail == value)
                {
                    return;
                }

                _toEmail = value;
                RaisePropertyChanged(ToEmailPropertyName);
            }
        }

        public string FromEmail
        {
            get
            {
                return _fromEmail;
            }
            set
            {
                if (_fromEmail == value)
                {
                    return;
                }

                _fromEmail = value;
                RaisePropertyChanged(FromEmailPropertyName);
            }
        }

        public string Subject 
        {
            get { return _subject; }
            set 
            {
                if (_subject == value)
                {
                    return;
                }

                _subject = value;
                RaisePropertyChanged(SubjectPropertyName);
            }
        }

        public string Message {
            get { return _message; }
            set 
            {
                if (_message == value)
                {
                    return;
                }

                _message = value;
                RaisePropertyChanged(MessagePropertyName);
            }
        }

        public string PDFAttachement
        {
            get { return _pdfAttachement; }
            set 
            {
                if (_pdfAttachement == value)
                {
                    return;
                }

                _pdfAttachement = value;
                RaisePropertyChanged(PDFAttachementPropertyName);
            }
        }

        public string XMLAttachement
        {
            get { return _xmlAttachement; }
            set 
                {
                    if (_xmlAttachement == value)
                    {
                        return;
                    }
                    
                    _xmlAttachement = value;
                    RaisePropertyChanged(XMLAttachementPropertyName);
                }
        }

        public bool Ssl { get { return _ssl; } 
            set 
            {
                if (_ssl == value)
                {
                    return;
                }

                _ssl = value;
                RaisePropertyChanged(SslPropertyName);
            } 
        }

        public bool SendingEmail
        {
            get { return _sendEmail; }
            set
            {
                if (_sendEmail == value)
                {
                    return;
                }

                _sendEmail = value;
                RaisePropertyChanged(SendingEmailPropertyName);
                SendEmailCommand.RaiseCanExecuteChanged();
            }
        }


        private bool SendEmail()
        {
            try
            {
                if (ValidarDatos())
                {
                    ConfiguracionEmail configEmail = Empresa.ConfiguracionEmail.Single(ce => ce.intID_Empresa == Empresa.intID);

                    List<string> toEmails = new List<string>(ToEmail.Split(';'));
                    toEmails.RemoveAll(item => item == string.Empty);

                    Messenger.Default.Send<bool>(wpfEFac.Models.MailSender.SendMail(FromEmail, configEmail.strPasswordEmail,
                        toEmails, Subject, Message,
                        new List<string>() { XMLAttachement, PDFAttachement },
                        configEmail.strSMTPHost, configEmail.intPort, Ssl));

                    return true;
                }
                else
                {
                    Messenger.Default.Send<bool>(false);
                    return false;
                }
            }
            catch (Exception)
            {
                Messenger.Default.Send<bool>(false);
                return false;
            }

            
        }

        private bool ValidarDatos()
        {
            try
            {
                string[] toEmails = ToEmail.Split(';');

                foreach (var item in toEmails)
                {
                    if (!Validador.ValidarCorreoElectronico(item)) 
                    {
                        if (!(toEmails[toEmails.Length - 1] == item))
                        {
                            throw new ApplicationException("Correo invalido");
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetFromatMails()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in _destinationEmails)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }

        public void RaisePropertyChanged(string propertyName) 
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void CreatePDFFile(Models.Factura f)
        {
            try
            {
                if (!string.IsNullOrEmpty(PDFAttachement))
                {
                    return;
                }

                PreFacturaViewModel pfvm = new PreFacturaViewModel();

                var direccionEmisor = pfvm.GetDireccionEmpresa(f.Empresa.intID);

                var direccionReceptor = pfvm.GetDireccionCliente(f.Clientes.intID);

                Helpers.CFDReporte infoReporte = FillInfoReporte(f, direccionEmisor, direccionReceptor);

               // wpfEFac.Views.Reports.CFD pdfExporter = new wpfEFac.Views.Reports.CFD(infoReporte);

               // wpfEFac.Views.Reports.CFD ventanaReporte = new wpfEFac.Views.Reports.CFD(infoReporte);

                //string fileName = "PDF_" + f.Empresa.strRFC + "_" + f.dtmFecha.ToString("dd_MM_yyyy");

                //fileName = ventanaReporte.DoPDF(infoReporte, fileName);

                //PDFAttachement = fileName;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al generar archivo pdf adjunto a correo");
            }
            
        }

        private Helpers.CFDReporte FillInfoReporte(Models.Factura f, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionReceptor)
        {
            Helpers.CFDReporte infoReporte = new wpfEFac.Helpers.CFDReporte()
            {
                Folio = "Folio: " + f.strSerie + "-" + f.strFolio,
                Fecha = "Fecha: " + f.dtmFecha.ToString("yyyy-MM-dd") + "T" + f.dtmFecha.ToString("HH:mm:ss"),
                NumeroCertificado = "Certificado: " + f.Certificates.strNumeroCertificadoSelloDigital,
                NumeroAprobacion = "No. Aprob. " + f.Empresa.Folios.First(fol => fol.chrStatus == "A").intNumero_Aprovacion,
                AnoArobacion = "Año aprobacion" + f.Certificates.dtmValidoDesde.Year,

                Logo = "file:///" + f.Empresa.strLogo,
                NombreEmisor = f.Empresa.strRazonSocial,
                DomicilioEmisor = direccionEmisor.strCalle + " " +
                direccionEmisor.strNoExterior + " " + direccionEmisor.strColonia + " " +
                direccionEmisor.strCodigoPostal + " " + direccionEmisor.strMunicipio + " " +
                direccionEmisor.Estado.strNombreEstado + " " + direccionEmisor.Paises.strNombrePais,
                RFCEmisor = f.Empresa.strRFC,
                Telefono = f.Empresa.strTelefono,

                NombreCliente = f.Clientes.strRazonSocial,
                DomicilioCliente = direccionReceptor.strCalle + " " +
                direccionReceptor.strNoExterior + " " + direccionReceptor.strColonia + " " +
                direccionReceptor.strCodigoPostal + " " + direccionReceptor.strMunicipio + " " +
                direccionReceptor.Estado.strNombreEstado + " " + direccionReceptor.Paises.strNombrePais,
                RFCCliente = f.Clientes.strRFC,

                Proveedor = "Proveedor " + f.strProveedor, // prueva
                NoPedido = "No. Pedido " + f.strNumero,
                NoContrato = "No. Contrato " + f.strNumeroContrato,
                Usuario = f.Usuarios.strNombre,
                Estimacion = "Estimación: " + f.intEstimacion,

                ImporteLetra = ConvertidorNumeroLetra.NumeroALetras(f.dcmTotal.ToString("F"), "PESOS"),
                SubTotal = f.dcmSubTotal.ToString("F"),

                IVA = f.dcmIVA.ToString("F"),
                Total = f.dcmTotal.ToString("F"),

                Observaciones = f.strObervaciones,

                Conceptos = GetConceptosReporte(f.Detalle_Factura),

                SelloDigital = f.strSelloDigital,

                CadenaOriginal = f.strCadenaOriginal,
                Cedula = "file:///" + f.Empresa.strCedula,
            };
            return infoReporte;
        }

        private List<Detalle_PreFactura> GetConceptosReporte(System.Data.Objects.DataClasses.EntityCollection<Detalle_Factura> entityCollection)
        {
            List<Detalle_PreFactura> result = new List<Detalle_PreFactura>();

            foreach (var item in entityCollection)
            {
                //Thest this should change to the data base

                result.Add(new Detalle_PreFactura()
                {
                    intCantidad = item.dcmCantidad,
                    dcmDescento = item.dcmDescuento,
                    dcmImporte = item.dcmImporte.ToString("N"),
                    strUnidad = item.Productos.UnidadMedida.strDescripcion,
                    strConcepto = item.strConcepto,
                    dcmPrecioUnitario = item.dcmPrecioUnitario.ToString("N"),
                    strPartida = item.strPatida, //Thest
                    dcmIIVA = item.dcmIVA.Value.ToString("F")
                });
            }

            return result;
        }

        public void CreateXMLFile(Models.Factura f)
        {
            try 
	        {
                if (!string.IsNullOrEmpty(XMLAttachement))
                {
                    return;
                }
                
                PreFacturaViewModel pfvm = new PreFacturaViewModel();
                dlleFac.MyFactE myf = new dlleFac.MyFactE();

                Direcciones_Fiscales direccionEmisor;
                Direcciones_Fiscales direccionReceptor;
                List<dlleFac.Concepto> c;
                dlleFac.Impuestos impuesto;
                List<dlleFac.Traslado> tr;

                GetValuesCFD(f, pfvm, out direccionEmisor, out direccionReceptor, out c, out impuesto, out tr);

                dlleFac.ComprobanteFiscalDigital cfd = CreateCFDDllObject(f, direccionEmisor, direccionReceptor, c, impuesto, tr);

                string fileName = "XML_" + f.Empresa.strRFC + "_" + f.dtmFecha.ToString("dd_MM_yyyy") + ".xml";

                //myf.GetXML(f.strCadenaOriginal, f.strSelloDigital, f.Certificates.strNumeroCertificadoSelloDigital, fileName, cfd);

                XMLAttachement = fileName;
	        }
	        catch (Exception)
	        {
		        throw new ApplicationException("Error al crear archivo XML adjunto en e-mail");
	        }
        }

        private dlleFac.ComprobanteFiscalDigital CreateCFDDllObject(Models.Factura f, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionReceptor, List<dlleFac.Concepto> c, dlleFac.Impuestos impuesto, List<dlleFac.Traslado> tr)
        {
            dlleFac.ComprobanteFiscalDigital cfd = new dlleFac.ComprobanteFiscalDigital()
            {
                Version = "2.0",
                Serie = f.strSerie,
                Folio = f.strFolio,
                Fecha = f.dtmFecha.ToString("yyyy-MM-dd") + "T" + f.dtmFecha.ToString("HH:mm:ss"),
                NoAprobacion = f.Empresa.Folios.First(fol => fol.chrStatus == "A").intNumero_Aprovacion.ToString(),
                AñoAprobacion = f.Certificates.dtmValidoDesde.Year.ToString(),
                TipoComprobante = f.CFD.strTipoCFD,
                FormaPago = f.strForma_Pago,
                SubTotal = f.dcmSubTotal.ToString("F"),
                Total = f.dcmTotal.ToString("F"),
                Emisor = new dlleFac.Emisor() { RFCEmisor = f.Empresa.strRFC, NombreEmisor = f.Empresa.strNombreComercial },
                DomicilioFiscalEmisor = new dlleFac.DomicilioFiscal()
                {
                    Calle = direccionEmisor.strCalle,
                    NumeroExterior = direccionEmisor.strNoExterior,
                    Colonia = direccionEmisor.strColonia,
                    Localidad = direccionEmisor.strPoblacionLocalidad,
                    Referencia = null,
                    Municipio = direccionEmisor.strMunicipio,
                    Estado = direccionEmisor.Estado.strNombreEstado,
                    Pais = direccionEmisor.Paises.strNombrePais,
                    CodigoPostal = direccionEmisor.strCodigoPostal
                },

                ExpedidoEn = new dlleFac.DomicilioFiscal()
                {
                    Calle = direccionEmisor.strCalle,
                    NumeroExterior = direccionEmisor.strNoExterior,
                    Colonia = direccionEmisor.strColonia,
                    Localidad = direccionEmisor.strPoblacionLocalidad,
                    Referencia = null,
                    Municipio = direccionEmisor.strMunicipio,
                    Estado = direccionEmisor.Estado.strNombreEstado,
                    Pais = direccionEmisor.Paises.strNombrePais,
                    CodigoPostal = direccionEmisor.strCodigoPostal
                },

                Receptor = new dlleFac.Receptor()
                {
                    RFCReceptor = f.Clientes.strRFC,
                    NombreReceptor = f.Clientes.strRazonSocial,
                },

                DomicilioFiscalReceptor = new dlleFac.DomicilioFiscal()
                {
                    Calle = direccionReceptor.strCalle,
                    NumeroExterior = direccionReceptor.strNoExterior,
                    Colonia = direccionReceptor.strColonia,
                    Localidad = direccionReceptor.strPoblacionLocalidad,
                    Referencia = null,
                    Municipio = direccionReceptor.strMunicipio,
                    Estado = direccionReceptor.Estado.strNombreEstado,
                    Pais = direccionReceptor.Paises.strNombrePais,
                    CodigoPostal = direccionReceptor.strCodigoPostal
                },

                Conceptos = c,

                Impuestos = new dlleFac.Impuestos()
                {
                    TotalTraslados = impuesto.TotalTraslados,
                    Traslados = tr
                }
            };

            return cfd;
        }

        private void GetValuesCFD(Models.Factura f, PreFacturaViewModel pfvm,
            out Direcciones_Fiscales direccionEmisor, out Direcciones_Fiscales direccionReceptor,
            out List<dlleFac.Concepto> c, out dlleFac.Impuestos impuesto,
            out List<dlleFac.Traslado> tr)
        {
            direccionEmisor = pfvm.GetDireccionEmpresa(f.Empresa.intID);

            direccionReceptor = pfvm.GetDireccionCliente(f.Clientes.intID);

            c = new List<dlleFac.Concepto>();

            impuesto = new dlleFac.Impuestos();

            tr = new List<dlleFac.Traslado>();

            foreach (var item in f.Detalle_Factura)
            {
                c.Add(new dlleFac.Concepto()
                {
                    Cantidad = item.dcmCantidad.ToString(),
                    UnidadMedida = item.Productos.UnidadMedida.strDescripcion,
                    NoIdentificacion = item.Productos.strCodigo,
                    Descripcion = item.Productos.strNombre,
                    ValorUnitario = item.dcmPrecioUnitario.ToString(),
                    Importe = item.dcmImporte.ToString("F")
                });
            }
            /*
            foreach (var item in f.Traslados)
            {
                tr.Add(new dlleFac.Traslado()
                {
                    Importe = item.dcmImporte.ToString("F"),
                    TipoImpuesto = item.strTipoImpuesto,
                    TotalImpuestosTraslados = item.TotalImpuestoTrasladado.ToString("F"),
                    Tasa = item.dcmTasa.ToString("F")
                });

                impuesto.TotalTraslados = item.TotalImpuestoTrasladado.ToString("F");
            }
             */
        }


        public Empresa Empresa { get; set; }
    }
}
