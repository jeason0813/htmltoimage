using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF.PDF_Pages_Background
{
    public partial class Add_Elements_Over_Main_Content : System.Web.UI.Page
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

            // Set a handler for AfterRenderPdfPageEvent where to add the stamp in each PDF page over the main content
            htmlToPdfConverter.AfterRenderPdfPageEvent += new AfterRenderPdfPageDelegate(htmlToPdfConverter_AfterRenderPdfPageEvent);

            try
            {
                // The buffer to receive the generated PDF document
                byte[] outPdfBuffer = null;

                if (convertUrlRadioButton.Checked)
                {
                    string url = urlTextBox.Text;

                    // Convert the HTML page given by an URL to a PDF document in a memory buffer
                    outPdfBuffer = htmlToPdfConverter.ConvertUrl(url);
                }
                else
                {
                    string htmlString = htmlStringTextBox.Text;
                    string baseUrl = baseUrlTextBox.Text;

                    // Convert a HTML string with a base URL to a PDF document in a memory buffer
                    outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlString, baseUrl);
                }

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Add_Elements_Over_Main_Content.pdf; size={0}", outPdfBuffer.Length.ToString()));

                // Write the PDF document buffer to HTTP response
                Response.BinaryWrite(outPdfBuffer);

                // End the HTTP response and stop the current page processing
                Response.End();
            }
            finally
            {
                // Uninstall the handler
                htmlToPdfConverter.AfterRenderPdfPageEvent -= new AfterRenderPdfPageDelegate(htmlToPdfConverter_AfterRenderPdfPageEvent);
            }
        }

        void htmlToPdfConverter_AfterRenderPdfPageEvent(AfterRenderPdfPageParams eventParams)
        {
            if (!addStampCheckBox.Checked)
                return;

            // Get the rendered PDF page
            PdfPage pdfPage = eventParams.Page;

            int pageNumber = eventParams.PageNumber;
            int pageCount = eventParams.PageCount;
            bool isOddPage = pageNumber % 2 != 0;

            // Get the PDF document
            Document pdfDocument = pdfPage.Document;

            // Get the PDF page drawable area width and height
            float pdfPageWidth = pdfPage.ClientRectangle.Width;
            float pdfPageHeight = pdfPage.ClientRectangle.Height;

            // Create a .NET font
            Font timesNewRomanFont = new Font("Times New Roman", 50, GraphicsUnit.Point);

            // Create a PDF font
            PdfFont pdfFont = pdfDocument.AddFont(timesNewRomanFont, true);

            // The stamp text
            string text = String.Format("Stamp on Page {0} of {1}", pageNumber, pageCount);

            // Measure the text 
            float textWidth = pdfFont.GetTextWidth(text);

            // Calculate the PDF page diagonal
            float pdfPageDiagonal = (float)Math.Sqrt(pdfPageWidth * pdfPageWidth + pdfPageHeight * pdfPageHeight);

            // The text location on PDF page diagonal
            float xLocation = (pdfPageDiagonal - textWidth) / 2;

            // Create the stamp as a rotated text element
            TextElement stampTextElement = new TextElement(xLocation, 0, text, pdfFont);
            stampTextElement.ForeColor = isOddPage ? Color.Blue : Color.Green;
            stampTextElement.Rotate((float)(Math.Atan(pdfPageHeight / pdfPageWidth) * (180 / Math.PI)));
            stampTextElement.Opacity = 75;

            // Add the stamp to PDF page
            pdfPage.AddElement(stampTextElement);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/PDF_Pages_Back/Add_Elements_in_Front.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/PDF_Pages_Back/Add_Elements_in_Front.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("PDF_Background_Foreground");
                Master.SelectNode("Add_Elements_Over_Main_Content");
            }
        }
        
        protected void convertUrlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            urlPanel.Visible = convertUrlRadioButton.Checked;
            htmlStringPanel.Visible = !convertUrlRadioButton.Checked;
        }

        protected void convertHtmlRadioButton_CheckedChanged(object sender, EventArgs e)
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
    }
}