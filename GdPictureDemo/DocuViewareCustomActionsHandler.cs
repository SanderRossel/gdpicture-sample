using GdPicture14;
using GdPicture14.WEB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
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

        public void HandleSignPdf(CustomActionEventArgs e)
        {
            var status = e.docuVieware.GetNativePDF(out GdPicturePDF pdf);
            ThrowIfFailed(status, "Opening the PDF");

            pdf.FlattenFormFields();

            // Step 1. This step is mandatory and should be done first.
            status = pdf.SetSignatureCertificateFromP12(@"C:\Temp\mycert.pfx", "mypass1!");
            ThrowIfFailed(status, "Setting certificate");

            // Step 2. This step is mandatory and should be done second.
            status = pdf.SetSignatureInfo("My company", "Important PDF", "The Netherlands", "contact@mycompany.com");
            ThrowIfFailed(status, "Setting signature info");

            // Steps 3 and 4 are optional and can be done in any order.
            // Step 5 is mandatory and should be done last.

            // Step 3a.
            status = pdf.SetSignaturePos(10, 10, 300, 100);
            ThrowIfFailed(status, "Setting signature position");

            // Step 3b.
            status = pdf.SetSignatureText("Signature", string.Empty, 12, Color.Navy,
                TextAlignment.TextAlignmentCenter, TextAlignment.TextAlignmentCenter, true);
            ThrowIfFailed(status, "Setting signature text");

            // Step 3c.
            string imageName = pdf.AddJpegImageFromFile(Path.Combine(env.WebRootPath, "Documents", "star.jpg"));
            status = pdf.GetStat();
            ThrowIfFailed(status, $"Setting signature image {imageName}");
            status = pdf.SetSignatureStampImage(imageName);
            ThrowIfFailed(status, "Setting signature image stamp");

            // Step 3d.
            status = pdf.SetSignatureValidationMark(true);
            ThrowIfFailed(status, "Setting signature validation mark");

            // Step 4a.
            status = pdf.SetSignatureCertificationLevel(PdfSignatureCertificationLevel.NoChanges);
            ThrowIfFailed(status, "Setting certification level");

            // Step 4b.
            status = pdf.SetSignatureHash(PdfSignatureHash.SHA512);
            ThrowIfFailed(status, "Setting hash algorithm");

            // Step 4c.
            status = pdf.SetSignatureTimestampInfo("Timestamp server URL", "Username", "Password");
            ThrowIfFailed(status, "Setting timestamp server");

            // Step 5. This step is mandatory and should be done last.
            string filePath = @"C:\Temp\signed.pdf";
            status = pdf.ApplySignature(filePath, PdfSignatureMode.PdfSignatureModeAdobePPKMS, true);
            ThrowIfFailed(status, "Applying the signature");

            var pdfToSave = new GdPicturePDF();
            pdfToSave.LoadFromFile(filePath);
            using MemoryStream stream = new MemoryStream();
            pdfToSave.SaveToStream(stream);
            byte[] content = stream.ToArray();
            e.result = Convert.ToBase64String(content);
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
