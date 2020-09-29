using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AIOBOT
{
    class Utils
    {
        public void secondWebhook_AS(string productname, string size, string imageURI)
        {
            var data = "{\"username\":\"A+S Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");            
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discordapp.com/api/webhooks/700328288564019229/U6AiRx-LiBeWhhUlf6Q2l_L5BS8ASwtaJvZh9Nyn5nF_icwfUTDURuroyGSlZzb1qSkP"),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void UserWebhook_AS(string productname, string size, string imageURI, string webhook_url)
        {
            var data = "{\"username\":\"A+S Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");            
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(webhook_url),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void secondWebhook_MCT(string productname, string size, string imageURI)
        {
            var data = "{\"username\":\"MCT Tokyo\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "代金引換");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discordapp.com/api/webhooks/700328288564019229/U6AiRx-LiBeWhhUlf6Q2l_L5BS8ASwtaJvZh9Nyn5nF_icwfUTDURuroyGSlZzb1qSkP"),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void UserWebhook_MCT(string productname, string size, string imageURI, string webhook_url)
        {
            var data = "{\"username\":\"MCT Tokyo Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "代金引換");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(webhook_url),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }


        public void secondWebhook_FTC(string productname, string size, string imageURI)
        {
            var data = "{\"username\":\"FTC Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discordapp.com/api/webhooks/700328288564019229/U6AiRx-LiBeWhhUlf6Q2l_L5BS8ASwtaJvZh9Nyn5nF_icwfUTDURuroyGSlZzb1qSkP"),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void UserWebhook_FTC(string productname, string size, string imageURI, string webhook_url)
        {
            var data = "{\"username\":\"FTC Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(webhook_url),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }


        public void secondWebhook_ARK(string productname, string size, string imageURI)
        {
            var data = "{\"username\":\"ARKTZ Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discordapp.com/api/webhooks/700328288564019229/U6AiRx-LiBeWhhUlf6Q2l_L5BS8ASwtaJvZh9Nyn5nF_icwfUTDURuroyGSlZzb1qSkP"),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void UserWebhook_ARK(string productname, string size, string imageURI, string webhook_url)
        {
            var data = "{\"username\":\"ARKTZ Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(webhook_url),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }


        public void secondWebhook_Mortar(string productname, string size, string imageURI)
        {
            var data = "{\"username\":\"Mortar Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discordapp.com/api/webhooks/700328288564019229/U6AiRx-LiBeWhhUlf6Q2l_L5BS8ASwtaJvZh9Nyn5nF_icwfUTDURuroyGSlZzb1qSkP"),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void UserWebhook_Mortar(string productname, string size, string imageURI, string webhook_url)
        {
            var data = "{\"username\":\"Mortar Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Color\",\"value\":\"product_style\",\"inline\":true},{\"name\":\"Size\",\"value\":\"product_size\",\"inline\":true},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");
            string temp3 = "";
            if (size != "")
            {
                temp3 = temp2.Replace("product_size", size);
            }
            else
            {
                temp3 = temp2.Replace("product_size", "N/A");
            }
            var temp4 = temp3.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(webhook_url),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void secondWebhook_Zingaro(string productname, string imageURI)
        {
            var data = "{\"username\":\"Zingaro Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");                     
            var temp4 = temp2.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discordapp.com/api/webhooks/700328288564019229/U6AiRx-LiBeWhhUlf6Q2l_L5BS8ASwtaJvZh9Nyn5nF_icwfUTDURuroyGSlZzb1qSkP"),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

        public void UserWebhook_Zingaro(string productname, string imageURI, string webhook_url)
        {
            var data = "{\"username\":\"Zingaro Success\",\"embeds\":[{\"author\":{\"name\":\"AIO BOT JP\"},\"title\":\"Product Details\",\"fields\":[{\"name\":\"Product Name\",\"value\":\"product_name\"},{\"name\":\"Mode\",\"value\":\"product_mode\"},{\"name\":\"Success Time\",\"value\":\"product_time\"}],\"thumbnail\":{\"url\":\"product_image\"}}]}";
            var temp1 = data.Replace("product_name", productname);
            string temp2 = temp1.Replace("product_style", "N/A");            
            var temp4 = temp2.Replace("product_image", imageURI);
            var temp5 = temp4.Replace("product_time", DateTime.Now.ToString("hh:mm:ss fff"));
            var temp6 = temp5.Replace("product_mode", "General");
            var temp7 = new StringContent(temp6, Encoding.UTF8, "application/json");
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(webhook_url),
                    Method = HttpMethod.Post,
                    Headers = {

                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    },
                    Content = temp7
                };
                httpClient.SendAsync(request);
            }
            catch
            {
            }
        }

    }
}
