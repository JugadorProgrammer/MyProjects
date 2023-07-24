using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
namespace WebServerCSharp
{
    public static class UsersId
    {
        public static int Id { get; set; }
    }

    struct HTTPHeaders
    {
        public string Method;
        public string RealPath;
        public string File;
        public bool Chrome;
        public bool IsCookie;

        public static HTTPHeaders Parse(string headers)
        {
            HTTPHeaders result = new HTTPHeaders();
            result.Method = Regex.Match(headers, @"\A\w[a-zA-Z]+", RegexOptions.Multiline).Value;
            result.File = Regex.Match(headers, @"(?<=\w\s)([\Wa-zA-Z0-9]+)(?=\sHTTP)", RegexOptions.Multiline).Value;
            
            result.RealPath = $"{AppDomain.CurrentDomain.BaseDirectory}{result.File}";
            if (headers.Contains("Google Chrome"))
            {
                result.Chrome = true;
            }
            if (headers.Contains("Cookie: ") && headers.Contains("MyCookieId"))
            {
                result.IsCookie = true;
            }
            return result;
        }

        public static string FileExtention(string file)
        {
            return Regex.Match(file, @"(?<=[\W])\w+(?=[\W]{0,}$)").Value;
        }
    }

    class Client
    {
        Socket _client;
        HTTPHeaders Headers;
        public Client(Socket socket)
        {
            _client = socket;
            byte[] data = new byte[_client.ReceiveBufferSize];
            string request = "";
            _client.Receive(data);
            request += Encoding.UTF8.GetString(data);
            if (request == "")
            {
                _client.Close();
                return;
            }
            Headers = HTTPHeaders.Parse(request);
            Console.WriteLine($"[{_client.RemoteEndPoint}]\nFile: {Headers.File}\nDate: {DateTime.Now}");

            if (Headers.RealPath.IndexOf("..") != -1)
            {
                SendError(404);
                _client.Close();
                return;
            }
            string cookie;
            if (Headers.RealPath.Contains("Argee"))
            {
                cookie = $"\nSet-cookie: MyCookieId={UsersId.Id}; expires={DateTime.MaxValue}";
                Headers.RealPath = Headers.RealPath.Replace("Argee", "Index.html");
            }
            else
            {
                cookie = "";
            }

            if (File.Exists(Headers.RealPath))
            {
                GetSheet(cookie);
            }
            else if (Headers.RealPath.Contains("/"))
            {
                try
                {

                    int code = int.Parse(Headers.RealPath.Split("/").LastOrDefault().Replace(".html", ""));
                    SendError(code);
                }
                catch
                {
                    SendError(404);
                }
            }

            _client.Close();
        }

        public void GetSheet(string cookie = "")
        {
            try
            {
                if (!Headers.IsCookie)
                {
                    if (Headers.RealPath.Contains("secretJson.json"))
                    {
                        SendError(404); return;
                    }
                    ++UsersId.Id;
                }
                
                string contentType = GetContentType();
                if (Headers.Chrome && contentType == $"video/mp4") { SendError(403, "Chrome does not support mp4 file format"); return; }
                
                FileStream fs = new FileStream(Headers.RealPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                string headers = $"HTTP/1.1 200 OK\nserver: MyServerOnTrueSharp\ndate: {DateTime.Now}\nConnection: keep-alive\nContent-type: {contentType}\nContent-Length: {fs.Length}{cookie}\n\n";
                // OUTPUT HEADERS 
                byte[] data = Encoding.UTF8.GetBytes(headers);
                _client.Send(data, data.Length, SocketFlags.None);
                // OUTPUT CONTENT
                data = new byte[fs.Length];
                int length = fs.Read(data, 0, data.Length);
                _client.Send(data, data.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Func: GetSheet()    link: {Headers.RealPath}\nException: {ex}/nMessage: {ex.Message}");
            }
        }
        string GetContentType()
        {
            string result = "";
            string format = HTTPHeaders.FileExtention(Headers.File);
            switch (format)
            {
                //image
                case "gif":
                case "jpeg":
                case "pjpeg":
                case "png":
                case "tiff":
                case "webp":
                    result = $"image/{format}";
                    break;
                case "svg":
                    result = $"image/svg+xml";
                    break;
                case "ico":
                    result = $"image/vnd.microsoft.icon";
                    break;
                case "wbmp":
                    result = $"image/vnd.map.wbmp";
                    break;
                case "jpg":
                    result = $"image/jpeg";
                    break;
                // text
                case "css":
                    result = $"text/css";
                    break;
                case "html":
                    result = $"text/{format}";
                    break;
                case "javascript":
                case "js":
                    result = $"text/javascript";
                    break;
                case "php":
                    result = $"text/html";
                    break;
                case "htm":
                    result = $"text/html";
                    break;
                case "mp4":
                case "video/mp4":
                    result = $"video/mp4";
                    break;

                default:
                    result = "application/unknown";
                    break;
            }
            return result;
        }

        public void SendError(int code, string msg = "")
        {
            string html = $"<html><head><title></title></head><body><h1>Error {code + "\t" + msg}</h1></body></html>";
            string headers = $"HTTP/1.1 {code}\nContent-type: text/html\nContent-Length: {html.Length}\n\n{html}";
            byte[] data = Encoding.UTF8.GetBytes(headers);
            _client.Send(data, data.Length, SocketFlags.None);
            _client.Close();
        }
    }
}
