using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF.Headers_and_Footers
{
    public partial class Header_Footer_Auto_Resize : System.Web.UI.Page
    {
        // Define the HTML to PDF converter object as a class member to make it accessible in the headerHtml_NavigationCompletedEvent handler
        // where the header height will be automatically adjusted
        private HtmlToPdfConverter htmlToPdfConverter;

        // Indicates if a line should be drawn at the botom of the header
        private bool drawHeaderLine = true;

        protected void convertToPdfButton_Click(object sender, EventArgs e)
        {
            // Create a HTML to PDF converter object with default settings
            htmlToPdfConverter = new HtmlToPdfConverter();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            htmlToPdfConverter.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
            // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
            htmlToPdfConverter.ConversionDelay = 2;

            // Enable header in the generated PDF document
            htmlToPdfConverter.PdfDocumentOptions.ShowHeader = true;

            string headerHtmlUrl = Server.MapPath("~/DemoAppFiles/Input/HTML_Files/Header_HTML.html");
            Document documentObject = null;
            try
            {
                if (autoResizeHeaderRadioButton.Checked)
                {
                    // Create a HTML element to be added in header
                    HtmlToPdfElement headerHtml = new HtmlToPdfElement(headerHtmlUrl);

                    // Install a handler where to set the automatically calculated header height
                    headerHtml.NavigationCompletedEvent += new NavigationCompletedDelegate(headerHtml_NavigationCompletedEvent);

                    // Add the HTML element to header
                    // When the element is rendered in header by converter, the headerHtml_NavigationCompletedEvent handler 
                    // will be invoked and the header height will be automatically calculated
                    htmlToPdfConverter.PdfHeaderOptions.AddElement(headerHtml);

                    // Call the converter to produce a Document object
                    documentObject = htmlToPdfConverter.ConvertUrlToPdfDocumentObject(urlTextBox.Text);

                    // Uninstall the handler
                    headerHtml.NavigationCompletedEvent -= new NavigationCompletedDelegate(headerHtml_NavigationCompletedEvent);

                    // Draw a line at the header bottom
                    if (drawHeaderLine)
                    {
                        float headerWidth = documentObject.Header.Width;
                        float headerHeight = documentObject.Header.Height;

                        // Create a line element for the bottom of the header
                        LineElement headerLine = new LineElement(0, headerHeight - 1, headerWidth, headerHeight - 1);

                        // Set line color
                        headerLine.ForeColor = Color.Gray;

                        // Add line element to the bottom of the header
                        documentObject.Header.AddElement(headerLine);
                    }

                    // Save the PDF document in a memory buffer
                    byte[] outPdfBuffer = documentObject.Save();

                    // Send the PDF as response to browser

                    // Set response content type
                    Response.AddHeader("Content-Type", "application/pdf");

                    // Instruct the browser to open the PDF file as an attachment or inline
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Auto_Resize_Header_Footer.pdf; size={0}", outPdfBuffer.Length.ToString()));

                    // Write the PDF document buffer to HTTP response
                    Response.BinaryWrite(outPdfBuffer);

                    // End the HTTP response and stop the current page processing
                    Response.End();
                }
                else
                {
                    // Create a HTML to PDF element to be added in header
                    HtmlToPdfElement headerHtml = new HtmlToPdfElement(headerHtmlUrl);

                    // Set a fixed header height in points
                    htmlToPdfConverter.PdfHeaderOptions.HeaderHeight = float.Parse(headerHeightTextBox.Text);

                    // Set the HTML element to fit the container height
                    headerHtml.FitHeight = true;

                    // Add HTML element to fit the fixed header height
                    htmlToPdfConverter.PdfHeaderOptions.AddElement(headerHtml);

                    // Draw a line at the header bottom
                    if (drawHeaderLine)
                    {
                        // Calculate the header width based on PDF page size and margins
                        float headerWidth = htmlToPdfConverter.PdfDocumentOptions.PdfPageSize.Width -
                                    htmlToPdfConverter.PdfDocumentOptions.LeftMargin - htmlToPdfConverter.PdfDocumentOptions.RightMargin;

                        // Calculate header height
                        float headerHeight = htmlToPdfConverter.PdfHeaderOptions.HeaderHeight;

                        // Create a line element for the bottom of the header
                        LineElement headerLine = new LineElement(0, headerHeight - 1, headerWidth, headerHeight - 1);

                        // Set line color
                        headerLine.ForeColor = Color.Gray;

                        // Add line element to the bottom of the header
                        htmlToPdfConverter.PdfHeaderOptions.AddElement(headerLine);
                    }

                    // Convert the HTML page to a PDF document in a memory buffer
                    byte[] outPdfBuffer = htmlToPdfConverter.ConvertUrl(urlTextBox.Text);

                    // Send the PDF as response to browser

                    // Set response content type
                    Response.AddHeader("Content-Type", "application/pdf");

                    // Instruct the browser to open the PDF file as an attachment or inline
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Auto_Resize_Header_Footer.pdf; size={0}", outPdfBuffer.Length.ToString()));

                    // Write the PDF document buffer to HTTP response
                    Response.BinaryWrite(outPdfBuffer);

                    // End the HTTP response and stop the current page processing
                    Response.End();
                }
            }
            finally
            {
                // Close the PDF document
                if (documentObject != null)
                    documentObject.Close();
            }
        }

        /// <summary>
        /// This handler is called after the navigation to header HTML completed. The document header is resized in this event handler
        /// </summary>
        /// <param name="eventParams">The event parameter containing the HTML content size in pixels and points</param>
        void headerHtml_NavigationCompletedEvent(NavigationCompletedParams eventParams)
        {
            // Get the header HTML width and height from event parameters
            float headerHtmlWidth = eventParams.HtmlContentWidthPt;
            float headerHtmlHeight = eventParams.HtmlContentHeightPt;

            // Calculate the header width from coverter settings
            float headerWidth = htmlToPdfConverter.PdfDocumentOptions.PdfPageSize.Width - htmlToPdfConverter.PdfDocumentOptions.LeftMargin -
                        htmlToPdfConverter.PdfDocumentOptions.RightMargin;

            // Calculate a resize factor to fit the header width
            float resizeFactor = 1;
            if (headerHtmlWidth > headerWidth)
                resizeFactor = headerWidth / headerHtmlWidth;

            // Calculate the header height to preserve the HTML aspect ratio
            float headerHeight = headerHtmlHeight * resizeFactor;

            if (!(headerHeight < htmlToPdfConverter.PdfDocumentOptions.PdfPageSize.Height - htmlToPdfConverter.PdfDocumentOptions.TopMargin -
                        htmlToPdfConverter.PdfDocumentOptions.BottomMargin))
            {
                throw new Exception("The header height cannot be bigger than PDF page height");
            }

            // Set the calculated header height
            htmlToPdfConverter.PdfDocumentOptions.DocumentObject.Header.Height = headerHeight;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/Headers_and_Footers/Header_Footer_Auto_Resize.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/Headers_and_Footers/Header_Footer_Auto_Resize.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("Headers_and_Footers");
                Master.SelectNode("Header_Footer_Auto_Resize");
            }
        }

        protected void autoResizeHeaderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            fixedHeightOptionsPanel.Visible = fitHeaderHeightRadioButton.Checked;
        }

        protected void fitHeaderHeightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            fixedHeightOptionsPanel.Visible = fitHeaderHeightRadioButton.Checked;
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