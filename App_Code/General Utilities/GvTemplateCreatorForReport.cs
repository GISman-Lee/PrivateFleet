using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.GeneralUtilities;
using System.Data.Common;

/// <summary>
/// Summary description for GvTemplateCreatorForReport
/// </summary>
public class GvTemplateCreatorForReport : ITemplate
{

    private DataControlRowType templateType;
    private String ColName = null;
    private string DateToGroup = null;

    public GvTemplateCreatorForReport(DataControlRowType _TemplateType, String _ColName, String _DateToGroup)
    {
        this.ColName = _ColName;
        this.templateType = _TemplateType;
        this.DateToGroup = _DateToGroup;
    }

    #region ITemplate Members

    public void InstantiateIn(Control container)
    {
        switch (templateType)
        {
            case DataControlRowType.Header:
                Literal ltHeaderText = new Literal();
                ltHeaderText.Text = "<b>" + ColName + "</b>";
                container.Controls.Add(ltHeaderText);
                break;
            case DataControlRowType.DataRow:
                Label lblDate = new Label();
                lblDate.Text = DateToGroup;
                container.Controls.Add(lblDate);

                GridView gvInner = new GridView();
                //gvInner.AutoGenerateColumns = false;
                GetDataOfDate(DateToGroup, gvInner);

                container.Controls.Add(gvInner);
                break;
            default: break;

        }
    }

    private void GetDataOfDate(string DateToGroup, GridView gvInner)
    {
        Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
        DbCommand objcmd = null;

        DataTable dtData = null;

        objcmd = DataAccess.GetCommand(CommandType.Text, "select * from tblQuotationHeader where Date='" + DateToGroup + "'");
        dtData = DataAccess.GetDataTable(objcmd);

        gvInner.DataSource = dtData;
        gvInner.DataBind();
    }

    #endregion
}
