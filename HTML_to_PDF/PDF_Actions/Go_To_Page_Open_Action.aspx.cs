﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_PDF.PDF_Actions
{
    public partial class Go_To_Page_Open_Action : System.Web.UI.Page
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
                // Convert a HTML page to a PDF document object
                pdfDocument = htmlToPdfConverter.ConvertUrlToPdfDocumentObject(urlTextBox.Text);

                int goToPageNumber = int.Parse(pageNumberTextBox.Text);
                if (goToPageNumber > pdfDocument.Pages.Count)
                {
                    return;
                }

                // Get destination PDF page
                PdfPage goToPage = pdfDocument.Pages[goToPageNumber - 1];

                // Get the destination point in PDF page
                float goToX = float.Parse(xLocationTextBox.Text);
                float goToY = float.Parse(yLocationTextBox.Text);

                PointF goToLocation = new PointF(goToX, goToY);

                // Get the destination view mode
                DestinationViewMode viewMode = SelectedViewMode();

                // Create the destination in PDF document
                ExplicitDestination goToDestination = new ExplicitDestination(goToPage, goToLocation, viewMode);

                // Set the zoom level when the destination is displayed
                if (viewMode == DestinationViewMode.XYZ)
                    goToDestination.ZoomPercentage = int.Parse(zoomLevelTextBox.Text);

                // Set the document Go To open action
                pdfDocument.OpenAction.Action = new PdfActionGoTo(goToDestination);

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Go_To_Page_Open_Action.pdf; size={0}", outPdfBuffer.Length.ToString()));

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

        private DestinationViewMode SelectedViewMode()
        {
            switch (viewModeComboBox.SelectedValue)
            {
                case "X, Y and Zoom":
                    return DestinationViewMode.XYZ;
                case "Fit Window":
                    return DestinationViewMode.Fit;
                case "Fit Horizontally":
                    return DestinationViewMode.FitH;
                case "Fit Vertically":
                    return DestinationViewMode.FitV;
                default:
                    return DestinationViewMode.XYZ;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                viewModeComboBox.SelectedValue = "X, Y and Zoom";

                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_PDF/PDF_Actions/Go_To_Page_Open_Action.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_PDF/PDF_Actions/Go_To_Page_Open_Action.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_PDF");
                Master.ExpandNode("PDF_Actions");
                Master.SelectNode("Go_To_Page_Open_Action");
            }
        }

        protected void viewModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            zoomLevelPanel.Visible = SelectedViewMode() == DestinationViewMode.XYZ;
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