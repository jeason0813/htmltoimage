﻿<%@ Page Title="Merge Multiple HTML Pages into a Single PDF" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Header_Footer_on_Merged_HTML.aspx.cs" Inherits="EvoHtmlToPdfDemo.HTML_to_PDF.Headers_and_Footers.Header_Footer_on_Merged_HTML" %>

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
                            Merge Multiple HTML Pages into a Single PDF
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
                                                            EVO HTML to PDF Converter allows you to merge multiple HTML document into a single
                                                            PDF document. The Full Description and a Code Sample can be accessed from the top
                                                            tabs.
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
                                                                    <td style="padding-bottom: 5px; font-weight: bold">
                                                                        First HTML Page URL
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="firstUrlTextBox" runat="server" Text="http://www.cnn.com" Width="500px"></asp:TextBox>
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
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-bottom: 5px; font-weight: bold">
                                                                        Second HTML Page URL
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="secondUrlTextBox" runat="server" Text="http://news.google.com" Width="500px"></asp:TextBox>
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
                                                            <table>
                                                                <tr>
                                                                    <td style="font-weight: bold" colspan="2">
                                                                        HTML Merge Options
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="startNewPageCheckBox" Checked="false" runat="server" Text="Start the Second HTML on a New PDF Page" />
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
                                                            <table>
                                                                <tr>
                                                                    <td style="font-weight: bold" colspan="2">
                                                                        Enable Header and Footer
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="addHeaderCheckBox" runat="server" Text="Add Header" Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="addFooterCheckBox" runat="server" Text="Add Footer" Checked="true" />
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
                                                            <table>
                                                                <tr>
                                                                    <td style="font-weight: bold" colspan="2">
                                                                        Header Options
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="showHeaderInFirstPageCheckBox" runat="server" Text="Show in First Page"
                                                                                        Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="showHeaderInOddPagesCheckBox" runat="server" Text="Show in Odd Pages"
                                                                                        Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="showHeaderInEvenPagesCheckBox" runat="server" Text="Show in Even Pages"
                                                                                        Checked="true" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="drawHeaderLineCheckBox" runat="server" Text="Draw Header Line"
                                                                                        Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    Spacing:
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="headerSpacingTextBox" runat="server" Width="35px">0</asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    pt
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    First Page Spacing:
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="firstPageSpacingTextBox" runat="server" Width="35px">0</asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    pt
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
                                                            <table>
                                                                <tr>
                                                                    <td style="font-weight: bold" colspan="2">
                                                                        Footer Options
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="showFooterInFirstPageCheckBox" runat="server" Text="Show in First Page"
                                                                                        Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="showFooterInOddPagesCheckBox" runat="server" Text="Show in Odd Pages"
                                                                                        Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="showFooterInEvenPagesCheckBox" runat="server" Text="Show in Even Pages"
                                                                                        Checked="true" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="addPageNumbersInFooterCheckBox" runat="server" Text="Add Page Numbers"
                                                                                        Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="drawFooterLineCheckBox" runat="server" Text="Draw Footer Line"
                                                                                        Checked="true" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    Spacing:
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="footerSpacingTextBox" runat="server" Width="35px">0</asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    pt
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
