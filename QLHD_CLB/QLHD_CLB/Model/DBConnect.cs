using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QLHD_CLB.Model
{
    class DBConnect
    {
        string constr = "Data Source = THAIBINH-LAPTOP; Initial Catalog = QuanLyCauLacBo; User ID = sa; Password = 123";
        public SqlConnection con { get; set; }

        public DBConnect()
        {
            con = new SqlConnection(constr);
        }

        public void openConnect()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeConnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        // Thêm, sửa, xoá dữ liệu
        public int getNonQuery(string sql)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;

            int k = cmd.ExecuteNonQuery();
            con.Close();
            return k;
        }

        // Lấy dữ liệu
        public DataTable getSqlDataAdapter(string sql)
        {
            openConnect();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            closeConnect();
            return dt;
        }

        // Lấy giá trị trả về như COUNT, SUM, MAX, MIN, AVG
        public object getScalar(string sql)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            object k = cmd.ExecuteScalar();
            con.Close();
            return k;
        }




    }
}
