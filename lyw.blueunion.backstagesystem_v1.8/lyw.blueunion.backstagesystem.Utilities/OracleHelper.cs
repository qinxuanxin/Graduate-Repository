using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OracleClient;
using System.Data;

namespace lyw.blueunion.backstagesystem.Utilities
{

    /// <summary>
    /// OracleServer数据库访问的通用工具类
    /// </summary>
    public abstract class OracleHelper
    {
        //只读的静态数据库连接字符串
        public static readonly string connString = ConfigurationManager.ConnectionStrings["constr"].ToString();

        #region 执行 增 删 改
        /// <summary>
        /// 执行 增 删 改
        /// </summary>
        /// <param name="Oracle">要执行的Oracle</param>
        /// <param name="param">参数</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string sql, params OracleParameter[] param)
        {
            //实例化连接对象，并指定连接字符串，自动释放资源，不用关闭
            using (OracleConnection conn = new OracleConnection(connString))
            {
                //实例化命令对象，指定Oracle，与连接对象
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    //如果有参数
                    if (param != null)
                    {
                        //批量添加参数
                        cmd.Parameters.AddRange(param);
                    }
                    //打开连接
                    conn.Open();
                    //执行Oracle并返回影响行数
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region 执行 查询
        /// <summary>
        /// 执行 查询
        /// </summary>
        /// <param name="Oracle">要执行的Oracle</param>
        /// <param name="param">参数</param>
        /// <returns>数据集</returns>
        public static OracleDataReader ExecuteReader(string Oracle, params OracleParameter[] param)
        {
            //实例化连接对象，并指定连接字符串
            OracleConnection conn = new OracleConnection(connString);
            //实例化命令对象，指定Oracle，与连接对象
            using (OracleCommand cmd = new OracleCommand(Oracle, conn))
            {
                //如果有参数
                if (param != null)
                {
                    //批量添加参数
                    cmd.Parameters.AddRange(param);
                }
                //打开连接
                conn.Open();
                //执行Oracle并返回影响行数,如果将返回的OracleDataReader关闭时也将关闭连接
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        /// <summary>
        /// 执行查询存储过程，并且返回一个OracleDataReader对象(使用OracleDataReader对象执行)
        /// </summary>
        /// <param name="sql">存储过程名</param>
        /// <param name="ps">存储过程中需要的参数</param>
        /// <returns>读取器对象</returns>
        public static OracleDataReader ExecuteReaderProc(string Oracle, params OracleParameter[] param)
        {
            //实例化连接对象，并指定连接字符串
            OracleConnection conn = new OracleConnection(connString);
            //实例化命令对象，指定Oracle，与连接对象
            using (OracleCommand cmd = new OracleCommand(Oracle, conn))
            {
                //如果有参数
                if (param != null)
                {
                    //批量添加参数
                    cmd.Parameters.AddRange(param);
                }
                //打开连接
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                //执行Oracle并返回影响行数,如果将返回的OracleDataReader关闭时也将关闭连接
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        #endregion

        #region 完成数据的查询，返回DataTable
        /// <summary>
        /// 完成数据的查询，返回DataTable
        /// </summary>
        /// <param name="Oracle">要执行的Oracle</param>
        /// <param name="param">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string sql, params OracleParameter[] param)
        {
            //实例化连接对象，并指定连接字符串，自动释放资源，不用关闭
            using (OracleConnection conn = new OracleConnection(connString))
            {
                OracleDataAdapter adp = new OracleDataAdapter(sql, conn);
                if (param != null)
                {
                    adp.SelectCommand.Parameters.AddRange(param);
                }
                DataTable dt = new DataTable();
                adp.Fill(dt);
                return dt;
            }
        }
        #endregion
        #region 返回首行首列
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="OracleSql">要执行的OracleSql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static int ExecuteCount(string OracleSql, params OracleParameter[] param)
        {
            //实例化连接对象，并指定连接字符串
            OracleConnection conn = new OracleConnection(connString);
            //实例化命令对象，指定Oracle，与连接对象
            using (OracleCommand cmd = new OracleCommand(OracleSql, conn))
            {
                //如果有参数
                if (param != null)
                {
                    //批量添加参数
                    cmd.Parameters.AddRange(param);
                }
                //打开连接
                conn.Open();
                //执行Oracle并返回影响行数,如果将返回的OracleDataReader关闭时也将关闭连接
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        #endregion

        internal static void Close()
        {
            throw new NotImplementedException();
        }
    }
}
    

