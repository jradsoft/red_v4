using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;

namespace wpfEFac.Helpers
{
    public static class ExtensionsMethods
    {

        /// <summary>
        /// Extension method is use to get the name that belongs to an entity.
        /// </summary>
        /// <param name="context">
        /// Extension method.
        /// </param>
        /// <param name="entity">
        /// Entity that you wish to now the name inside the context if exist.
        /// </param>
        /// <returns>
        /// Returns the entity name if exist in the given context.
        /// otherwise returns null.
        /// </returns>
        public static string GetEntitySetFullName(this ObjectContext context, EntityObject entity)
        {
            // If the EntityKey exists, simply get the Entity Set name from the key
            if (entity.EntityKey != null)
            {
                return entity.EntityKey.EntitySetName;
            }
            else
            {
                string entityTypeName = entity.GetType().Name;
                var container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);

                string entitySetName;

                try
                {
                    // returns the first that founds in the sequence
                    entitySetName = (from meta in container.BaseEntitySets
                                            where meta.ElementType.Name == entityTypeName
                                            select meta.Name).First();
                }
                catch (Exception)
                {
                    // If theres an exception returns null.
                    entitySetName = null;
                }

                return entitySetName;
            }
        }

        public static List<System.Data.Metadata.Edm.EntitySetBase> GetEntitySet(this ObjectContext context, EntityObject entity)
        {
            var container = context.MetadataWorkspace.
                GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);

            context.MetadataWorkspace.LoadFromAssembly(entity.GetType().Assembly);
            var ospaceType = context.MetadataWorkspace.GetItem<EntityType>(entity.GetType().FullName, DataSpace.OSpace);
            var entityType = context.MetadataWorkspace.GetEdmSpaceType(ospaceType) as EntityType;

            while (entityType.BaseType != null)
            {
                entityType = entityType.BaseType as EntityType;
            }

            var posibleEntitySets = from es in container.BaseEntitySets
                                    where es.ElementType == entityType
                                    select es;

            return posibleEntitySets.ToList();

            //return container.BaseEntitySets.Single(meta => meta.ElementType.Name == entity.GetType().Name).ElementType.GetCollectionType();
        }
    }
}
