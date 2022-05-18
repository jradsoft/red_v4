using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;

namespace wpfEFac.Models
{
    public class ConnectorOleDb
    {
         OdbcConnection nwindConn;

         public ConnectorOleDb(string excelfile)
		{
			//
			// TODO: Add constructor logic here
			//
            
            
            String connect;
            
            connect = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + excelfile + ";" + "Extended Properties=Paradox 5.0;";
                        
			nwindConn = new OdbcConnection(connect);
	
			nwindConn.Open();	
		}
        
        public OdbcConnection  get_conn()
        {
            return (nwindConn);
        }
        
        public DataTable select(string sql)
        {
            OdbcCommand cmd = nwindConn.CreateCommand();
            cmd.CommandText = sql;


            OdbcDataAdapter custDA = new OdbcDataAdapter();
            custDA.SelectCommand = cmd;

            DataSet ds = new DataSet();
            custDA.Fill(ds, "data");
            return (ds.Tables[0]);

        }

    }
}
