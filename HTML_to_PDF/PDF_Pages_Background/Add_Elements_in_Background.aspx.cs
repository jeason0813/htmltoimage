using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF.PDF_Pages_Background
{
    public partial class Add_Elements_in_Background : System.Web.UI.Page
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

            // Set a handler for BeforeRenderPdfPageEvent where to set the background image in each PDF page before main content is rendered
            htmlToPdfConverter.BeforeRenderPdfPageEvent += new BeforeRenderPdfPageDelegate(htmlToPdfConverter_BeforeRenderPdfPageEvent);

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
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Add_Elements_in_Background.pdf; size={0}", outPdfBuffer.Length.ToString()));

                // Write the PDF document buffer to HTTP response
                Response.BinaryWrite(outPdfBuffer);

                // End the HTTP response and stop the current page processing
                Response.End();
            }
            finally
            {
                // Uninstall the handler
                htmlToPdfConverter.BeforeRenderPdfPageEvent -= new BeforeRenderPdfPageDelegate(htmlToPdfConverter_BeforeRenderPdfPageEvent);
            }
        }

        /// <summary>
        /// The BeforeRenderPdfPageEvent event handler where a background image is set in each PDF page
        /// before the main content is rendered
        /// </summary>
        /// <param name="eventParams">The event parameter containing the PDF page being rendered</param>
        void htmlToPdfConverter_BeforeRenderPdfPageEvent(BeforeRenderPdfPageParams eventParams)
        {
            if (!addBackgroundImageCheckBox.Checked)
                return;

            // Get the PDF page being rendered
            PdfPage pdfPage = eventParams.Page;

            // Get the PDF page drawable area width and height
            float pdfPageWidth = pdfPage.ClientRectangle.Width;
            float pdfPageHeight = pdfPage.ClientRectangle.Height;

            // The image to be added as background
            string backgroundImagePath = Server.MapPath("~/DemoAppFiles/Input/Images/background.jpg");

            // The image element to add in background
            ImageElement backgroundImageElement = new ImageElement(0, 0, pdfPageWidth, pdfPageHeight, backgroundImagePath);
            backgroundImageElement.KeepAspectRatio = true;
            backgroundImageElement.EnlargeEnabled = true;

            // Add the background image element to PDF page before the main content is rendered
            pdfPage.AddElement(backgroundImageElement);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/PDF_Pages_Back/Add_Elements_in_Back.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/PDF_Pages_Back/Add_Elements_in_Back.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("PDF_Background_Foreground");
                Master.SelectNode("Add_Elements_in_Background");
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