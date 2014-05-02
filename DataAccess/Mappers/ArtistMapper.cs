using DataAccess.Entities;
using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DataAccess.Mappers
{
    internal class ArtistMapper : ASqlMapper<ArtistEntity>
    {
        internal ArtistMapper(string connectionString)
        {
            this.connectionString = connectionString;
            this.entityMap = new Dictionary<int, ArtistEntity>();
        }

        protected override string insertProcedureName
        {
            get { return StoredProcedures.INSERT_ARTIST; }
        }

        protected override string selectAllProcedureName
        {
            get { return StoredProcedures.SELECT_ALL_ARTISTS; }
        }

        protected override string updateProcedureName
        {
            get { return StoredProcedures.UPDATE_ARTIST; }
        }

        public ArtistEntity Create(string artistName)
        {
            ArtistEntity artist = new ArtistEntity(artistName, 0, DateTime.MinValue, false);

            insert(artist);

            return artist;
        }

        public List<ArtistEntity> ReadAll()
        {
            List<ArtistEntity> artists = selectAll();

            // Finalize before returning!

            return artists;
        }

        public void Update(ArtistEntity artist)
        {
            update(artist);
            // also update other mappers!
        }

        public void Delete(ArtistEntity artist)
        {
            artist.Deleted = true;

            Update(artist);
        }

        protected override ArtistEntity entityFromReader(SqlDataReader reader)
        {
            string artistName = (string)reader["ArtistName"];
            int id = (int)reader["ArtistId"];
            DateTime lastModified = (DateTime)reader["LastModified"];
            bool deleted = (bool)reader["Deleted"];

            return new ArtistEntity(artistName, id, lastModified, deleted);
        }

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
