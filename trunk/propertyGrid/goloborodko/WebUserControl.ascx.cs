using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
//namespace Interlogic.WebControls
//{
    public partial class WebPropertyGrid: System.Web.UI.UserControl
    {

        private object _selectedObject;
        private bool _showCategory;

        public bool ShowCategory
        {
            get { return _showCategory; }
            set { _showCategory = value; }
        }


        private CategoryList  categories;
        private PropertyDescriptionList listProperties;
        public object SelectedObject
        {
            get
            { 
                
                return _selectedObject;
            }
            set { _selectedObject = value;
                  ProcessSelectedObject(_selectedObject);
            }
        }
        int PropertyDescriptionSorter(PropertyDescription left, PropertyDescription right) 
        { 
            return string.CompareOrdinal(left.DisplayName, right.DisplayName) ; 
        }
        private void ProcessSelectedObject(Object selectedObject)
        {
            
            
            listProperties = new PropertyDescriptionList() ;
            categories = new CategoryList();
            foreach (PropertyDescriptor propDesc in TypeDescriptor.GetProperties(selectedObject))
            {

                if (propDesc.IsBrowsable)
                {
                    if (!categories.ContainsKey(propDesc.Category))
                    {                        
                        categories[propDesc.Category] = new Category(propDesc.Category);
                    }
                    Category thisCategory= categories[propDesc.Category];
                    PropertyDescription wrappedProperty = new PropertyDescription(propDesc, _selectedObject, thisCategory);
                    thisCategory.AddProperty(wrappedProperty );
                    listProperties.Add(wrappedProperty );
                }
            }
            listProperties.Sort(PropertyDescriptionSorter);
           
            
        }
   
       
       
        
        
   
        private static string ColorToCSS(Color c)
        {
            return String.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
        }

        private void RenderHeader(HtmlTextWriter writer)
        {
            writer.Write(String.Format(@"<style type=""text/css"">            
                            .property-name, .property-value, .category, .property-tab
                            {{
                                 border-color: {0};
                                 padding: 1px 1px 1px 4px;
                                 text-overflow:ellipsis;
	                             overflow:hidden;
	                             white-space:nowrap;     
    
                            }}
	
	
	                        
                            .standart-values, .string-editor, .readonly-value
                            {{
                                width:98%;
                                border:0;                            
                                height:16px;                               
                            }}
                            .readonly-value
                            {{
                                color:gray;
                            }}
                            .property-tab
                            {{
                                
                                background-color: {0};                                
                                border-width:0;
                            }}
                            .collapse, .expand
                            {{
                                width  :12px;
                                height :12px;
                                background-repeat: no-repeat;
                                border-width:0;
                            }}
                            .collapse
                            {{
                               background-image: url(images/expanded.png);
        
                            }}
                            .expand
                            {{
                               background-image: url(images/collapsed.png);
            
                            }}
                            .propertygrid-container
                            {{
                                    width:400px;
                                   
                              
                            }}
                            .propertygrid-table
                            {{
                                height:600px;
                                overflow:auto;
                            }}
                            .propertygrid
                            {{
                                table-layout:fixed;                           
                                font-family:verdana;
                                font-size:8pt;
                                border-color: {0};
                                border-width: 1px;
                                border-collapse : collapse; 
                                
                            }}
                          
                            .category
                            {{
                                font-weight: bold;
                                background-color:{0};   
                                border:0;  
                                padding:3px;
                            }}
                            .property-name,.property-value
                            {{       
                                margin:0;                         
                                
                                
                            }}    
                            .description
                            {{
                                 background-color:{0}; 
                                font-family:verdana;
                                font-size:8pt;
                                height: 120px;
                                
                            }}                        
                             #description-text
                            {{
                                
                                
                            }}                        
                            #description-title
                            {{
                                font-weight:bold;
                                
                            }}  
                            </style>
                            <script src='jquery-1.3.1.js' type='text/javascript'></script>                            
                            <script src='PropertyGrid.js' type='text/javascript'></script>                            
                           <div class='propertygrid-container'>
11234
                            <div class='propertygrid-table'>
                           <table class='propertygrid' border='1'>
                           
                            <tbody>
", ColorToCSS(System.Drawing.Color.LightGray  )) );
        }
        private void RenderFooter(HtmlTextWriter writer)
        {
            writer.Write(@"</tbody></table>
                </div>
            <div class='description' id='description'>
               <span id='description-title'></span><br/>
                <span id='description-text'></span>
            </div>                                                
            </div>");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            RenderHeader(writer);
            if (ShowCategory)
            {
                categories.Render(writer);
            }
            else
            {
                listProperties.Render(writer);
            }
            RenderFooter(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

//}