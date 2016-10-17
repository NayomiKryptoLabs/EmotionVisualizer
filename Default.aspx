<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmotionVisualizer.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="../../Scripts/jquery-2.0.0.min.js" type="text/javascript"></script>
<script src="../../Scripts/highcharts.js" type="text/javascript"></script>
<head runat="server">
    <title>home</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p style="background-color:darkblue;color:white;font-size:22px;font-family:Candara; margin:0;text-align:center">Student Emotion Data Application</p>
            <p style="align-items:center;margin-left:250px">
            <asp:Image ID="image" runat="server" ImageAlign="Middle" ImageUrl="~/Image.JPG"></asp:Image></p>
        </div>

        <div style="margin-left:auto;margin-right:auto;width:50%">
            <p style="font-family:Candara;font:bold; float:left">Student Name : &nbsp;</p><br/>
            <asp:DropDownList ID="dropdown" runat="server" style="font-family:Candara;font:bold;margin-right: 50px;">
                <asp:ListItem Text="HAMDAN BASHER" Value="1"></asp:ListItem>
                <asp:ListItem Text="KHALID SAEED JASIM" Value="2"></asp:ListItem>
                <asp:ListItem Text="JASIM ESSA" Value="3"></asp:ListItem>
                <asp:ListItem Text="NAGLA SALAH" Value="4"></asp:ListItem>
                <asp:ListItem Text="EISSA IBRAHIM" Value="5"></asp:ListItem>
                <asp:ListItem Text="RAWDHA ABDULLAH" Value="6"></asp:ListItem>
                <asp:ListItem Text="DINA YOUSIF" Value="7"></asp:ListItem>
                <asp:ListItem Text="NOURA FAISAL" Value="8"></asp:ListItem>
            </asp:DropDownList>
            <br/> &nbsp;
        </div>
        <div style="margin-left:auto;margin-right:auto;width:50%">
            <p style="font-family:Candara;font:bold;float:left">Date : &nbsp</p><br/>
            <asp:Calendar ID="calendar" runat="server" style="font-family:Candara;font:bold"></asp:Calendar>
            <br/><br/>
            <asp:Button ID="submitButton" runat="server" Text="Display Graphs" OnClick="displayGraphsActionMethod" style="font-family:Candara;font:bold;font-size:medium;background-color:gainsboro">
            </asp:Button>
        </div>
    </form>
</body>
</html>
