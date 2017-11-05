using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.PDF_Creator.Headers_and_Footers
{
    public partial class Header_Footer_Auto_Resize : System.Web.UI.Page
    {
        // Define the PDF Document object as a class member to make it accessible in the headerHtml_NavigationCompletedEvent handler
        // where the header height will be automatically adjusted
        private Document pdfDocument;

        // Indicates if a line should be drawn at the botom of the header
        private bool drawHeaderLine = true;

        protected void createPdfButton_Click(object sender, EventArgs e)
        {
            // Create a PDF document
            pdfDocument = new Document();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            pdfDocument.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Add a PDF page to PDF document
            PdfPage pdfPage = pdfDocument.AddPage();

            string headerHtmlUrl = Server.MapPath("~/DemoAppFiles/Input/HTML_Files/Header_HTML.html");
            try
            {
                // Add the header to PDF document
                if (autoResizeHeaderRadioButton.Checked)
                {
                    // Create the document header with a default height
                    // It will be automatically resized in headerHtml_NavigationCompletedEvent handler
                    pdfDocument.AddHeaderTemplate(50);

                    // Create a HTML element to be added in header
                    HtmlToPdfElement headerHtml = new HtmlToPdfElement(headerHtmlUrl);

                    // Install a handler where to create the document header based on HTML element height
                    headerHtml.NavigationCompletedEvent += new NavigationCompletedDelegate(headerHtml_NavigationCompletedEvent);

                    // Add the HTML element to header
                    // When the element is rendered in header by converter, the headerHtml_NavigationCompletedEvent handler 
                    // will be invoked and the header height will be automatically calculated
                    pdfDocument.Header.AddElement(headerHtml);

                    // Uninstall the handler
                    headerHtml.NavigationCompletedEvent += new NavigationCompletedDelegate(headerHtml_NavigationCompletedEvent);
                }
                else
                {
                    // Create the document header with a fixed height
                    pdfDocument.AddHeaderTemplate(float.Parse(headerHeightTextBox.Text));

                    // Create a HTML to PDF element to be added in header
                    HtmlToPdfElement headerHtml = new HtmlToPdfElement(headerHtmlUrl);

                    // Set the HTML element to fit the container height
                    headerHtml.FitHeight = true;

                    // Add HTML element to fit the fixed header height
                    pdfDocument.Header.AddElement(headerHtml);

                }

                // Draw a line at the header bottom
                if (drawHeaderLine)
                {
                    float headerWidth = pdfDocument.Header.Width;
                    float headerHeight = pdfDocument.Header.Height;

                    // Create a line element for the bottom of the header
                    LineElement headerLine = new LineElement(0, headerHeight - 1, headerWidth, headerHeight - 1);

                    // Set line color
                    headerLine.ForeColor = Color.Gray;

                    // Add line element to the bottom of the header
                    pdfDocument.Header.AddElement(headerLine);
                }

                // Create a HTML to PDF element to add to document
                HtmlToPdfElement htmlToPdfElement = new HtmlToPdfElement(urlTextBox.Text);

                // Optionally set a delay before conversion to allow asynchonous scripts to finish
                htmlToPdfElement.ConversionDelay = 2;

                // Add the HTML to PDF element to document
                pdfPage.AddElement(htmlToPdfElement);

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

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
            finally
            {
                // Close the PDF document
                pdfDocument.Close();
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

            // Get the header width
            float headerWidth = pdfDocument.Header.Width;

            // Calculate a resize factor to fit the header width
            float resizeFactor = 1;
            if (headerHtmlWidth > headerWidth)
                resizeFactor = headerWidth / headerHtmlWidth;

            // Calculate the header height to preserve the HTML aspect ratio
            float headerHeight = headerHtmlHeight * resizeFactor;

            if (!(headerHeight < pdfDocument.Pages[0].PageSize.Height - pdfDocument.Pages[0].Margins.Top -
                        pdfDocument.Pages[0].Margins.Bottom))
            {
                throw new Exception("The header height cannot be bigger than PDF page height");
            }

            // Set the calculated header height
            pdfDocument.Header.Height = headerHeight;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/PDF_Creator/Headers_and_Footers/Header_Footer_Auto_Resize.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/PDF_Creator/Headers_and_Footers/Header_Footer_Auto_Resize.html"));

                Master.CollapseAll();
                Master.ExpandNode("PDF_Creator");
                Master.ExpandNode("PDF_Creator_Headers_and_Footers");
                Master.SelectNode("PDF_Creator_Header_Footer_Auto_Resize");
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