using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;


class DbWrapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="conStr"></param>
    /// <returns></returns>
    public static SortedList<int, int> getOrderCodes(string conStr)
    {
        SortedList<int,int> Codes = new SortedList<int, int>();
        OleDbConnection con = new OleDbConnection(conStr);
        con.Open();
        if (con.State == System.Data.ConnectionState.Open)
        {
            Program.Logger("Соединение БД установлено");

            OleDbCommand cmd = new OleDbCommand("SELECT CODE FROM OrderCards ORDER BY CODE", con);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Codes.Add(reader.GetInt32(0), 0);
                }
            }
            catch (OleDbException e)
            {
                Program.Logger(e.Message);
                return Codes;
            }

            con.Close();
            Program.Logger("Соединение БД закрыто");
        }
        else
        {
            Program.Logger("Ошибка подключения к БД");
            return Codes;
        }
        return Codes;
    }

    /// <summary>
    /// Сохранить заказы в БД
    /// </summary>
    /// <param name="conStr"></param>
    /// <param name="cards"></param>
    public static void saveOrders(string conStr, List<OrderCard> cards)
    {
        string strCmd = "INSERT INTO OrderCards(CODE, NOMER_ZAKAZA, NAIM_ZAKAZA, OBOZN_DSE, NAIMEN_DSE, OBOZN_OSNAS, NAIM_OSNAS, " +
                        "OSNOVANIE, KOLVO, SROK_PLAN) VALUES (?,?,?,?,?,?,?,?,?,?)";
        OleDbConnection con = new OleDbConnection(conStr);
        con.Open();
        Program.Logger("Соединение БД установлено");
        if (con.State == System.Data.ConnectionState.Open)
        {

            OleDbCommand cmd = new OleDbCommand(strCmd, con);
            OleDbParameter p1 = new OleDbParameter();
            OleDbParameter p2 = new OleDbParameter();
            OleDbParameter p3 = new OleDbParameter();
            OleDbParameter p4 = new OleDbParameter();
            OleDbParameter p5 = new OleDbParameter();
            OleDbParameter p6 = new OleDbParameter();
            OleDbParameter p7 = new OleDbParameter();
            OleDbParameter p8 = new OleDbParameter();
            OleDbParameter p9 = new OleDbParameter();
            OleDbParameter p10 = new OleDbParameter();
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);
            cmd.Parameters.Add(p10);
            
            try
            {
                foreach (OrderCard card in cards)
                {
                    p1.Value = card.CODE;
                    p2.Value = card.KZNAME;
                    
                    if (card.OSNNAME!="")
                        p3.Value = card.OSNNAME;
                    else
                        p3.Value = DBNull.Value;

                    if (card.DSESIGN != "")
                        p4.Value = card.DSESIGN;
                    else
                        p4.Value = DBNull.Value;

                    if (card.DSENAME != "")
                        p5.Value = card.DSENAME;
                    else
                        p5.Value = DBNull.Value;

                    if (card.OSNSIGN != "")
                        p6.Value = card.OSNSIGN;
                    else
                        p6.Value = DBNull.Value;

                    if (card.OSNNAME != "")
                        p7.Value = card.OSNNAME;
                    else
                        p7.Value = DBNull.Value;

                    if (card.OSNOVANIE != "")
                        p8.Value = card.OSNOVANIE;
                    else
                        p8.Value = DBNull.Value;

                    p9.Value = card.QUANTITY;
                    p10.Value = card.MAKEPLAN_DATE;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (OleDbException e)
            {
                Program.Logger(e.Message);
            }

            con.Close();
            Program.Logger("Соединение БД закрыто");
        }
    }
}

