using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF.File_Links_Attachments
{
    public partial class Create_File_Links_and_Atachments : System.Web.UI.Page
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

            Document pdfDocument = null;
            try
            {
                string htmlWithLinksAndAttachMarkers = htmlStringTextBox.Text;
                string baseUrl = baseUrlTextBox.Text;

                // Convert a HTML string with markers for file links and attachments to a PDF document object
                pdfDocument = htmlToPdfConverter.ConvertHtmlToPdfDocumentObject(htmlWithLinksAndAttachMarkers, baseUrl);

                // Display the attachments panel when the PDF document is opened in a PDF viewer
                pdfDocument.ViewerPreferences.PageMode = ViewerPageMode.UseAttachments;

                // Create File Attachments

                // Create an attachment from a file without icon
                string fileAttachmentPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_File.txt");
                pdfDocument.AddFileAttachment(fileAttachmentPath, "Attachment from File");

                // Create an attachment from a stream without icon
                string fileStreamAttachmentPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_Stream.txt");
                System.IO.FileStream attachmentStream = new System.IO.FileStream(fileStreamAttachmentPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                pdfDocument.AddFileAttachment(attachmentStream, "Attachment_Stream.txt", "Attachment from Stream");

                // Create an attachment from file with paperclip icon in PDF
                HtmlElementMapping attachFromFileIconMapping = htmlToPdfConverter.HtmlElementsMappingOptions.HtmlElementsMappingResult.GetElementByMappingId("attach_from_file_icon");
                if (attachFromFileIconMapping != null)
                {
                    PdfPage attachFromFilePage = attachFromFileIconMapping.PdfRectangles[0].PdfPage;
                    RectangleF attachFromFileIconRectangle = attachFromFileIconMapping.PdfRectangles[0].Rectangle;

                    string fileAttachmentWithIconPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_File_Icon.txt");

                    // Create the attachment from file
                    FileAttachmentElement attachFromFileElement = new FileAttachmentElement(attachFromFileIconRectangle, fileAttachmentWithIconPath);
                    attachFromFileElement.IconType = FileAttachmentIcon.Paperclip;
                    attachFromFileElement.Text = "Attachment from File with Paperclip Icon";
                    attachFromFileElement.IconColor = Color.Blue;
                    attachFromFilePage.AddElement(attachFromFileElement);
                }

                // Create an attachment from stream with pushpin icon in PDF
                System.IO.FileStream attachmentStreamWithIcon = null;
                HtmlElementMapping attachFromStreamIconMapping = htmlToPdfConverter.HtmlElementsMappingOptions.HtmlElementsMappingResult.GetElementByMappingId("attach_from_stream_icon");
                if (attachFromStreamIconMapping != null)
                {
                    PdfPage attachFromStreamPage = attachFromStreamIconMapping.PdfRectangles[0].PdfPage;
                    RectangleF attachFromStreamIconRectangle = attachFromStreamIconMapping.PdfRectangles[0].Rectangle;

                    string fileStreamAttachmentWithIconPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_Stream_Icon.txt");

                    attachmentStreamWithIcon = new System.IO.FileStream(fileStreamAttachmentWithIconPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                    // Create the attachment from stream
                    FileAttachmentElement attachFromStreamElement = new FileAttachmentElement(attachFromStreamIconRectangle, attachmentStreamWithIcon, "Attachment_Stream_Icon.txt");
                    attachFromStreamElement.IconType = FileAttachmentIcon.PushPin;
                    attachFromStreamElement.Text = "Attachment from Stream with Pushpin Icon";
                    attachFromStreamElement.IconColor = Color.Green;
                    attachFromStreamPage.AddElement(attachFromStreamElement);
                }

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=File_Links_and_Attachments.pdf; size={0}", outPdfBuffer.Length.ToString()));

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
                string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string rootUrl = currentPageUrl.Substring(0, currentPageUrl.LastIndexOf('/')) + "/../../";

                htmlStringTextBox.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/HTML_Files/File_Attachments.html"));
                baseUrlTextBox.Text = rootUrl + "DemoAppFiles/Input/HTML_Files/";

                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/Links_Attach/Create_Links_Atach.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/Links_Attach/Create_Links_Atach.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("File_Links_and_Attachments");
                Master.SelectNode("Create_File_Links_and_Attachments");
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