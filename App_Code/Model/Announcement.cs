using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Announcement 的摘要说明
/// </summary>
public class Announcement
{
    public string name { get; set; }
    public string detail { get; set; }
    public string owner {  get; set; }
    public int uid { get; set; }
    public DateTime createtime {  get; set; }
}