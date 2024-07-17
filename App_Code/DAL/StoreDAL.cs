using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// StoreDAL 的摘要说明
/// </summary>
public class StoreDAL
{
    string strCon = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
    public bool Payday(double salary)
    {
        return true;
    }
    /// <summary>
    /// 按时间降序排列的公告
    /// </summary>
    /// <returns></returns>
    public DataSet Announcement()
    {
        DataSet ds = new DataSet();
        string str = "select * from announcement where isdeleted=0 order by time desc";
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str,conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    /// <summary>
    /// 最新的公告的发布时间
    /// </summary>
    /// <returns></returns>
    public string LateTime()
    {
        string time = null;
        string str = "select time from announcement where isdeleted=0 order by time desc limit 1";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            time = cmd.ExecuteScalar()?.ToString();
            conn.Close();
            return time;
        }
    }
    public DataSet AnnouncementRemoved()
    {
        DataSet ds = new DataSet();
        string str = "select * from announcement where isdeleted=1 order by time desc";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();

            return ds;
        }
    }
    public bool AddAnnouncement(Announcement announcement)
    {
        string str = "insert into announcement(name,detail,time,isdeleted,owner) values(@name,@detail,@time,0,@owner)";
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@name", announcement.name);
            cmd.Parameters.AddWithValue("@detail", announcement.detail);
            cmd.Parameters.AddWithValue("@time", announcement.createtime.ToString("G"));
            cmd.Parameters.AddWithValue("@owner", announcement.owner);
            conn.Open();
            int i= cmd.ExecuteNonQuery();
            return i > 0;
        }
    }
    public bool RemoveAnnouncement(int id)
    {
        string str = "update announcement set isdeleted = 1 where id =@id";
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd =new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            return i > 0;
        }
    }
    public bool ReUpAnnouncement(int id)
    {
        string str = "update announcement set isdeleted = 0 where id =@id";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            return i > 0;
        }
    }
    public List<string> AnnouncementSingle(int id)
    {
        string str = "select * from announcement where id=@id";
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            List<string> T = new List<string>();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr[i].ToString() != "")
                    {
                        T.Add(dr[i].ToString());
                    }
                    else
                    {
                        T.Add("");
                    }
                }
            }
            //T.RemoveAt(T.Count-1);
            return T;
        }
    }
    /// <summary>
    /// 资产，重点就是一个钱
    /// </summary>
    /// <returns></returns>
    public List<string> Asset()
    {
        string str = "select name,money from asset where id = 1";
        using(MySqlConnection conn=new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            List<string> T = new List<string>();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr[i].ToString() != "")
                    {
                        T.Add(dr[i].ToString());
                    }
                    else
                    {
                        T.Add("");
                    }
                }
            }
            //T.RemoveAt(T.Count-1);
            return T;
        }
    }
    /// <summary>
    /// 资产使用
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool NewAsset(double money)
    {
        string str = "update asset set money = money + @money where id =1";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@money", money);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            return i > 0;
        }
    }
    /// <summary>
    /// 资产情况记录
    /// </summary>
    /// <returns></returns>
    public DataSet AssetRecords()
    {
        DataSet ds = new DataSet();
        string str = "select * from assetrecords order by time desc";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    public string AssetRecordsSingle()
    {
        string str = "select sum(records) from assetrecords where reason='出库' or reason='入库'";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            string ans = cmd.ExecuteScalar()?.ToString();
            conn.Close();

            return ans;
        }
    }
    /// <summary>
    /// 添加资产情况记录
    /// </summary>
    /// <param name="money"></param>
    /// <param name="reason"></param>
    /// <param name="remain">由计算得出，当前账户余额-money。可能会导致负债</param>
    /// <returns></returns>
    public bool NewRecords(double money, string reason,double remain)
    {
        string str = "insert into assetrecords(time,records,remain,reason) values(@time,@records,@remain,@reason)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("G"));
            cmd.Parameters.AddWithValue("@records", money);
            cmd.Parameters.AddWithValue("@remain", remain);
            cmd.Parameters.AddWithValue("@reason", reason);
            conn.Open();
            int i= cmd.ExecuteNonQuery();
            return i > 0;
        }
    }

    /// <summary>
    /// 员工的商品入库记录功能，入库操作完成后检查确认为0，不确认的话无法入库。最好在员工入库操作后及时确认信息
    /// </summary>
    /// <param name="goods">商品信息</param>
    /// <param name="uaccount">员工账号</param>
    /// <param name="pname">供货商名</param>
    /// <param name="cost">进货开销</param>
    /// <returns>true完成操作；false失败</returns>
    public bool Stockinlog(Goods goods, string uaccount, string pname, double percost)
    {
        string str = //"update goodsstockinlog set count = @count where name = @gname," +
            "insert into goodsstockinlog (gname,uaccount,pname,count,notes,time,percost,cost,ischecked)" +
            "values(@gname,@uaccount,@pname,@count,@notes,@time,@percost,@cost,0)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gname", goods.Gname);
            cmd.Parameters.AddWithValue("@uaccount", uaccount);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@count", goods.Gcount);
            cmd.Parameters.AddWithValue("@notes", goods.notes);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("G"));
            cmd.Parameters.AddWithValue("@percost", percost);
            cmd.Parameters.AddWithValue("@cost", percost * goods.Gcount);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i == 0)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 店长的确认入库功能，修改检查状态为1，对应商品数量增加，产生开销
    /// </summary>
    /// <param name="id">入库ID</param>
    /// <param name="count">该单入库货物量</param>
    /// <param name="gname">货物名</param>
    /// <param name="cost">开销</param>
    /// <returns></returns>
    public bool CheckStockin(int id, double count, string gname, double cost)
    {
        string str = "update goodsstockinlog set ischecked = 1 where id = @id;" +
                     "update goods set gcount = gcount + @count where gname = @gname;" +
                     "update asset set money = money - @cost;"+
                     "insert into assetrecords(time,records,reason) values(@time,@records,@reason)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@count", count);
            cmd.Parameters.AddWithValue("@gname", gname);
            cmd.Parameters.AddWithValue("@cost", cost);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("G"));
            cmd.Parameters.AddWithValue("@records", -cost);
            cmd.Parameters.AddWithValue("@reason", "入库");
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i > 0;
        }
    }
    public DataSet SelectStockinlog()
    {
        DataSet ds = new DataSet();
        string str = "select * from goodsstockinlog order by id desc";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    /// <summary>
    /// 商品类型进货花费统计
    /// </summary>
    /// <returns></returns>
    public DataSet SelectStockinPricebyGtype()
    {
        DataSet ds = new DataSet();
        string str = "select gtype, sum(goodsstockinlog.cost) as cost from goodsstockinlog join goods on goodsstockinlog.gname = goods.gname where ischecked=1 GROUP BY gtype";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    /// <summary>
    /// 商品类型进货量统计
    /// </summary>
    /// <returns></returns>
    public DataSet SelectStockinCountbyGtype()
    {
        DataSet ds = new DataSet();
        string str = "select gtype, sum(goodsstockinlog.count) as count from goodsstockinlog join goods on goodsstockinlog.gname = goods.gname where ischecked=1 GROUP BY gtype";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    public DataSet SelectStockinlogPersonal(string Uaccount)
    {
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        string str = "select * from goodsstockinlog where uaccount =@uaccount order by time desc";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@uaccount", Uaccount);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
    }
    public bool DeleteStockinlogPersonal(int id)
    {
        string str = "delete from goodsstockinlog where id =@id and ischecked=0";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i > 0;
        }
    }
    public List<string> SelectStockinlogSingle(int id)
    {
        string str = "select * from goodsstockinlog where id =@id ";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            List<string> T = new List<string>();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr[i].ToString() != "")
                    {
                        T.Add(dr[i].ToString());
                    }
                    else
                    {
                        T.Add("");
                    }
                }
            }
            //T.RemoveAt(T.Count-1);
            return T;
        }
    }
    public bool UpdateStockinlogPersonal(int id, double count, double percost, string notes)
    {
        string str = "update goodsstockinlog set count=@count,percost=@percost,cost=@cost,notes=@notes where id =@id and ischecked=0";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@count", count);
            cmd.Parameters.AddWithValue("@percost", percost);
            cmd.Parameters.AddWithValue("@cost", percost*count);
            cmd.Parameters.AddWithValue("@notes", notes);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i > 0;
        }
    }
    /// <summary>
    /// 员工的商品出库记录功能，出库操作完成后检查确认为0，不确认的话无法出库。最好在员工出库操作后及时确认信息
    /// </summary>
    /// <param name="goods">出库的商品信息</param>
    /// <param name="uaccount">员工账号</param>
    /// <returns>true记录成功，false记录失败</returns>
    public bool Stockoutlog(Goods goods, string uaccount)
    {
        string str = "insert into goodsstockoutlog(gname,uaccount,count,notes,time,perprice,ischecked,price) " +
            "values (@gname,@uaccount,@count,@notes,@time,@perprice,0,@price)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gname", goods.Gname);
            cmd.Parameters.AddWithValue("@uaccount", uaccount);
            cmd.Parameters.AddWithValue("@count", goods.Gcount);
            cmd.Parameters.AddWithValue("@notes", goods.notes);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("G"));
            cmd.Parameters.AddWithValue("@perprice", goods.Price);
            cmd.Parameters.AddWithValue("@price", goods.Price * goods.discount * goods.Gcount);//售价为单价*折扣*数量
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i != 0)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 店长的确认出库功能，修改检查状态为1，对应商品数量增加，获得收益
    /// </summary>
    /// <param name="id">出库ID</param>
    /// <param name="count">该单出库货物量</param>
    /// <param name="gname">货物名</param>
    /// <param name="price">收入</param>
    /// <returns>true如果操作成功；false操作失败</returns>
    public bool CheckStockout(int id, double count, string gname, double price)
    {
        string str = "update goodsstockoutlog set ischecked = 1 where id = @id;" +
                    "update goods set gcount = gcount - @count where gname = @gname;" +
                    "update asset set money = money + @price;" +
                    "insert into assetrecords(time,records,reason) values(@time,@records,@reason)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@count", count);
            cmd.Parameters.AddWithValue("@gname", gname);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("G"));
            cmd.Parameters.AddWithValue("@records", price);
            cmd.Parameters.AddWithValue("@reason", "出库");
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i != 0)
            {
                return true;
            }
        }
        return false;
    }
    public DataSet SelectStockoutlog()
    {
        DataSet ds = new DataSet();
        string str = "select * from goodsstockoutlog order by time desc";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    /// <summary>
    /// 商品类型销售额统计
    /// </summary>
    /// <returns></returns>
    public DataSet SelectStockoutPricebyGtype()
    {
        DataSet ds = new DataSet();
        string str = "select gtype, sum(goodsstockoutlog.price) as price from goodsstockoutlog join goods on goodsstockoutlog.gname = goods.gname where ischecked=1 GROUP BY gtype";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    /// <summary>
    /// 商品类型销售量统计
    /// </summary>
    /// <returns></returns>
    public DataSet SelectStockoutCountbyGtype()
    {
        DataSet ds = new DataSet();
        string str = "select gtype, sum(goodsstockoutlog.count) as count from goodsstockoutlog join goods on goodsstockoutlog.gname = goods.gname where ischecked=1 GROUP BY gtype";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    public DataSet SelectStockoutlogPersonal(string Uaccount)
    {
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        string str = "select * from goodsstockoutlog where uaccount =@uaccount order by time desc";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@uaccount", Uaccount);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
    }
    public bool DeleteStockoutlogPersonal(int id)
    {
        string str = "delete from goodsstockoutlog where id =@id and ischecked=0";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i > 0;
        }
    }
    public List<string> SelectStockoutlogSingle(int id)
    {
        string str = "select * from goodsstockoutlog where id =@id ";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            List<string> T = new List<string>();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr[i].ToString() != "")
                    {
                        T.Add(dr[i].ToString());
                    }
                    else
                    {
                        T.Add("");
                    }
                }
            }
            //T.RemoveAt(T.Count-1);
            return T;
        }
    }
    public bool UpdateStockoutlogPersonal(int id, double count,double discount , double perprice, string notes ,bool isspecial)
    {
        string str = "update goodsstockoutlog set count=@count,perprice=@perprice,notes=@notes,price=@price where id =@id and ischecked=0";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@count", count);
            cmd.Parameters.AddWithValue("@perprice", perprice);
            cmd.Parameters.AddWithValue("@notes", notes);
            if (!isspecial)
            {
                cmd.Parameters.AddWithValue("@price", perprice * count * discount);
            }
            else
            {
                cmd.Parameters.AddWithValue("@price", perprice * count);
            }
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i > 0;
        }
    }
}