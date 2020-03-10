using GdPicture14;
using GdPicture14.WEB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GdPictureDemo.Pages
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DocuViewareController : ControllerBase
    {
        [HttpPost]
        public string Init([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.init(jsonString);
        }
        [HttpPost]
        public string Close([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.close(jsonString);
        }
        [HttpPost]
        public string SelectPage([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.selectpage(jsonString);
        }
        [HttpPost]
        public string Rotate([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.rotate(jsonString);
        }
        [HttpPost]
        public string Bookmarks([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.bookmarks(jsonString);
        }
        [HttpPost]
        public string Thumbnails([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.thumbnails(jsonString);
        }
        [HttpPost]
        public string PageView([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.pageview(jsonString);
        }
        [HttpPost]
        public string RunDestination([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.rundestination(jsonString);
        }
        [HttpPost]
        public string TextSearch([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.textsearch(jsonString);
        }
        [HttpPost]
        public string ZoomMode([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.zoommode(jsonString);
        }
        [HttpPost]
        public string ZoomSet([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.zoomset(jsonString);
        }
        [HttpPost]
        public string SinglePageViewMode([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.singlepageviewmode(jsonString);
        }
        [HttpPost]
        public string ZoomIn([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.zoomin(jsonString);
        }
        [HttpPost]
        public string ZoomOut([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.zoomout(jsonString);
        }
        [HttpPost]
        public string ZoomRect([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.zoomrect(jsonString);
        }
        [HttpPost]
        public string NewAnnotation([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.newannotation(jsonString);
        }
        [HttpPost]
        public string NewAnnotationComment([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.newannotationcomment(jsonString);
        }
        [HttpPost]
        public string Print([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.print(jsonString);
        }
        [HttpPost]
        public string CustomAction([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.customaction(jsonString);
        }
        [HttpGet]
        public async Task Print(string sessionID, string pageRange, bool printAnnotations)
        {
            var message = DocuViewareControllerActionsHandler.print(sessionID, pageRange, printAnnotations);
            await HandleGet(message, Response);
        }
        [HttpGet]
        public async Task Save(string sessionID, string fileName, string format, string pageRange)
        {
            var message = DocuViewareControllerActionsHandler.save(sessionID, fileName, format, pageRange);
            await HandleGet(message, Response);
        }
        private async Task HandleGet(HttpResponseMessage message, HttpResponse response)
        {
            response.StatusCode = (int)message.StatusCode;

            foreach (var header in message.Headers)
            {
                response.Headers.TryAdd(header.Key, new StringValues(header.Value.ToArray()));
            }

            using var stream = await message.Content.ReadAsStreamAsync();
            await stream.CopyToAsync(response.Body);
            await response.Body.FlushAsync();
        }
        [HttpGet]
        public async Task TwainServiceSetupDownload(string sessionID)
        {
            var message = DocuViewareControllerActionsHandler.twainservicesetupdownload(sessionID);
            await HandleGet(message, Response);
        }
        [HttpPost]
        public string FormfieldUpdate([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.formfieldupdate(jsonString);
        }
        [HttpPost]
        public string AnnotUpdate([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.annotupdate(jsonString);
        }
        [HttpPost]
        public string LoadFromUri([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.loadfromuri(jsonString);
        }
        [HttpPost]
        public string LoadFromFile([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.loadfromfile(jsonString);
        }
        [HttpPost]
        public string SetPassword([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.setpassword(jsonString);
        }
        [HttpPost]
        public string MovePage([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.movepage(jsonString);
        }
        [HttpPost]
        public string Redact([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.redact(jsonString);
        }
        [HttpPost]
        public string Digisign([FromBody]object jsonString)
        {
            return DocuViewareControllerActionsHandler.digisign(jsonString);
        }
    }
}
