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
    public partial class Text_Notes : System.Web.UI.Page
    {
        protected void createPdfButton_Click(object sender, EventArgs e)
        {
            // Create a PDF document
            Document pdfDocument = new Document();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            pdfDocument.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

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
                TextElement titleTextElement = new TextElement(xLocation, yLocation, "Add Text Notes to a PDF Document", titleFont);
                AddElementResult addElementResult = pdfPage.AddElement(titleTextElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 15;

                // Add the text element
                string text = "Click the next icon to open the the text note:";
                float textWidth = subtitleFont.GetTextWidth(text);
                TextElement textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                RectangleF textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement textNoteElement = new TextNoteElement(textNoteRectangle, "This is an initially closed text note");
                textNoteElement.NoteIcon = TextNoteIcon.Note;
                textNoteElement.Open = false;
                pdfPage.AddElement(textNoteElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "This is an already opened text note:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement textNoteOpenedElement = new TextNoteElement(textNoteRectangle, "This is an initially opened text note");
                textNoteOpenedElement.NoteIcon = TextNoteIcon.Note;
                textNoteOpenedElement.Open = true;
                pdfPage.AddElement(textNoteOpenedElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "Click the next icon to open the the help note:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement helpNoteElement = new TextNoteElement(textNoteRectangle, "This is an initially closed help note");
                helpNoteElement.NoteIcon = TextNoteIcon.Help;
                helpNoteElement.Open = false;
                pdfPage.AddElement(helpNoteElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "This is an already opened help note:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement helpNoteOpenedElement = new TextNoteElement(textNoteRectangle, "This is an initially opened help note");
                helpNoteOpenedElement.NoteIcon = TextNoteIcon.Help;
                helpNoteOpenedElement.Open = true;
                pdfPage.AddElement(helpNoteOpenedElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "Click the next icon to open the comment:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement commentNoteElement = new TextNoteElement(textNoteRectangle, "This is an initially closed comment note");
                commentNoteElement.NoteIcon = TextNoteIcon.Comment;
                commentNoteElement.Open = false;
                pdfPage.AddElement(commentNoteElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "This is an already opened comment:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement commentNoteOpenedElement = new TextNoteElement(textNoteRectangle, "This is an initially opened comment note");
                commentNoteOpenedElement.NoteIcon = TextNoteIcon.Comment;
                commentNoteOpenedElement.Open = true;
                pdfPage.AddElement(commentNoteOpenedElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "Click the next icon to open the paragraph note: ";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement paragraphNoteElement = new TextNoteElement(textNoteRectangle, "This is an initially closed paragraph note");
                paragraphNoteElement.NoteIcon = TextNoteIcon.Paragraph;
                paragraphNoteElement.Open = false;
                pdfPage.AddElement(paragraphNoteElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "Click the next icon to open the new paragraph note:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement newParagraphNoteElement = new TextNoteElement(textNoteRectangle, "This is an initially closed new paragraph note");
                newParagraphNoteElement.NoteIcon = TextNoteIcon.NewParagraph;
                newParagraphNoteElement.Open = false;
                pdfPage.AddElement(newParagraphNoteElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Add the text element
                text = "Click the next icon to open the key note:";
                textWidth = subtitleFont.GetTextWidth(text);
                textElement = new TextElement(xLocation, yLocation, text, subtitleFont);
                addElementResult = pdfPage.AddElement(textElement);

                textNoteRectangle = new RectangleF(xLocation + textWidth + 1, yLocation, 10, 10);

                // Create the text note
                TextNoteElement keyNoteElement = new TextNoteElement(textNoteRectangle, "This is an initially closed key note");
                keyNoteElement.NoteIcon = TextNoteIcon.Key;
                keyNoteElement.Open = false;
                pdfPage.AddElement(keyNoteElement);

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Text_Notes.pdf; size={0}", outPdfBuffer.Length.ToString()));

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
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/PDF_Creator/Text_Notes.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/PDF_Creator/Text_Notes.html"));

                Master.CollapseAll();
                Master.ExpandNode("PDF_Creator");
                Master.SelectNode("PDF_Creator_Text_Notes");
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