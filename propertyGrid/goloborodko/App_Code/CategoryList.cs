using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for CategoryList
/// </summary>
public class CategoryList : Dictionary<string, Category>
{
    public void Render(HtmlTextWriter writer)
    {
        foreach(KeyValuePair <string, Category> c in this)
            c.Value.Render(writer);

    }
	public CategoryList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
