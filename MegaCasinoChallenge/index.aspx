<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MegaCasinoChallenge.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="index.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <asp:Image ID="imgOne" runat="server" Height="150px" Width="150px" />
                <asp:Image ID="imgTwo" runat="server" Height="150px" Width="150px" />
                <asp:Image ID="imgThree" runat="server" Height="150px" Width="150px" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Place a Bet: $"></asp:Label>
            <asp:TextBox ID="txtPlayerBet" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnPullLever" runat="server" OnClick="btnPullLever_Click" Text="Pull the Lever" />
            <br />
            <br />
            <asp:Label ID="lblBetResult" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server">Player Money: </asp:Label>
            <asp:Label ID="lblPlayerMoney" runat="server"></asp:Label>
            <br />
            <br />
             <p><span class="cherry">1 Cherry - x2 Your Bet<br />2 Cherries - x3 Your Bet<br />3 Cherries - x4 Your Bet</span><br />
                 <br /><span class="jackpot">3 7's - Jackpot - x100 Your Bet</span><br /><br /><span class="however">***HOWEVER***
                 <br /><br />If there's even one BAR you win nothing</span></p>
        </div>
    </form>
</body>
</html>
