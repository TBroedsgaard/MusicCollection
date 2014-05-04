using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DataAccess.Mappers
{
    internal abstract class ASqlMapper<TEntity> where TEntity : Entity
    {
        protected Dictionary<int, TEntity> entityMap;
        protected string connectionString;

        protected abstract string insertProcedureName { get; }
        protected abstract string selectAllProcedureName { get; }
        protected abstract string updateProcedureName { get; }

        protected abstract TEntity entityFromReader(SqlDataReader reader);

        protected abstract void addInsertParameters(TEntity entity, 
            SqlParameterCollection parameters);
        protected abstract void addUpdateParameters(TEntity entity,
            SqlParameterCollection parameters);

        /// <summary>
        /// Prepares a connection to database using connection string, creates a new StoredProcedure
        /// command and adds parameters using the EntityMapper's implementation of 
        /// addInsertParameters. Opens the connection and executes non-query command, and 
        /// overwrites Id and LastModified fields of Entity with values set in output parameters.
        /// </summary>
        /// <param name="entity">The Entity to insert in the database</param>
        /// <returns>The Entity that has been inserted with overwritten values of Id and 
        /// LastModified</returns>
        protected TEntity insert(TEntity entity)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = insertProcedureName;
                    addInsertParameters(entity, cmd.Parameters);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    entity.Id = (int)cmd.Parameters["@Id"].Value;
                    entity.LastModified = (DateTime)cmd.Parameters["@LastModified"].Value;

                    entityMap.Add(entity.Id, entity);
                }
            }

            return entity;
        }

        /// <summary>
        /// Prepares a connection to database using connection string, creates a new StoredProcedure
        /// command. Opens the connection and executes reader, and for each row in reader passes
        /// data to entityFromReader to create new instances of Entity. Each new Entity is loaded
        /// into the entityMap, and if the Entity is not deleted it is added to the list of entities
        /// to return.
        /// </summary>
        /// <param name="entity">The Entity to insert in the database</param>
        /// <returns>A list of all Entities that have not been deleted</returns>
        protected List<TEntity> selectAll()
        {
            List<TEntity> entities = new List<TEntity>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = selectAllProcedureName;

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TEntity entity = entityFromReader(reader);

                            if (!entityMap.ContainsKey(entity.Id))
                            {
                                entityMap.Add(entity.Id, entity);
                            }
                            else
                            {
                                // NOTE: This means data in cache is not overwritten!
                                // Also means you can't discard changes!
                                // Either provide the Mapper class with Flush(entity) and FlushAll()
                                // metods, or provide a 'overwrite' bool to Read statements, which
                                // if true will overwrite what is in cache, so that changes may
                                // be discarded
                                entity = entityMap[entity.Id];
                            }

                            if (entity.Deleted == false)
                            {
                                entities.Add(entity);
                            }
                        }
                    }
                }
            }

            return entities;
        }

        /// <summary>
        /// Prepares a connection to database using connection string, creates a new StoredProcedure
        /// command and adds parameters using the EntityMapper's implementation of 
        /// addUpdateParameters. Opens the connection and executes non-query command, and 
        /// overwrites LastModified field of Entity with value set in output parameter.
        /// </summary>
        /// <param name="entity">The Entity to update in the database</param>
        /// <returns>The Entity that has been updated with overwritten value of LastModified
        /// </returns>
        protected TEntity update(TEntity entity)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = updateProcedureName;
                    addUpdateParameters(entity, cmd.Parameters);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    entity.LastModified = (DateTime)cmd.Parameters["@LastModified"].Value;
                }
            }

            return entity;
        }
    }
}
