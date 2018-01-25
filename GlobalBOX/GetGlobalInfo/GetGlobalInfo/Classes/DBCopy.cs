using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace Pulsar
{
    public class DBCopy
    {
        public static void copy(string db1, string db2)
        {
            DataTable schemaTable;
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data source=" + db1; //"Test1.mdb";
            conn.Open();
            cmd.Connection = conn;
            
            if (File.Exists(db1))
            {
                String FileName = (Path.GetDirectoryName(db1) + @"\" + Path.GetFileNameWithoutExtension(db1) + "_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString().Replace(":", "-") + Path.GetExtension(db1)).Replace("/", "-");
                try
                {
                    File.Copy(db1, FileName);
                }
                catch (Exception)
                {
                }                
            }

            string templetDataTable = db2; //@"Test2.mdb";
            string clientDataTable = db1; //@"c:\Test1.mdb";
            //string templetBackupDataTable = @"c:\TestBackup.mdb";
            if (File.Exists(templetDataTable))
            {   //**********MDB Data Migration**************//
                schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                //1. Copy Existing Data from  Clientfolder to the New Templet
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string fields = GetTableFieldsFromReader(@"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data source=" + db1, schemaTable.Rows[i].ItemArray[2].ToString());
                    string query = "INSERT INTO " + schemaTable.Rows[i].ItemArray[2].ToString() + " IN '" + templetDataTable + "' SELECT " + fields + " FROM " + schemaTable.Rows[i].ItemArray[2].ToString() + "";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    try
                    {
                        int cmdresults = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //txtErrorDetails.Visible = true;
                        //txtErrorDetails.Text = ex.ToString();
                        //errorLogger = new StreamWriter(@"ErrorLog" + DateTime.Now.ToString("ddMMyyyyMMhhss") + ".Log");
                        //errorLogger.WriteLine(txtErrorDetails.Text + " \n Error While Updating Table.."
                        //                       + schemaTable.Rows[i].ItemArray[2].ToString() + "{" + ex.ToString() + "}");
                        //errorLogger.Flush();
                        continue;
                    }
                }

                conn.Close();
                //try
                //{
                //    //2. Move Client MDB to seperate folder
                //    File.Move(clientDataTable, templetBackupDataTable);
                //    //3. Move Updated templet MDB to Client application data folder
                //    File.Move(templetDataTable, clientDataTable);
                //    //4. Delete old Client MDB 
                //    // File.Delete(templetBackupDataTable);
                //}
                //catch (Exception ex)
                //{
                //}
            }
        }


        //static void CreateTableFromReader(string tableName, SqlDataReader reader)
        private static String GetTableFieldsFromReader(string connection_string, string tableName)
        {
            string fields = null;

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connection_string))
                {
                    string sSql = string.Empty;

                    sSql = "SELECT * FROM [" + tableName + "]";

                    OleDbCommand cmd = new OleDbCommand(sSql, conn);
                    conn.Open();

                    OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                    //DataTable schemaTable = rdr.GetSchemaTable();                    


                    List<string> columns = new List<string>();
                    //string createTable = @"CREATE TABLE {0} ({1})";

                    var dt = reader.GetSchemaTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        columns.Add(String.Format("[{0}]", dr["ColumnName"]));
                    }

                    fields = String.Join(", ", columns.ToArray());
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Unable to attach to this table with current user; check database security permissions.", "Field Information");
            }

            return fields;
        }
    }
}

