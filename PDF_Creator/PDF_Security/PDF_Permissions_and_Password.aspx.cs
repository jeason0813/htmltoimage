﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.PDF_Creator.PDF_Security
{
    public partial class PDF_Permissions_and_Password : System.Web.UI.Page
    {
        protected void convertToPdfButton_Click(object sender, EventArgs e)
        {
            // Create a PDF document
            Document pdfDocument = new Document();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            pdfDocument.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Add a page to PDF document
            PdfPage pdfPage = pdfDocument.AddPage();

            // Set user and owner passwords
            if (userPasswordTextBox.Text.Length > 0)
                pdfDocument.Security.UserPassword = userPasswordTextBox.Text;

            if (ownerPasswordTextBox.Text.Length > 0)
                pdfDocument.Security.OwnerPassword = ownerPasswordTextBox.Text;

            if (pdfDocument.Security.UserPassword != null && pdfDocument.Security.UserPassword.Length > 0 &&
                (pdfDocument.Security.OwnerPassword == null || pdfDocument.Security.OwnerPassword.Length == 0 ||
                pdfDocument.Security.UserPassword == pdfDocument.Security.OwnerPassword))
            {
                // A user password is set but the owner password is not set or is identical to user password
                // Set a different owner password
                pdfDocument.Security.OwnerPassword = pdfDocument.Security.UserPassword + "_owner";
            }

            // Set PDF document permissions
            pdfDocument.Security.CanPrint = printEnabledCheckBox.Checked;
            pdfDocument.Security.CanCopyContent = copyContentEnabledCheckBox.Checked;
            pdfDocument.Security.CanEditContent = editContentEnabledCheckBox.Checked;
            pdfDocument.Security.CanEditAnnotations = editAnnotationsEnabledCheckBox.Checked;
            pdfDocument.Security.CanFillFormFields = fillFormFieldsEnabledCheckBox.Checked;

            try
            {
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
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Set_Permissions_Password.pdf; size={0}", outPdfBuffer.Length.ToString()));

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
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/PDF_Creator/PDF_Security/PDF_Permissions.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/PDF_Creator/PDF_Security/PDF_Permissions.html"));

                Master.CollapseAll();
                Master.ExpandNode("PDF_Creator");
                Master.ExpandNode("PDF_Creator_PDF_Security");
                Master.SelectNode("PDF_Creator_PDF_Permissions_and_Password");
            }
        }

        protected void editContentEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (editContentEnabledCheckBox.Checked)
                fillFormFieldsEnabledCheckBox.Checked = true;
        }

        protected void editAnnotationsEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (editAnnotationsEnabledCheckBox.Checked)
                fillFormFieldsEnabledCheckBox.Checked = true;
        }

        protected void fillFormFieldsEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!fillFormFieldsEnabledCheckBox.Checked)
            {
                editContentEnabledCheckBox.Checked = false;
                editAnnotationsEnabledCheckBox.Checked = false;
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