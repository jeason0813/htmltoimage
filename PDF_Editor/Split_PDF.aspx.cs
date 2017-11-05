using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.PDF_Editor
{
    public partial class Split_PDF : System.Web.UI.Page
    {
        protected void splitPdfButton_Click(object sender, EventArgs e)
        {
            Document pdfDocumentToSplit = null;
            Document splitResultDocument1 = null;
            Document splitResultDocument2 = null;
            try
            {
                // Load the PDF document to split
                // The document must remain opened until the PDF documents resulted after split are saved
                string pdfFileToSplitPath = Server.MapPath("~/DemoAppFiles/Input/PDF_Files/PDF_Document.pdf");
                pdfDocumentToSplit = new Document(pdfFileToSplitPath);

                // Get the page count of the document to split
                int pageCount = pdfDocumentToSplit.Pages.Count;

                // Check if the document has the minimum required number of page
                if (pageCount < 2)
                {
                    return;
                }

                // Create the first PDF document resulted after split
                splitResultDocument1 = new Document();
                // Set license key received after purchase
                splitResultDocument1.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";
                // Copy pages from loaded document into split result document
                splitResultDocument1.AppendDocument(pdfDocumentToSplit, 0, pageCount / 2);
                // Save the first PDF document document resulted after split in a memory buffer
                byte[] outPdfBuffer1 = splitResultDocument1.Save();

                // Create the second PDF document resulted after split  
                splitResultDocument2 = new Document();
                // Set license key received after purchase
                splitResultDocument2.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";
                // Copy pages from loaded document into split result document
                splitResultDocument2.AppendDocument(pdfDocumentToSplit, pageCount / 2, pageCount - pageCount / 2);
                // Save the second PDF document document resulted after split in a memory buffer
                byte[] outPdfBuffer2 = splitResultDocument2.Save();

                // Send the PDF as response to browser

                // Set response content type
                Response.AddHeader("Content-Type", "application/pdf");

                // Instruct the browser to open the PDF file as an attachment or inline
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Split_PDF_1.pdf.pdf; size={0}", outPdfBuffer1.Length.ToString()));

                // Write the PDF document buffer to HTTP response
                Response.BinaryWrite(outPdfBuffer1);

                // End the HTTP response and stop the current page processing
                Response.End();
            }
            finally
            {
                // Close all the PDF documents after the PDF documents resulted after split were already saved

                // Close the first split result document
                if (splitResultDocument1 != null)
                    splitResultDocument1.Close();

                // Close the second split result document
                if (splitResultDocument2 != null)
                    splitResultDocument2.Close();

                // Close the document to split
                if (pdfDocumentToSplit != null)
                    pdfDocumentToSplit.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/PDF_Editor/Split_PDF.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/PDF_Editor/Split_PDF.html"));

                string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string rootUrl = currentPageUrl.Substring(0, currentPageUrl.LastIndexOf('/')) + "/../";

                pdfFileToSplitHyperLink.NavigateUrl = rootUrl + "DemoAppFiles/Input/PDF_Files/PDF_Document.pdf";

                Master.CollapseAll();
                Master.ExpandNode("PDF_Editor");
                Master.SelectNode("Split_PDF");
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