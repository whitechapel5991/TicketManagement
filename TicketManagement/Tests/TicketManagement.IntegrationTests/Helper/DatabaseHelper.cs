// ****************************************************************************
// <copyright file="DatabaseHelper.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TicketManagement.IntegrationTests.Helper
{
    internal class DatabaseHelper
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString;
        private static readonly string SnapshotName = "D177A4D5_6947_461A_B1A1_3E5FD51036E9";
        private static readonly string MainDbName = "TicketManagementTest";

        internal static bool SnapshotExists()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"USE master; SELECT * FROM sys.databases WHERE name = '{SnapshotName}'and source_database_id is not null";
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
        }

        internal static void CreateSnapshot()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var getLogicalNameSql = "SELECT name FROM sys.master_files WHERE database_id = DB_ID() AND type = 0";
                string logicalDbName;
                using (var logicalNameCommand = new SqlCommand(getLogicalNameSql, connection))
                {
                    logicalDbName = (string)logicalNameCommand.ExecuteScalar();
                }

                var sql = $"USE master; CREATE DATABASE {SnapshotName} ON (NAME = {logicalDbName}, FILENAME ='{GetSnapshotPath()}')"
                          + $" AS SNAPSHOT OF {MainDbName};";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        internal static string GetSnapshotPath()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var getBackupDirSql =
                    @"EXEC  master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer',N'BackupDirectory'";

                using (var logicalNameCommand = new SqlCommand(getBackupDirSql, connection))
                {
                    var reader = logicalNameCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        return $"{reader.GetString(1)}\\{SnapshotName}.ss";
                    }
                }
            }

            throw new Exception("Failed to get snaphot path");
        }

        internal static void RestoreFromSnapshot()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                ExecuteNonQuery(connection, $"USE master; ALTER DATABASE {MainDbName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
                ExecuteNonQuery(connection, $"USE master; RESTORE DATABASE {MainDbName} FROM DATABASE_SNAPSHOT = '{SnapshotName}' WITH REPLACE,RECOVERY");
                ExecuteNonQuery(connection, $"USE master; ALTER DATABASE {MainDbName} SET MULTI_USER WITH ROLLBACK IMMEDIATE;");

                connection.Close();
            }
        }

        internal static void DropSnapshot()
        {
            ExecuteNonQuery($"USE master; DROP DATABASE {SnapshotName}");
        }

        internal static void ExecuteNonQuery(SqlConnection connection, string sql)
        {
            using (var sqlCommand = new SqlCommand(sql, connection))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        internal static void ExecuteNonQuery(string sql)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
