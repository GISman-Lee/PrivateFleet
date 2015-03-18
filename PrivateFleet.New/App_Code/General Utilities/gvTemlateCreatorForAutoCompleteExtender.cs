using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

/// <summary>
/// Summary description for gvTemlateCreatorForAutoCompleteExtender
/// </summary>
public class gvTemlateCreatorForAutoCompleteExtender : ITemplate
{
    private DataControlRowType templateType;
    private string columnName;
    private string FuntionName;
    private string BindingExpression;
     int Cnt = 0;


    public gvTemlateCreatorForAutoCompleteExtender(DataControlRowType _TemplateType, String _ColName,  String _BindingExpression)
    {
        this.templateType = _TemplateType;
        this.columnName = _ColName;
        this.BindingExpression = _BindingExpression;
        
    }


    #region ITemplate Members

    public void InstantiateIn(Control container)
    {
        switch (templateType)
        {
            case DataControlRowType.Header:
                Literal ltHeaderText = new Literal();
                ltHeaderText.Text = "<b>" + columnName + "</b>";
                container.Controls.Add(ltHeaderText);
                break;


            case DataControlRowType.DataRow:

                TextBox txtValue = new TextBox();
                //DropDownList txtValue = new DropDownList();
                txtValue.ID = "txtValue" + Cnt;
                
                txtValue.Attributes.Add("value", txtValue.Text);
                txtValue.AutoCompleteType = AutoCompleteType.Disabled;
                              
                txtValue.DataBinding += new EventHandler(txtValue_DataBinding);                
               // txtValue.Attributes.Add("onfocus", "this.style.backgroundImage='footerarrow.png';");
                 txtValue.Attributes.Add("onblur", "this.style.backgroundColor='white';");

                              

                AjaxControlToolkit.AutoCompleteExtender AutoComplete = new AjaxControlToolkit.AutoCompleteExtender();
                AutoComplete.TargetControlID = "txtValue" + (Cnt);
                AutoComplete.ServicePath = "~/AutoComplete.asmx";
                AutoComplete.ServiceMethod="RequestParametervalue"+Cnt;
                AutoComplete.CompletionInterval = 10;
                AutoComplete.CompletionSetCount = 10;
                AutoComplete.MinimumPrefixLength=1;
                AutoComplete.ID = "Auto" + Cnt;
                container.Controls.Add(txtValue);
                //txtValue.CssClass = "txtBold";
                container.Controls.Add(AutoComplete);
                Cnt++;
               

                break;


            default:
                break;
        }

    #endregion
    }

    void txtValue_DataBinding(object sender, EventArgs e)
    {
       // DropDownList txtValue = (DropDownList)sender;
        TextBox txtValue = (TextBox)sender;
        GridViewRow Container = (GridViewRow)txtValue.NamingContainer;
        object DataValue = DataBinder.Eval(Container.DataItem, BindingExpression);
        if (DataValue != null)
        {
            txtValue.Text = DataValue.ToString();
            
        }
    }
}