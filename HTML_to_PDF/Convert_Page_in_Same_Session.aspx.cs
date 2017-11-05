﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF
{
    public partial class Convert_Page_in_Same_Session : System.Web.UI.Page
    {
        protected void convertToPdfButton_Click(object sender, EventArgs e)
        {
            // Save variables in Session object
            Session["firstName"] = firstNameTextBox.Text;
            Session["lastName"] = lastNameTextBox.Text;
            Session["gender"] = maleRadioButton.Checked ? "Male" : "Female";
            Session["haveCar"] = haveCarCheckBox.Checked;
            Session["carType"] = carTypeDropDownList.SelectedValue;
            Session["comments"] = commentsTextBox.Text;

            // Execute the Display_Session_Variables.aspx page and get the HTML string 
            // rendered by this page
            TextWriter outTextWriter = new StringWriter();
            Server.Execute("Display_Session_Variables.aspx", outTextWriter);

            string htmlStringToConvert = outTextWriter.ToString();

            // Create a HTML to PDF converter object with default settings
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            htmlToPdfConverter.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
            // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
            htmlToPdfConverter.ConversionDelay = 2;

            // Use the current page URL as base URL
            string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri;

            // Convert the page HTML string to a PDF document in a memory buffer
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlStringToConvert, baseUrl);

            // Send the PDF as response to browser

            // Set response content type
            Response.AddHeader("Content-Type", "application/pdf");

            // Instruct the browser to open the PDF file as an attachment or inline
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Convert_Page_in_Same_Session.pdf; size={0}", outPdfBuffer.Length.ToString()));

            // Write the PDF document buffer to HTTP response
            Response.BinaryWrite(outPdfBuffer);

            // End the HTTP response and stop the current page processing
            Response.End();
        }

        protected void previewLinkButton_Click(object sender, EventArgs e)
        {
            // Save variables in Session object
            Session["firstName"] = firstNameTextBox.Text;
            Session["lastName"] = lastNameTextBox.Text;
            Session["gender"] = maleRadioButton.Checked ? "Male" : "Female";
            Session["haveCar"] = haveCarCheckBox.Checked;
            Session["carType"] = carTypeDropDownList.SelectedValue;
            Session["comments"] = commentsTextBox.Text;

            // Redirect to page to convert
            Response.Redirect("Display_Session_Variables.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/Convert_Page_in_Same_Session.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/Convert_Page_in_Same_Session.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.SelectNode("Convert_Page_in_Same_Session");
            }
        }

        protected void demoMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Live_Demo":
                    demoMultiView.SetActiveView(liveDemoView);
                    break;
                case "Description":
                    demoMultiView.SetActiveView(descriptionView);
                    break;
                case "Sample_Code":
                    demoMultiView.SetActiveView(sampleCodeView);
                    break;
                default:
                    break;
            }
        }

        protected void haveCarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            selectCarTypePanel.Visible = haveCarCheckBox.Checked;
        }
    }
}