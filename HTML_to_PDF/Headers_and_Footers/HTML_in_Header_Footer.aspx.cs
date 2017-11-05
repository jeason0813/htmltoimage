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
    public partial class HTML_in_Header_Footer : System.Web.UI.Page
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

            // Install a handler where you can set header and footer visibility or create a custom header and footer in each page
            htmlToPdfConverter.PrepareRenderPdfPageEvent += new PrepareRenderPdfPageDelegate(htmlToPdfConverter_PrepareRenderPdfPageEvent);

            // Add Header

            // Enable header in the generated PDF document
            htmlToPdfConverter.PdfDocumentOptions.ShowHeader = addHeaderCheckBox.Checked;

            // Optionally add a space between header and the page body
            // The spacing for first page and the subsequent pages can be set independently
            // Leave this option not set for no spacing
            htmlToPdfConverter.PdfDocumentOptions.Y = float.Parse(firstPageSpacingTextBox.Text);
            htmlToPdfConverter.PdfDocumentOptions.TopSpacing = float.Parse(headerSpacingTextBox.Text);

            // Draw header elements
            if (htmlToPdfConverter.PdfDocumentOptions.ShowHeader)
                DrawHeader(htmlToPdfConverter, drawHeaderLineCheckBox.Checked);

            // Add Footer

            // Enable footer in the generated PDF document
            htmlToPdfConverter.PdfDocumentOptions.ShowFooter = addFooterCheckBox.Checked;

            // Optionally add a space between footer and the page body
            // Leave this option not set for no spacing
            htmlToPdfConverter.PdfDocumentOptions.BottomSpacing = float.Parse(footerSpacingTextBox.Text);

            // Draw footer elements
            if (htmlToPdfConverter.PdfDocumentOptions.ShowFooter)
                DrawFooter(htmlToPdfConverter, addPageNumbersInFooterCheckBox.Checked, drawFooterLineCheckBox.Checked);

            // Convert the HTML page to a PDF document in a memory buffer
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertUrl(urlTextBox.Text);

            // Send the PDF as response to browser

            // Set response content type
            Response.AddHeader("Content-Type", "application/pdf");

            // Instruct the browser to open the PDF file as an attachment or inline
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=HTML_in_Header_Footer.pdf; size={0}", outPdfBuffer.Length.ToString()));

            // Write the PDF document buffer to HTTP response
            Response.BinaryWrite(outPdfBuffer);

            // End the HTTP response and stop the current page processing
            Response.End();
        }

        /// <summary>
        /// The handler for HtmlToPdfConverter.PrepareRenderPdfPageEvent event where you can set the visibility of header and footer
        /// in each page or you can add a custom header or footer in a page
        /// </summary>
        /// <param name="eventParams">The event parameter containin the PDF page to customize before rendering</param>
        void htmlToPdfConverter_PrepareRenderPdfPageEvent(PrepareRenderPdfPageParams eventParams)
        {
            // Set the header visibility in first, odd and even pages
            if (addHeaderCheckBox.Checked)
            {
                if (eventParams.PageNumber == 1)
                    eventParams.Page.ShowHeader = showHeaderInFirstPageCheckBox.Checked;
                else if ((eventParams.PageNumber % 2) == 0 && !showHeaderInEvenPagesCheckBox.Checked)
                    eventParams.Page.ShowHeader = false;
                else if ((eventParams.PageNumber % 2) == 1 && !showHeaderInOddPagesCheckBox.Checked)
                    eventParams.Page.ShowHeader = false;
            }

            // Set the footer visibility in first, odd and even pages
            if (addFooterCheckBox.Checked)
            {
                if (eventParams.PageNumber == 1)
                    eventParams.Page.ShowFooter = showFooterInFirstPageCheckBox.Checked;
                else if ((eventParams.PageNumber % 2) == 0 && !showFooterInEvenPagesCheckBox.Checked)
                    eventParams.Page.ShowFooter = false;
                else if ((eventParams.PageNumber % 2) == 1 && !showFooterInOddPagesCheckBox.Checked)
                    eventParams.Page.ShowFooter = false;
            }
        }

        /// <summary>
        /// Draw the header elements
        /// </summary>
        /// <param name="htmlToPdfConverter">The HTML to PDF Converter object</param>
        /// <param name="drawHeaderLine">A flag indicating if a line should be drawn at the bottom of the header</param>
        private void DrawHeader(HtmlToPdfConverter htmlToPdfConverter, bool drawHeaderLine)
        {
            string headerHtmlUrl = Server.MapPath("~/DemoAppFiles/Input/HTML_Files/Header_HTML.html");

            // Set the header height in points
            htmlToPdfConverter.PdfHeaderOptions.HeaderHeight = 60;

            // Set header background color
            htmlToPdfConverter.PdfHeaderOptions.HeaderBackColor = Color.White;

            // Create a HTML element to be added in header
            HtmlToPdfElement headerHtml = new HtmlToPdfElement(headerHtmlUrl);

            // Set the HTML element to fit the container height
            headerHtml.FitHeight = true;

            // Add HTML element to header
            htmlToPdfConverter.PdfHeaderOptions.AddElement(headerHtml);

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
        }

        /// <summary>
        /// Draw the footer elements
        /// </summary>
        /// <param name="htmlToPdfConverter">The HTML to PDF Converter object</param>
        /// <param name="addPageNumbers">A flag indicating if the page numbering is present in footer</param>
        /// <param name="drawFooterLine">A flag indicating if a line should be drawn at the top of the footer</param>
        private void DrawFooter(HtmlToPdfConverter htmlToPdfConverter, bool addPageNumbers, bool drawFooterLine)
        {
            string footerHtmlUrl = Server.MapPath("~/DemoAppFiles/Input/HTML_Files/Footer_HTML.html");

            // Set the footer height in points
            htmlToPdfConverter.PdfFooterOptions.FooterHeight = 60;

            // Set footer background color
            htmlToPdfConverter.PdfFooterOptions.FooterBackColor = Color.WhiteSmoke;

            // Create a HTML element to be added in footer
            HtmlToPdfElement footerHtml = new HtmlToPdfElement(footerHtmlUrl);

            // Set the HTML element to fit the container height
            footerHtml.FitHeight = true;

            // Add HTML element to footer
            htmlToPdfConverter.PdfFooterOptions.AddElement(footerHtml);

            // Add page numbering
            if (addPageNumbers)
            {
                // Create a text element with page numbering place holders &p; and & P;
                TextElement footerText = new TextElement(0, 30, "Page &p; of &P;  ",
                    new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 10, System.Drawing.GraphicsUnit.Point));

                // Align the text at the right of the footer
                footerText.TextAlign = HorizontalTextAlign.Right;

                // Set page numbering text color
                footerText.ForeColor = Color.Navy;

                // Embed the text element font in PDF
                footerText.EmbedSysFont = true;

                // Add the text element to footer
                htmlToPdfConverter.PdfFooterOptions.AddElement(footerText);
            }

            if (drawFooterLine)
            {
                // Calculate the footer width based on PDF page size and margins
                float footerWidth = htmlToPdfConverter.PdfDocumentOptions.PdfPageSize.Width -
                            htmlToPdfConverter.PdfDocumentOptions.LeftMargin - htmlToPdfConverter.PdfDocumentOptions.RightMargin;

                // Create a line element for the top of the footer
                LineElement footerLine = new LineElement(0, 0, footerWidth, 0);

                // Set line color
                footerLine.ForeColor = Color.Gray;

                // Add line element to the bottom of the footer
                htmlToPdfConverter.PdfFooterOptions.AddElement(footerLine);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/Headers_and_Footers/HTML_in_Header_Footer.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/Headers_and_Footers/HTML_in_Header_Footer.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("Headers_and_Footers");
                Master.SelectNode("HTML_in_Header_Footer");
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