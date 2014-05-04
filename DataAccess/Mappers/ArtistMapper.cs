using DataAccess.Entities;
using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DataAccess.Mappers
{
    internal class ArtistMapper : ASqlMapper<ArtistEntity>
    {
        /// <summary>
        /// Instantiates a new ArtistMapper and initializes the connection string and entityMap
        /// (inherited from ASqlMapper)
        /// </summary>
        /// <param name="connectionString">The connection string for the database</param>
        internal ArtistMapper(string connectionString)
        {
            this.connectionString = connectionString;
            this.entityMap = new Dictionary<int, ArtistEntity>();
        }

        /// <summary>
        /// Creates a new ArtistEntity object with default Entity parameters, and inserts it into
        /// the database
        /// </summary>
        /// <param name="artistName">Name of Artist</param>
        /// <returns>ArtistEntity with Id and LastModified set by database</returns>
        internal ArtistEntity Create(string artistName)
        {
            ArtistEntity artist = new ArtistEntity(artistName, 0, DateTime.MinValue, false);

            insert(artist);

            return artist;
        }

        /// <summary>
        /// Reads all ArtistEntities by selecting all from the database
        /// </summary>
        /// <returns>A list of all ArtistEntities in the database</returns>
        internal List<ArtistEntity> ReadAll()
        {
            List<ArtistEntity> artists = selectAll();

            // Finalize before returning!

            return artists;
        }

        /// <summary>
        /// Saves changes to specified ArtistEntity in the database
        /// </summary>
        /// <param name="artist">The AristEntity to update</param>
        internal void Update(ArtistEntity artist)
        {
            update(artist);
            // also update other mappers!
        }

        /// <summary>
        /// Sets artist.Deleted to true and updates the ArtistEntity in the database
        /// </summary>
        /// <param name="artist">The ArtistEntity to update</param>
        internal void Delete(ArtistEntity artist)
        {
            artist.Deleted = true;

            Update(artist);
        }

        /// <summary>
        /// Name of Stored Procedure to insert ArtistEntities into database
        /// </summary>
        protected override string insertProcedureName
        {
            get { return StoredProcedures.INSERT_ARTIST; }
        }

        /// <summary>
        /// Name of Stored Procedure to select all ArtistEntities into database
        /// </summary>
        protected override string selectAllProcedureName
        {
            get { return StoredProcedures.SELECT_ALL_ARTISTS; }
        }

        /// <summary>
        /// Name of Stored Procedure to update ArtistEntities in database
        /// </summary>
        protected override string updateProcedureName
        {
            get { return StoredProcedures.UPDATE_ARTIST; }
        }

        /// <summary>
        /// Creates a new ArtistEntity using the data in the SqlDataReader reader
        /// </summary>
        /// <param name="reader">A reader containing ArtistName, ArtistId, LastModified and Deleted
        /// of an ArtistEntity</param>
        /// <returns>The created ArtistEntity</returns>
        protected override ArtistEntity entityFromReader(SqlDataReader reader)
        {
            string artistName = (string)reader["ArtistName"];
            int id = (int)reader["ArtistId"];
            DateTime lastModified = (DateTime)reader["LastModified"];
            bool deleted = (bool)reader["Deleted"];

            return new ArtistEntity(artistName, id, lastModified, deleted);
        }

        /// <summary>
        /// Takes an ArtistEntity and a SqlParameterCollection and adds SqlParameters for insert 
        /// stored procedure to the SqlParameterCollection
        /// </summary>
        /// <param name="entity">The ArtistEntity to extract parameter data from</param>
        /// <param name="parameters">The SqlParameterCollection to add the finished SqlParameters
        /// to</param>
        protected override void addInsertParameters(ArtistEntity entity, SqlParameterCollection parameters)
        {
            SqlParameter parameter = new SqlParameter("@ArtistName", entity.ArtistName);
            parameters.Add(parameter);

            parameter = new SqlParameter("@Id", entity.Id);
            parameter.Direction = System.Data.ParameterDirection.Output;
            parameters.Add(parameter);

            parameter = new SqlParameter("@LastModified", entity.LastModified);
            parameter.Direction = System.Data.ParameterDirection.Output;
            parameters.Add(parameter);
        }

        /// <summary>
        /// Takes an ArtistEntity and a SqlParameterCollection and adds SqlParameters for update
        /// stored procedure to the SqlParameterCollection
        /// </summary>
        /// <param name="entity">The ArtistEntity to extract parameter data from</param>
        /// <param name="parameters">The SqlParameterCollection to add the finished SqlParameters
        /// to</param>
        protected override void addUpdateParameters(ArtistEntity entity, SqlParameterCollection parameters)
        {
            SqlParameter parameter = new SqlParameter("@ArtistName", entity.ArtistName);
            parameters.Add(parameter);

            parameter = new SqlParameter("@Id", entity.Id);
            parameters.Add(parameter);

            parameter = new SqlParameter("@LastModified", entity.LastModified);
            parameter.Direction = System.Data.ParameterDirection.Output;
            parameters.Add(parameter);

            parameter = new SqlParameter("@Deleted", entity.Deleted);
            parameters.Add(parameter);
        }
    }
}
