﻿<%@ Page Title="Select Conversion Triggering Mode" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Conversion_Triggering_Modes.aspx.cs" Inherits="EvoHtmlToPdfDemo.HTML_to_PDF.Triggering_Modes.Conversion_Triggering_Modes" %>

<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%; height: 100%">
        <tr>
            <td style="height: 20px; padding-bottom: 1px">
                <asp:Menu ID="demoMenu" runat="server" Orientation="Horizontal" Font-Bold="True"
                    OnMenuItemClick="demoMenu_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="Live Demo" ToolTip="Live Demo" Value="Live_Demo" Selected="True">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Description" ToolTip="Description" Value="Description"></asp:MenuItem>
                        <asp:MenuItem Text="Sample Code" ToolTip="Sample Code" Value="Sample_Code"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle BackColor="White" Font-Underline="True" />
                    <StaticMenuItemStyle Font-Size="14px" ForeColor="Black" BackColor="WhiteSmoke" BorderStyle="Solid"
                        BorderWidth="1px" HorizontalPadding="20px" ItemSpacing="1px" VerticalPadding="5px"
                        Font-Bold="False" Font-Underline="False" />
                    <StaticSelectedStyle BackColor="White" Font-Underline="False" ForeColor="Navy" />
                </asp:Menu>
            </td>
        </tr>
        <tr>
            <td style="border: solid 1px Black; height: 776px; padding-top: 10px; padding-left: 10px;
                padding-right: 10px; padding-bottom: 0px">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td style="font-size: 14px; font-weight: bold; height: 20px; padding-bottom: 15px;
                            padding-top: 5px">
                            Select Conversion Triggering Mode
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <asp:MultiView ID="demoMultiView" runat="server" ActiveViewIndex="0">
                                <asp:View ID="liveDemoView" runat="server">
                                    <table style="width: 535px; height: 855px">
                                        <tr>
                                            <td style="vertical-align: top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            EVO HTML to PDF Converter allows you to select when you want to start conversion
                                                            of the HTML page to PDF. You can start conversion immediately after the page was
                                                            loaded, delayed after the page was loaded with a constant time interval or manually
                                                            by calling the evoConverter.startConversion() method from the HTML page JavaScript
                                                            code. The Full Description and a Code Sample can be accessed from the top tabs.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="convertHtmlRadioButton" GroupName="HtmlPageSource" runat="server"
                                                                            Text="Convert HTML String" OnCheckedChanged="convertHtmlRadioButton_CheckedChanged"
                                                                            AutoPostBack="True" Font-Bold="False" Checked="True" />
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="convertUrlRadioButton" GroupName="HtmlPageSource" runat="server"
                                                                            Text="HTML Page URL or Local File to Convert" OnCheckedChanged="convertUrlRadioButton_CheckedChanged"
                                                                            AutoPostBack="True" Font-Bold="False" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="urlPanel" runat="server" Visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td style="padding-bottom: 5px; font-weight: bold" colspan="3">
                                                                            HTML Page to Convert URL
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="urlTextBox" runat="server" Width="435px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 5px">
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="viewHtmlLinkButton" runat="server" OnClick="viewHtmlLinkButton_Click">View HTML</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="htmlStringPanel" runat="server" Visible="true">
                                                                <table>
                                                                    <tr>
                                                                        <td style="font-weight: bold">
                                                                            HTML String with Manual Triggering
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="htmlStringTextBox" runat="server" TextMode="MultiLine" Height="450px"
                                                                                Width="500px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-weight: bold">
                                                                            Base URL
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="baseUrlTextBox" runat="server" Text="" Width="500px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="font-weight: bold" colspan="2">
                                                                        Select Triggering Mode
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px">
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButton ID="autoRadioButton" GroupName="TriggeringMode" runat="server" Text="Auto"
                                                                                        Checked="True" Font-Bold="False" AutoPostBack="True" 
                                                                                        oncheckedchanged="autoRadioButton_CheckedChanged" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="delayedRadioButton" GroupName="TriggeringMode" runat="server"
                                                                                        Text="Delayed" Font-Bold="False" AutoPostBack="True" 
                                                                                        oncheckedchanged="delayedRadioButton_CheckedChanged" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="manualRadioButton" GroupName="TriggeringMode" runat="server"
                                                                                        Text="Manual" Font-Bold="False" AutoPostBack="True" 
                                                                                        oncheckedchanged="manualRadioButton_CheckedChanged" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Panel ID="conversionDelayPanel" runat="server" Visible="false">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Delay Conversion:
                                                                                                </td>
                                                                                                <td style="width: 5px">
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="conversionDelayTextBox" runat="server" Width="35px">3</asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 5px">
                                                                                                </td>
                                                                                                <td>
                                                                                                    sec
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="convertToPdfButton" runat="server" Text="Convert HTML to PDF" OnClick="convertToPdfButton_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="descriptionView" runat="server">
                                    <div style="width: 535px; height: 855px; overflow: scroll; font-size: 11px">
                                        <asp:Literal ID="descriptionLiteral" runat="server"></asp:Literal>
                                    </div>
                                </asp:View>
                                <asp:View ID="sampleCodeView" runat="server">
                                    <div style="width: 535px; height: 855px; overflow: scroll; font-size: 11px">
                                        <asp:Literal ID="sampleCodeLiteral" runat="server"></asp:Literal>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
