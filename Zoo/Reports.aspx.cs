using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Zoo
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Report();
        }

        private void Report()
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения

            #region Отчет по сотрудникам
            Table1();

            decimal Emp = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                decimal ZapEmp = Decimal.Parse(row.Cells[1].Text);
                Emp += ZapEmp;
            }
            Label1.Text = Emp.ToString() + " рублей";

            //Суммарно за год
            con.Open();
            string query1 = @"select Sum([ZapEmp])*12 as Zap from Employee";
            SqlCommand command1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(command1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            Label2.Text = dt1.Rows[0][0].ToString() + " рублей";
            command1.ExecuteNonQuery();
            con.Close();
            #endregion

            #region Диаграмма 1
            string query = "SELECT NamePos, Sum([ZapEmp]) AS Zap FROM Employee LEFT JOIN Position ON Employee.PositionId = Position.PositionId GROUP BY NamePos";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable ChartData = ds.Tables[0];

            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                XPointMember[count] = ChartData.Rows[count]["NamePos"].ToString();
                YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Zap"]);
            }
            Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.Series[0]["PieLabelStyle"] = "Outside";
            Chart1.Series[0]["PieDrawingStyle"] = "SoftEdge";
            #endregion

            #region Отчет по вольерам
            Table2();

            decimal Cells = 0;
            foreach (GridViewRow row in GridView2.Rows)
            {
                decimal PriceCell = Decimal.Parse(row.Cells[1].Text);
                Cells += PriceCell;
            }
            Label3.Text = Cells.ToString() + " рублей";
            #endregion

            #region Диаграмма 2
            string query2 = "SELECT NameCell, sum(PriceCell) as  PriceCell FROM Cells group by NameCell ORDER BY PriceCell";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            DataTable ChartData2 = ds2.Tables[0];

            string[] XCell = new string[ChartData2.Rows.Count];
            int[] YCell = new int[ChartData2.Rows.Count];

            for (int count = 0; count < ChartData2.Rows.Count; count++)
            {
                XCell[count] = ChartData2.Rows[count]["NameCell"].ToString();
                YCell[count] = Convert.ToInt32(ChartData2.Rows[count]["PriceCell"]);
            }
            Chart2.Series[0].Points.DataBindXY(XCell, YCell);
                 
            Chart2.Series[0].ChartType = SeriesChartType.Pie;
            Chart2.Series[0]["PieLabelStyle"] = "Outside";
            Chart2.Series[0]["PieDrawingStyle"] = "SoftEdge";
            #endregion

            #region Отчет по корму
            Table3();

            decimal Food = 0;
            foreach (GridViewRow row in GridView3.Rows)
            {
                decimal PriceFood = Decimal.Parse(row.Cells[1].Text);
                Food += PriceFood;
            }
            Label4.Text = Food.ToString() + " рублей";
            #endregion

            #region Диаграмма 3
            string query3 = "select TypeFood, sum(PriceFood) as PriceFood from Foods  GROUP BY TypeFood ORDER BY PriceFood";
            SqlDataAdapter da3 = new SqlDataAdapter(query3, con);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            DataTable ChartData3 = ds3.Tables[0];

            string[] XFood = new string[ChartData3.Rows.Count];
            int[] YFood = new int[ChartData3.Rows.Count];

            for (int count = 0; count < ChartData3.Rows.Count; count++)
            {
                XFood[count] = ChartData3.Rows[count]["TypeFood"].ToString();
                YFood[count] = Convert.ToInt32(ChartData3.Rows[count]["PriceFood"]);
            }
            Chart3.Series[0].Points.DataBindXY(XFood, YFood);
                 
            Chart3.Series[0].ChartType = SeriesChartType.Pie;
            Chart3.Series[0]["PieLabelStyle"] = "Outside";
            Chart3.Series[0]["PieDrawingStyle"] = "SoftEdge";
            #endregion
        }

        #region Таблица 1
        private void Table1()
        {
            string strConn = "Server=KOMOR;Database=Zoo;Trusted_Connection=True";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            con.Open();
            string sql = @" SELECT NamePos, Sum([ZapEmp]) AS Zap 
                            FROM Employee
                            LEFT JOIN Position
                            ON Employee.PositionId=Position.PositionId
                            GROUP BY NamePos";
            SqlCommand com = new SqlCommand(sql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }
        #endregion

        #region Таблица2
        private void Table2()
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            con.Open();
            string sql = @" SELECT NameCell, sum(PriceCell) as PriceCell 
                            FROM cells group by NameCell
                            ORDER BY PriceCell";
            SqlCommand com = new SqlCommand(sql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            con.Close();
        }
        #endregion

        #region Таблица3
        private void Table3()
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            con.Open();
            string sql = @" SELECT TypeFood, sum(PriceFood) as PriceFood 
                            FROM Foods  
                            GROUP BY TypeFood
                            ORDER BY PriceFood";
            SqlCommand com = new SqlCommand(sql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            GridView3.DataSource = dt;
            GridView3.DataBind();
            con.Close();
        }
        #endregion
    }
}