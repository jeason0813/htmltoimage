using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.PDF_Creator
{
    public partial class File_Attachments : System.Web.UI.Page
    {
        protected void createPdfButton_Click(object sender, EventArgs e)
        {
            // Create a PDF document
            Document pdfDocument = new Document();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            pdfDocument.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Display the attachments panel when the PDF document is opened in a PDF viewer
            pdfDocument.ViewerPreferences.PageMode = ViewerPageMode.UseAttachments;

            // Add a page to PDF document
            PdfPage pdfPage = pdfDocument.AddPage();

            try
            {
                // The titles font used to mark various sections of the PDF document
                PdfFont titleFont = pdfDocument.AddFont(new Font("Times New Roman", 10, FontStyle.Bold, GraphicsUnit.Point));
                PdfFont subtitleFont = pdfDocument.AddFont(new Font("Times New Roman", 8, FontStyle.Regular, GraphicsUnit.Point));

                float xLocation = 5;
                float yLocation = 5;

                // Add document title
                TextElement titleTextElement = new TextElement(xLocation, yLocation, "Attach Files and Streams to a PDF Document", titleFont);
                AddElementResult addElementResult = pdfPage.AddElement(titleTextElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 15;

                // Create an attachment from a file without icon
                string fileAttachmentPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_File.txt");
                pdfDocument.AddFileAttachment(fileAttachmentPath, "Attachment from File");

                // Create an attachment from a stream without icon
                string fileStreamAttachmentPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_Stream.txt");
                System.IO.FileStream attachmentStream = new System.IO.FileStream(fileStreamAttachmentPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                pdfDocument.AddFileAttachment(attachmentStream, "Attachment_Stream.txt", "Attachment from Stream");

                // Add the text element
                string text = "Click the next icon to open the attachment from a file:";
                float textWidth = subtitleFont.GetTextWidth(text);
                TextElement textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                // Create an attachment from file with paperclip icon in PDF
                string fileAttachmentWithIconPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_File_Icon.txt");
                // Create the attachment from file
                RectangleF attachFromFileIconRectangle = new RectangleF(xLocation + textWidth + 3, yLocation, 6, 10);
                FileAttachmentElement attachFromFileElement = new FileAttachmentElement(attachFromFileIconRectangle, fileAttachmentWithIconPath);
                attachFromFileElement.IconType = FileAttachmentIcon.Paperclip;
                attachFromFileElement.Text = "Attachment from File with Paperclip Icon";
                attachFromFileElement.IconColor = Color.Blue;
                pdfPage.AddElement(attachFromFileElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "Click the next icon to open the attachment from a stream:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                // Create an attachment from stream with pushpin icon in PDF
                string fileStreamAttachmentWithIconPath = Server.MapPath("~/DemoAppFiles/Input/Attach_Files/Attachment_Stream_Icon.txt");
                System.IO.FileStream attachmentStreamWithIcon = new System.IO.FileStream(fileStreamAttachmentWithIconPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                // Create the attachment from stream
                RectangleF attachFromStreamIconRectangle = new RectangleF(xLocation + textWidth + 3, yLocation, 6, 10);
                FileAttachmentElement attachFromStreamElement = new FileAttachmentElement(attachFromStreamIconRectangle, attachmentStreamWithIcon, "Attachment_Stream_Icon.txt");
                attachFromStreamElement.IconType = FileAttachmentIcon.PushPin;
                attachFromStreamElement.Text = "Attachment from Stream with Pushpin Icon";
                attachFromStreamElement.IconColor = Color.Green;
                pdfPage.AddElement(attachFromStreamElement);

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=File_Attachments.pdf; size={0}", outPdfBuffer.Length.ToString()));

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/PDF_Creator/File_Attachments.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/PDF_Creator/File_Attachments.html"));

                Master.CollapseAll();
                Master.ExpandNode("PDF_Creator");
                Master.SelectNode("File_Attachments");
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