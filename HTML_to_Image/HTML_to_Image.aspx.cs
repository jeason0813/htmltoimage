﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.HTML_to_Image
{
    public partial class HTML_to_Image : System.Web.UI.Page
    {
        protected void convertToImageButton_Click(object sender, EventArgs e)
        {
            // Create a HTML to Image converter object with default settings
            HtmlToImageConverter htmlToImageConverter = new HtmlToImageConverter();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            htmlToImageConverter.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Set HTML Viewer width in pixels which is the equivalent in converter of the browser window width
            htmlToImageConverter.HtmlViewerWidth = int.Parse(htmlViewerWidthTextBox.Text);

            // Set HTML viewer height in pixels to convert the top part of a HTML page 
            // Leave it not set to convert the entire HTML
            if (htmlViewerHeightTextBox.Text.Length > 0)
                htmlToImageConverter.HtmlViewerHeight = int.Parse(htmlViewerHeightTextBox.Text);

            // Set if the created image has a transparent background
            htmlToImageConverter.TransparentBackground = SelectedImageFormat() == System.Drawing.Imaging.ImageFormat.Png ? transparentBackgroundCheckBox.Checked : false;

            // Set the maximum time in seconds to wait for HTML page to be loaded 
            // Leave it not set for a default 60 seconds maximum wait time
            htmlToImageConverter.NavigationTimeout = int.Parse(navigationTimeoutTextBox.Text);

            // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
            // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
            if (conversionDelayTextBox.Text.Length > 0)
                htmlToImageConverter.ConversionDelay = int.Parse(conversionDelayTextBox.Text);

            System.Drawing.Image[] imageTiles = null;

            if (convertUrlRadioButton.Checked)
            {
                string url = urlTextBox.Text;

                // Convert the HTML page given by an URL to a set of Image objects
                imageTiles = htmlToImageConverter.ConvertUrlToImageTiles(url);
            }
            else
            {
                string htmlString = htmlStringTextBox.Text;
                string baseUrl = baseUrlTextBox.Text;

                // Convert a HTML string with a base URL to a set of Image objects
                imageTiles = htmlToImageConverter.ConvertHtmlToImageTiles(htmlString, baseUrl);
            }

            // Save the first image tile to a memory buffer

            System.Drawing.Image outImage = imageTiles[0];

            // Create a memory stream where to save the image
            System.IO.MemoryStream imageOutputStream = new System.IO.MemoryStream();

            // Save the image to memory stream
            outImage.Save(imageOutputStream, SelectedImageFormat());

            // Write the memory stream to a memory buffer
            imageOutputStream.Position = 0;
            byte[] outImageBuffer = imageOutputStream.ToArray();

            // Close the output memory stream
            imageOutputStream.Close();

            // Send the image as response to browser

            string imageFormatName = imageFormatComboBox.SelectedValue.ToLower();

            // Set response content type
            Response.AddHeader("Content-Type", "image/" + (imageFormatName == "jpg" ? "jpeg" : imageFormatName));

            // Instruct the browser to open the image file as an attachment or inline
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}; size={1}", "HTML_to_Image." + imageFormatName, outImageBuffer.Length.ToString()));

            // Write the image buffer to HTTP response
            Response.BinaryWrite(outImageBuffer);

            // End the HTTP response and stop the current page processing
            Response.End();
        }

        private System.Drawing.Imaging.ImageFormat SelectedImageFormat()
        {
            switch (imageFormatComboBox.SelectedValue)
            {
                case "Png":
                    return System.Drawing.Imaging.ImageFormat.Png;
                case "Jpg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case "Bmp":
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                default:
                    return System.Drawing.Imaging.ImageFormat.Png;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imageFormatComboBox.SelectedValue = "Png";

                urlPanel.Visible = convertUrlRadioButton.Checked;
                htmlStringPanel.Visible = !convertUrlRadioButton.Checked;

                sampleCodeLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Code_Samples/CSharp/AspNet/HTML_to_Image/HTML_to_Image.html"));
                descriptionLiteral.Text = System.IO.File.ReadAllText(Server.MapPath("~/DemoAppFiles/Input/Descriptions/AspNet/HTML_to_Image/HTML_to_Image.html"));

                Master.CollapseAll();
                Master.ExpandNode("HTML_to_Image");
                Master.SelectNode("Convert_HTML_to_Image");
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

        protected void imageFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            transparentBackgroundCheckBox.Visible = SelectedImageFormat() == System.Drawing.Imaging.ImageFormat.Png;
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