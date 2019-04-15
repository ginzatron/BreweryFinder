using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using System.IO;


namespace SampleApi.Tests.DAL
{
    [TestClass]
    public class BreweryFinderDAOTests
    {
        protected string ConnectionString { get; } = "Server=.\\SQLEXPRESS;Initial Catalog = BreweryDB; Integrated Security = True;";

        /// <summary>
        /// holds the newly generated BreweryId
        /// </summary>
        protected int BreweryId { get; private set; }

        /// <summary>
        /// the transaction for each test
        /// </summary>
        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            //Begin the transaction
            transaction = new TransactionScope();

            //Get Sql script to create new database with 1 brewery for testing purposes
            string sql = File.ReadAllText("test-script.sql");

            //Open a connection and Execute the test script
            using (SqlConnection conn =new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                //if there is a row to Read
                if (reader.Read())
                {
                    this.BreweryId = Convert.ToInt32(reader["id"]);
                }
            }
        }

    }
}
