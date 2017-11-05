﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF.Page_Breaks
{
    public partial class Avoid_Page_Breaks_Inside_HTML_Elements_Using_API : System.Web.UI.Page
    {
        protected void convertToPdfButton_Click(object sender, EventArgs e)
        {
            // Create a HTML to PDF converter object with default settings
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            htmlToPdfConverter.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
            // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
            htmlToPdfConverter.ConversionDelay = 2;

            // Set the CSS selectors of the HTML elements for which to avoid page breaks inside in the generated PDF document
            htmlToPdfConverter.PdfDocumentOptions.AvoidHtmlElementsBreakSelectors = new string[] { htmlElementsSelectorTextBox.Text };

            byte[] outPdfBuffer = null;

            if (convertHtmlRadioButton.Checked)
            {
                string htmlWithForm = htmlStringTextBox.Text;
                string baseUrl = baseUrlTextBox.Text;

                // Convert the HTML string to a PDF document in a memory buffer
                outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlWithForm, baseUrl);
            }
            else
            {
                string url = urlTextBox.Text;

                // Convert the HTML page to a PDF document in a memory buffer
                outPdfBuffer = htmlToPdfConverter.ConvertUrl(url);
            }

            // Send the PDF as response to browser

            // Set response content type
            Response.AddHeader("Content-Type", "application/pdf");

            // Instruct the browser to open the PDF file as an attachment or inline
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Avoid_Page_Breaks_Inside_HTML_Elements_Using_API.pdf; size={0}", outPdfBuffer.Length.ToString()));

            // Write the PDF document buffer to HTTP response
            Response.BinaryWrite(outPdfBuffer);

            // End the HTTP response and stop the current page processing
            Response.End();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string rootUrl = currentPageUrl.Substring(0, currentPageUrl.LastIndexOf('/')) + "/../../";

                htmlStringTextBox.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/HTML_Files/Page_Breaks_Inside_Avoid_Using_API.html"));
                baseUrlTextBox.Text = rootUrl + "DemoAppFiles/Input/HTML_Files/";

                urlTextBox.Text = rootUrl + "DemoAppFiles/Input/HTML_Files/Page_Breaks_Inside_Avoid_Using_API.html";

                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/Page_Breaks/Avoid_Page_Breaks_in_API.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/Page_Breaks/Avoid_Page_Breaks_in_API.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("Page_Breaks_Control");
                Master.SelectNode("Avoid_Page_Breaks_Inside_HTML_Elements_Using_API");
            }
        }

        protected void convertHtmlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            urlPanel.Visible = convertUrlRadioButton.Checked;
            htmlStringPanel.Visible = !convertUrlRadioButton.Checked;
        }

        protected void convertUrlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            urlPanel.Visible = convertUrlRadioButton.Checked;
            htmlStringPanel.Visible = !convertUrlRadioButton.Checked;
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

        protected void viewHtmlLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(urlTextBox.Text);
        }
    }
}