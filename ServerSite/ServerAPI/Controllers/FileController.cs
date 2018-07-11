using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ServerAPI.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {

        [HttpGet]
        public virtual IHttpActionResult Get()
        {
            string patchImg = HttpContext.Current.Server.MapPath("~/UploadedFiles") + "/aaaaa.mp4";
            string imageName = "xxxx.mp4";

            //converting Pdf file into bytes array  
            var dataBytes = File.ReadAllBytes(patchImg);
            //adding bytes to memory stream   
            var dataStream = new MemoryStream(dataBytes);
            return new imageResult(dataStream, Request, imageName);
        }

        [HttpPost]
        public KeyValuePair<bool, string> UploadFile()
        {
            try
            {

                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {

                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);

                        return new KeyValuePair<bool, string>(true, "File uploaded successfully.");
                    }

                    return new KeyValuePair<bool, string>(true, "Could not get the uploaded file.");
                }

                return new KeyValuePair<bool, string>(true, "No file found to upload.");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
            }
        }


    }

    public class imageResult : IHttpActionResult
    {
        MemoryStream imageStuff;
        string FileName;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;
        public imageResult(MemoryStream data, HttpRequestMessage request, string filename)
        {
            imageStuff = data;
            httpRequestMessage = request;
            FileName = filename;
        }
        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(imageStuff);
            //httpResponseMessage.Content = new ByteArrayContent(imageStuff.ToArray());  
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = FileName;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
        }
    }
}
