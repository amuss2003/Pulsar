using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

/// <summary>
/// Presents basic usage of PostFile().  
/// 
/// Note: the accompanying class Upload is an excerpt of a larger class, Salient.Web.HttpLib.HttpRequestUtility, 
/// that contains many convenience overloads that have been omitted for brevity.
/// 
/// e.g.
/// <code>Upload.PostFile(new Uri("http://mysite.com/myHandler"), null, @"c:\temp\myPic.jgp", null, null, null, null);</code>
/// 
/// could be accomplished with this overload
/// 
/// <code>Upload.PostFile(new Uri("http://mysite.com/myHandler"), @"c:\temp\myPic.jgp");</code>
/// 
/// I suggest that after this brief introduction that you pull the full source from http://salient.codeplex.com.
/// </summary>
public class UploadUsage
{
    public void UploadFile()
    {
        string filePath = Path.GetFullPath("TestFiles/TextFile1.txt");
        string responseText;
        Upload.PostFile(new Uri("http://localhost/myhandler.ashx"), null, filePath, null, null, null, null);
    }

    public void UploadFileWithFormFields()
    {
        string filePath = Path.GetFullPath("TestFiles/TextFile1.txt");

        // this represents fields from a form
        NameValueCollection postData = new NameValueCollection();
        postData.Add("fieldName", "fieldValue");

        string responseText;
        Upload.PostFile(new Uri("http://localhost/myhandler.ashx"), postData, filePath, null, null, null, null);
    }

    public void UploadFileWithFormFieldsCookiesAndHeaders()
    {
        string filePath = Path.GetFullPath("TestFiles/TextFile1.txt");

        // this represents fields from a form
        NameValueCollection postData = new NameValueCollection();
        postData.Add("fieldName", "fieldValue");

        // this could be an existing CookieContainer used in a previous request
        // to contain session or other coookies. Typically used to maintain
        // session state across several requests.
        CookieContainer cookies = new CookieContainer();

        // you can send additional request headers with the headers collection
        NameValueCollection headers = new NameValueCollection();
        headers.Add("x-headerName", "header value");

        // content type of the posted file.
        // if null, the content type will be determined by the filename.
        // defaults to application/octet-stream
        const string fileContentType = null;

        // the key used to identify this file. typically unused.
        // if null, 'file' will be submitted.
        const string fileFieldName = null;

        string responseText;
        Upload.PostFile(new Uri("http://localhost/myhandler.ashx"), postData, filePath, fileContentType, fileFieldName,
                        cookies, headers);
    }


    public void UploadStream()
    {
        // You may also upload data from an open and positioned stream
        MemoryStream fileData = new MemoryStream(new byte[] { 0, 1, 2, 3, 4 });

        // The name to associate with the uploaded data. 
        // Content-type will be determined from this value
        const string fileName = "foo.bin";


        Upload.PostFile(new Uri("http://localhost/myhandler.ashx"), null, fileData, fileName, null, null, null, null);
    }


    public void UploadAndGetResponse()
    {
        MemoryStream fileData = new MemoryStream(new byte[] { 0, 1, 2, 3, 4 });
        const string fileName = "foo.bin";

        using (
            WebResponse response = Upload.PostFile(new Uri("http://localhost/myhandler.ashx"), null, fileData, fileName,
                                                   null, null, null, null))
        {
            // the stream returned by WebResponse.GetResponseStream will contain any content returned by the server after upload

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string responseText = reader.ReadToEnd();
            }
        }
    }
}
