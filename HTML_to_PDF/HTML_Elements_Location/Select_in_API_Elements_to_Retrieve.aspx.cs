﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF.HTML_Elements_Location
{
    public partial class Select_in_API_Elements_to_Retrieve : System.Web.UI.Page
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

            // Select the HTML elements for which to retrieve location and other information from HTML document
            htmlToPdfConverter.HtmlElementsMappingOptions.HtmlElementSelectors = new string[] { htmlElementsSelectorTextBox.Text };

            Document pdfDocument = null;
            try
            {
                // Convert HTML page to a PDF document object which can be further modified to highlight the selected elements
                pdfDocument = htmlToPdfConverter.ConvertUrlToPdfDocumentObject(urlTextBox.Text);

                // Highlight the selected elements in PDF with colored rectangles
                foreach (HtmlElementMapping htmlElementInfo in htmlToPdfConverter.HtmlElementsMappingOptions.HtmlElementsMappingResult)
                {
                    // Get other information about HTML element
                    string htmlElementTagName = htmlElementInfo.HtmlElementTagName;
                    string htmlElementID = htmlElementInfo.HtmlElementId;

                    // Hightlight the HTML element in PDF

                    // A HTML element can span over many PDF pages and therefore the mapping of the HTML element in PDF document consists 
                    // in a list of rectangles, one rectangle for each PDF page where this element was rendered
                    foreach (HtmlElementPdfRectangle htmlElementLocationInPdf in htmlElementInfo.PdfRectangles)
                    {
                        // Get the HTML element location in PDF page
                        PdfPage htmlElementPdfPage = htmlElementLocationInPdf.PdfPage;
                        RectangleF htmlElementRectangleInPdfPage = htmlElementLocationInPdf.Rectangle;

                        // Highlight the HTML element element with a colored rectangle in PDF
                        RectangleElement highlightRectangle = new RectangleElement(htmlElementRectangleInPdfPage.X, htmlElementRectangleInPdfPage.Y,
                            htmlElementRectangleInPdfPage.Width, htmlElementRectangleInPdfPage.Height);

                        if (htmlElementTagName.ToLower() == "h1")
                            highlightRectangle.ForeColor = Color.Blue;
                        else if (htmlElementTagName.ToLower() == "h2")
                            highlightRectangle.ForeColor = Color.Green;
                        else if (htmlElementTagName.ToLower() == "h3")
                            highlightRectangle.ForeColor = Color.Red;
                        else if (htmlElementTagName.ToLower() == "h4")
                            highlightRectangle.ForeColor = Color.Yellow;
                        else if (htmlElementTagName.ToLower() == "h5")
                            highlightRectangle.ForeColor = Color.Indigo;
                        else if (htmlElementTagName.ToLower() == "h6")
                            highlightRectangle.ForeColor = Color.Orange;
                        else
                            highlightRectangle.ForeColor = Color.Navy;

                        highlightRectangle.LineStyle.LineDashStyle = LineDashStyle.Solid;

                        htmlElementPdfPage.AddElement(highlightRectangle);
                    }
                }

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Select_in_API_HTML_Elements_to_Retrieve.pdf; size={0}", outPdfBuffer.Length.ToString()));

                // Write the PDF document buffer to HTTP response
                Response.BinaryWrite(outPdfBuffer);

                // End the HTTP response and stop the current page processing
                Response.End();
            }
            finally
            {
                // Close the PDF document
                if (pdfDocument != null)
                    pdfDocument.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/HTML_Elements_Location/Select_in_API.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/HTML_Elements_Location/Select_in_API.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("HTML_Elements_Location_in_PDF");
                Master.SelectNode("Select_in_API_Elements_to_Retrieve");
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
    }
}