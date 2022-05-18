using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wpfEFac.Views.Clientes;
using System.Transactions;

namespace wpfEFac.Models
{
    public class BusClientes
    {
        private eFacDBEntities entidad = new eFacDBEntities();

        DirectorioClientes directorio = new DirectorioClientes();
        AgregarCliente agregar = new AgregarCliente();
        
  
        public bool AgregarCliente( string RFC, string RazonSocial, string NombreComercial,
            string Giro, string TipoComprobante, string Telefono, string TelefonoMovil, string Email,
            string RetencionIVA, string RetencionISR, string Contacto, string WebSite, string Calle, string NoExterior, string NoInterior,
            string Colonia, int Pais, int Estado, string Municipio, string Poblacion, string Codigo, int IDE, int Addenda)
        {
            try
            {
                entidad.Connection.Open();

                using (TransactionScope scope = new TransactionScope()) 
                {
                    Clientes c = new Clientes();

                    c.intID = Identifier.GetID(typeof(Clientes).Name, IDE, c, EntityEnum.Clientes, entidad);
                    c.strRFC = RFC;
                    c.strRazonSocial = RazonSocial;
                    c.strNombreComercial = NombreComercial;
                    c.strGiro = Giro;
                    c.strTipodeInscripcion = TipoComprobante;
                    c.strTelefono = Telefono;
                    c.strTelefonoMovil = TelefonoMovil;
                    c.strEmail = Email;
                    c.chrRetencionIVA = RetencionIVA;
                    c.chrRetencionISR = RetencionISR;
                    c.strContacto = Contacto;
                    c.strWebSite = WebSite;
                    c.intID_Empresa = IDE;
                   // c.idAddenda = Addenda;
                    Direcciones_Fiscales df = new Direcciones_Fiscales();
                    
                    df.intID = Identifier.GetID(typeof(Direcciones_Fiscales).Name, IDE, df, EntityEnum.Direcciones_Fiscales, entidad);
                    df.strTipoEntidad = "C";
                    df.strIDCliente = c.intID;
                    df.strCalle = Calle;
                    df.strNoExterior = NoExterior;
                    df.strNoInterior = NoInterior;
                    df.intIDPais = Pais;
                    df.intIDEstado = Estado;
                    df.strMunicipio = Municipio;
                    df.strPoblacionLocalidad = Poblacion;
                    df.strCodigoPostal = Codigo;
                    df.chrStatus = "1";
                    df.strColonia = Colonia;
                    df.intID_Empresa = IDE;

                    entidad.Clientes.AddObject(c);
                    entidad.Direcciones_Fiscales.AddObject(df);

                    entidad.SaveChanges();

                    scope.Complete();

                    entidad.AcceptAllChanges();

                    entidad.Connection.Close();

                    Identifier.IsUsing = false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AgregarProducto(/*int ID,*/ 
                string Codigo,
                string CodigoBarras,
                string Nombre,
                string NombreCorto,
                string Descripcion,
                decimal Descuento,

                decimal Precio1,
                decimal Precio2,
                decimal Precio3,
                decimal Precio4,
                decimal Precio5,

                int Categoria,
                int Unidad,

                string IVA,
                decimal porcIVA,

                string retIVA,
                decimal porcRetIVA,
                
                string retISR,
                decimal porcRetISR,
                
                string retIeps,
                decimal porcRetIeps,

                decimal existencias,
                decimal stockMin,
                decimal stockMax,
                decimal puntoReorden,
                Int32 IDE

            )
        {
            try
            {

                Productos p = new Productos();
                p.intID = Identifier.GetID(typeof(Productos).Name, IDE, p, EntityEnum.Productos, entidad);
                p.intID_Empresa = IDE;

                p.strCodigo = Codigo;
                p.strCodigoBarras = CodigoBarras;
                p.strNombre = Nombre;
                p.strNombreCorto = NombreCorto;
                p.strDescripcion = Descripcion;
                p.dcmDescuent = Descuento;   

                p.dcmPrecio1 = Precio1;
                p.dcmPrecio2 = Precio2;
                p.dcmPrecio3 = Precio3;
                p.dcmPrecio4 = Precio4;
                p.dcmPrecio5 = Precio5;

                
                p.intID_Categoria = Categoria;
                p.intUnidad = Unidad;

                p.gravaIva = IVA;
                p.porcIva = porcIVA;

                p.gravaRetIva = retIVA;
                p.porcRetIva = porcRetIVA;
                
                p.gravaRetIsr = retISR;
                p.porcRetIsr = porcRetISR;

                p.gravaIeps = retIeps;
                p.porcIeps = porcRetIeps;

                p.existencias = existencias;
                p.stockMin = stockMin;
                p.stockMax = stockMax;
                p.puntoReorden = puntoReorden;


                entidad.Productos.AddObject(p);

                entidad.SaveChanges();

                Identifier.IsUsing = false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        public bool AgregarUsuario(int ID, string Nombre, string Password, int IdGrupo, int IdEmpresa, string Email)
        {
            try
            {
                Usuarios u = new Usuarios()
                {
                    intID = ID,
                    strNombre = Nombre,
                    strPassword = Password,
                    intID_Grupo = IdGrupo,
                    intID_Empresa = IdEmpresa,
                    strEmail = Email
                };

                entidad.Usuarios.AddObject(u);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AgregarGrupo(/*int ID,*/ string Nombre, DateTime FechaCreacion, int IDE)
        {
            try
            {
                Grupos g = new Grupos()
                {
                    //intID = ID,                    
                    strDescripcion = Nombre,
                    dtmFechaCreacion = FechaCreacion,
                    intID_Empresa = IDE
                };

                entidad.Grupos.AddObject(g);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool EditarParametros(int ID, decimal IVAGeneral, decimal IVAZonaFronteriza, decimal RetencionIVA, decimal RetencionISR, char chrIVAZonaFronteriza)
        {
            try
            {
                Configuracion_Regional cr = new Configuracion_Regional()
                {
                    intID = ID,
                    dcmIVAGeneral = IVAGeneral,
                    dcmIVAZonaFronteriza = IVAZonaFronteriza,
                    dcmRetencionIVA = RetencionIVA,
                    dcmRetencionISR = RetencionISR,
                    chrIVAZonaFronteriza = chrIVAZonaFronteriza.ToString()
                };

                entidad.Configuracion_Regional.AddObject(cr);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool AgregarPais(/*int ID,*/ string Nombre)
        {
            try
            {
                Paises p = new Paises()
                {                    
                    //intID = ID,
                    strNombrePais = Nombre
                };

                entidad.Paises.AddObject(p);

                entidad.SaveChanges();               

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }


         

        public bool AgregarVehiculo( string placa, string modelo,string ano, string poliza, string aseguradoraCarga,
            string aseguradoraRespCiv,string tipo)
        {
            try
            {
                Vehiculos v = new Vehiculos()
                {
                    //intID = ID,
                    strPlaca = placa,
                    strModelo = modelo,
                    strAno = ano,
                    strNoPoliza = poliza,
                    strAseguradoraCarga = aseguradoraCarga,
                    strAseguradoraRepCivil = aseguradoraRespCiv,
                    strTipoVehiculo = tipo
                    

                };

                entidad.Vehiculos.AddObject(v);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool EditarVehiculo(string id,string placa, string modelo, string ano, string poliza, string aseguradoraCarga,
            string aseguradoraRespCiv, string tipo) {

                try
                {
                    Vehiculos v = entidad.Vehiculos.First(x =>x.strPlaca ==id);
                    {
                    
                       // v.strPlaca = placa;
                        v.strModelo = modelo;
                        v.strAno = ano;
                        v.strNoPoliza = poliza;
                        v.strAseguradoraCarga = aseguradoraCarga;
                        v.strAseguradoraRepCivil = aseguradoraRespCiv;
                        v.strTipoVehiculo = tipo;


                    };

                    

                    entidad.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                    throw;
                }
        
        }


        public bool AgregarDatosEmpresa(/*int ID,*/ string Tipo, int IDCLiente, string RFC, string RazonSocial, string NombreComercial,
            string Giro, string TipoComprobante, string Calle, string NoExterior, string NoInterior,
            string Colonia, int Pais, int Estado, string Municipio, string Poblacion,
            string CodigoPostal, string Telefono1, string Telefono2, string TelefonoMovial, string Email1,
            string Email2, string WebSite)
        {
            try
            {
                Empresa e = new Empresa()
                {
                    //intID = ID,
                    strRazonSocial = RazonSocial,
                    strNombreComercial = NombreComercial,
                    strGiro = Giro,
                    strRFC = RFC,
                    //strIDCFD = "1",
                    strTelefono = Telefono1,
                    strTelefono2 = Telefono2,
                    strTelefonoMovil = TelefonoMovial,
                    strEmail = Email1,
                    strEmail2 = Email2,
                    strWebSite = WebSite
                };

                Direcciones_Fiscales df = new Direcciones_Fiscales()
                {
                    intID = 1,
                    strTipoEntidad = Tipo,
                    strIDCliente = IDCLiente,
                    strCalle = Calle,
                    strNoInterior = NoInterior,
                    strNoExterior = NoExterior,
                    intIDPais = Pais,
                    intIDEstado = Estado,
                    strMunicipio = Municipio,
                    strPoblacionLocalidad = Poblacion,
                    strCodigoPostal = CodigoPostal,
                    chrStatus = "1",
                    strColonia = Colonia
                };

                entidad.Empresa.AddObject(e);

                entidad.Direcciones_Fiscales.AddObject(df);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool AgregarEstado(/*int ID,*/ int Pais, string Nombre)
        {
            try
            {
                Estado e = new Estado()
                {
                    //intID = ID,
                    intID_Pais = Pais,                    
                    strNombreEstado = Nombre
                };

                entidad.Estado.AddObject(e);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool EditarCliente(int idCliente, string RFC, string Razon, string Comercial, string Giro, string Tipo,
            string Calle, string NoExterior, string NoInterior, string Colonia, int Pais, int Estado,
            string Municipio, string Poblacion, string Codigo, string TCasa, string TOficina, string Email,
            string Contacto, string retIVA, string retISR, string Web,int Addenda)
        {
            try
            {
                Clientes c = entidad.Clientes.First(cliente => cliente.intID == idCliente);
                    
                c.strRFC = RFC;
                c.strRazonSocial = Razon;
                c.strNombreComercial = Comercial;
                c.strGiro = Giro;
                c.strTipodeInscripcion = Tipo;
                c.strTelefono = TCasa;
                c.strTelefonoMovil = TOficina;
                c.strEmail = Email;
                c.strContacto = Contacto;
                c.chrRetencionIVA = retIVA;
                c.chrRetencionISR = retISR;
                c.strWebSite = Web;
              //  c.idAddenda = Addenda;

                Direcciones_Fiscales df = entidad.Direcciones_Fiscales.First(cli => cli.strIDCliente == idCliente && cli.strTipoEntidad == "C");
                
                df.strCalle = Calle;
                df.strNoExterior = NoExterior;
                df.strNoInterior = NoInterior;
                df.strColonia = Colonia;
                df.intIDPais = Pais;
                df.intIDEstado = Estado;
                df.strMunicipio = Municipio;
                df.strPoblacionLocalidad = Poblacion;
                df.strCodigoPostal = Codigo;                                

                entidad.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;                
            }
        
        }

        public bool EditarProducto(
                string Codigo,
                string CodigoBarras,
                string Nombre,
                string NombreCorto,
                string Descripcion,
                decimal Descuento,

                decimal Precio1,
                decimal Precio2,
                decimal Precio3,
                decimal Precio4,
                decimal Precio5,

                int Categoria,
                int Unidad,

                string IVA,
                decimal porcIVA,

                string retIVA,
                decimal porcRetIVA,
                
                string retISR,
                decimal porcRetISR,
                
                string retIeps,
                decimal porcRetIeps,

                decimal existencias,
                decimal stockMin,
                decimal stockMax,
                decimal puntoReorden
            )
        {
            try
            {
                Productos p = entidad.Productos.First(c => c.strCodigo == Codigo);

                p.strCodigo = Codigo;
                p.strCodigoBarras = CodigoBarras;
                p.strNombre = Nombre;
                p.strNombreCorto = NombreCorto;
                p.strDescripcion = Descripcion;
                p.dcmDescuent = Descuento;

                p.dcmPrecio1 = Precio1;
                p.dcmPrecio2 = Precio2;
                p.dcmPrecio3 = Precio3;
                p.dcmPrecio4 = Precio4;
                p.dcmPrecio5 = Precio5;


                p.intID_Categoria = Categoria;
                p.intUnidad = Unidad;

                p.gravaIva = IVA;
                p.porcIva = porcIVA;

                p.gravaRetIva = retIVA;
                p.porcRetIva = porcRetIVA;

                p.gravaRetIsr = retISR;
                p.porcRetIsr = porcRetISR;

                p.gravaIeps = retIeps;
                p.porcIeps = porcRetIeps;

                p.existencias = existencias;
                p.stockMin = stockMin;
                p.stockMax = stockMax;
                p.puntoReorden = puntoReorden;

                

                entidad.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditarUsuario(int ID, string Nombre, int IDGrupo, int IDEmpresa, string Email, string password)
        {
            try
            {
                Usuarios u = entidad.Usuarios.First(usu => usu.intID == ID);

                u.strNombre = Nombre;
                u.intID_Grupo = IDGrupo;
                u.intID_Empresa = IDEmpresa;
                u.strEmail = Email;
                u.strPassword = password;

                entidad.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditarGrupo(int ID, string Nombre, DateTime FechaCreacion)
        {
            try
            {
                Grupos g = entidad.Grupos.First(gru => gru.intID == ID);

                g.strDescripcion = Nombre;
                
                g.dtmFechaCreacion = FechaCreacion;
                

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminarCliente(int ID) 
        {
            try
            {
                entidad.Connection.Open();

                using (TransactionScope scope = new TransactionScope()) 
                {
                    Clientes clienteEliminar = entidad.Clientes.First(c => c.intID == ID);

                    try
                    {
                        foreach (var item in clienteEliminar.Direcciones_Fiscales.ToList())
                        {
                            entidad.DeleteObject(item);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }

                    entidad.Clientes.DeleteObject(clienteEliminar);

                    entidad.SaveChanges();

                    scope.Complete();

                    entidad.AcceptAllChanges();

                    entidad.Connection.Close();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AgregarEmpresa( string RFC, string Razon, string Nombre, string Giro,
            string Tipo, string Calle, string Exterior, string Interior, string Colonia, int Pais,
            int Estado, string Municipio, string Poblacion, string CP, string Telefono1,
            string Telefono2, string Celular, string Email1, string Email2, string Web, string Logo,
            string Cedula)
        {
            try
            {
                Empresa e = new Empresa()
                {
                    
                    intIDConfiguracionRegional = 1,
                    strRazonSocial = Razon,
                    strNombreComercial = Nombre,
                    strGiro = Giro,
                    strRFC = RFC,
                    intID_CFD = 1,
                    strTelefono = Telefono1,
                    strTelefonoMovil = Celular,
                    strEmail = Email1,
                    strWebSite = Web,
                    strTelefono2 = Telefono2,
                    strLogo = Logo,
                    strCedula = Cedula,
                    strDirectorioXML = "",
                    strDirectorioPDF = ""                    
                };

                var id_Direccion_Fiscal = entidad.Direcciones_Fiscales.Count();
                id_Direccion_Fiscal += 1;

                Direcciones_Fiscales df = new Direcciones_Fiscales()
                {
                    intID = id_Direccion_Fiscal,
                    strTipoEntidad = "E",
                    
                    strCalle = Calle,
                    strColonia = Colonia, strNoExterior = Exterior,
                    strNoInterior = Interior,
                    intIDPais = Pais,
                    intIDEstado = Estado,
                    strMunicipio = Municipio,
                    strPoblacionLocalidad = Poblacion,
                    strCodigoPostal = CP,
                    chrStatus = "1",
                    
                };

                entidad.Empresa.AddObject(e);

                entidad.Direcciones_Fiscales.AddObject(df);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AgregarCertificados(/*int ID,*/ string CertificadoSelloDigital, string LlaveCertificado,
            string CSD, string Key, string Password, string Desde, string Hasta, string FechaSubida, int ID_Empresa)
        {
            try
            {
                Certificates c = new Certificates()
                {
                    //intID = ID,
                    strNumeroCertificadoSelloDigital = CertificadoSelloDigital,
                    strCertificadoSelloDigitalPath = CSD,
                    strLlaveCertificadoPath = Key,
                    strContraseñaSAT = Password,
                    dtmValidoDesde = DateTime.Parse(Desde),
                    dtmValidoHasta = DateTime.Parse(Hasta),
                    dtmFechaSubida = DateTime.Parse(FechaSubida),
                    intID_Empresa = ID_Empresa,
                    chrStatus = "A"
                };

                entidad.Certificates.AddObject(c);

                entidad.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AgregarFolios(/*int ID,*/ int ID_Certificado, string FolioInical, string FolioFinal,
            string NoAprobacion, string Serie, string AnioAprobacion, int IDEmpresa, string FolioActual)
        {
            try
            {
                Folios f = new Folios()
                {
                    //intID = ID,
                    intID_Certificate = ID_Certificado,
                    intFolio_Inicial = int.Parse(FolioInical),
                    intFolio_Final = int.Parse(FolioInical),
                    intNumero_Aprovacion = int.Parse(NoAprobacion),
                    strSerie = Serie,
                    strAño_Aprovacion = AnioAprobacion,
                    intID_Empresa = IDEmpresa,
                    chrStatus = "A",
                    intFolioActual = int.Parse(FolioActual)
                };

                entidad.Folios.AddObject(f);
                entidad.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}