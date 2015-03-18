<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRoleAccess.ascx.cs" Inherits="User_Controls_UserRoleAccess_ucRoleAccess" %>

<script type="text/javascript">
  function OnTreeClick(evt)
{

    var src = window.event != window.undefined ? window.event.srcElement : evt.target;

    var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
    if(isChkBoxClick)
    {
        var parentTable = GetParentByTagName("table", src);
        var nxtSibling = parentTable.nextSibling;
        if(nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
        {
            if(nxtSibling.tagName.toLowerCase() == "div") //if node has children
            {
                //check or uncheck children at all levels
                CheckUncheckChildren(parentTable.nextSibling, src.checked);
            }
        }
        //check or uncheck parents at all levels
        
        CheckUncheckParents(src, src.checked);
    }
}
function CheckUncheckChildren(childContainer, check)
{
    var childChkBoxes = childContainer.getElementsByTagName("input");
    var childChkBoxCount = childChkBoxes.length;
    for(var i = 0; i<childChkBoxCount;i++)
    {
        childChkBoxes[i].checked = check;
    }
}
function CheckUncheckParents(srcChild, check)
{

        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if(parentNodeTable)
        {
            var checkUncheckSwitch;

            if(check) //checkbox checked
            {
                //var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                //if(isAllSiblingsChecked)
                //    checkUncheckSwitch = true;
                //else
                //    return; //do not need to check parent if any(one or more) child not checked
                
                checkUncheckSwitch = true;
            }
            else //checkbox unchecked
            {
                /*Comments By Sujata: uncomment this code if it is required */
                /*to uncheck parents if a single child is unchekd*/
                /*///////////////////////////*/
                //checkUncheckSwitch = false;
                
                
                var isAllSiblingsChecked = AreAllSiblingsUnChecked(srcChild);
                if(isAllSiblingsChecked)
                    checkUncheckSwitch = false;
                else
                    return;
            }

            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            
            if(inpElemsInParentTable.length > 0)
            {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    
}

function AreAllSiblingsUnChecked(chkBox)
{
    var parentDiv = GetParentByTagName("div", chkBox);
    var childCount = parentDiv.childNodes.length;
    for(var i=0; i<childCount;i++)
    {
        if(parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
        {
            if(parentDiv.childNodes[i].tagName.toLowerCase() == "table")
            {
                var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                //if any of sibling nodes are not checked, return false
                if(prevChkBox.checked)
                {
                    return false;
                }
            }
        }
    }
    return true;
}

function AreAllSiblingsChecked(chkBox)
{
    var parentDiv = GetParentByTagName("div", chkBox);
    var childCount = parentDiv.childNodes.length;
    for(var i=0; i<childCount;i++)
    {
        if(parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
        {
            if(parentDiv.childNodes[i].tagName.toLowerCase() == "table")
            {
                var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                //if any of sibling nodes are not checked, return false
                if(!prevChkBox.checked)
                {
                    return false;
                }
            }
        }
    }
    return true;
}
//utility function to get the container of an element by tagname
function GetParentByTagName(parentTagName, childElementObj)
{
    var parent = childElementObj.parentNode;
    while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
    {
        parent = parent.parentNode;
    }
    return parent;
}

</script>

<table width="95%" align="center">
    <tr>
        <td style="height: 25px;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 83px">
            Select Role :
        </td>
        <td>
            <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table width="95%" align="center" id="tblAccess" runat="server" visible="false">
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td>
            <asp:TreeView ID="tvSHAvailable" CssClass="treeview"  runat="server" ShowCheckBoxes="All">
            </asp:TreeView>
        </td>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td>
            <asp:ImageButton ID="ibtnSubmit" runat="server" ImageUrl="~/Images/Submit.gif" ImageAlign="AbsMiddle"
                OnClick="ibtnSubmit_Click" />
        </td>
    </tr>
</table>
