<%@  Control Language="C#" AutoEventWireup="true" CodeFile="ucSeriesAccessories.ascx.cs"
    Inherits="User_Controls_Request_ucSeriesAccessories" %>
<table width="100%" align="left">
    <tr >
        <td class="subheading">
            <strong >Accessories</strong>
            
        <%--    Replace Default Accessories by Accessories--%>
        </td>
    </tr>
    <tr>
        <td>
            <asp:gridview id="gvAccessories" runat="server" autogeneratecolumns="False" CellPadding="3" 
            CellSpacing="1" Width="100%" BorderColor="#acacac">
            <%--Removed Field  EmptyDataText="No Records Found"--%>
                <Columns>
                    <asp:BoundField DataField="accessory" HeaderText="Accessory"></asp:BoundField>
                    <asp:BoundField DataField="specification" HeaderText="Specification"></asp:BoundField>
                </Columns>
                <HeaderStyle ForeColor="Navy" />
            </asp:gridview>
        </td>
    </tr>
  
</table>
