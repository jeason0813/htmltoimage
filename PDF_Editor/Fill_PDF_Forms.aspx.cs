using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.PDF_Editor
{
    public partial class Fill_PDF_Forms : System.Web.UI.Page
    {
        protected void fillPdfButton_Click(object sender, EventArgs e)
        {
            Document pdfDocument = null;
            try
            {
                // Load the PDF document with a form to fill
                string pdfFileWithForm = Server.MapPath("~/DemoAppFiles/Input/PDF_Files/Fill_PDF_Forms.pdf");
                pdfDocument = new Document(pdfFileWithForm);

                // Set license key received after purchase to use the converter in licensed mode
                // Leave it not set to use the converter in demo mode
                pdfDocument.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

                // Get the First Name text box field by name from PDF form fields collection and fill a text value
                PdfFormField firstNameTextBoxField = pdfDocument.Form.Fields["firstName"];
                if (firstNameTextBoxField != null)
                    firstNameTextBoxField.Value = firstNameTextBox.Text;

                // Get the Last Name text box field by name from PDF form fields collection and fill a text value
                PdfFormField lastNameTextBoxField = pdfDocument.Form.Fields["lastName"];
                if (lastNameTextBoxField != null)
                    lastNameTextBoxField.Value = lastNameTextBox.Text;

                // Get the Password text box field by name from PDF form fields collection and fill a text value
                PdfFormField passwordTextBoxField = pdfDocument.Form.Fields["password"];
                if (passwordTextBoxField != null)
                    passwordTextBoxField.Value = passwordTextBox.Text;

                // Get the Gender radio buttons group field by name from form fields collection and set the selected value
                PdfFormField genderRadioGroupField = pdfDocument.Form.Fields["gender"];
                if (genderRadioGroupField != null)
                    genderRadioGroupField.Value = maleRadioButton.Checked ? "male" : "female";

                // Get the 'I have a car' check box field by name from form fields collection and set the checked status
                PdfFormField haveCarCheckBoxField = pdfDocument.Form.Fields["haveCar"];
                if (haveCarCheckBoxField != null)
                    haveCarCheckBoxField.Value = haveCarCheckBox.Checked;

                // Get the 'Vehicle Type' combo box field by name from form fields collection and set the selected value
                PdfFormField vehicleTypeComboBoxField = pdfDocument.Form.Fields["vehicleType"];
                if (vehicleTypeComboBoxField != null)
                    vehicleTypeComboBoxField.Value = carTypeComboBox.SelectedValue;

                // Get the Comments multi line text box field by name from PDF form fields collection and fill a text value
                PdfFormField commentsTextBoxField = pdfDocument.Form.Fields["comments"];
                if (commentsTextBoxField != null)
                    commentsTextBoxField.Value = commentsTextBox.Text;

                // Flatten PDF form fields if this was requested
                if (flattenPdfFormCheckBox.Checked)
                    pdfDocument.Form.FlattenFields();

                // Save the PDF document in a memory buffer
                byte[] outPdfBuffer = pdfDocument.Save();

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Filled_PDF_Form.pdf.pdf; size={0}", outPdfBuffer.Length.ToString()));

                // Write the PDF document buffer to HTTP response
                Response.BinaryWrite(outPdfBuffer);

                // End the HTTP response and stop the current page processing
                Response.End();
            }
            finally
            {
                // Close the document to stamp
                if (pdfDocument != null)
                    pdfDocument.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carTypeComboBox.SelectedValue = "Volvo";

                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/PDF_Editor/Fill_PDF_Forms.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/PDF_Editor/Fill_PDF_Forms.html"));

                string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string rootUrl = currentPageUrl.Substring(0, currentPageUrl.LastIndexOf('/')) + "/../";

                pdfFileToFillHyperLink.NavigateUrl = rootUrl + "DemoAppFiles/Input/PDF_Files/Fill_PDF_Forms.pdf";

                Master.CollapseAll();
                Master.ExpandNode("PDF_Editor");
                Master.SelectNode("Fill_PDF_Forms");
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