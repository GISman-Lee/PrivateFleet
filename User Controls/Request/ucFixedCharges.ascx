<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFixedCharges.ascx.cs" Inherits="User_Controls_Request_ucFixedCharges" %>

<table width="100%" align="left">
    <tr style ="width:13.7px">
       <td class="subheading" >
            <strong>Fixed Charges</strong>
        </td>
    </tr>
    <tr>
        <td>
            <asp:gridview id="gvCharges" runat="server" autogeneratecolumns="False" CellPadding="4" 
            CellSpacing="1" Width="100%" BorderColor="#acacac">
                <Columns>
                    <asp:BoundField DataField="Type"></asp:BoundField>
                    <%--Remove the header text due as per client discussion--%>
                </Columns>
                <HeaderStyle ForeColor="Navy" />
            </asp:gridview>
        </td>
    </tr>
</table>