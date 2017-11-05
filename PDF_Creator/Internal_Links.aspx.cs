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
    public partial class Internal_Links : System.Web.UI.Page
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

            // Add second page to PDF document
            PdfPage secondPdfPage = pdfDocument.AddPage();

            // Add third page to PDF document
            PdfPage thirdPdfPage = pdfDocument.AddPage();

            try
            {
                // The titles font used to mark various sections of the PDF document
                PdfFont titleFont = pdfDocument.AddFont(new Font("Times New Roman", 10, FontStyle.Bold, GraphicsUnit.Point));
                PdfFont subtitleFont = pdfDocument.AddFont(new Font("Times New Roman", 8, FontStyle.Regular, GraphicsUnit.Point));

                // The links text font
                PdfFont linkTextFont = pdfDocument.AddFont(new Font("Times New Roman", 8, FontStyle.Bold, GraphicsUnit.Point));
                linkTextFont.IsUnderline = true;

                float xLocation = 5;
                float yLocation = 5;

                // Add document title
                TextElement titleTextElement = new TextElement(xLocation, yLocation, "Create Internal Links in PDF Document", titleFont);
                AddElementResult addElementResult = pdfPage.AddElement(titleTextElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 15;

                // Add a text in second page
                TextElement secondPageTextElement = new TextElement(5, 5, "This text is the target of an internal text link", subtitleFont);
                secondPdfPage.AddElement(secondPageTextElement);

                // Add a text in third page
                TextElement thirdPageTextElement = new TextElement(5, 5, "This text is the target of an internal image link", subtitleFont);
                thirdPdfPage.AddElement(thirdPageTextElement);

                // Make a text in PDF an internal link to the second page of the PDF document

                // Add the text element
                string text = "Click this text to go to the second page of this document!";
                float textWidth = linkTextFont.GetTextWidth(text);
                TextElement linkTextElement = new TextElement(xLocation, yLocation, text, linkTextFont);
                linkTextElement.ForeColor = Color.Navy;
                addElementResult = pdfPage.AddElement(linkTextElement);

                // Make the text element an internal link to the second page of this document
                RectangleF linkRectangle = new RectangleF(xLocation, yLocation, textWidth, addElementResult.EndPageBounds.Height);
                // Create the destination in second page
                ExplicitDestination secondPageDestination = new ExplicitDestination(secondPdfPage, new PointF(5, 5));
                // Create the internal link from text element to second page
                InternalLinkElement internalLink = new InternalLinkElement(linkRectangle, secondPageDestination);

                // Add the internal link to PDF document
                pdfPage.AddElement(internalLink);

                yLocation = addElementResult.EndPageBounds.Bottom + 10;

                // Make an image in PDF an internal link to the third page of the PDF document

                TextElement subtitleTextElement = new TextElement(xLocation, yLocation, "Click the image below to go to the third page of this document:", subtitleFont);
                addElementResult = pdfPage.AddElement(subtitleTextElement);

                yLocation = addElementResult.EndPageBounds.Bottom + 5;

                // Add the image element
                ImageElement linkImageElement = new ImageElement(xLocation, yLocation, 120, Server.MapPath("~/DemoAppFiles/Input/Images/logo.jpg"));
                addElementResult = pdfPage.AddElement(linkImageElement);

                // Make the image element an internal link to the third page of this document
                linkRectangle = addElementResult.EndPageBounds;
                // Create the destination in third page
                ExplicitDestination thirdPageDestination = new ExplicitDestination(thirdPdfPage, new PointF(5, 5));
                // Create the internal link from image element to third page
                internalLink = new InternalLinkElement(linkRectangle, thirdPageDestination);

                // Add the internal link to PDF document
                pdfPage.AddElement(internalLink);

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Internal_Links.pdf; size={0}", outPdfBuffer.Length.ToString()));

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
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/PDF_Creator/Internal_Links.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/PDF_Creator/Internal_Links.html"));

                Master.CollapseAll();
                Master.ExpandNode("PDF_Creator");
                Master.SelectNode("Internal_Links");
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