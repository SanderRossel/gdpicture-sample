using GdPicture14;
using GdPicture14.WEB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace GdPictureDemo
{
    public class DocuViewareCustomActionsHandler
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<DocuViewareCustomActionsHandler> logger;

        public DocuViewareCustomActionsHandler(IWebHostEnvironment env,
            ILogger<DocuViewareCustomActionsHandler> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void SetStar(CustomActionEventArgs e)
        {
            var status = e.docuVieware.GetNativePDF(out GdPicturePDF pdf);
            ThrowIfFailed(status, "Opening the PDF");

            string imageId = pdf.AddJpegImageFromFile(Path.Combine(env.WebRootPath, "Images", "star.jpg"));
            status = pdf.DrawImage(imageId, 1, 1, 100, 100);
            ThrowIfFailed(status, "Setting the image");

            status = e.docuVieware.RedrawPage();
            ThrowIfFailed(status, "Redrawing page");

            e.message = new DocuViewareMessage("Star was set successfully!");
            e.result = "This result is returned to the front-end.";
        }

        private void ThrowIfFailed(GdPictureStatus status, string step)
        {
            if (status != GdPictureStatus.OK)
            {
                string message = $"{step} failed: {status.ToString()}";
                logger.LogError(message);
                throw new Exception(message);
            }
        }
    }
}
