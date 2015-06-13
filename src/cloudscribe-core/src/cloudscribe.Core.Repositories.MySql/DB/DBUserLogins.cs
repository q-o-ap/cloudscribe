﻿// Author:					Joe Audette
// Created:					2014-08-10
// Last Modified:			2015-06-13
// 
//
// You must not remove this notice, or any other, from this software.

using cloudscribe.DbHelpers.MySql;
using Microsoft.Framework.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace cloudscribe.Core.Repositories.MySql
{

    internal class DBUserLogins
    {
        internal DBUserLogins(
            string dbReadConnectionString,
            string dbWriteConnectionString,
            ILoggerFactory loggerFactory)
        {
            logFactory = loggerFactory;
            readConnectionString = dbReadConnectionString;
            writeConnectionString = dbWriteConnectionString;
        }

        private ILoggerFactory logFactory;
        //private ILogger log;
        private string readConnectionString;
        private string writeConnectionString;


        public async Task<bool> Create(string loginProvider, string providerKey, string userId)
        {

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("INSERT INTO mp_UserLogins (");
            sqlCommand.Append("LoginProvider ,");
            sqlCommand.Append("ProviderKey, ");
            sqlCommand.Append("UserId ");
            sqlCommand.Append(") ");

            sqlCommand.Append("VALUES (");
            sqlCommand.Append("?LoginProvider, ");
            sqlCommand.Append("?ProviderKey, ");
            sqlCommand.Append("?UserId ");
            sqlCommand.Append(")");

            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[3];

            arParams[0] = new MySqlParameter("?LoginProvider", MySqlDbType.VarChar, 128);
            arParams[0].Value = loginProvider;

            arParams[1] = new MySqlParameter("?ProviderKey", MySqlDbType.VarChar, 128);
            arParams[1].Value = providerKey;

            arParams[2] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[2].Value = userId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > -1);

        }



        public async Task<bool> Delete(
            string loginProvider,
            string providerKey,
            string userId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserLogins ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("LoginProvider = ?LoginProvider AND ");
            sqlCommand.Append("ProviderKey = ?ProviderKey AND ");
            sqlCommand.Append("UserId = ?UserId ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[3];

            arParams[0] = new MySqlParameter("?LoginProvider", MySqlDbType.VarChar, 128);
            arParams[0].Value = loginProvider;

            arParams[1] = new MySqlParameter("?ProviderKey", MySqlDbType.VarChar, 128);
            arParams[1].Value = providerKey;

            arParams[2] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[2].Value = userId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public async Task<bool> DeleteByUser(string userId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserLogins ");
            sqlCommand.Append("WHERE ");

            sqlCommand.Append("UserId = ?UserId ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[1];

            arParams[0] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[0].Value = userId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);
            return (rowsAffected > 0);

        }

        public async Task<bool> DeleteBySite(Guid siteGuid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserLogins ");
            sqlCommand.Append("WHERE ");

            sqlCommand.Append("UserId IN (SELECT UserGuid FROM mp_Users WHERE SiteGuid = ?SiteGuid) ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[1];

            arParams[0] = new MySqlParameter("?SiteGuid", MySqlDbType.VarChar, 36);
            arParams[0].Value = siteGuid.ToString();

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);


        }

        public async Task<DbDataReader> Find(string loginProvider, string providerKey)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  * ");
            sqlCommand.Append("FROM	mp_UserLogins ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("LoginProvider = ?LoginProvider AND ");
            sqlCommand.Append("ProviderKey = ?ProviderKey ");

            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[2];

            arParams[0] = new MySqlParameter("?LoginProvider", MySqlDbType.VarChar, 128);
            arParams[0].Value = loginProvider;

            arParams[1] = new MySqlParameter("?ProviderKey", MySqlDbType.VarChar, 128);
            arParams[1].Value = providerKey;

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                sqlCommand.ToString(),
                arParams);

        }

        public async Task<DbDataReader> GetByUser(string userId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  * ");
            sqlCommand.Append("FROM	mp_UserLogins ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("UserId = ?UserId  ");

            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[1];

            arParams[0] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[0].Value = userId;

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                sqlCommand.ToString(),
                arParams);

        }

        
    }
}
