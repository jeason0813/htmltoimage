﻿<%@ Page Title="Convert HTML to PDF/A and PDF/X Documents" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Create_PDF_A_and_PDF_X_Description.aspx.cs"
    Inherits="EvoHtmlToPdfDemo.HTML_to_PDF.PDF_Standards.Create_PDF_A_and_PDF_X_Description" %>

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
            <td style="border: solid 1px Black; padding-top: 10px; padding-left: 10px; padding-right: 10px;
                padding-bottom: 0px">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td style="font-size: 14px; font-weight: bold; height: 20px; padding-bottom: 15px;
                            padding-top: 5px">
                            Convert HTML to PDF/A and PDF/X Documents
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
                                                            EVO HTML to PDF Converter allows you to convert a HTML to full PDF, PDF/A subset
                                                            or PDF/X subset. In this demo you can choose between these standards when you create
                                                            the PDF document. The Full Description and a Code Sample can be accessed from the
                                                            top tabs.
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
                                                                        HTML Page URL or Local File to Convert
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="urlTextBox" runat="server" Text="http://www.evopdf.com" Width="500px"></asp:TextBox>
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
                                                                        PDF Standard
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
                                                                                    <asp:RadioButton ID="fullPdfRadioButton" GroupName="PdfStandard" runat="server" Text="Full PDF"
                                                                                        Checked="True" Font-Bold="False" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="pdfARadioButton" GroupName="PdfStandard" runat="server" Text="PDF/A-1b"
                                                                                        Font-Bold="False" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="pdfXRadioButton" GroupName="PdfStandard" runat="server" Text="PDF/X-1a"
                                                                                        Font-Bold="False" />
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
