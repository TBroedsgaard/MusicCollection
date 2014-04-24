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
