using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class OracleDBHelper
    {

        public static string ConnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ZHCGDB"].ConnectionString;
            }
        }

        public static OracleConnection Connection
        {
            get
            {
                return new OracleConnection(ConnString);
            }
        }

        /// <summary>
        /// 获取数据库数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet Get_DataSet(string sql)
        {
            DataSet ds = new DataSet();

            OracleConnection conn = new OracleConnection(ConnString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            oda.Fill(ds);
            if (conn.State == ConnectionState.Open)
                conn.Close();

            return ds;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>  
        public static bool ExecuteSqlTran(List<Dictionary<string, OracleParameter[]>> list)
        {
            bool re = false;
            using (OracleConnection connection = new OracleConnection(ConnString))
            {
                connection.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection;
                OracleTransaction tx = connection.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        foreach (var item in list[i])
                        {
                            string key = item.Key;
                            OracleParameter[] values = item.Value;
                            if (key.Trim().Length > 1)
                            {
                                cmd.CommandText = key;
                                if (values != null)
                                {
                                    cmd.Parameters.AddRange(values);
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tx.Commit();
                    re = true;
                }
                catch (Exception E)
                {
                    FileStream fs = new FileStream("D:\\Q.txt", FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.WriteLine(E.Message);
                    sw.Close();
                    fs.Close();

                    re = false;
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
            return re;

        }

        public static int ExecuteCommand(string sql)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection.Open();
           
            try
            {
                int result = cmd.ExecuteNonQuery();
                try
                {
                    FileStream fs = new FileStream("D:\\Q.txt", FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.WriteLine(result);
                    sw.WriteLine("QQQQQQQQ");
                    sw.Close();
                    fs.Close();
                }
                catch (Exception e)
                {

                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                FileStream fs = new FileStream("d:\\w.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(e.Message.ToString());

                sw.Close();
                fs.Close();
                return 0;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        public static int ExecuteCommand(string sql, OracleParameter[] values)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection.Open();
            try
            {
                cmd.Parameters.AddRange(values);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                FileStream fs = new FileStream("d:\\T.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(e.Message);

                sw.Close();
                fs.Close();
                return 0;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        public static int GetScalar(string sql)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection.Open();
            try
            {
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        public static int GetScalar(string sql, OracleParameter[] values)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection.Open();
            try
            {
                cmd.Parameters.AddRange(values);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        public static OracleDataReader GetReader(string sql)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection.Open();
            OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public static OracleDataReader GetReader(string sql, OracleParameter[] values)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            cmd.Connection.Open();
            OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public static DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            OracleCommand cmd = new OracleCommand(sql, Connection);
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            finally
            {
                cmd.Connection.Dispose();
            }
        }

        public static DataSet GetDataSet(string sql, OracleParameter[] values)
        {
            DataSet ds = new DataSet();
            OracleCommand cmd = new OracleCommand(sql, Connection);
            try
            {
                cmd.Parameters.AddRange(values);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            finally
            {
                cmd.Connection.Dispose();
            }
        }
    }
}
