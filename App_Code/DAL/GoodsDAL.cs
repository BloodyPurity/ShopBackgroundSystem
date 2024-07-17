using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;

/// <summary>
/// GoodsDAL 的摘要说明
/// </summary>
public class GoodsDAL
{
    string strCon = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
    /// <summary>
    /// 开店，设置启动资金
    /// </summary>
    /// <param name="name"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool CreateStore(string name,double money)
    {
        string str = "insert into asset(name,isstorepage,money) values(@name,1,@money)";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@money", money);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i != 0)
            {
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 添加商品类型
    /// </summary>
    /// <param name="gtype">要添加的商品类型，可以防止重复添加</param>
    /// <returns>true成功，false失败</returns>
    public bool AddGtype(string gtype)
    {
        string str = "insert into goodstype(name) select @name from dual where not exists (select name from goodstype where name= @name)";
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@name", gtype);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i != 0)
            {
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// Todo
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public bool RemoveGtype(string gtype)
    {
        return false;
    }
    /// <summary>
    /// 获取数据集
    /// </summary>
    /// <returns></returns>
    public DataSet SelectGtype()
    {
        string str = "select * from goodstype order by id asc";
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        using (MySqlConnection  conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            da=new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }

    }
    public DataSet SelectGtypeddl()
    {
        string str = "select name from goodstype order by id asc";
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    /// <summary>
    /// 添加商品，不可以重复添加同名商品
    /// </summary>
    /// <param name="goods"></param>
    /// <returns></returns>
    public bool AddGoods(Goods goods)
    {
        int i=0;
        string str0 = "select gname from goods where gname = @gname";
        string str = "insert into goods(gname,gtype,gcount,price,discount,notes,isdeleted) values(@gname,@gtype,@gcount,@price,@discount,@notes,0)";
        using(MySqlConnection conn =new MySqlConnection(strCon))
        {
            MySqlCommand cmd0 = new MySqlCommand(str0, conn);
            MySqlCommand cmd = new MySqlCommand(str,conn);
            cmd0.Parameters.AddWithValue("@gname", goods.Gname);
            cmd.Parameters.AddWithValue("@gname", goods.Gname);
            cmd.Parameters.AddWithValue("@gtype", goods.Gtype);
            cmd.Parameters.AddWithValue("@gcount", goods.Gcount);
            cmd.Parameters.AddWithValue("@price", goods.Price);
            cmd.Parameters.AddWithValue("@discount", goods.discount);
            cmd.Parameters.AddWithValue("@notes", goods.notes);
            conn.Open();
            if (cmd0.ExecuteScalar()==null)
            {
                i = cmd.ExecuteNonQuery();
            }
            conn.Close();
            if (i != 0)
            {
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 软删除，其实还存在
    /// </summary>
    /// <param name="id">商品的ID</param>
    /// <returns></returns>
    public bool RemoveGoods(string gname)
    {
        string str = "update goods set isdeleted =1 where gname = @gname";
        using(MySqlConnection conn =new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gname", gname);
            conn.Open();
            int i =cmd.ExecuteNonQuery();
            conn.Close();
            return i > 0;
        }
    }
    /// <summary>
    /// 重新上架
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool ReUpGoods(int id)
    {
        string str = "update goods set isdeleted =0 where id = @id";
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
    /// <summary>
    /// 获取数据集
    /// </summary>
    /// <returns></returns>
    public DataSet SelectGoods()
    {
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        string str = "select * from goods where isdeleted = 0";
        using(MySqlConnection conn =new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
        }
        return ds;
    }
    public DataSet SelectGoods(string gname)
    {
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        string str = "select * from goods where isdeleted = 0 and gname=@gname";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gname", gname);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
        }
        return ds;
    }
    public DataSet SelectGoodsddl(string gtype)
    {
        string str = "select gname from goods where gtype =@gtype and isdeleted=0 order by id asc";
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gtype", gtype);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
    /// <summary>
    /// 仅限上架的单个商品的全部信息，用于编辑
    /// </summary>
    /// <param name="gname">商品名</param>
    /// <returns></returns>
    public List<string> SelectGoodsSingle(string gname)
    {
        string str = "select * from goods where gname=@gname and isdeleted = 0";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gname", gname);
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
            return T;
        }
    }
    public DataSet SelectGoodsoff()
    {
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        string str = "select * from goods where isdeleted = 1";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
        }
        return ds;
    }
    public bool UpdateGoods(Goods goods)
    {
        string str = "update goods set gcount=@gcount,price=@price,discount=@discount,notes=@notes where gname=@gname";
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gname", goods.Gname);
            cmd.Parameters.AddWithValue("@gcount", goods.Gcount);
            cmd.Parameters.AddWithValue("@price", goods.Price);
            cmd.Parameters.AddWithValue("@discount", goods.discount);
            cmd.Parameters.AddWithValue("@notes", goods.notes);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            return i > 0;
        }
    }
    public string GetImageGoods(string gname)
    {
        string str = "select gicon from goods where gname =@gname";
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gname", gname);
            conn.Open();
            var i = cmd.ExecuteScalar();
            conn.Close();
            if (i != null && i.ToString() != "")
            {
                string rst = i.ToString();
                return rst;
            }
            else return null;
        }
    }
    public int SetImageGoods(string ImageURL, string gname)
    {
        string preUrl = GetImageGoods(gname);
        string str = "update goods set gicon =@gicon where gname =@gname";
        if (preUrl != null && preUrl != "")
        {
            preUrl = preUrl.Replace("~/", " ").Trim();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                File.Delete(Path.Combine(path, preUrl));
            }
            catch
            {

            }
        }
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gicon", ImageURL);
            cmd.Parameters.AddWithValue("@gname", gname);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
    /// <summary>
    /// 店长操作，对某一类或者是一种商品打折或者涨价，不可同时选择
    /// </summary>
    /// <param name="gname">商品名</param>
    /// <param name="gtype">商品类型</param>
    /// <param name="discount">折扣乘数，打折就填小于1，涨价就填大于1</param>
    /// <returns></returns>
    public bool Discount(string gname,string gtype,double discount)
    {
        string str;
        if (!string.IsNullOrEmpty(gname) && string.IsNullOrEmpty(gtype))
        {
            str = "update goods set discount = @discount where gname=@gname";
            using (MySqlConnection conn = new MySqlConnection(strCon))
            {
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@discount", discount);
                cmd.Parameters.AddWithValue("@gname", gname);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i > 0;
            }
        }
        else if (string.IsNullOrEmpty(gname) && !string.IsNullOrEmpty(gtype))
        {
            str = "update goods set discount = @discount where gtype=@gtype";
            using (MySqlConnection conn = new MySqlConnection(strCon))
            {
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@discount", discount);
                cmd.Parameters.AddWithValue("@gtype", gtype);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i > 0;
            }
        }
        else return false;
        
    }
    /// <summary>
    /// 添加供货商，供店长或者店员使用，可重复添加同一供货商，只要货物种类不同
    /// </summary>
    /// <param name="name">供货商名</param>
    /// <param name="gtype">商品类型</param>
    /// <param name="description">相关描述</param>
    /// <param name="uaccount">供货商及该类商品的负责人</param>
    /// <returns>true添加成功，false添加失败</returns>
    public bool AddProvider(string name,string gtype,string description,string uaccount)
    {
        string str = "insert into provider(name,description,goodstype,holder) " +
            "select @name,@description,@gtype,@uaccount from dual " +
            "where not exists (select name,goodstype from provider where name= @name and goodstype = @gtype)";
        using( MySqlConnection conn =new MySqlConnection( strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@gtype", gtype);
            cmd.Parameters.AddWithValue("@uaccount", uaccount);
            int i= cmd.ExecuteNonQuery();
            conn.Close();
            if(i != 0)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="name"></param>
    /// <param name="gtype"></param>
    /// <returns></returns>
    public bool RemoveProvider(string name,string gtype)
    {
        return false;
    }
    /// <summary>
    /// 查看所有供货商
    /// </summary>
    /// <returns></returns>
    public DataSet SelectProvider()
    {
        string str = "select * from Provider";
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        using(MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str,conn);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            conn.Close();
            da.Fill(ds);
        }
        return ds;
    }
    /// <summary>
    /// 在入库时需要的根据进货类型来选择供应商
    /// </summary>
    /// <param name="gtype"></param>
    /// <returns></returns>
    public DataSet SelectProviderddl(string gtype)
    {
        string str = "select name from provider where goodstype =@gtype order by id asc";
        DataSet ds = new DataSet();
        MySqlDataAdapter da;
        using (MySqlConnection conn = new MySqlConnection(strCon))
        {
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("@gtype",gtype);
            conn.Open();
            da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
}