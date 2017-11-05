<%@ Page Title="Fill PDF Forms" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Fill_PDF_Forms.aspx.cs" Inherits="EvoHtmlToPdfDemo.PDF_Editor.Fill_PDF_Forms" %>

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
                            Fill PDF Forms
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
                                                            EVO HTML to PDF Converter allows you to fill the PDF form of a PDF document. The
                                                            resulted PDF document can be saved to a file, stream or memory buffer. The Full
                                                            Description and a Code Sample can be accessed from the top tabs.
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
                                                                        PDF File with Form to Fill
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
                                                                        <asp:HyperLink ID="pdfFileToFillHyperLink" runat="server" Target="_blank">Open Initial PDF with an Empty Form</asp:HyperLink>
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
                                                                        Form Fields Values
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
                                                                                    First Name:
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="firstNameTextBox" runat="server" Width="60px">John</asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 25px">
                                                                                </td>
                                                                                <td>
                                                                                    Last Name:
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="lastNameTextBox" runat="server" Width="60px">Smith</asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 25px">
                                                                                </td>
                                                                                <td>
                                                                                    Password:
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="passwordTextBox" runat="server" Width="60px">secret</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
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
                                                                                    Gender:
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="maleRadioButton" GroupName="Gender" runat="server" Text="Male"
                                                                                        Checked="True" Font-Bold="False" />
                                                                                </td>
                                                                                <td style="width: 30px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="femaleRadioButton" GroupName="Gender" runat="server" Text="Female"
                                                                                        Font-Bold="False" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
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
                                                                                    <asp:CheckBox ID="haveCarCheckBox" runat="server" Checked="true" Text="I have a car" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td>
                                                                                    Car Type:
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="carTypeComboBox" runat="server">
                                                                                        <asp:ListItem>Volvo</asp:ListItem>
                                                                                        <asp:ListItem>Saab</asp:ListItem>
                                                                                        <asp:ListItem>Audi</asp:ListItem>
                                                                                        <asp:ListItem>Opel</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
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
                                                                        Comments:
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="commentsTextBox" runat="server" Height="109px" Width="400px" TextMode="MultiLine">My comments
Line 1
Line 2
                                                                        </asp:TextBox>
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
                                                                    <td>
                                                                        <asp:Button ID="fillPdfButton" runat="server" Text="Fill PDF Form" OnClick="fillPdfButton_Click" />
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="flattenPdfFormCheckBox" runat="server" Text="Flatten PDF Form Fields" />
                                                                    </td>
                                                                </tr>
                                                            </table>
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
