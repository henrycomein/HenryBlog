using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Henry.Common;


namespace Henry.DataAccessLayer
{
    internal static class SqlParamerCheck
    {
        public static string CheckSqlParamer(this string s)
        {
            if (s == null) return string.Empty;
            else return s.Replace("'", "''");
        }
        /// <summary>
        /// 检验xml变量
        /// </summary>
        /// <param name="xmldata">xml变量</param>
        /// <returns></returns>
        public static string CheckSqlXmlParamer(this string xmldata)
        {
            if (xmldata == null)
                return string.Empty;
            return xmldata.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;");
        }
    }
    public class MySqlHelper 
    {
        private static readonly string _connectString = ConfigurationManager.ConnectionStrings["DbConnectString"].ConnectionString;
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="sqlString">sql命令语句</param>
        /// <returns>符合要求的数据</returns>
        public static DataTable ExecuteQueryList(string sqlString)
        {
            return ExecuteQueryList(sqlString, null);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="sqlString">sql命令语句</param>
        /// <param name="paramers">参数</param>
        /// <returns>符合要求的数据</returns>
        public static DataTable ExecuteQueryList(string sqlString,params SqlParameter[] paramers)
        {
            DataTable result = null;
            try
            {
                using (var connection = new SqlConnection(_connectString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var command = new SqlCommand(sqlString, connection))
                    {
                        if (paramers != null) command.Parameters.AddRange(paramers);
                        var adapter = new SqlDataAdapter(command);
                        DataSet resultDataSet = new DataSet();
                        adapter.Fill(resultDataSet,"ds");
                        result = resultDataSet.Tables[0];
                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.DbError(e);
            }
            return result;
        }

        /// <summary>
        /// 获取数据并分页
        /// </summary>
        /// <param name="tablename">表名称</param>
        /// <param name="colname">列名称</param>
        /// <param name="condition">条件语句</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">每页显示条数</param>
        /// <param name="totalcount">总共数据条数</param>
        /// <returns></returns>
        public static DataTable ExecuteQueryListWithPage(string tablename,string colname,string condition,int pageindex,int pagesize,out int totalcount)
        {
            DataTable result = null;
            totalcount=0;
            try
            {
                var sqlString = string.Format("EXECUTE Paging @tableName=N'{0}',@fieldName=N'{1}',@sqlWhereStr=N'{2}',@pageIndex={3},@pageSize={4}",
                   tablename, colname.CheckSqlParamer(), condition.CheckSqlParamer(), pageindex, pagesize);
                using (var connection = new SqlConnection(_connectString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var command = new SqlCommand(sqlString, connection))
                    {
                        var adapter = new SqlDataAdapter(command);
                        DataSet resultDataSet = new DataSet();
                        adapter.Fill(resultDataSet, "ds");
                        result = resultDataSet.Tables[0];
                        totalcount=Convert.ToInt32(resultDataSet.Tables[1].Rows[0][0]);
                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.DbError(e);
            }
            return result;
        }
        /// <summary>
        /// 自定义查询
        /// </summary>
        /// <param name="sqlString">sql命令语句</param>
        /// <returns>DataSet容器</returns>
        public static DataSet ExecuteCustomQuery(string sqlString)
        {
            return ExecuteCustomQuery(sqlString, null);
        }

        /// <summary>
        /// 自定义查询
        /// </summary>
        /// <param name="sqlString">sql命令语句</param>
        /// <param name="paramers">参数</param>
        /// <returns>DataSet容器</returns>
        public static DataSet ExecuteCustomQuery(string sqlString,params SqlParameter[] paramers)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(_connectString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var command = new SqlCommand(sqlString, connection))
                    {
                        if (paramers != null) command.Parameters.AddRange(paramers);
                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(ds,"ds");
                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.DbError(e);
            }
            return ds;
        }

        public static object ExecuteScalar(string sqlString)
        {
            return ExecuteScalar(sqlString, null);
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="sqlString">sql命令语句</param>
        /// <returns>单个值</returns>
        public static object ExecuteScalar(string sqlString, params SqlParameter[] paramers)
        {
            Object result = null;
            try
            {
                using (var connection = new SqlConnection(_connectString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var command = new SqlCommand(sqlString, connection))
                    {
                        if (paramers != null) command.Parameters.AddRange(paramers);
                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.DbError(e);
            }
            return result;
        }

        /// <summary>
        /// 执行增删改动作
        /// </summary>
        /// <param name="sqlString">命令语句</param>
        /// <returns>操作结果：成功或失败</returns>
        public static bool ExecuteNoQuery(string sqlString)
        {
            return ExecuteNoQuery(sqlString, null);
        }

        /// <summary>
        /// 执行增删改动作
        /// </summary>
        /// <param name="sqlString">命令语句</param>
        /// <returns>操作结果：成功或失败</returns>
        public static bool ExecuteNoQuery(string sqlString,params SqlParameter[] paramer)
        {
            bool result = false;
            try
            {
                using (var connection = new SqlConnection(_connectString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var command = new SqlCommand(sqlString, connection))
                    {
                        if (paramer != null) command.Parameters.AddRange(paramer);
                        int effectRows = command.ExecuteNonQuery();
                        result = effectRows > 0 ? true : false;
                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.DbError(e);
            }
            return result;
        }

    }
}
