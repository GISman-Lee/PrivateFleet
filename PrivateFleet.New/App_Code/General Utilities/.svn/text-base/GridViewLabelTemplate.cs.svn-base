using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GridViewLabelTemplate
/// </summary>
/// 
namespace Mechsoft.GeneralUtilities
{
    public class GridViewLabelTemplate : ITemplate
    {

        private DataControlRowType templateType;
        private string columnName;
        private string dataType;
        private string bindingText;
     
        
        public GridViewLabelTemplate(DataControlRowType type,
            string colname, string DataType, String BindingText)
        {
            templateType = type;
            columnName = colname;
            dataType = DataType;
            bindingText = BindingText;
            

        }


        public void InstantiateIn(System.Web.UI.Control container)
        {
            DataControlFieldCell hc = null;

            switch (templateType)
            {

                case DataControlRowType.Header:
                    // build the header for this column
                    Literal lc = new Literal();
                    lc.Text = "<b>" + columnName + "</b>";
                    container.Controls.Add(lc);
                    break;

                case DataControlRowType.DataRow:
                    // build one row in this column
                    Label lblValues = new Label();
                    switch (dataType)
                    {

                        case "String":


                            lblValues.Text = " '<%# Container.DataItem(" + bindingText + ") %>'";
                            lblValues.DataBind();
                            container.Controls.Add(lblValues);
                            break;
                    }
                    break;

               
                default:
                    break;
            }
        }

    }
}

