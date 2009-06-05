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
/// Summary description for Category
/// </summary>
public class Category
{
    string _name;
    PropertyDescriptionList  _properties;
    string _hash;

	public Category(string name )
	{
        _name =name;		
        _properties = new PropertyDescriptionList ();
        _hash = "category_" + name.GetHashCode().ToString();
	}
    public string CssClassName
    {
        get{
            return  _hash;
            }
    }
    public void  AddProperty(PropertyDescription property)
    {
        _properties.Add(property);
    }
    public void Render(HtmlTextWriter writer)
    {
        writer.Write(
        String.Format(
        @"<tr>
        <td class='property-tab category'>
        <div class='collapse' id='col_{1}' onclick='categoryShow(""col_{1}"",""{1}""); return false;'/>
        </td>
        <td colspan='2' class='category'>
        {0}
        </td>
        
        </tr><tr id='{1}'> ",_name,_hash ));
        _properties.Render(writer);
        writer.Write("</tr>");
    }
    
}
