using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data.Metadata.Edm;
using wpfEFac.Helpers;

namespace wpfEFac.Models
{
    public class Identifier
    {

        public static bool IsUsing = false;
        private static int LastValue;

        private Identifier()
        {

        }

        public static int GetID(string entityName, int idEmpresa, EntityObject entity, EntityEnum entitySet, eFacDBEntities ctx)
        {
            string nombre = ctx.GetEntitySetFullName(entity);

            if (nombre == null)
            {
                return -1;   
            }

            if (!CheckIfEntryExist(ctx, entityName, idEmpresa, entitySet))
            {
                if (!CheckNoIdExist(ctx,entityName, idEmpresa))
                {
                    return CreateID(ctx, entityName, idEmpresa);
                }
                else
	            {
                    return UpdateID(ctx, entityName, idEmpresa);
	            }
                    
            }
            else
            {
                return UpdateID(ctx, entityName, idEmpresa);
            }
        }

        private static bool CheckNoIdExist(eFacDBEntities entities, string entityName, int idEmpresa)
        {
            try
            {
                var count = (from id in entities.ConfiguracionContadorTabla
                         where id.intIdEmpresa == idEmpresa && id.strNombreTabla == entityName
                         select id).Count();

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static int UpdateID(eFacDBEntities entities, string entityName, int idEmpresa)
        {
            try
            {
                ConfiguracionContadorTabla conf = entities.ConfiguracionContadorTabla.SingleOrDefault(c => c.intIdEmpresa == idEmpresa && c.strNombreTabla == entityName);
                conf.intUltimoRegistro = conf.intUltimoRegistro + 1;
                entities.SaveChanges();
                return conf.intUltimoRegistro;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        private static int CreateID(eFacDBEntities entities, string entityName, int idEmpresa)
        {
            try 
	        {
                ConfiguracionContadorTabla conf = new ConfiguracionContadorTabla();
                conf.intIdEmpresa = idEmpresa;
                conf.strNombreTabla = entityName;
                conf.intUltimoRegistro = 1;
                conf.intId = GetIDConfiguracionTabla(entities.ConfiguracionContadorTabla, idEmpresa);

                entities.ConfiguracionContadorTabla.AddObject(conf);

                //entities.SaveChanges();

                return conf.intUltimoRegistro;

                //entities.ConfiguracionContadorTabla.AddObjec;
	        }
	        catch (Exception)
	        {
                return -1;
	        }
    
        }

        private static int GetIDConfiguracionTabla(ObjectSet<ConfiguracionContadorTabla> objectSet, int idEmpresa)
        {
            IsUsing = true;
            try
            {
                if (!IsUsing)
                {
                    var id = (from i in objectSet
                              where i.intIdEmpresa == idEmpresa
                              orderby i.intId
                              select i.intId).First();

                    LastValue = id++;
                    return LastValue;
                }
                else
                {
                    LastValue++;
                    return LastValue;
                }
               
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private static bool CheckIfEntryExist(eFacDBEntities entities, string entityName, int idEmpresa, EntityEnum entitySet)
        {
            int totalRegistros = GetCount(entities, entityName, idEmpresa, entitySet);

            if (totalRegistros > 0)
            {
                return true;
            }

            return false;
        }

        private static int GetCount(eFacDBEntities entities, string entityName, int idEmpresa, EntityEnum entitySet)
        {
            switch (entitySet)
            {
                case EntityEnum.UnidadMedida:
                    return entities.UnidadMedida.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Grupos:
                    return entities.Grupos.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Configuracion_Regional:
                    return entities.Configuracion_Regional.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Usuarios:
                    return entities.Usuarios.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.ConfiguracionEmail:
                    return entities.ConfiguracionEmail.Count(o => o.intID_Empresa == idEmpresa);
                //case EntityEnum.Traslados:
                //    return entities.Traslados.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Productos:
                    return entities.Productos.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Empresa:
                    return entities.Empresa.Count(o => o.intID == idEmpresa);
                case EntityEnum.ConfiguracionContadorTabla:
                    return entities.ConfiguracionContadorTabla.Count(o => o.intIdEmpresa == idEmpresa);
                case EntityEnum.Detalle_Factura:
                    return entities.Detalle_Factura.Count(o => o.Factura.intID_Empresa == idEmpresa);
                case EntityEnum.Folios:
                    return entities.Folios.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Clientes:
                    return entities.Clientes.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Certificates:
                    return entities.Certificates.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Factura:
                    return entities.Factura.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Direcciones_Fiscales:
                    return entities.Direcciones_Fiscales.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.Categorias:
                    return entities.Categorias.Count(o => o.intID_Empresa == idEmpresa);
                case EntityEnum.CFD:
                    return entities.CFD.Count(o => o.intId_Empresa == idEmpresa);
                case EntityEnum.Paises:
                    return entities.Paises.Count();
                case EntityEnum.Estado:
                    return entities.Estado.Count();
                default:
                    return 0;
            }
        }

    }
}
