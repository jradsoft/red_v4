using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Transactions;

namespace wpfEFac.Models
{
    public class EditarEmpresaViewModel
    {
        eFacDBEntities db = new eFacDBEntities();
        EmpresaData empdb;

        public EditarEmpresaViewModel()
        {
            empdb = new EmpresaData();
        }

        public Empresa GetEmpresa(int id) 
        {
            Empresa emp = empdb.getEmpresa(id);

            if (emp!=null)
            {
                return emp;
            }

            return null;
        }

        public Direcciones_Fiscales GetDireccion(int idEmpresa) 
        {
            Direcciones_Fiscales Direccion = empdb.GetDireccionFiscal(idEmpresa);

            if (Direccion != null)
            {
                return Direccion;
            }

            return null;
        }

        public ObservableCollection<CFD> GetCFD() 
        {
            return empdb.GetCFD();
        }

        public ObservableCollection<Folios> GetFolios()
        {
            return empdb.GetFolios();
        }

        public ObservableCollection<Paises> GetPais()
        {
            return empdb.GetPais();
        }

        public ObservableCollection<Estado> GetEstado()
        {
            return empdb.GetEstado();
        }

        public bool UpdateEmpresa(int idEmpresa, string razonSocial,
            string nombreComercial, string giro, string rfc, int idCFD,
            string telefono, string telefonoMovil,
            string webSite, string telefonoAlternativo, string logo,
            string cedula, string rutaXML, string rutaPDF, string calle, string colonia, string numeroInterio, string numeroExterior,
            int idPais, int idEstado, string municipo, string poblacion,
            string codigoPostal, string status, string emailEmpresa, string emailContador) 
        {
            try
            {
                Empresa empresa = db.Empresa.First(emp => emp.intID == idEmpresa);
                Direcciones_Fiscales direccion = empdb.GetDireccionFiscal(idEmpresa, db);

                //using (TransactionScope scope = new TransactionScope())
                //{

                    empresa.strRazonSocial = razonSocial;
                    empresa.strNombreComercial = nombreComercial;
                    empresa.strGiro = giro;
                    empresa.strRFC = rfc;
                    empresa.intID_CFD = idCFD;
                    empresa.strTelefono = telefono;
                    empresa.strTelefonoMovil = telefonoMovil;
                    empresa.strWebSite = webSite;
                    empresa.strTelefono2 = telefonoAlternativo;
                    empresa.strLogo = logo;
                    empresa.strCedula = cedula;
                    empresa.strDirectorioXML = rutaXML;
                    empresa.strDirectorioPDF = rutaPDF;
                    empresa.strEmail = emailEmpresa;
                    empresa.strEmail2 = emailContador;

                    int resultado = db.SaveChanges();

                    UpdateDireccionFiscalEmpresa(direccion, calle, colonia,
                        numeroInterio, numeroExterior, idPais,
                        idEstado, municipo, poblacion, codigoPostal,
                        status, db);

                    //scope.Complete();

                    db.AcceptAllChanges();
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally 
            {
                db.Connection.Close();
            }
        }

        public bool UpdateDireccionFiscalEmpresa(Direcciones_Fiscales direccion,
            string calle, string colonia, string numeroInterio, string numeroExterior,
            int idPais, int idEstado, string municipo, string poblacion, 
            string codigoPostal, string status, eFacDBEntities db) 
        {
            try
            {
                direccion.strCalle = calle;
                direccion.strColonia = colonia;
                direccion.strNoInterior = numeroInterio;
                direccion.strNoExterior = numeroExterior;
                direccion.intIDPais = idPais;
                direccion.intIDEstado = idEstado;
                direccion.strMunicipio = municipo;
                direccion.strPoblacionLocalidad = poblacion;
                direccion.strCodigoPostal = codigoPostal;
                direccion.chrStatus = status;
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateConfiguracionCorreoElectronico(Empresa emp,int idConfiguracionCorreo, string host,
            int port, string emailRespaldo, string emailContador, string password,
            string emailEmpresarial, string emailAlternativo, bool enableSSL)
        {
            try
            {
                Empresa empresa = db.Empresa.Single(empre => empre.intID == emp.intID);

                ConfiguracionEmail configEmail = db.ConfiguracionEmail.Single(ce => ce.intID == idConfiguracionCorreo);

                configEmail.strSMTPHost = host;
                configEmail.intPort = port;
                configEmail.strE_MailRespaldo = emailRespaldo;
                configEmail.strE_MailContador = emailContador;
                configEmail.strPasswordEmail = password;
               // configEmail.EnableSsl = enableSSL;

                empresa.strEmail = emailEmpresarial;
                empresa.strEmail2 = emailAlternativo;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCer(Empresa currentEmpresa, string numerCertificado,
            string rutaArchivoCer, string rutaArchivoKey, string contraseña,
            DateTime validoDesde, DateTime validoHasta, DateTime fechaSubida) 
        {
            try
            {
                Empresa empresa = db.Empresa.Single(empre => empre.intID == currentEmpresa.intID);

                Certificates certificates = db.Certificates.FirstOrDefault(c => c.intID_Empresa == empresa.intID);

                certificates.strNumeroCertificadoSelloDigital = numerCertificado;
                certificates.strCertificadoSelloDigitalPath = rutaArchivoCer;
                certificates.strLlaveCertificadoPath = rutaArchivoKey;
                certificates.strContraseñaSAT = contraseña;
                certificates.dtmValidoDesde = validoDesde;
                certificates.dtmValidoHasta = validoHasta;
                certificates.dtmFechaSubida = fechaSubida;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateFolios(Empresa emp, int folioIncial, int folioFinal,
            int numerAprobacion, string serie, string ano_aprobacion, int folioActual)
        {
            try
            {
                Empresa empresa = db.Empresa.Single(empre => empre.intID == emp.intID);

                Folios folio = db.Folios.FirstOrDefault(f => f.intID_Empresa == emp.intID);

                folio.intFolio_Inicial = folioIncial;
                folio.intFolio_Final = folioFinal;
                folio.intNumero_Aprovacion = numerAprobacion;
                folio.strSerie = serie;
                folio.strAño_Aprovacion = ano_aprobacion;
                folio.intFolioActual = folioActual;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
