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
using System.ComponentModel;

/// <summary>
/// Summary description for PropertyDescription
/// </summary>
public class PropertyDescription
{
    PropertyDescriptor _source;
    Category _category;
    object _selectedObject;
	public PropertyDescription(PropertyDescriptor source,Object selectedObject,Category category)
	{
        _source = source;
        _selectedObject = selectedObject;
        _category = category;
	}
    public string  CategoryName { 
        get {
        return _source.Category;
    }
    }
    public Category  Category
    {
        get
        {
            return _category;
        }
    }
    public string DisplayName
    {
        get
        {
            return _source.DisplayName;
        }
    }
    public void Render(HtmlTextWriter writer)
    {
        writer.Write(
        String.Format(
        @"<tr class='{0}'>
        <td class='property-tab'>
        </td>
        <td width='50%' class='property-name'>
        {1}
        </td>
        <td  width='50%' class='property-value'>        
        ", Category.CssClassName, _source.DisplayName));
        RenderValue(writer);
        writer.Write(@"</td></tr>");        
    }
    private void RenderValue(HtmlTextWriter writer)
    {
        string scriptDescription = String.Format("descriptionShow('{0}','{1}');", HttpUtility.HtmlEncode(_source.DisplayName), HttpUtility.HtmlEncode(_source.Description));
            if(_source.IsReadOnly)
            {
                writer.Write(String.Format(@"<span onfocus=""{1}"" class='readonly-value'>{0}</span>", _source.Converter.ConvertToString(_selectedObject),scriptDescription));
            }
            else
            
                if(_source.Converter.GetStandardValuesSupported())
                {
                    writer.Write(String.Format(@"<select onfocus=""{1}"" class='standart-values' id='{0}'>",_source.Name,scriptDescription));
                    foreach (object s in _source.Converter.GetStandardValues())
                    {
                        string value = _source.Converter.ConvertToString(s);
                        string currentValue=_source.GetValue(_selectedObject).ToString();
                        if (value ==  currentValue)
                        {
                            writer.Write(@"<option onfocus=""{1}"" selected=""selected"">{0}</option>", value,scriptDescription);
                        }
                        else
                        {
                            writer.Write(@"<option>{0}</option>", value);
                        }
                    } writer.Write("</select>");
                }
            
            else
            if(_source.Converter.CanConvertFrom(typeof(string)))
            {
                  writer.Write(String.Format(@"<input onfocus=""{2}"" class='string-editor' type='text' id='{0}' value='{1}'>",_source.Name,HttpUtility.HtmlEncode(_source.GetValue(_selectedObject).ToString()),scriptDescription ));
                  
            }
                      
                     
           
    }



}
