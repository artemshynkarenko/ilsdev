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
/// Summary description for PropertyDescriptionList
/// </summary>
public class PropertyDescriptionList : List<PropertyDescription>
{
  public void  Render(HtmlTextWriter writer)
  {
      foreach (PropertyDescription property in this)
          property.Render(writer );
  }
	public PropertyDescriptionList()
	{
		//
		// TODO: Add constructor logic here
		//
	}

}
