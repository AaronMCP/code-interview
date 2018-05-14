using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Sybase.Data.AseClient;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    #region Data Access Layer for GCRIS2.0
    /// <summary>
    /// This is the data access layer class for Kodak GCRIS2.0
    /// It supports oracle,SQL,Sybase
    /// </summary>
    public class RisDAL : IDisposable
    {
        #region Private variables
        #region Private variable For SQL
        protected DbProviderFactory objFactory = null;
        protected DbConnection objConnection;
        protected DbCommand objCommand;
        protected DbDataAdapter adapter;
        private DbParameterCollection m_oSelectParams;
        private DbParameter objParam;
        #endregion
        #region Private variable For Oracle
        protected OracleConnection oracleConnection = null;
        protected OracleCommand oracleCommand;
        private OracleDataAdapter oracleAdapter;
        private OracleParameterCollection objOracleParamCollection;
        private OracleParameter oracleParam;
        #endregion
        #region Private variable for Sybase
        protected AseConnection objAseConnection = null;
        protected AseCommand objAseCommand = null;
        private AseDataAdapter aseDataAdapter = null;
        private AseParameterCollection aseSelectParams;
        private AseParameter aseParam;
        #endregion
        #endregion

        #region Property for Database type read from Configuration File
        protected string m_szDatabaseType = null;

        public string DatabaseType
        {
            get
            {
                return m_szDatabaseType;
            }

        }
        #endregion

        #region Property for Driver Class Name
        protected string m_szDriverClassName = null;

        public string DriverClassName
        {
            get
            {
                return m_szDriverClassName;
            }
        }
        #endregion

        #region Property for Parameter Collection
        private KodakDALParameterCollection m_oParams;
        public KodakDALParameterCollection Parameters
        {
            get
            { return m_oParams; }
        }
        #endregion

        #region Property for Connection String
        private string m_szConnectionString = null;
        public string ConnectionString
        {
            get
            {
                return m_szConnectionString;
            }

        }
        #endregion

        #region Constructor
        public RisDAL()
        {
            try
            {
                m_oParams = new KodakDALParameterCollection(this);
                m_szDatabaseType = ConfigurationManager.AppSettings["DatabaseType"];
                m_szDriverClassName = ConfigurationManager.AppSettings["DriverClassName"];

                m_szConnectionString = ConstructConnString();

                if (m_szDatabaseType == "Sybase")
                {

                    objAseConnection = new AseConnection();
                    objAseConnection.ConnectionString = m_szConnectionString.ToString();
                    objAseCommand = new AseCommand();
                    objAseCommand.Connection = objAseConnection;
                    aseSelectParams = objAseCommand.Parameters;
                    aseParam = objAseCommand.CreateParameter();


                }
                else if (m_szDatabaseType == "Oracle")
                {

                    oracleConnection = new OracleConnection();
                    oracleConnection.ConnectionString = m_szConnectionString.ToString();
                    oracleCommand = new OracleCommand();
                    oracleConnection.Open();
                    oracleCommand.Connection = oracleConnection;
                    oracleCommand.CommandType = CommandType.Text;
                    oracleCommand.CommandText = "alter session set nls_comp=ANSI";
                    oracleCommand.ExecuteNonQuery();
                    oracleCommand.CommandText = "alter session set nls_sort=GENERIC_BASELETTER";
                    oracleCommand.ExecuteNonQuery();
                    oracleCommand.CommandText = "";
                    objOracleParamCollection = oracleCommand.Parameters;
                    oracleParam = oracleCommand.CreateParameter();

                }
                else
                {
                    string szconnectionProv = ConfigurationManager.AppSettings["Provider"];
                    // Create the DbProviderFactory
                    if (szconnectionProv != null)
                    {

                        objFactory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["Provider"].ToString());
                        objConnection = objFactory.CreateConnection();
                        objCommand = objFactory.CreateCommand();

                        objConnection.ConnectionString = m_szConnectionString.ToString();
                        objCommand.Connection = objConnection;
                        m_oSelectParams = objCommand.Parameters;
                        objParam = objCommand.CreateParameter();

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    HandleExceptions(ex);
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Database error");
                }
            }

        }

        public RisDAL(string connString)
        {
            try
            {
                m_oParams = new KodakDALParameterCollection(this);
                m_szDatabaseType = ConfigurationManager.AppSettings["DatabaseType"];
                m_szDriverClassName = ConfigurationManager.AppSettings["DriverClassName"];


                MyCryptography crytoGraphy = new MyCryptography("GCRIS2-20061025");
                m_szConnectionString = connString;// connStringcrytoGraphy.DeEncrypt(connString);

                if (m_szDatabaseType == "Sybase")
                {

                    objAseConnection = new AseConnection();
                    objAseConnection.ConnectionString = m_szConnectionString.ToString();
                    objAseCommand = new AseCommand();
                    objAseCommand.Connection = objAseConnection;
                    aseSelectParams = objAseCommand.Parameters;
                    aseParam = objAseCommand.CreateParameter();


                }
                else if (m_szDatabaseType == "Oracle")
                {

                    oracleConnection = new OracleConnection();
                    oracleConnection.ConnectionString = m_szConnectionString.ToString();
                    oracleCommand = new OracleCommand();
                    oracleConnection.Open();
                    oracleCommand.Connection = oracleConnection;
                    oracleCommand.CommandType = CommandType.Text;
                    oracleCommand.CommandText = "alter session set nls_comp=ANSI";
                    oracleCommand.ExecuteNonQuery();
                    oracleCommand.CommandText = "alter session set nls_sort=GENERIC_BASELETTER";
                    oracleCommand.ExecuteNonQuery();
                    oracleCommand.CommandText = "";
                    objOracleParamCollection = oracleCommand.Parameters;
                    oracleParam = oracleCommand.CreateParameter();

                }
                else
                {
                    string szconnectionProv = ConfigurationManager.AppSettings["Provider"];
                    // Create the DbProviderFactory
                    if (szconnectionProv != null)
                    {

                        objFactory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["Provider"].ToString());
                        objConnection = objFactory.CreateConnection();
                        objCommand = objFactory.CreateCommand();

                        objConnection.ConnectionString = m_szConnectionString.ToString();
                        objCommand.Connection = objConnection;
                        m_oSelectParams = objCommand.Parameters;
                        objParam = objCommand.CreateParameter();

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    HandleExceptions(ex);
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Database error");
                }
            }

        }
        #endregion

        #region Constructor1
        public RisDAL(int nConn2)
        {
            try
            {
                m_oParams = new KodakDALParameterCollection(this);
                m_szDatabaseType = ConfigurationManager.AppSettings["DatabaseType"];
                m_szDriverClassName = ConfigurationManager.AppSettings["DriverClassName"];

                m_szConnectionString = ConstructConnString2();

                if (m_szDatabaseType == "Sybase")
                {

                    objAseConnection = new AseConnection();
                    objAseConnection.ConnectionString = m_szConnectionString.ToString();
                    objAseCommand = new AseCommand();
                    objAseCommand.Connection = objAseConnection;
                    aseSelectParams = objAseCommand.Parameters;
                    aseParam = objAseCommand.CreateParameter();


                }
                else if (m_szDatabaseType == "Oracle")
                {

                    oracleConnection = new OracleConnection();
                    oracleConnection.ConnectionString = m_szConnectionString.ToString();
                    oracleCommand = new OracleCommand();
                    oracleConnection.Open();
                    oracleCommand.Connection = oracleConnection;
                    oracleCommand.CommandType = CommandType.Text;
                    oracleCommand.CommandText = "alter session set nls_comp=ANSI";
                    oracleCommand.ExecuteNonQuery();
                    oracleCommand.CommandText = "alter session set nls_sort=GENERIC_BASELETTER";
                    oracleCommand.ExecuteNonQuery();
                    oracleCommand.CommandText = "";
                    objOracleParamCollection = oracleCommand.Parameters;
                    oracleParam = oracleCommand.CreateParameter();

                }
                else
                {
                    string szconnectionProv = ConfigurationManager.AppSettings["Provider"];
                    // Create the DbProviderFactory
                    if (szconnectionProv != null)
                    {

                        objFactory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["Provider"].ToString());
                        objConnection = objFactory.CreateConnection();
                        objCommand = objFactory.CreateCommand();

                        objConnection.ConnectionString = m_szConnectionString.ToString();
                        objCommand.Connection = objConnection;
                        m_oSelectParams = objCommand.Parameters;
                        objParam = objCommand.CreateParameter();

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    HandleExceptions(ex);
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Database error");
                }
            }

        }
        #endregion


        //Construct the string of connection
        public string ConstructConnString()
        {
            string strConn = "";
            try
            {

                strConn = ConfigurationManager.AppSettings["Connectionstring"];
                MyCryptography crytoGraphy = new MyCryptography("GCRIS2-20061025");
                strConn = crytoGraphy.DeEncrypt(strConn);

            }
            catch (Exception e)
            {
            }
            return strConn;
        }
        //Construct the string of connection
        public string ConstructConnString2()
        {
            string strConn = "";
            try
            {

                strConn = ConfigurationManager.AppSettings["Connectionstring2"];
                MyCryptography crytoGraphy = new MyCryptography("GCRIS2-20061025");
                strConn = crytoGraphy.DeEncrypt(strConn);

            }
            catch (Exception e)
            {
            }
            return strConn;
        }

        #region CreateDBaseConnection
        public IDbConnection CreateDBaseConnection()
        {
            if (m_szDatabaseType == "Sybase")
            {
                objAseConnection = new AseConnection();
                objAseConnection.ConnectionString = ConnectionString;
                return (IDbConnection)objAseConnection;
            }
            else if (m_szDatabaseType == "Oracle")
            {
                oracleConnection = new OracleConnection();
                oracleConnection.ConnectionString = ConnectionString;
                return (IDbConnection)oracleConnection;
            }
            else
            {
                objConnection = objFactory.CreateConnection();
                objConnection.ConnectionString = ConnectionString;
                return (IDbConnection)objConnection;
            }

        }
        #endregion

        #region CreateDBaseCommand
        public IDbCommand CreateDBaseCommand(IDbConnection objDbConnection)
        {
            if (m_szDatabaseType == "Sybase")
            {
                objAseCommand = new AseCommand();
                objAseCommand.Connection = (AseConnection)objDbConnection;
                return (IDbCommand)objAseCommand;
            }
            else if (m_szDatabaseType == "Oracle")
            {
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = (OracleConnection)oracleConnection;

                return (IDbCommand)oracleCommand;
            }
            else
            {
                objCommand = objFactory.CreateCommand();
                objCommand.Connection = (DbConnection)objDbConnection;
                return (IDbCommand)objCommand;
            }
        }
        #endregion

        #region CreateDBaseDataAdapter
        public IDbDataAdapter CreateDBaseDataAdapter()
        {
            if (m_szDatabaseType == "Sybase")
            {
                aseDataAdapter = new AseDataAdapter();
                return (IDbDataAdapter)aseDataAdapter;
            }
            else if (m_szDatabaseType == "Oracle")
            {
                oracleAdapter = new OracleDataAdapter();
                return (IDbDataAdapter)oracleAdapter;
            }
            else
            {
                adapter = objFactory.CreateDataAdapter();
                return (IDbDataAdapter)adapter;
            }
        }
        #endregion

        #region CreateDBaseParameter
        public IDbDataParameter CreateDBaseParameter()
        {
            if (m_szDatabaseType == "Sybase")
            {
                aseParam = new AseParameter();
                return (IDbDataParameter)aseParam;
            }
            else if (m_szDatabaseType == "Oracle")
            {
                oracleParam = new OracleParameter();
                return (IDbDataParameter)oracleParam;
            }
            else
            {
                objParam = objFactory.CreateParameter();
                return (IDbDataParameter)objParam;
            }
        }
        #endregion

        #region ExecuteQuery
        #region Execute Query --- input --string szQuery, ConnectionState connectionstate,System.Data.DataSet dsDataset, string szTable, System.Data.CommandType eCommandType
        /// <summary>
        /// Executes a query with the given input parameter
        /// </summary>
        /// <param name="szQuery">query to execute</param>
        /// <param name="connectionstate">connection state</param>
        /// <param name="dsDataset">Dataset to add results to.</param>
        /// <param name="szTable">Table within dataset.  Table is created if it doesn't exist.</param>
        /// <param name="eCommandType">CommandType.Text for Select queryies, CommandType.StoredProcedures for stored procedures.</param>
        public void ExecuteQuery(string szQuery, ConnectionState connectionstate, System.Data.DataSet dsDataset, string szTable, System.Data.CommandType eCommandType)
        {
            OnExecuteQuery(szQuery, connectionstate, dsDataset, szTable, eCommandType);
        }
        #endregion

        #region ExecuteQuery -- input -- string szQuery, ConnectionState connectionstate, System.Data.DataTable dtbTable, System.Data.CommandType eCommandType
        /// <summary>
        /// Executes a query with the given input parameter
        /// </summary>
        /// <param name="szQuery">query to execute</param>
        /// <param name="connectionstate">connection state</param>
        /// <param name="dtbTable">Table for results.</param>
        /// <param name="eCommandType">CommandType.Text for Select queryies, CommandType.StoredProcedures for stored procedures.</param>
        public void ExecuteQuery(string szQuery, ConnectionState connectionstate, System.Data.DataTable dtbTable, System.Data.CommandType eCommandType)
        {
            OnExecuteQuery(szQuery, connectionstate, dtbTable, eCommandType);

        }
        #endregion

        #region Execute Query -- input -- string szQuery, System.Data.DataSet dsDataset, string szTable, System.Data.CommandType eCommandType
        /// <summary>
        /// Executes a select query and appends the results into the given table in a dataset.
        /// </summary>
        /// <param name="szQuery">query to execute</param>
        /// <param name="dsDataset">Dataset to add results to.</param>
        /// <param name="szTable">Table within dataset.  Table is created if it doesn't exist.</param>
        /// <param name="eCommandType">CommandType.Text for Select queryies, CommandType.StoredProcedures for stored procedures.</param>
        public void ExecuteQuery(string szQuery, System.Data.DataSet dsDataset, string szTable, System.Data.CommandType eCommandType)
        {
            OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dsDataset, szTable, eCommandType);
        }
        #endregion

        #region Execute Query -- input -- string szQuery, System.Data.DataTable dtbTable, System.Data.CommandType eCommandType
        /// <summary>
        /// Executes a query and appends the results into the DataTable.
        /// </summary>
        /// <param name="szQuery">query to execute</param>
        /// <param name="dtbTable">Table for results.</param>
        /// <param name="eCommandType">CommandType.Text for Select queryies, CommandType.StoredProcedures for stored procedures.</param>
        public void ExecuteQuery(string szQuery, System.Data.DataTable dtbTable, System.Data.CommandType eCommandType)
        {
            OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dtbTable, eCommandType);
        }
        #endregion

        #region Execute Query -- input -- string szQuery, System.Data.DataSet dsDataset, string szTable
        /// <summary>
        /// Executes a select query and appends the results into the given table in a dataset.
        /// </summary>
        /// <param name="szQuery">Sql select query to execute</param>
        /// <param name="dsDataset">Dataset to add results to.</param>
        /// <param name="szTable">Table within dataset.  Table is created if it doesn't exist.</param>
        public void ExecuteQuery(string szQuery, System.Data.DataSet dsDataset, string szTable)
        {
            OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dsDataset, szTable, CommandType.Text);
        }
        #endregion

        #region Execute Query -- input -- string szQuery,ConnectionState connectionstate, System.Data.DataSet dsDataset, string szTable
        /// <summary>
        /// Executes a select query and appends the results into the given table in a dataset.
        /// </summary>
        /// <param name="szQuery">select query to execute</param>
        ///<param name="connectionstate">connection state</param>
        /// <param name="dsDataset">Dataset to add results to.</param>
        /// <param name="szTable">Table within dataset.  Table is created if it doesn't exist.</param>
        public void ExecuteQuery(string szQuery, ConnectionState connectionstate, System.Data.DataSet dsDataset, string szTable)
        {
            OnExecuteQuery(szQuery, connectionstate, dsDataset, szTable, CommandType.Text);
        }
        #endregion

        #region Execute Query -- input -- string szQuery, System.Data.DataTable dtbTable
        /// <summary>
        /// Executes a select query and appends the results into the given DataTable.
        /// </summary>
        /// <param name="szQuery">select query to execute.</param>
        /// <param name="dtbTable">Table for results.</param>
        public void ExecuteQuery(string szQuery, System.Data.DataTable dtbTable)
        {
            OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dtbTable, CommandType.Text);
        }
        #endregion

        #region Execute Query -- input -- string szQuery,ConnectionState connectionstate, System.Data.DataTable dtbTable
        /// <summary>
        /// Executes a select query and appends the results into the given DataTable.
        /// </summary>
        /// <param name="szQuery">select query to execute.</param>
        /// <param name="connectionstate">connection state</param>
        /// <param name="dtbTable">Table for results.</param>
        public void ExecuteQuery(string szQuery, ConnectionState connectionstate, System.Data.DataTable dtbTable)
        {
            OnExecuteQuery(szQuery, connectionstate, dtbTable, CommandType.Text);
        }
        #endregion

        #region Execute Query -- input -- string szQuery
        /// <summary>
        /// Executes a select query and appends the results into the given DataTable.  
        /// </summary>
        /// <param name="szQuery">select query to execute.</param>
        /// <returns>New DataTable with data.</returns>
        public System.Data.DataTable ExecuteQuery(string szQuery)
        {
            System.Data.DataTable dtbTable = new System.Data.DataTable();
            OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dtbTable, CommandType.Text);
            return dtbTable;
        }
        #endregion

        #region Execute Query -- input -- string szQuery,ConnectionState connectionstate
        /// <summary>
        /// Executes a select query and appends the results into the given DataTable.  
        /// </summary>
        /// <param name="szQuery">select query to execute.</param>
        /// <returns>New DataTable with data.</returns>
        public System.Data.DataTable ExecuteQuery(string szQuery, ConnectionState connectionstate)
        {
            System.Data.DataTable dtbTable = new System.Data.DataTable();
            OnExecuteQuery(szQuery, connectionstate, dtbTable, CommandType.Text);
            return dtbTable;
        }
        #endregion

        #region Execute Query -- string szQuery, ConnectionState connectionstate,System.Data.DataSet dsDataset, string szTable
        /// <summary>
        /// Executes a stored procedure and appends the results into the DataSet with the given
        /// table name.  This method also supports stored procedures that return multiple result sets 
        /// (have multiple select queries).  With multiple result sets the first table is names szTable,
        /// and each additional result is saved in the dataset with a number after the table name.  For
        /// example if szTable is "Role" then the first select is stored in "Role" and the second
        /// select is stored in "Role1".
        /// </summary>
        /// <param name="szQuery">Procedure to execute</param>
        /// <param name="dsDataset">Dataset to place data in.</param>
        /// <param name="szTable">Datatable name.</param>
        public void ExecuteQuerySP(string szQuery, ConnectionState connectionstate, System.Data.DataSet dsDataset, string szTable)
        {
            OnExecuteQuery(szQuery, connectionstate, dsDataset, szTable, CommandType.StoredProcedure);
        }
        #endregion

        #region Execute Query -- input -- string szQuery, System.Data.DataSet dsDataset, string szTable
        /// <summary>
        /// Executes a stored procedure and appends the results into the DataSet with the given
        /// table name.  This method also supports stored procedures that return multiple result sets 
        /// (have multiple select queries).  With multiple result sets the first table is names szTable,
        /// and each additional result is saved in the dataset with a number after the table name.  For
        /// example if szTable is "Role" then the first select is stored in "customer" and the second
        /// select is stored in "Role1".
        /// </summary>
        /// <param name="szQuery">Procedure to execute</param>
        /// <param name="dsDataset">Dataset to place data in.</param>
        /// <param name="szTable">Datatable name.</param>
        public void ExecuteQuerySP(string szQuery, System.Data.DataSet dsDataset, string szTable)
        {
            OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dsDataset, szTable, CommandType.StoredProcedure);
        }
        #endregion

        #region Execute Query -- input -- string szQuery,ConnectionState connectionstate, System.Data.DataTable dtbTable
        /// <summary>
        /// Executes a stored procedure and appends the results into the given DataTable.  
        /// </summary>
        /// <param name="szQuery">Procedure to execute</param>
        /// <param name="dtbTable">Table to populate</param>
        public void ExecuteQuerySP(string szQuery, ConnectionState connectionstate, System.Data.DataTable dtbTable)
        {
            OnExecuteQuery(szQuery, connectionstate, dtbTable, CommandType.StoredProcedure);
        }
        #endregion

        #region Execute Query SP -- input -- string szQuery, System.Data.DataTable dtbTable
        /// <summary>
        /// Executes a stored procedure and appends the results into the given DataTable.  
        /// </summary>
        /// <param name="szQuery">Procedure to execute</param>
        /// <param name="dtbTable">Table to populate</param>
        public void ExecuteQuerySP(string szQuery, System.Data.DataTable dtbTable)
        {
            if (m_szDatabaseType == "Oracle")
            {

                OnExecuteOracleSP(szQuery, ConnectionState.CloseOnExit, dtbTable, CommandType.StoredProcedure);
            }
            else
            {
                OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dtbTable, CommandType.StoredProcedure);
            }

        }
        #endregion

        #region Execute Query SP -- input -- string szQuery
        /// <summary>
        /// Execute a stored procedure and return the results in a datatable.
        /// </summary>
        /// <param name="szQuery">Procedure to execute.</param>
        public System.Data.DataTable ExecuteQuerySP(string szQuery)
        {
            System.Data.DataTable dtbTable = new System.Data.DataTable();
            if (m_szDatabaseType == "Oracle")
            {
                OnExecuteOracleSP(szQuery, ConnectionState.CloseOnExit, dtbTable, CommandType.StoredProcedure);

            }
            else
            {

                OnExecuteQuery(szQuery, ConnectionState.CloseOnExit, dtbTable, CommandType.StoredProcedure);
            }
            return dtbTable;
        }
        #endregion


        #region Execute Procedure for DataTable---Oracle
        /// <summary>
        /// Call oracle procedure and return table
        /// </summary>
        /// <param name="szQuery"></param>
        /// <param name="connectionstate"></param>
        /// <param name="dtbTable"></param>
        /// <param name="eCommandType"></param>
        private void OnExecuteOracleSP(string szQuery, ConnectionState connectionstate, DataTable dtbTable, CommandType eCommandType)
        {

            try
            {
                PreparOracleCommand(szQuery, eCommandType);

                oracleAdapter = new OracleDataAdapter();
                oracleAdapter.SelectCommand = oracleCommand;
                oracleAdapter.Fill(dtbTable);

            }

            catch (OracleException ex)
            {

                if (ex != null)
                {
                    HandleExceptions(ex);
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Database error");
                }


            }
            finally
            {
                oracleCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    if (oracleConnection.State == System.Data.ConnectionState.Open)
                    {
                        oracleConnection.Close();
                    }
                }
                oracleAdapter.Dispose();
            }
        }

        #endregion

        #region Base Method for the Execute Query for DataSet
        protected void OnExecuteQuery(string szQuery, ConnectionState connectionstate, DataSet dsDataset, string szTable, CommandType eCommandType)
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                PreparSybaseCommand(szQuery, eCommandType);
                objAseCommand.Connection = objAseConnection;
                aseDataAdapter = new AseDataAdapter();
                aseDataAdapter.SelectCommand = objAseCommand;
                try
                {
                    aseDataAdapter.Fill(dsDataset, szTable);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objAseCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objAseConnection.State == System.Data.ConnectionState.Open)
                        {
                            objAseConnection.Close();
                        }
                    }
                }
            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {
                PreparOracleCommand(szQuery, eCommandType);
                oracleCommand.Connection = oracleConnection;
                oracleAdapter = new OracleDataAdapter();
                oracleAdapter.SelectCommand = oracleCommand;

                try
                {
                    oracleAdapter.Fill(dsDataset, szTable);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (oracleConnection.State == System.Data.ConnectionState.Open)
                        {
                            oracleConnection.Close();
                        }
                    }
                }

            }
            #endregion
            #region For SQL
            else
            {
                PreparDbCommand(szQuery, eCommandType);
                objCommand.Connection = objConnection;
                adapter = objFactory.CreateDataAdapter();
                adapter.SelectCommand = objCommand;
                try
                {
                    adapter.Fill(dsDataset, szTable);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objConnection.State == System.Data.ConnectionState.Open)
                        {
                            objConnection.Close();
                        }
                    }

                }
            #endregion

            }
        }
        #endregion

        #region Base Method for the Execute Query for Datatable
        protected void OnExecuteQuery(string szQuery, ConnectionState connectionstate, DataTable dtbTable, CommandType eCommandType)
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                PreparSybaseCommand(szQuery, eCommandType);
                objAseCommand.Connection = objAseConnection;
                aseDataAdapter = new AseDataAdapter();
                aseDataAdapter.SelectCommand = objAseCommand;
                try
                {
                    aseDataAdapter.Fill(dtbTable);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objAseCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objAseConnection.State == System.Data.ConnectionState.Open)
                        {
                            objAseConnection.Close();
                        }
                    }
                    aseDataAdapter.Dispose();
                }
            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {
                PreparOracleCommand(szQuery, eCommandType);
                oracleCommand.Connection = oracleConnection;
                oracleAdapter = new OracleDataAdapter();
                oracleAdapter.SelectCommand = oracleCommand;

                try
                {
                    oracleAdapter.Fill(dtbTable);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (oracleConnection.State == System.Data.ConnectionState.Open)
                        {
                            oracleConnection.Close();
                        }
                    }
                    oracleAdapter.Dispose();
                }

            }
            #endregion
            #region For SQL
            else
            {
                PreparDbCommand(szQuery, eCommandType);
                objCommand.Connection = objConnection;
                adapter = objFactory.CreateDataAdapter();
                adapter.SelectCommand = objCommand;
                try
                {
                    adapter.Fill(dtbTable);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objConnection.State == System.Data.ConnectionState.Open)
                        {
                            objConnection.Close();
                        }
                    }
                    adapter.Dispose();
                }
            #endregion

            }
        }
        #endregion
        #endregion

        #region Execute Scalar

        #region Execute Scalar -- input -- string szQuery, System.Data.CommandType eCommandType, ConnectionState connectionstate
        /// <summary>
        /// Executes a query that brings back one row with one column.  Usually used with queries that
        /// return scalar values, like "select count(*) from tRole".  The value is returned. 
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <param name="eCommandType">Type of query.  CommandType.Text is a normal insert, update,
        /// delete, or select query.  CommandType.StoredProcedure is for Stored Procedures.</param>
        /// <param name="connectionstate">connection state</param>
        /// <returns>The value of the first column in the first row.</returns>
        public object ExecuteScalar(string szQuery, System.Data.CommandType eCommandType, ConnectionState connectionstate)
        {
            object oReturn = OnExecuteScalar(szQuery, eCommandType, connectionstate);
            return oReturn;
        }
        #endregion

        #region Execute Scalar -- input -- string szQuery, System.Data.CommandType eCommandType
        /// <summary>
        /// Executes a query that brings back one row with one column.  Usually used with queries that
        /// return scalar values, like "select count(*) from tRole".  The value is returned. 
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <param name="eCommandType">Type of query.  CommandType.Text is a normal insert, update,
        /// delete, or select query.  CommandType.StoredProcedure is for Stored Procedures.</param>
        /// <returns>The value of the first column in the first row.</returns>
        public object ExecuteScalar(string szQuery, System.Data.CommandType eCommandType)
        {
            object oReturn = OnExecuteScalar(szQuery, eCommandType, ConnectionState.CloseOnExit);
            return oReturn;
        }
        #endregion

        #region Execute Scalar -- string szQuery, ConnectionState connectionstate
        /// <summary>
        /// Executes a query that brings back one row with one column.  Usually used with queries that
        /// return scalar values, like "select count(*) from tRole".  The value is returned. 
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <returns>The value of the first column in the first row.</returns>
        public object ExecuteScalar(string szQuery, ConnectionState connectionstate)
        {
            object oReturn = OnExecuteScalar(szQuery, System.Data.CommandType.Text, connectionstate);
            return oReturn;
        }

        #endregion

        #region Execute Scalar -- string szQuery
        /// <summary>
        /// Executes a query that brings back one row with one column.  Usually used with queries that
        /// return scalar values, like "select count(*) from tRole".  The value is returned. 
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <returns>The value of the first column in the first row.</returns>
        public object ExecuteScalar(string szQuery)
        {
            object oReturn = OnExecuteScalar(szQuery, System.Data.CommandType.Text, ConnectionState.CloseOnExit);
            return oReturn;
        }
        #endregion

        #region Execute Scalar SP -- input -- string szQuery, ConnectionState connectionstate
        /// <summary>
        /// Executes a query that brings back one row with one column.  Usually used with queries that
        /// return scalar values, like "select count(*) from tRole".  The value is returned. 
        /// </summary>
        /// <param name="szQuery">Stored procedure name</param>
        /// <returns>The value of the first column in the first row.</returns>
        public object ExecuteScalarSP(string szQuery, ConnectionState connectionstate)
        {
            object oReturn = OnExecuteScalar(szQuery, System.Data.CommandType.StoredProcedure, connectionstate);
            return oReturn;
        }
        #endregion

        #region Execute Scalar SP -- input -- string szQuery
        /// <summary>
        /// Executes a query that brings back one row with one column.  Usually used with queries that
        /// return scalar values, like "select count(*) from tRole".  The value is returned. 
        /// </summary>
        /// <param name="szQuery">Stored procedure name</param>
        /// <returns>The value of the first column in the first row.</returns>
        public object ExecuteScalarSP(string szQuery)
        {
            object oReturn = OnExecuteScalar(szQuery, System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit);
            return oReturn;
        }
        #endregion

        #region Protected Base Method for Execute Scalar and Execute Scalar SP
        protected object OnExecuteScalar(string szQuery, CommandType eCommandType, ConnectionState connectionstate)
        {
            object o = null;
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                PreparSybaseCommand(szQuery, eCommandType);
                try
                {
                    if (objAseConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objAseConnection.Open();
                    }
                    o = objAseCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objAseCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        objAseConnection.Close();
                    }
                }

            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {
                PreparOracleCommand(szQuery, eCommandType);
                try
                {
                    if (oracleConnection.State == System.Data.ConnectionState.Closed)
                    {
                        oracleConnection.Open();
                    }
                    o = oracleCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        oracleConnection.Close();
                    }
                }

            }
            #endregion
            #region For SQL
            else
            {
                PreparDbCommand(szQuery, eCommandType);
                try
                {
                    if (objConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    o = objCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        objConnection.Close();
                    }
                }
            }
            #endregion
            return o;
        }
        #endregion
        #endregion

        #region Execute Non Query

        #region ExecuteNonQuery -- input -- string szQuery, System.Data.CommandType eCommandType,ConnectionState connectionstate
        /// <summary>
        /// Executes query that doesn't bring back a result set.  This can be an insert, update, delete
        /// or a stored procedure.  *Note this will work with a select, but you don't get access to the
        /// results.
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <param name="eCommandType">Type of query.  CommandType.Text is a normal insert, update,
        /// delete, or select query.  CommandType.StoredProcedure is for Stored Procedures.</param>
        /// <param name="connectionstate">connection state</param>
        /// <returns>The number of records affected.  -1 if not known.</returns>
        public int ExecuteNonQuery(string szQuery, System.Data.CommandType eCommandType, ConnectionState connectionstate)
        {
            int iReturn = OnExecuteNonQuery(szQuery, eCommandType, connectionstate);
            return iReturn;
        }
        #endregion

        #region ExecuteNonQuery -- input -- string szQuery, System.Data.CommandType eCommandType
        /// <summary>
        /// Executes query that doesn't bring back a result set.  This can be an insert, update, delete
        /// or a stored procedure.  *Note this will work with a select, but you don't get access to the
        /// results.
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <param name="eCommandType">Type of query.  CommandType.Text is a normal insert, update,
        /// delete, or select query.  CommandType.StoredProcedure is for Stored Procedures.</param>
        /// <param name="connectionstate">connection state</param>
        /// <returns>The number of records affected.  -1 if not known.</returns>
        public int ExecuteNonQuery(string szQuery, System.Data.CommandType eCommandType)
        {
            int iReturn = OnExecuteNonQuery(szQuery, eCommandType, ConnectionState.CloseOnExit);
            return iReturn;
        }
        #endregion

        #region ExecuteNonQuery -- input -- string szQuery,ConnectionState connectionstate
        /// <summary>
        /// Executes text query that doesn't bring back a result set.  This can be an insert, update, delete
        /// .  *Note this will work with a select, but you don't get access to the
        /// results.
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <param name="connectionstate">connection state</param>
        /// <returns>The number of records affected.  -1 if not known.</returns>
        public int ExecuteNonQuery(string szQuery, ConnectionState connectionstate)
        {
            int iReturn = OnExecuteNonQuery(szQuery, System.Data.CommandType.Text, connectionstate);
            return iReturn;
        }
        #endregion

        #region ExecuteNonQuery -- input -- string szQuery
        /// <summary>
        /// Executes text query that doesn't bring back a result set.  This can be an insert, update, delete
        /// .  *Note this will work with a select, but you don't get access to the
        /// results.
        /// </summary>
        /// <param name="szQuery">Query to execute.</param>
        /// <returns>The number of records affected.  -1 if not known.</returns>
        public int ExecuteNonQuery(string szQuery)
        {
            int iReturn = OnExecuteNonQuery(szQuery, System.Data.CommandType.Text, ConnectionState.CloseOnExit);
            return iReturn;
        }
        #endregion

        #region ExecuteNonQuerySP -- input -- string szQuery,ConnectionState connectionstate
        /// <summary>
        /// Executes a stored procedure that doesn't bring back a result set.  
        /// </summary>
        /// <param name="szQuery">Stored procedure name.</param>
        /// <param name="connectionstate">connection state</param>
        /// <returns>The number of records affected.  -1 if not known.  However, if the stored procedure has "Set Nocount on" then you don't get the actual count.</returns>
        public int ExecuteNonQuerySP(string szQuery, ConnectionState connectionstate)
        {
            int iReturn = OnExecuteNonQuery(szQuery, System.Data.CommandType.StoredProcedure, connectionstate);
            return iReturn;
        }
        #endregion

        #region ExecuteNonQuerySP -- input -- string szQuery
        /// <summary>
        /// Executes a stored procedure that doesn't bring back a result set.  
        /// </summary>
        /// <param name="szQuery">Stored procedure name.</param>
        /// <returns>The number of records affected.  -1 if not known.  However, if the stored procedure has "Set Nocount on" then you don't get the actual count.</returns>
        public int ExecuteNonQuerySP(string szQuery)
        {
            int iReturn = OnExecuteNonQuery(szQuery, System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit);
            return iReturn;
        }
        #endregion

        #region Protected base method for Execute Non Query
        protected int OnExecuteNonQuery(string szQuery, CommandType eCommandType, ConnectionState connectionstate)
        {
#if DEBUG
            Console.WriteLine(string.Format("OnExecuteNonQuery: SQL:{0}", szQuery));
#endif
            int i = -1;
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                PreparSybaseCommand(szQuery, eCommandType);
                //objAseCommand.CommandText = query;
                //objAseCommand.CommandType = commandtype;
                try
                {
                    if (objAseConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objAseConnection.Open();
                    }
                    i = objAseCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objAseCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        objAseConnection.Close();
                    }
                }

            }
            #endregion
            #region For Oracle
            if (m_szDatabaseType == "Oracle")
            {
                PreparOracleCommand(szQuery, eCommandType);
                try
                {
                    if (oracleConnection.State == System.Data.ConnectionState.Closed)
                    {
                        oracleConnection.Open();
                    }
                    i = oracleCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        oracleConnection.Close();
                    }
                }

            }
            #endregion
            #region For SQL
            else
            {
                PreparDbCommand(szQuery, eCommandType);
                //objCommand.CommandText = query;
                //objCommand.CommandType = commandtype;

                try
                {
                    if (objConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    i = objCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        objConnection.Close();
                    }

                }
            }
            #endregion
            return i;
        }
        #endregion
        #endregion

        #region ExecuteSave

        #region ExecuteSave -- Input --- System.Data.DataTable dtbTable, string szInsert, string szUpdate, string szDelete, System.Data.CommandType eCommandType
        /// <summary>
        /// This method saves all changed rows in a datatable to the database.  ADO.NET uses the row status
        /// to determine if the row is new, modified, and deleted and executes the appropriate query.  
        /// 
        /// You need to specify how the columns in the datatable are mapped to parameters in the query.  
        /// You also must specify if the parameter
        /// is used by the insert, update, and delete queries.  By default the mapped parameters are used
        /// for the insert and update.
        /// </summary>
        /// <param name="dtbTable">DataTable that contains the modified rows.</param>
        /// <param name="szInsert">Sql Insert query or procedure to use to insert new rows</param>
        /// <param name="szUpdate">Sql Update query or procedure to change row</param>
        /// <param name="szDelete">Sql Delete query or procedure to delete row</param>
        /// <param name="eCommandType">Indicates if szInsert, szUpdate, and szDelete are SQL Queries or stored procedures</param>
        public void ExecuteSave(System.Data.DataTable dtbTable, string szInsert, string szUpdate, string szDelete, System.Data.CommandType eCommandType)
        {
            OnExecuteSave(dtbTable, szInsert, szUpdate, szDelete, eCommandType);
        }
        #endregion

        #region ExecuteSave -- Input -- System.Data.DataSet dsDataset, string szTable, string szInsert, string szUpdate, string szDelete, System.Data.CommandType eCommandType
        /// <summary>
        /// This method saves all changed rows in a datatable to the database.  ADO.NET uses the row status
        /// to determine if the row is new, modified, and deleted and executes the appropriate query.  
        /// 
        /// You need to specify how the columns in the datatable are mapped to parameters in the query.  
        /// You also must specify if the parameter
        /// is used by the insert, update, and delete queries.  By default the mapped parameters are used
        /// for the insert and update.
        /// </summary>
        /// <param name="dsDataset">Dataset that holds the data to save to the database.</param>
        /// <param name="szTable">Datatable to save</param>
        /// <param name="szInsert">Sql Insert query or procedure to use to insert new rows</param>
        /// <param name="szUpdate">Sql Update query or procedure to change row</param>
        /// <param name="szDelete">Sql Delete query or procedure to use to delete a row</param>
        /// <param name="eCommandType">Indicates if szInsert, szUpdate, and szDelete are SQL Queries or stored procedures</param>
        public void ExecuteSave(System.Data.DataSet dsDataset, string szTable, string szInsert, string szUpdate, string szDelete, System.Data.CommandType eCommandType)
        {
            OnExecuteSave(dsDataset.Tables[szTable], szInsert, szUpdate, szDelete, eCommandType);
        }

        #endregion

        #region ExecuteSave -- Input -- System.Data.DataSet dsDataset, string szTable, string szInsert, string szUpdate, string szDelete
        /// <summary>
        /// This method saves all changed rows in a datatable to the database.  ADO.NET uses the row status
        /// to determine if the row is new, modified, and deleted and executes the appropriate query.  
        /// 
        /// You need to specify how the columns in the datatable are mapped to parameters in the query.  
        /// You also must specify if the parameter
        /// is used by the insert, update, and delete queries.  By default the mapped parameters are used
        /// for the insert and update.
        /// </summary>
        /// <param name="dsDataset">Dataset that holds the data to save to the database</param>
        /// <param name="szTable">Datatable to save</param>
        /// <param name="szInsert">Sql Insert query to use to insert new rows</param>
        /// <param name="szUpdate">Sql Update query to change row</param>
        /// <param name="szDelete">Sql Delete query to use to delete row</param>
        public void ExecuteSave(System.Data.DataSet dsDataset, string szTable, string szInsert, string szUpdate, string szDelete)
        {
            OnExecuteSave(dsDataset.Tables[szTable], szInsert, szUpdate, szDelete, System.Data.CommandType.Text);
        }
        #endregion

        #region ExecuteSave -- Input -- System.Data.DataTable dtbTable, string szInsert, string szUpdate, string szDelete
        /// <summary>
        /// This method saves all changed rows in a datatable to the database.  ADO.NET uses the row status
        /// to determine if the row is new, modified, and deleted and executes the appropriate query.  
        /// 
        /// You need to specify how the columns in the datatable are mapped to parameters in the query.  
        /// You also must specify if the parameter
        /// is used by the insert, update, and delete queries.  By default the mapped parameters are used
        /// for the insert and update.
        /// </summary>
        /// <param name="dtbTable">DataTable that contains the modified rows.</param>
        /// <param name="szInsert">Sql Insert query to use to insert new rows</param>
        /// <param name="szUpdate">Sql Update query to change row</param>
        /// <param name="szDelete">Sql Delete query or procedure to delete row</param>
        public void ExecuteSave(System.Data.DataTable dtbTable, string szInsert, string szUpdate, string szDelete)
        {
            OnExecuteSave(dtbTable, szInsert, szUpdate, szDelete, System.Data.CommandType.Text);
        }
        #endregion

        #region ExecuteSaveSP -- input -- System.Data.DataSet dsDataset, string szTable, string szInsert, string szUpdate, string szDelete
        /// <summary>
        /// This method saves all changed rows in a datatable to the database.  ADO.NET uses the row status
        /// to determine if the row is new, modified, and deleted and executes the appropriate query.  
        /// 
        /// You need to specify how the columns in the datatable are mapped to parameters in the query.  
        /// You also must specify if the parameter
        /// is used by the insert, update, and delete queries.  By default the mapped parameters are used
        /// for the insert and update.
        /// </summary>
        /// <param name="dsDataset">Dataset that holds the data to save to the database</param>
        /// <param name="szTable">Datatable to save</param>
        /// <param name="szInsert">Stored Procedure to use to insert new rows</param>
        /// <param name="szUpdate">Stored Procedure to change row</param>
        /// <param name="szDelete">Stored Procedure to delete row</param>
        public void ExecuteSaveSP(System.Data.DataSet dsDataset, string szTable, string szInsert, string szUpdate, string szDelete)
        {
            OnExecuteSave(dsDataset.Tables[szTable], szInsert, szUpdate, szDelete, CommandType.StoredProcedure);
        }
        #endregion

        #region ExecuteSaveSP -- input -- System.Data.DataTable dtbTable, string szInsert, string szUpdate, string szDelete
        /// <summary>
        /// This method saves all changed rows in a datatable to the database.  ADO.NET uses the row status
        /// to determine if the row is new, modified, and deleted and executes the appropriate query.  
        /// 
        /// You need to specify how the columns in the datatable are mapped to parameters in the query.  
        /// You also must specify if the parameter
        /// is used by the insert, update, and delete queries.  By default the mapped parameters are used
        /// for the insert and update.
        /// </summary>
        /// <param name="dtbTable">DataTable that contains the modified rows.</param>
        /// <param name="szInsert">Stored Procedure to use to insert new rows</param>
        /// <param name="szUpdate">Stored Procedure to change row</param>
        /// <param name="szDelete">Stored Procedure to delete row</param>
        public void ExecuteSaveSP(System.Data.DataTable dtbTable, string szInsert, string szUpdate, string szDelete)
        {
            OnExecuteSave(dtbTable, szInsert, szUpdate, szDelete, CommandType.StoredProcedure);
        }
        #endregion

        #region Protected Base Method for the Execute Save
        protected void OnExecuteSave(DataTable dtbTable, string szInsert, string szUpdate, string szDelete, System.Data.CommandType eCommandType)
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                AseDataAdapter oAseDataAdapter = new AseDataAdapter();
                try
                {
                    if (szInsert != null)
                    {
                        if (oAseDataAdapter.InsertCommand == null)
                        {
                            oAseDataAdapter.InsertCommand = objAseConnection.CreateCommand();
                        }
                        PreparSybaseCommand(szInsert, eCommandType, oAseDataAdapter.InsertCommand, ParameterUsage.Insert);
                    }
                    if (szUpdate != null)
                    {
                        if (oAseDataAdapter.UpdateCommand == null)
                        {
                            oAseDataAdapter.UpdateCommand = objAseConnection.CreateCommand();
                        }
                        PreparSybaseCommand(szUpdate, eCommandType, oAseDataAdapter.UpdateCommand, ParameterUsage.Update);
                    }
                    if (szDelete != null)
                    {
                        if (oAseDataAdapter.DeleteCommand == null)
                        {
                            oAseDataAdapter.DeleteCommand = objAseConnection.CreateCommand();
                        }
                        PreparSybaseCommand(szDelete, eCommandType, oAseDataAdapter.DeleteCommand, ParameterUsage.Delete);
                    }
                    oAseDataAdapter.Update(dtbTable);
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oAseDataAdapter.Dispose();
                }
            }

            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {
                OracleDataAdapter oOracleAdapter = new OracleDataAdapter();
                try
                {
                    if (szInsert != null)
                    {
                        if (oOracleAdapter.InsertCommand == null)
                        {
                            oOracleAdapter.InsertCommand = oracleConnection.CreateCommand();
                        }
                        PreparOracleCommand(szInsert, eCommandType, oOracleAdapter.InsertCommand, ParameterUsage.Insert);
                    }
                    if (szUpdate != null)
                    {
                        if (oOracleAdapter.UpdateCommand == null)
                        {
                            oOracleAdapter.UpdateCommand = oracleConnection.CreateCommand();
                        }
                        PreparOracleCommand(szUpdate, eCommandType, oOracleAdapter.UpdateCommand, ParameterUsage.Update);
                    }
                    if (szDelete != null)
                    {
                        if (oOracleAdapter.DeleteCommand == null)
                        {
                            oOracleAdapter.DeleteCommand = oracleConnection.CreateCommand();
                        }
                        PreparOracleCommand(szDelete, eCommandType, oOracleAdapter.DeleteCommand, ParameterUsage.Delete);
                    }
                    oOracleAdapter.Update(dtbTable);
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oOracleAdapter.Dispose();
                }

            }
            #endregion

            #region For SQL
            else
            {
                adapter = objFactory.CreateDataAdapter();
                try
                {
                    if (szInsert != null)
                    {
                        if (adapter.InsertCommand == null)
                        {
                            adapter.InsertCommand = objConnection.CreateCommand();
                        }
                        PreparDbCommand(szInsert, eCommandType, adapter.InsertCommand, ParameterUsage.Insert);
                    }
                    if (szUpdate != null)
                    {
                        if (adapter.UpdateCommand == null)
                        {
                            adapter.UpdateCommand = objConnection.CreateCommand();
                        }
                        PreparDbCommand(szUpdate, eCommandType, adapter.UpdateCommand, ParameterUsage.Update);
                    }
                    if (szDelete != null)
                    {
                        if (adapter.DeleteCommand == null)
                        {
                            adapter.DeleteCommand = objConnection.CreateCommand();
                        }
                        PreparDbCommand(szDelete, eCommandType, adapter.DeleteCommand, ParameterUsage.Delete);
                    }
                    adapter.Update(dtbTable);
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    adapter.Dispose();
                }
            }
            #endregion
        } //ExecuteSave()

        #endregion


        #endregion

        #region Sybase Command Preparation (PreparSybaseCommand)
        /// <summary>
        /// Prepare the command for Sybase as a whole
        /// </summary>
        /// <param name="szQuery">Query name</param>
        /// <returns></returns>
        protected AseCommand PreparSybaseCommand(string szQuery, CommandType eCommandType)
        {
            PrepareQuery(ref szQuery, eCommandType);
            objAseCommand.CommandText = szQuery;
            objAseCommand.CommandType = eCommandType;
            return objAseCommand;
        }
        #endregion

        #region Overloaded Prepare Sybase method (PreparSybaseCommand) for ExecuteSave Method
        private void PreparSybaseCommand(string szQuery, CommandType eCommandType, AseCommand oAseCommand, ParameterUsage eUsage)
        {
            PrepareQuery(ref szQuery, eCommandType);
            oAseCommand.CommandText = szQuery;
            oAseCommand.CommandType = eCommandType;
            foreach (KodakDALParameter oKodakDALParameter in Parameters)
            {
                if ((eUsage & oKodakDALParameter.ParameterUsage) != 0)
                {
                    AseParameter oAseParameter = (AseParameter)oKodakDALParameter.InternalType;
                    AseParameter oAseParameterNew = new AseParameter(
                        oAseParameter.ParameterName, oAseParameter.AseDbType, oAseParameter.Size,
                        oAseParameter.Direction, oAseParameter.IsNullable, oAseParameter.Precision,
                        oAseParameter.Scale, oAseParameter.SourceColumn, oAseParameter.SourceVersion,
                        oAseParameter.Value);
                    oAseCommand.Parameters.Add(oAseParameterNew);
                }
            }
        }
        #endregion

        #region Oracle Command Preparation (PreparOracleCommand)
        /// <summary>
        /// Prepare the command for Oracle as a whole
        /// </summary>
        /// <param name="szQuery">Query name</param>
        /// <returns></returns>
        protected OracleCommand PreparOracleCommand(string szQuery, CommandType eCommandType)
        {
            oracleCommand.CommandText = szQuery;
            oracleCommand.CommandType = eCommandType;
            return oracleCommand;
        }
        #endregion

        #region Overloaded Prepare Oracle method (PreparOracleCommand) for ExecuteSave Method
        private void PreparOracleCommand(string szQuery, CommandType eCommandType, OracleCommand oracleCommand, ParameterUsage eUsage)
        {
            PrepareQuery(ref szQuery, eCommandType);
            oracleCommand.CommandText = szQuery;
            oracleCommand.CommandType = eCommandType;
            foreach (KodakDALParameter oKodakDALParameter in Parameters)
            {
                if ((eUsage & oKodakDALParameter.ParameterUsage) != 0)
                {
                    OracleParameter oOracleParameter = (OracleParameter)oKodakDALParameter.InternalType;

                    OracleParameter oOracleParameterNew = new OracleParameter(
                        oOracleParameter.ParameterName, oOracleParameter.OracleType, oOracleParameter.Size,
                        oOracleParameter.Direction, oOracleParameter.IsNullable, oOracleParameter.Precision,
                        oOracleParameter.Scale, oOracleParameter.SourceColumn, oOracleParameter.SourceVersion,
                        oOracleParameter.Value);
                    oracleCommand.Parameters.Add(oOracleParameterNew);

                }
            }
        }
        #endregion

        #region SQL Command Preparation (PreparDbCommand)
        /// <summary>
        /// Prepare the command for SQL and Oracle as a whole
        /// </summary>
        /// <param name="szQuery">Query name</param>
        /// <returns></returns>
        protected DbCommand PreparDbCommand(string szQuery, CommandType eCommandType)
        {
            PrepareQuery(ref szQuery, eCommandType);
            objCommand.CommandText = szQuery;
            objCommand.CommandType = eCommandType;
            objCommand.CommandTimeout = 0;
            return objCommand;
        }
        #endregion

        #region Overloaded PrepareDBCommand Method (PrepareDBCommand) for Oracle and SQL for ExecuteSave Method
        private void PreparDbCommand(string szQuery, CommandType eCommandType, DbCommand oDbCommand, ParameterUsage eUsage)
        {
            PrepareQuery(ref szQuery, eCommandType);
            oDbCommand.CommandText = szQuery;
            oDbCommand.CommandType = eCommandType;
            oDbCommand.CommandTimeout = 0;
            oDbCommand.Connection = objConnection;
            foreach (KodakDALParameter oKodakDALParameter in Parameters)
            {
                if ((eUsage & oKodakDALParameter.ParameterUsage) != 0)
                {
                    //KodakDALParameter oDbParameter = (KodakDALParameter)oKodakDALParameter.InternalType;
                    //KodakDALParameter oDbParameterNew = new KodakDALParameter(;
                    DbParameter oDbParameter = (DbParameter)oKodakDALParameter.InternalType;
                    DbParameter oDbParameterNew = objFactory.CreateParameter();
                    oDbParameterNew.ParameterName = oDbParameter.ParameterName;
                    oDbParameterNew.DbType = oDbParameter.DbType;
                    oDbParameterNew.Size = oDbParameter.Size;
                    oDbParameterNew.Direction = oDbParameter.Direction;
                    oDbParameterNew.IsNullable = oDbParameter.IsNullable;
                    oDbParameterNew.SourceColumn = oDbParameter.SourceColumn;
                    oDbParameterNew.SourceVersion = oDbParameter.SourceVersion;
                    oDbParameterNew.Value = oDbParameter.Value;
                    oDbCommand.Parameters.Add(oDbParameterNew);
                }
            }
        }
        #endregion

        #region Prepare Query
        /// <summary>
        /// Formation of Query with dbo.
        /// </summary>
        /// <param name="szQuery">Query name</param>
        private void PrepareQuery(ref string szQuery, CommandType eType)
        {
            if (eType == CommandType.StoredProcedure)
            {
                if (szQuery.IndexOf('.', 0) == -1)
                {
                    // szQuery = "dbo." + szQuery;
                }

            }

        }
        #endregion

        #region More Enum for Connection State
        public enum ConnectionState
        {
            KeepOpen, CloseOnExit
        }
        #endregion

        #region Handle Exception
        private void HandleExceptions(Exception ex)
        {
            //ILogManager logmanager = new LogManager();
            //logmanager.Fatal(10,"DAL",10,ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
        }
        #endregion

        #region Begin Transaction
        public void BeginTransaction()
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                try
                {
                    if (objAseConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objAseConnection.Open();
                    }
                    objAseCommand.Transaction = objAseConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {
                try
                {
                    if (oracleConnection.State == System.Data.ConnectionState.Closed)
                    {
                        oracleConnection.Open();
                    }
                    oracleCommand.Transaction = oracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
            }
            #endregion
            #region For SQL
            else
            {
                try
                {
                    if (objConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    objCommand.Transaction = objConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
            }
            #endregion
        }
        #endregion

        #region CommitTransaction
        public void CommitTransaction()
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                try
                {
                    objAseCommand.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objAseConnection.Close();
                }
            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {
                try
                {
                    oracleCommand.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleConnection.Close();
                }
            }
            #endregion
            #region For SQL
            else
            {
                try
                {
                    objCommand.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objConnection.Close();
                }
            }
            #endregion
        }
        #endregion

        #region RollbackTransaction
        public void RollbackTransaction()
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {
                try
                {
                    objAseCommand.Transaction.Rollback();
                    objAseConnection.Close();
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objAseConnection.Close();
                }
            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {
                try
                {
                    oracleCommand.Transaction.Rollback();
                    oracleConnection.Close();
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleConnection.Close();
                }
            }
            #endregion
            #region For SQL
            else
            {
                try
                {
                    if (objCommand.Transaction != null)
                    {
                        objCommand.Transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    //   HandleExceptions(ex);
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    objConnection.Close();
                }
            }
            #endregion
        }
        #endregion

        #region Create Parameter
        /// <summary>
        /// Creation of parameter object to use across
        /// </summary>
        /// <returns></returns>
        protected internal IDbDataParameter CreateParameter()
        {
            if (m_szDatabaseType == "Sybase")
            {
                //AseParameter oP = new AseParameter();
                aseParam = objAseCommand.CreateParameter();
                aseParam.ParameterName = "";
                aseSelectParams.Add(aseParam);
                return (IDbDataParameter)aseParam;
            }
            else if (m_szDatabaseType == "Oracle")
            {
                oracleParam = oracleCommand.CreateParameter();
                oracleParam.ParameterName = "";
                objOracleParamCollection.Add(oracleParam);

                return (IDbDataParameter)oracleParam;
            }
            else
            {
                objParam = objCommand.CreateParameter();
                m_oSelectParams.Add(objParam);
                return (IDbDataParameter)objParam;
            }
        }

        protected internal IDbDataParameter CreateParameter(string szName, OracleType oracleType, ParameterDirection paramDire)
        {

            oracleParam = oracleCommand.CreateParameter();
            oracleParam.ParameterName = szName;
            oracleParam.Direction = paramDire;
            oracleParam.OracleType = oracleType;
            objOracleParamCollection.Add(oracleParam);

            return (IDbDataParameter)oracleParam;

        }

        protected internal IDbDataParameter CreateParameter(string szName, OracleType oracleType, int nValue, ParameterDirection paramDire)
        {

            oracleParam = oracleCommand.CreateParameter();
            oracleParam.ParameterName = szName;
            oracleParam.Direction = paramDire;
            oracleParam.OracleType = oracleType;
            oracleParam.Value = nValue;
            objOracleParamCollection.Add(oracleParam);

            return (IDbDataParameter)oracleParam;

        }

        protected internal IDbDataParameter CreateParameter(string szName, OracleType oracleType, int nSize, string strValue, ParameterDirection paramDire)
        {

            oracleParam = oracleCommand.CreateParameter();
            oracleParam.ParameterName = szName;
            oracleParam.Direction = paramDire;
            oracleParam.OracleType = oracleType;
            oracleParam.Size = nSize;
            oracleParam.Value = strValue;
            objOracleParamCollection.Add(oracleParam);

            return (IDbDataParameter)oracleParam;

        }
        #endregion

        #region OnParameterCleared
        protected internal void OnParametersCleared()
        {
            if (m_szDatabaseType == "Sybase")
            {
                aseSelectParams.Clear();
            }
            else if (m_szDatabaseType == "Oracle")
            {
                objOracleParamCollection.Clear();
            }
            else
            {
                m_oSelectParams.Clear();
            }
        }
        #endregion

        #region Enum String Conversion
        public enum StringConversion
        {
            UpperCase = 1,
            Trim = 2,
            UpperCaseAndTrim = UpperCase | Trim,
            NullBlank = 4,
            UpperCaseTrimNullBlank = UpperCase | Trim | NullBlank,
        }
        #endregion

        #region Large object
        /// <summary>
        /// Write large object data to db
        /// </summary>
        /// <param name="strTableName">The table will be written</param>
        /// <param name="strKeyColName">The name of key column will be used to search the record</param>
        /// <param name="strKeyValue">The value of Key Column will be used to search the record</param>
        /// <param name="strLOColName">The column name of large object</param>
        /// <param name="loValue">The data of large object</param>
        /// <param name="nSize">The size of loValue</param>
        /// <param name="connectionstate"></param>
        public void WriteLargeObj(string strTableName, string strKeyColName, string strKeyValue, string strLOColName, byte[] loValue, int nSize, ConnectionState connectionstate)
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {

            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {

                try
                {

                    string strSQL = string.Format("SELECT {0},{1} FROM {2} WHERE {3}='{4}'",
                        strKeyColName, strLOColName, strTableName, strKeyColName, strKeyValue);

                    oracleAdapter = new OracleDataAdapter(strSQL, oracleConnection);

                    DataSet loDS = new DataSet(strTableName);
                    strSQL = string.Format("UPDATE {0} SET {1}=:vLargeObject WHERE {2}=:vKeyGuid",
                        strTableName, strLOColName, strKeyColName);
                    oracleAdapter.UpdateCommand = new OracleCommand(strSQL, oracleConnection);
                    oracleAdapter.UpdateCommand.Parameters.Add(":vLargeObject",
                    OracleType.Blob, nSize, strLOColName);
                    oracleAdapter.UpdateCommand.Parameters.Add(":vKeyGuid",
                    OracleType.NVarChar, 64, strKeyColName);

                    oracleAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    // Configures the schema to match with Data Source
                    oracleAdapter.FillSchema(loDS, SchemaType.Source, strTableName);

                    // Fills the DataSet with 'drivers' table data
                    oracleAdapter.Fill(loDS, strTableName);

                    // Get the current driver ID row for updation
                    DataTable loTable = loDS.Tables[strTableName];
                    DataRow loRow = loTable.Rows.Find(strKeyValue);

                    // Start the edit operation on the current row in
                    // the 'drivvers' table within the dataset.
                    loRow.BeginEdit();
                    // Assign the value of the Photo if not empty
                    if (loValue.Length != 0)
                    {
                        loRow[strLOColName] = loValue;
                    }
                    // End the editing current row operation
                    loRow.EndEdit();

                    // Update the database table 'drivers'
                    oracleAdapter.Update(loDS, strTableName);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (oracleConnection.State == System.Data.ConnectionState.Open)
                        {
                            oracleConnection.Close();
                        }
                    }
                }

            }
            #endregion
            #region For SQL
            else
            {
                SqlCommand cmd = null;
                try
                {
                    cmd = new SqlCommand();
                    cmd.Connection = objConnection as SqlConnection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("UPDATE {0} SET {1}=@VALUE WHERE {2}='{3}'",
                        strTableName, strLOColName, strKeyColName, strKeyValue);
                    cmd.Parameters.AddWithValue("@VALUE", loValue);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                    }

                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objConnection.State == System.Data.ConnectionState.Open)
                        {
                            objConnection.Close();
                        }
                    }

                }

            }
            #endregion

        }
        /// <summary>
        /// Write large object data to db
        /// </summary>
        /// <param name="strTableName">The table will be written</param>
        /// <param name="strKeyColName">The name of key column will be used to search the record</param>
        /// <param name="strKeyValue">The value of Key Column will be used to search the record</param>
        /// <param name="strLOColName">The column name of large object</param>
        /// <param name="strLOValue">The data of large object</param>
        /// <param name="nSize">The size of loValue</param>
        /// <param name="connectionstate"></param>
        public void WriteLargeObj(string strTableName, string strKeyColName, string strKeyValue, string strLOColName, string strLOValue, int nSize, ConnectionState connectionstate)
        {
            #region For Sybase
            if (m_szDatabaseType == "Sybase")
            {

            }
            #endregion
            #region For Oracle
            else if (m_szDatabaseType == "Oracle")
            {

                try
                {

                    string strSQL = string.Format("SELECT {0},{1} FROM {2} WHERE {3}='{4}'",
                        strKeyColName, strLOColName, strTableName, strKeyColName, strKeyValue);

                    oracleAdapter = new OracleDataAdapter(strSQL, oracleConnection);

                    DataSet loDS = new DataSet(strTableName);
                    strSQL = string.Format("UPDATE {0} SET {1}=:vLargeObject WHERE {2}=:vKeyGuid",
                        strTableName, strLOColName, strKeyColName);
                    oracleAdapter.UpdateCommand = new OracleCommand(strSQL, oracleConnection);
                    oracleAdapter.UpdateCommand.Parameters.Add(":vLargeObject",
                    OracleType.Clob, nSize, strLOColName);
                    oracleAdapter.UpdateCommand.Parameters.Add(":vKeyGuid",
                    OracleType.NVarChar, 64, strKeyColName);

                    oracleAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    // Configures the schema to match with Data Source
                    oracleAdapter.FillSchema(loDS, SchemaType.Source, strTableName);

                    // Fills the DataSet with 'drivers' table data
                    oracleAdapter.Fill(loDS, strTableName);

                    // Get the current driver ID row for updation
                    DataTable loTable = loDS.Tables[strTableName];
                    DataRow loRow = loTable.Rows.Find(strKeyValue);

                    // Start the edit operation on the current row in
                    // the 'drivvers' table within the dataset.
                    loRow.BeginEdit();
                    // Assign the value of the Photo if not empty
                    loRow[strLOColName] = strLOValue;

                    // End the editing current row operation
                    loRow.EndEdit();

                    // Update the database table 'drivers'
                    oracleAdapter.Update(loDS, strTableName);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    oracleCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (oracleConnection.State == System.Data.ConnectionState.Open)
                        {
                            oracleConnection.Close();
                        }
                    }
                }

            }
            #endregion
            #region For SQL
            else
            {
                SqlCommand cmd = null;
                try
                {
                    cmd = new SqlCommand();
                    cmd.Connection = objConnection as SqlConnection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("UPDATE {0} SET {1}=@VALUE WHERE {2}='{3}'",
                        strTableName, strLOColName, strKeyColName, strKeyValue);
                    cmd.Parameters.AddWithValue("@VALUE", strLOValue);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        HandleExceptions(ex);
                        throw new Exception(ex.Message);
                    }
                    else
                    {
                        throw new Exception("Database error");
                    }
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                    }

                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objConnection.State == System.Data.ConnectionState.Open)
                        {
                            objConnection.Close();
                        }
                    }

                }

            }
            #endregion

        }
        #endregion

        #region Sealed Class KodakDALParameterEnumerator
        public sealed class KodakDALParameterEnumerator : IEnumerator
        {
            IEnumerator m_oEnum;

            public KodakDALParameterEnumerator(System.Collections.Hashtable oParams)
            {
                m_oEnum = oParams.Values.GetEnumerator();
            }
            public object Current
            {
                get
                {
                    return m_oEnum.Current;
                }
            }

            public bool MoveNext()
            {
                return m_oEnum.MoveNext();
            }
            public void Reset()
            {
                m_oEnum.Reset();
            }
        }
        #endregion

        #region Sealed Class for Parameter Collection
        /// <summary>
        /// This is a sealed class for the Parameter collection
        /// </summary>
        public sealed class KodakDALParameterCollection : IEnumerable
        {
            #region Private members
            private RisDAL m_oCon;
            private System.Collections.Hashtable m_oParams;
            #endregion

            #region Internal Constructor
            internal KodakDALParameterCollection(RisDAL oCon)
            {
                m_oCon = oCon;
                m_oParams = System.Collections.Specialized.CollectionsUtil.CreateCaseInsensitiveHashtable(100);
            }
            #endregion

            #region Implementing IEnumerator
            public IEnumerator GetEnumerator()
            {
                return new KodakDALParameterEnumerator(m_oParams);
            }
            #endregion

            #region Private Methods

            #region PrepString -- szVal,eMode
            private void PrepString(ref string szVal, StringConversion eMode)
            {
                if (szVal == null)
                {
                    if ((eMode & StringConversion.NullBlank) == StringConversion.NullBlank)
                    {
                        szVal = "";
                    }
                    return;
                }

                if ((eMode & StringConversion.Trim) == StringConversion.Trim)
                    szVal = szVal.Trim();
                if ((eMode & StringConversion.UpperCase) == StringConversion.UpperCase)
                    szVal = szVal.ToUpper();
            }
            #endregion

            #region PrepString -- oVal, eMode
            private void PrepString(ref object oVal, StringConversion eMode)
            {
                if ((null == oVal) || (oVal == DBNull.Value))
                {
                    if ((eMode & StringConversion.NullBlank) == StringConversion.NullBlank)
                    {
                        oVal = "";
                    }
                    return;
                }

                if (oVal is string)
                {
                    string szTemp = oVal as string;
                    PrepString(ref szTemp, eMode);
                    oVal = szTemp;
                }
            }
            #endregion
            #endregion

            #region Clear
            /// <summary>
            /// Removes all parameters from the collection.
            /// </summary>
            public void Clear()
            {
                m_oParams.Clear();
                m_oCon.OnParametersCleared();
            }
            #endregion

            #region Contains
            /// <summary>
            /// Check if the collection has the given paramater
            /// </summary>
            /// <param name="szKey">Parameter name.  Must begin with '@'</param>
            /// <returns>True if exists.</returns>
            public bool Contains(string szKey)
            {
                return m_oParams.Contains(szKey);
            }
            #endregion

            #region Properties

            #region Get the given parameter
            /// <summary>
            /// Get the given parameter.  Name must begin with '@'
            /// </summary>
            public KodakDALParameter this[string key]
            {
                get
                {
                    return (KodakDALParameter)m_oParams[key];
                }
            }

            #endregion

            #region Returns the number of parameters in the collection.
            /// <summary>
            /// Returns the number of parameters in the collection.
            /// </summary>
            public int Count
            {
                get
                {
                    return m_oParams.Count;
                }
            }

            #endregion

            #endregion

            #region Remove At
            /// <summary>
            /// Removes the names parameter.
            /// </summary>
            /// <param name="szKey">Parameter to remove.  Must begin with '@'</param>
            public void RemoveAt(string szKey)
            {
                m_oParams.Remove(szKey);
            }

            #endregion

            #region Add
            public KodakDALParameter Add(string szName, object oVal)
            {
                KodakDALParameter oP = new KodakDALParameter(m_oCon.CreateParameter());
                oP.ParameterName = szName;
                oP.Value = oVal;
                m_oParams.Add(szName, oP);
                return oP;
            }

            public KodakDALParameter Add(string szName, OracleType oracleType, ParameterDirection paramDire)
            {
                KodakDALParameter oP = new KodakDALParameter((IDbDataParameter)m_oCon.CreateParameter(szName, oracleType, paramDire));
                m_oParams.Add(szName, oP);
                return oP;
            }

            public KodakDALParameter Add(string szName, OracleType oracleType, int nValue, ParameterDirection paramDire)
            {
                KodakDALParameter oP = new KodakDALParameter((IDbDataParameter)m_oCon.CreateParameter(szName, oracleType, nValue, paramDire));
                m_oParams.Add(szName, oP);
                return oP;
            }

            public KodakDALParameter Add(string szName, OracleType oracleType, int nSize, string strValue, ParameterDirection paramDire)
            {
                KodakDALParameter oP = new KodakDALParameter((IDbDataParameter)m_oCon.CreateParameter(szName, oracleType, nSize, strValue, paramDire));
                m_oParams.Add(szName, oP);
                return oP;
            }



            #endregion

            #region Overloaded Add Methods

            #region Add -- input szName,eType
            /// <summary>
            /// Creates an input parameter with a null value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eType">Type of data the parameter holds</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter Add(string szName, DbType eType)
            {
                KodakDALParameter oP = new KodakDALParameter(m_oCon.CreateParameter());
                oP.ParameterName = szName;
                oP.DbType = eType;
                m_oParams.Add(szName, oP);
                return oP;
            }
            #endregion

            #region Add -- input szName,eType,iSize
            /// <summary>
            /// Creates an input parameter with a null value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eType">Type of data the parameter holds</param>
            /// <param name="iSize">Size of column in bytes.  Int32=4 bytes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter Add(string szName, DbType eType, int iSize)
            {
                KodakDALParameter oP = new KodakDALParameter(m_oCon.CreateParameter());
                oP.ParameterName = szName;
                oP.DbType = eType;
                oP.Size = iSize;
                m_oParams.Add(szName, oP);
                return oP;
            }
            #endregion

            #region AddInt
            /// <summary>
            /// Creates a 32 bit integer parameter with a null value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddInt(string szName, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Int32);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion


            /// <summary>
            /// Creates a 32 bit integer parameter with an initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="iVal">Initial Value</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddInt(string szName, int iVal, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Int32);
                oP.Value = iVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddInt -- szName, oVal
            /// <summary>
            /// Creates a 32 bit integer input parameter with an initial value or null.  
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddInt(string szName, object oVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Int32);
                if (oVal == null) oVal = DBNull.Value;
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddInt -- szName,iVal
            /// <summary>
            /// Creates a 32 bit integer input parameter with an initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="iVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddInt(string szName, int iVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Int32);
                oP.Value = iVal;
                return oP;
            }
            #endregion

            #region AddSmallInt -- szName,eDirection
            /// <summary>
            /// Creates a 16 bit integer parameter with a null value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddSmallInt(string szName, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Int16);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddSmallInt -- szName,iVal,eDirection
            /// <summary>
            /// Creates a 16 bit integer parameter with an initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="iVal">Initial Value</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddSmallInt(string szName, int iVal, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Int16);
                oP.Value = iVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddSmallInt -- szName,oVal
            /// <summary>
            /// Creates a 16 bit integer input parameter with an initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddSmallInt(string szName, object oVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Int16);
                if (oVal == null) oVal = DBNull.Value;
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddSmallInt -- szName, iVal
            /// <summary>
            /// Creates a 16 bit integer input parameter with an initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="iVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddSmallInt(string szName, int iVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Int16);
                oP.Value = iVal;
                return oP;
            }
            #endregion

            #region AddDouble -- szName, eDirection
            /// <summary>
            /// Creates a floating point parameter with initial value of null.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDouble(string szName, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Double);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddDouble -- szName,dVal,eDirection
            /// <summary>
            /// Creates a floating point parameter with initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dVal">Initial Value</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDouble(string szName, double dVal, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Double);
                oP.Value = dVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddDouble -- szName,oVal
            /// <summary>
            /// Creates a floating point parameter with initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDouble(string szName, object oVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Double);
                if (oVal == null) oVal = DBNull.Value;
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddDouble -- szName, dVal
            /// <summary>
            /// Creates a floating point parameter with initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDouble(string szName, double dVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Double);
                oP.Value = dVal;
                return oP;
            }
            #endregion

            #region AddDecimal -- szName,eDirection
            /// <summary>
            /// Creates a floating point parameter with initial value of null.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDecimal(string szName, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Decimal);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddDecimal -- szName,dVal,eDirection
            /// <summary>
            /// Creates a floating point parameter with initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dVal">Initial Value</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDecimal(string szName, decimal dVal, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Decimal);
                oP.Value = dVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddDecimal -- szName,oVal
            /// <summary>
            /// Creates a floating point parameter with initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDecimal(string szName, object oVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Decimal);
                if (oVal == null) oVal = DBNull.Value;
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddDecimal -- szName,dVal
            /// <summary>
            /// Creates a floating point parameter with initial value.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDecimal(string szName, decimal dVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Decimal);
                oP.Value = dVal;
                return oP;
            }
            #endregion

            #region AddChar -- szName,iSize,eDirection
            /// <summary>
            /// Creates a fixed width character parameter with initial value of null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="iSize">Size of column in bytes.</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, int iSize, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.String, iSize);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddChar -- szName,szVal,iSize,eDirection,eMode
            /// <summary>
            /// Creates a fixed width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="iSize">Size of column in bytes.</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <param name="eMode">Conversion to apply to string value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, string szVal, int iSize, ParameterDirection eDirection, StringConversion eMode)
            {
                KodakDALParameter oP = Add(szName, DbType.String, iSize);
                PrepString(ref szVal, eMode);
                oP.Value = szVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddChar -- szName,szVal,eDirection
            /// <summary>
            /// Creates a fixed width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="iSize">Size of column in bytes.</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, string szVal, int iSize, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.String, iSize);
                oP.Value = szVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddChar -- szName,szVal,iSize
            /// <summary>
            /// Creates a fixed width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="iSize">Size of column in bytes.</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, string szVal, int iSize)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                oP.Size = iSize;
                oP.Value = szVal;
                return oP;
            }
            #endregion

            #region AddChar -- szName,oVal,eMode
            /// <summary>
            /// Creates a fixed width character parameter with initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value or null</param>
            /// <param name="eMode">How to convert strings.</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, object oVal, StringConversion eMode)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                PrepString(ref oVal, eMode);
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddChar -- szName,oVal
            /// <summary>
            /// Creates a fixed width character parameter with initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value or null</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, object oVal)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                if (null == oVal) oVal = DBNull.Value;
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddChar -- szName,szVal,eMode
            /// <summary>
            /// Creates a fixed width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="eMode">How to convert string.</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, string szVal, StringConversion eMode)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                PrepString(ref szVal, eMode);
                oP.Value = szVal;
                return oP;
            }
            #endregion

            #region AddChar -- szName,szVal
            /// <summary>
            /// Creates a fixed width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddChar(string szName, string szVal)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                oP.Value = szVal;
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,iSize,eDirection
            /// <summary>
            /// Creates a variable width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="iSize">Size of column in bytes.</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, int iSize, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.String, iSize);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,szVal,iSize,eDirection,eMode
            /// <summary>
            /// Creates a variable width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="iSize">Size of column in bytes.</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <param name="eMode">How to convert string</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, string szVal, int iSize, ParameterDirection eDirection, StringConversion eMode)
            {
                KodakDALParameter oP = Add(szName, DbType.String, iSize);
                PrepString(ref szVal, eMode);
                oP.Value = szVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,szVal,iSize,eDirection
            /// <summary>
            /// Creates a variable width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="iSize">Size of column in bytes.</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, string szVal, int iSize, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.String, iSize);
                oP.Value = szVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,szVal,iSize
            /// <summary>
            /// Creates a variable width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="iSize">Size of data in bytes.  Int32=4 bytes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, string szVal, int iSize)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                oP.Size = iSize;
                oP.Value = szVal;
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,oVal,eMode
            /// <summary>
            /// Creates a variable width character parameter with initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value or null</param>
            /// <param name="eMode">How to convert string</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, object oVal, StringConversion eMode)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                PrepString(ref oVal, eMode);
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,oVal
            /// <summary>
            /// Creates a variable width character parameter with initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value or null</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, object oVal)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                if (null == oVal) oVal = DBNull.Value;
                if (oVal is string)
                {
                    string szTemp = oVal as string;
                    oP.Value = szTemp;
                }
                else
                {
                    oP.Value = oVal;
                }
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,szVal,eMode
            /// <summary>
            /// Creates a variable width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <param name="eMode">How to convert string</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, string szVal, StringConversion eMode)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                PrepString(ref szVal, eMode);
                oP.Value = szVal;
                return oP;
            }
            #endregion

            #region AddVarChar -- szName,szVal
            /// <summary>
            /// Creates a variable width character parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarChar(string szName, string szVal)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                if (szVal == null)
                {
                    oP.Value = DBNull.Value;
                }
                else
                {
                    oP.Value = szVal;
                }
                return oP;
            }
            #endregion

            #region AddDate -- input -- string szName, ParameterDirection eDirection
            /// <summary>
            /// Creates a date parameter with initial value = null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDate(string szName, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Date);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region  AddDate -- input -- string szName, DateTime dtVal, ParameterDirection eDirection
            /// <summary>
            /// Creates a date parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dtVal">Initial Value</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDate(string szName, DateTime dtVal, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Date);
                oP.Value = dtVal.Date;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddDate -- input -- string szName, DateTime dtVal
            /// <summary>
            /// Creates a date parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dtVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDate(string szName, DateTime dtVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Date);
                oP.Value = dtVal.Date;
                return oP;
            }

            #endregion

            #region AddTime -- input -- string szName, ParameterDirection eDirection
            /// <summary>
            /// Creates a date parameter with initial value = null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddTime(string szName, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Time);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddTime -- input -- string szName, DateTime dtVal, ParameterDirection eDirection
            /// <summary>
            /// Creates a date parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dtVal">Initial Value</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddTime(string szName, DateTime dtVal, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.Time);
                oP.Value = dtVal.TimeOfDay;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddTime -- input -- string szName, DateTime dtVal
            /// <summary>
            /// Creates a time parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dtVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddTime(string szName, DateTime dtVal)
            {
                KodakDALParameter oP = Add(szName, DbType.Time);
                oP.Value = dtVal.TimeOfDay;
                return oP;
            }
            #endregion

            #region AddDateTime -- szName,eDirection
            /// <summary>
            /// Creates a timestamp parameter with initial value = null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateTime(string szName, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.DateTime);
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddDateTime -- szName,dtVal,eDirection
            /// <summary>
            /// Creates a timestamp parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dtVal">Initial Value</param>
            /// <param name="eDirection">Indicates if paramater is input, output, or return value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateTime(string szName, DateTime dtVal, ParameterDirection eDirection)
            {
                KodakDALParameter oP = Add(szName, DbType.DateTime);
                oP.Value = dtVal;
                oP.Direction = eDirection;
                return oP;
            }
            #endregion

            #region AddDateTime -- szName,oVal
            /// <summary>
            /// Creates a timestamp parameter with initial value or null
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="oVal">Initial Value or null</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateTime(string szName, object oVal)
            {
                KodakDALParameter oP = Add(szName, DbType.DateTime);
                if (oVal == null) oVal = DBNull.Value;
                oP.Value = oVal;
                return oP;
            }
            #endregion

            #region AddDateTime -- szName,dtVal
            /// <summary>
            /// Creates a timestamp parameter with initial value
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="dtVal">Initial Value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateTime(string szName, DateTime dtVal)
            {
                KodakDALParameter oP = Add(szName, DbType.DateTime);
                oP.Value = dtVal;
                return oP;
            }
            #endregion


            #region Source Column Parameters For ExecuteSave

            #region AddCharSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage

            /// <summary>
            /// Creates a fixed width character parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddCharSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddCharSC -- input -- string szName, string szSourceColumn
            /// <summary>
            /// Creates a fixed width character parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddCharSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddVarCharSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a variable width character parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarCharSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddVarCharSC -- input -- string szName, string szSourceColumn
            /// <summary>
            /// Creates a variable width character parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddVarCharSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.String);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddIntSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a 32 bit integer parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddIntSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.Int32);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddIntSC -- input -- string szName, string szSourceColumn
            /// <summary>
            /// Creates a 32 bit integer input parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddIntSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.Int32);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddSmallIntSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a 16 bit integer parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddSmallIntSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.Int16);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddSmallIntSC -- input -- string szName, string szSourceColumn
            /// <summary>
            /// Creates a 16 bit integer input parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddSmallIntSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.Int16);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddDoubleSC -- input -- string szName, string szSourceColumn
            /// <summary>
            /// Creates a floating point parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDoubleSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.Double);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddDoubleSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a floating point parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDoubleSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.Double);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddDecimalSC -- input -- string szName, string szSourceColumn

            /// <summary>
            /// Creates a floating point parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDecimalSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.Decimal);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddDecimalSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a floating point parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDecimalSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.Decimal);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddDateSC -- input -- string szName, string szSourceColumn

            /// <summary>
            /// Creates a date parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.Date);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddDateSC -- input --  string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a date parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.Date);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddTimeSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a time parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddTimeSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.Time);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddTimeSC -- input -- string szName, string szSourceColumn
            /// <summary>
            /// Creates a time parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddTimeSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.Time);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion

            #region AddDateTimeSC -- input -- string szName, string szSourceColumn, ParameterUsage eUsage
            /// <summary>
            /// Creates a timestamp parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <param name="eUsage">Indicates if parameter is used for inserts, updates, and-or deletes</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateTimeSC(string szName, string szSourceColumn, ParameterUsage eUsage)
            {
                KodakDALParameter oP = Add(szName, DbType.DateTime);
                oP.SourceColumn = szSourceColumn;
                oP.ParameterUsage = eUsage;
                return oP;
            }
            #endregion

            #region AddDateTimeSC -- input -- string szName, string szSourceColumn
            /// <summary>
            /// Creates a timestamp parameter whose value comes from a DataTable column.
            /// </summary>
            /// <param name="szName">Parameter name beginning with '@'</param>
            /// <param name="szSourceColumn">DataTable column that holds the actual value</param>
            /// <returns>new KodakDALParameter object</returns>
            public KodakDALParameter AddDateTimeSC(string szName, string szSourceColumn)
            {
                KodakDALParameter oP = Add(szName, DbType.DateTime);
                oP.SourceColumn = szSourceColumn;
                return oP;
            }
            #endregion
            #endregion

        }
        #endregion

        #region Parameter Usage
        /// <summary>
        /// Defines how parameters are used with the ExecuteSave() methods.
        /// </summary>
        [Flags]
        public enum ParameterUsage
        {
            Insert = 1,
            Update = 2,
            InsertUpdate = Insert | Update,
            Delete = 4,
            InsertDelete = Insert | Delete,
            UpdateDelete = Update | Delete,
            InsertUpdateDelete = Insert | Update | Delete
        }
        #endregion

        #region Sealed Class for Parameter
        /// <summary>
        /// This is a sealed class for the individula parameter
        /// </summary>
        public sealed class KodakDALParameter : IDbDataParameter
        {
            IDbDataParameter m_oParam;
            ParameterUsage m_eUsage;

            #region Internal Constructor
            internal KodakDALParameter(IDbDataParameter oParam)
            {
                m_oParam = oParam;
                m_eUsage = ParameterUsage.InsertUpdate;
            }
            #endregion

            #region Different Parameter properties

            #region Parameter Usage
            /// <summary>
            /// Describes which type of queries we use the parameter with on the ExecuteSave() methods.
            /// </summary>
            public ParameterUsage ParameterUsage
            {
                get
                {
                    return m_eUsage;
                }
                set { m_eUsage = value; }
            }
            #endregion

            #region DBType Properties
            /// <summary>
            /// Parameter database type
            /// </summary>
            public DbType DbType
            {
                get
                {
                    return m_oParam.DbType;
                }
                set
                {
                    m_oParam.DbType = value;
                }
            }
            #endregion

            #region Parameter Direction Properties
            /// <summary>
            /// Is parameter used for input to stored proces, output, or return value.  
            /// </summary>
            public ParameterDirection Direction
            {
                get
                {
                    return m_oParam.Direction;
                }
                set
                {
                    m_oParam.Direction = value;
                }
            }
            #endregion

            #region Can Parameter hold null values
            /// <summary>
            /// Can Parameter hold null values.
            /// </summary>
            public bool IsNullable
            {
                get
                {
                    return m_oParam.IsNullable;
                }
            }
            #endregion

            #region Paramete name
            /// <summary>
            /// Parameter names must begin with '@'.
            /// </summary>
            public string ParameterName
            {
                get
                {
                    return m_oParam.ParameterName;
                }
                set { m_oParam.ParameterName = value; }
            }
            #endregion

            #region Source column in datatable.
            /// <summary>
            /// Source column in datatable.  Used when calling ExecuteSave() methods.
            /// </summary>
            public string SourceColumn
            {
                get
                {
                    return m_oParam.SourceColumn;
                }
                set
                {
                    m_oParam.SourceColumn = value;
                }
            }
            #endregion

            #region Current Value
            /// <summary>
            /// Current value.  DBNull.Value if null.
            /// </summary>
            public object Value
            {
                get
                {
                    return m_oParam.Value;
                }
                set
                {
                    m_oParam.Value = value;
                }
            }
            #endregion

            #region  Source version
            /// <summary>
            /// Which version of a DataTable column value to use.  I.E. original or modified.
            /// </summary>
            public DataRowVersion SourceVersion
            {
                get
                {
                    return m_oParam.SourceVersion;
                }
                set
                {
                    m_oParam.SourceVersion = value;
                }
            }
            #endregion

            #region Numeric precision
            /// <summary>
            /// Numeric precision.  Places after decimal point
            /// </summary>
            public byte Precision
            {
                get
                {
                    return m_oParam.Precision;
                }
                set
                {
                    m_oParam.Precision = value;
                }
            }
            #endregion

            #region Numeric Scale.
            /// <summary>
            /// Numeric Scale.  
            /// </summary>
            public byte Scale
            {
                get
                {
                    return m_oParam.Scale;
                }
                set { m_oParam.Scale = value; }
            }
            #endregion

            #region Size in bytes
            /// <summary>
            /// Size in bytes.
            /// </summary>
            public int Size
            {
                get
                {
                    return m_oParam.Size;
                }
                set
                {
                    m_oParam.Size = value;
                }
            }
            #endregion

            #region InternalType
            internal IDbDataParameter InternalType
            {
                get { return m_oParam; }
            }
            #endregion

            #endregion
        }
        #endregion

        #region IDisposable Members
        private bool m_bDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            // Take yourself off of the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool bDisposing)
        {
            if (m_bDisposed)
                return;
            // If disposing equals true, dispose all managed 
            // and unmanaged resources.
            if (bDisposing)
            {
                if (objConnection.State != System.Data.ConnectionState.Closed)
                {
                    objConnection.Close();
                }
            }
            // 2015-09-11, Oscar changed, actually it should not dispose connection in finalizer, but...
            // so we decide to add try-catch to prevent unhandled exception.
            try
            {
                objConnection.Dispose();
            }
            catch (Exception) { }
            m_bDisposed = true;
        }
        ~RisDAL()
        {
            Dispose(false);
        }
        #endregion

        #region Async ExecuteNonQuery --For SQLServer Only
        //Before callback function is invoked,the connection must not be closed,
        //and must not execute other sql command.        
        //in connection string,must set 'Asynchronous Processing=true'.
        public void BeginExecuteNonQuery(AsyncCallback callback, string szQuery)
        {
            BeginExecuteNonQuery(callback, szQuery, CommandType.Text);
        }

        public void BeginExecuteNonQuerySP(AsyncCallback callback, string szQuery)
        {
            BeginExecuteNonQuery(callback, szQuery, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute NonQuery asynchornosily
        /// </summary>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the command's execution has completed. </param>
        /// <param name="szQuery">Query text</param>
        /// <param name="eCommandType">Command Type</param>
        protected void BeginExecuteNonQuery(AsyncCallback callback, string szQuery, CommandType eCommandType)
        {
            if (m_szDatabaseType == "Sybase")
            {
                throw new Exception("Do not support sybase Async ExecuteNonQuery!");
            }
            else if (m_szDatabaseType == "Oracle")
            {
                throw new Exception("Do not support oracle Async ExecuteNonQuery!");
            }
            else
            {
                PreparDbCommand(szQuery, eCommandType);
                SqlCommand command = (SqlCommand)objCommand;
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                command.BeginExecuteNonQuery(callback, this);
            }
        }

        public int EndExecuteNonQuery(IAsyncResult result)
        {
            SqlCommand command = (SqlCommand)objCommand;
            return command.EndExecuteNonQuery(result);
        }

        #endregion
    }
    #endregion
}

