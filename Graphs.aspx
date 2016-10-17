<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Graphs.aspx.cs" Inherits="EmotionVisualizer.Graphs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="../../Scripts/jquery-2.0.0.min.js" type="text/javascript"></script>
<script src="../../Scripts/highcharts.js" type="text/javascript"></script>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
        </div>
        <div>
            <br />
            <asp:Literal ID="ltrChart2" runat="server"></asp:Literal>
        </div>
        <div>
            <br />
            <asp:Literal ID="ltrChart3" runat="server"></asp:Literal>
        </div>
        <div>
            <br />
            <asp:Literal ID="ltrChart4" runat="server"></asp:Literal>
        </div>
        <div>
            <br />
            <asp:Literal ID="ltrChart5" runat="server"></asp:Literal>
        </div>
        <div>
            <br />
            <asp:Literal ID="ltrChart6" runat="server"></asp:Literal>
        </div>
        <div>
            <br />
            <asp:Literal ID="ltrChart7" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
