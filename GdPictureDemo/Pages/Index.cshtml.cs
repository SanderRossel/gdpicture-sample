using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace GdPictureDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment env;

        public IndexModel(IWebHostEnvironment env) => this.env = env;

        public string Document { get; set; } = "Invoice_template_forms.pdf";
        public string DocumentPath { get; set; }
        public string SessionId { get; set; }

        public void OnGet()
        {
            DocumentPath = Path.Combine(env.WebRootPath, "Documents", Document);
            SessionId = HttpContext.Session.Id + "DocuVieware";
        }
    }
}
