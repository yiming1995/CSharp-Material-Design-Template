using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AIOBOT
{
    class MCTBotLogic
    {
        public string Get_Info()
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(Constant.g_strMCTProducts)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Product_List(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string ATC(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string Get_Json(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Post_Json(HttpClient httpClient, string url, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("Origin", "https://checkout.us.shopifycs.com");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetGateWay(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
        public string Get_ProductPage(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Shipping_Method(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Shipping(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation("Referer", "https://mct.tokyo/");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string AddtoCart(HttpClient httpClient, string url, MultipartFormDataContent formdata, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("accept-encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("content-type", "multipart/form-data");
                request.Headers.TryAddWithoutValidation("referer", referer);

                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetCartInfo(HttpClient httpClient)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(Constant.g_strMCTCartInfo)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Referer", "https://mct.tokyo/cart");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetPaymentDialog(HttpClient httpClient)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(Constant.g_strMCTPayDialog)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Referer", "https://mct.tokyo/");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
        public string GetCheckOutPage(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request);
                return response.Result.Content.ReadAsStringAsync().Result;
            }
        }
        public string GotoCheckOut(HttpClient httpClient, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(Constant.g_strMCTCART)))
            {
                request.Headers.TryAddWithoutValidation("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("accept-encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("content-type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("referer", Constant.g_strMCTCART);
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
        public string GetCheckoutInfo(HttpClient httpClient, string url, string auth_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("content-type", "application/json");
                request.Headers.TryAddWithoutValidation("x-shopify-checkout-authorization-token", auth_token);
                request.Headers.TryAddWithoutValidation("x-shopify-checkout-version", "2018-03-05");
                request.Headers.TryAddWithoutValidation("x-shopify-uniquetoken", "61911c04-b10c-4836-a74b-59d46d26ad1e");
                request.Headers.TryAddWithoutValidation("x-shopify-visittoken", "35076438-9AFB-41E5-33CA-0DF373E4F73C");
                var response = httpClient.SendAsync(request);
                return response.Result.Content.ReadAsStringAsync().Result;
            }
        }

        public void PostProduct(HttpClient httpClient)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://monorail-edge.shopifysvc.com/v1/produce")))
            {
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "text/plain");
                var response = httpClient.SendAsync(request).Result;
            }
        }

        public string Checkout1(HttpClient httpClient, string url, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string Checkout_Json(HttpClient httpClient, string url, FormUrlEncodedContent formdata, string auth_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("x-shopify-checkout-authorization-token", auth_token);
                request.Headers.TryAddWithoutValidation("x-shopify-checkout-version", "2018-03-05");
                request.Headers.TryAddWithoutValidation("x-shopify-uniquetoken", "61911c04-b10c-4836-a74b-59d46d26ad1e");
                request.Headers.TryAddWithoutValidation("x-shopify-visittoken", "35076438-9AFB-41E5-33CA-0DF373E4F73C");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string Payment_Json(HttpClient httpClient, string url, StringContent formdata, string auth_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("x-shopify-checkout-authorization-token", auth_token);
                request.Headers.TryAddWithoutValidation("x-shopify-checkout-version", "2018-03-05");
                request.Headers.TryAddWithoutValidation("x-shopify-uniquetoken", "61911c04-b10c-4836-a74b-59d46d26ad1e");
                request.Headers.TryAddWithoutValidation("x-shopify-visittoken", "35076438-9AFB-41E5-33CA-0DF373E4F73C");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
    class ArciBotLogic
    {
        public string Get_Info(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Host", "store.architectureandsneakers.com");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_LoginPage(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Host", "store.architectureandsneakers.com");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetProduct(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                string get_path = "/";
                if (url.Split('?').Length == 2)
                {
                    get_path = "/?" + url.Split('?')[1];
                }
                request.Headers.TryAddWithoutValidation(":authority", "store.architectureandsneakers.com");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", get_path);
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string AddtoCart(HttpClient httpClient, string url, string referer, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://store.architectureandsneakers.com");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetShopAPI(HttpClient httpClient, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/shops/to_shop_id?fqdn=soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetItemAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Host", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");

                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("X-SHOP-DOMAIN", "store.architectureandsneakers.com");
                request.Headers.TryAddWithoutValidation("X-BASKET-TOKEN", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetPaymentAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            string api_path = "/public/basket/payment_methods?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", api_path);
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "store.architectureandsneakers.com");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }

        }

        public string CheckOut_Cash(HttpClient httpClient, string url, string referer, string basket_token, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "POST");
                request.Headers.TryAddWithoutValidation(":path", "/public/basket/checkout");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "store.architectureandsneakers.com");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string CheckOut_Credit(HttpClient httpClient, string url, string basket_token, StringContent formdata, string signin_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "POST");
                request.Headers.TryAddWithoutValidation(":path", "/public/basket/checkout");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + signin_token);
                request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://soph.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "store.architectureandsneakers.com");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string SignIn_Status(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/gmo_id/PA01344409/signed_in_status");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "store.architectureandsneakers.com");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_CheckoutPage(HttpClient httpClient, string url)
        {
            string get_path = "/cart/?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/gmo_id/PA01344409/signed_in_status");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Sigin(HttpClient httpClient, string url, string referer, StringContent formdata, string shop_id)
        {
            string siginin_path = "/public/shops/" + shop_id + "/customers/signin";
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                //request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                //request.Headers.TryAddWithoutValidation(":method", "POST");
                //request.Headers.TryAddWithoutValidation(":path", siginin_path);
                //request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "store.architectureandsneakers.com");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Customer(HttpClient httpClient, string url, string shop_id, string sigin_token)
        {
            string get_path = "/public/shops/" + shop_id + "/customer";
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", get_path);
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + sigin_token);
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://soph.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "store.architectureandsneakers.com");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string BASE_Login(HttpClient httpClient, string url, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                //request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                //request.Headers.TryAddWithoutValidation(":method", "POST");
                //request.Headers.TryAddWithoutValidation(":path", siginin_path);
                //request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://soph.shop-pro.jp/secure/?mode=myaccount_login&shop_id=PA01344409");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "store.architectureandsneakers.com");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
    }



    /// <summary>
    /// //////////FTC
    /// 
    /// </summary>


    class FTCBotLogic
    {
        public string Get_Info(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");              
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_LoginPage(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "store.architectureandsneakers.com");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetProduct(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                string get_path = "/";
                if (url.Split('?').Length == 2)
                {
                    get_path = "/?" + url.Split('?')[1];
                }
                //request.Headers.TryAddWithoutValidation(":authority", "store.architectureandsneakers.com");
                //request.Headers.TryAddWithoutValidation(":method", "GET");
                //request.Headers.TryAddWithoutValidation(":path", get_path);
                //request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string StockStatus(HttpClient httpClient, FormUrlEncodedContent formdata,string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcstore.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string AddtoCart(HttpClient httpClient, string url, string referer, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcstore.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetShopAPI(HttpClient httpClient, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/shops/to_shop_id?fqdn=soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetItemAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("X-SHOP-DOMAIN", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("X-BASKET-TOKEN", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetPaymentAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            string api_path = "/public/basket/payment_methods?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {               
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }

        }

        public string CheckOut_Cash(HttpClient httpClient, string url, string referer, string basket_token, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {              
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string CheckOut_Credit(HttpClient httpClient, string url, string basket_token, StringContent formdata, string signin_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + signin_token);              
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://ftcjapan.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string SignIn_Status(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {          
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_CheckoutPage(HttpClient httpClient, string url)
        {
            string get_path = "/cart/?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/gmo_id/PA01344409/signed_in_status");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Sigin(HttpClient httpClient, string url, string referer, StringContent formdata, string shop_id)
        {
            string siginin_path = "/public/shops/" + shop_id + "/customers/signin";
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {           
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "ftcstore.jp");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Customer(HttpClient httpClient, string url, string shop_id, string sigin_token)
        {
            string get_path = "/public/shops/" + shop_id + "/customer";
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {               
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + sigin_token);
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://ftcjapan.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "ftcstore.jp");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string BASE_Login(HttpClient httpClient, string url, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://ftcjapan.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://ftcjapan.shop-pro.jp/secure/?mode=myaccount_login&shop_id=PA01036757");                
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
    }




    /// <summary>
    /// //////--------ARKTZ
    /// </summary>

    class ARKBotLogic
    {
        public string Get_Info(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_LoginPage(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetProduct(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                string get_path = "/";
                if (url.Split('?').Length == 2)
                {
                    get_path = "/?" + url.Split('?')[1];
                }
                //request.Headers.TryAddWithoutValidation(":authority", "store.architectureandsneakers.com");
                //request.Headers.TryAddWithoutValidation(":method", "GET");
                //request.Headers.TryAddWithoutValidation(":path", get_path);
                //request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string StockStatus(HttpClient httpClient, FormUrlEncodedContent formdata, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string AddtoCart(HttpClient httpClient, string url, string referer, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetShopAPI(HttpClient httpClient, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/shops/to_shop_id?fqdn=soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetItemAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("X-SHOP-DOMAIN", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("X-BASKET-TOKEN", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetPaymentAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            string api_path = "/public/basket/payment_methods?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }

        }

        public string CheckOut_Cash(HttpClient httpClient, string url, string referer, string basket_token, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string CheckOut_Credit(HttpClient httpClient, string url, string basket_token, StringContent formdata, string signin_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + signin_token);
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://arktz.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string SignIn_Status(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_CheckoutPage(HttpClient httpClient, string url)
        {
            string get_path = "/cart/?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/gmo_id/PA01344409/signed_in_status");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Sigin(HttpClient httpClient, string url, string referer, StringContent formdata, string shop_id)
        {
            string siginin_path = "/public/shops/" + shop_id + "/customers/signin";
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Customer(HttpClient httpClient, string url, string shop_id, string sigin_token)
        {
            string get_path = "/public/shops/" + shop_id + "/customer";
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + sigin_token);
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://arktz.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string BASE_Login(HttpClient httpClient, string url, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://arktz.shop-pro.jp/secure/?mode=myaccount_login&shop_id=PA01036757");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
    }


    /// <summary>
    /// /////////===========Zingaro==============
    /// </summary>



    class ZingaroBotLogic
    {
        public string Get_Info(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_LoginPage(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetProduct(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string StockStatus(HttpClient httpClient, FormUrlEncodedContent formdata, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string AddtoCart(HttpClient httpClient, string url, string referer, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
        public string Checkout(HttpClient httpClient, string url, string referer, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
        public string GetShopAPI(HttpClient httpClient, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/shops/to_shop_id?fqdn=soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetItemAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("X-SHOP-DOMAIN", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("X-BASKET-TOKEN", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetPaymentAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            string api_path = "/public/basket/payment_methods?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }

        }

        public string CheckOut_Cash(HttpClient httpClient, string url, string referer, string basket_token, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string CheckOut_Credit(HttpClient httpClient, string url, string basket_token, StringContent formdata, string signin_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + signin_token);
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://shop-zingaro.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string SignIn_Status(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_CheckoutPage(HttpClient httpClient, string url)
        {
            string get_path = "/cart/?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/gmo_id/PA01344409/signed_in_status");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Sigin(HttpClient httpClient, string url, string referer, StringContent formdata, string shop_id)
        {
            string siginin_path = "/public/shops/" + shop_id + "/customers/signin";
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Customer(HttpClient httpClient, string url, string shop_id, string sigin_token)
        {
            string get_path = "/public/shops/" + shop_id + "/customer";
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + sigin_token);
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://arktz.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string BASE_Login(HttpClient httpClient, string url, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://shop-zingaro.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://shop-zingaro.shop-pro.jp/secure/?mode=myaccount_login&shop_id=PA01446399");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
    }



    class ZozoBotLogic
    {
        public string Get_ProductPage(string producturl)
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(producturl)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Host", "zozo.jp");
                request.Headers.TryAddWithoutValidation("Referer", "https://zozo.jp/");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public List<System.Net.Cookie> getAkamaiCookie()
        {
            List<System.Net.Cookie> result = new List<Cookie>();
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var chromeoptions = new ChromeOptions();
            chromeoptions.AddArgument("headless");
            chromeoptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");
            var AkamaiBot = new ChromeDriver(driverService, chromeoptions);
            AkamaiBot.Url = "https://zozo.jp";
            Thread.Sleep(3000);
            foreach (var cookie in AkamaiBot.Manage().Cookies.AllCookies)
            {
                Cookie temp = new Cookie();
                temp.Name = cookie.Name;
                temp.Value = cookie.Value;
                temp.Path = cookie.Path;
                temp.Domain = cookie.Domain;
                result.Add(temp);
            }
            try
            {
                AkamaiBot.Close();
                AkamaiBot.Quit();
            }
            catch
            {

            }
            return result;
        }

        public void Get_Cookie(HttpClient httpClient, string product_url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://zozo.jp")))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Host", "zozo.jp");
                request.Headers.TryAddWithoutValidation("Origin", "https://zozo.jp/");
                request.Headers.TryAddWithoutValidation("Referer", product_url);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
            }
        }

        public string Get_Product(HttpClient httpClient, string producturl)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(producturl)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Host", "zozo.jp");
                request.Headers.TryAddWithoutValidation("Origin", "https://zozo.jp/");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string EventCookiePost(HttpClient httpClient, string product_url, StringContent formdata)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://ev.s4p.jp/ev")))
            {
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Host", "zozo.jp");
                request.Headers.TryAddWithoutValidation("Origin", "https://zozo.jp/");
                request.Headers.TryAddWithoutValidation("Referer", product_url);
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string ATC(HttpClient httpClient, string product_url, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://zozo.jp/_cart/default.html")))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Host", "zozo.jp");
                request.Headers.TryAddWithoutValidation("Origin", "https://zozo.jp/");
                request.Headers.TryAddWithoutValidation("Referer", product_url);
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
        public string Checkout_Credit(HttpClient httpClient, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://zozo.jp/_cart/default.html")))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Host", "zozo.jp");
                request.Headers.TryAddWithoutValidation("Origin", "https://zozo.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://zozo.jp/_cart/default.html?c=Message&no=1&name=PutMessage&");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
    }




    class MortarBotLogic
    {
        public string GetResponse(HttpClient httpClient, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation(":authority", "mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/login");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                if (referer != "")
                {
                    request.Headers.TryAddWithoutValidation("Referer", referer);
                }
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetResponse_CHECKOUT(HttpClient httpClient, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation(":authority", "mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/checkout");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                if (referer != "")
                {
                    request.Headers.TryAddWithoutValidation("Referer", referer);
                }
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }




        public string LoginPost(HttpClient httpClient, string url, string referer, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation(":authority", "mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation(":method", "POST");
                request.Headers.TryAddWithoutValidation(":path", "/login");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string CartPost(HttpClient httpClient, string url, string referer, StringContent formdata, string csrf_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation(":authority", "mortartokyo.com");
                request.Headers.TryAddWithoutValidation(":method", "POST");
                request.Headers.TryAddWithoutValidation(":path", "/api/v1/cart/items");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("origin", "https://mortartokyo.com");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("x-csrf-token", csrf_token);
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Payment_Preview(HttpClient httpClient, string url, string referer, StringContent formdata, string csrf_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation(":authority", "mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation(":method", "POST");
                request.Headers.TryAddWithoutValidation(":path", "/api/v1/cart/preview_order");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("origin", "https://mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("x-csrf-token", csrf_token);
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Payment_Confirm(HttpClient httpClient, FormUrlEncodedContent formdata, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.stripe.com/v1/tokens")))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation(":authority", "mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation(":method", "POST");
                request.Headers.TryAddWithoutValidation(":path", "/api/v1/cart/preview_order");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("origin", "https://js.stripe.com");
                request.Headers.TryAddWithoutValidation("Accept", "application/x-www-form-urlencoded");             
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Checkout_Payment(HttpClient httpClient, StringContent formdata,string csrf_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://mortartokyo.stores.jp/orders")))
            {
                
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8");
                request.Headers.TryAddWithoutValidation(":authority", "mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation(":method", "POST");
                request.Headers.TryAddWithoutValidation(":path", "/orders");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Referer", "https://mortartokyo.stores.jp/checkout");
                request.Headers.TryAddWithoutValidation("origin", "https://mortartokyo.stores.jp");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json;charset=UTF-8");
                request.Headers.TryAddWithoutValidation("x-csrf-token", csrf_token);
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
    }

    class ISBBotLogic
    {
        public string Get_Info(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_LoginPage(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetProduct(HttpClient httpClient, string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                string get_path = "/";
                if (url.Split('?').Length == 2)
                {
                    get_path = "/?" + url.Split('?')[1];
                }
                //request.Headers.TryAddWithoutValidation(":authority", "store.architectureandsneakers.com");
                //request.Headers.TryAddWithoutValidation(":method", "GET");
                //request.Headers.TryAddWithoutValidation(":path", get_path);
                //request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string StockStatus(HttpClient httpClient, FormUrlEncodedContent formdata, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string AddtoCart(HttpClient httpClient, string url, string referer, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                //request.Headers.TryAddWithoutValidation("Host", "soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetShopAPI(HttpClient httpClient, string url, string referer)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/shops/to_shop_id?fqdn=soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://soph.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetItemAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("X-SHOP-DOMAIN", "ftcstore.jp");
                request.Headers.TryAddWithoutValidation("X-BASKET-TOKEN", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string GetPaymentAPI(HttpClient httpClient, string url, string referer, string basket_token)
        {
            string api_path = "/public/basket/payment_methods?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }

        }

        public string CheckOut_Cash(HttpClient httpClient, string url, string referer, string basket_token, StringContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }


        public string CheckOut_Credit(HttpClient httpClient, string url, string basket_token, StringContent formdata, string signin_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + signin_token);
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://arktz.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                request.Headers.TryAddWithoutValidation("x-cart-domain-uniq", "true");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string SignIn_Status(HttpClient httpClient, string url, string referer, string basket_token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Headers.TryAddWithoutValidation("x-basket-token", basket_token);
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_CheckoutPage(HttpClient httpClient, string url)
        {
            string get_path = "/cart/?" + url.Split('?')[1];
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation(":authority", "api.shop-pro.jp");
                request.Headers.TryAddWithoutValidation(":method", "GET");
                request.Headers.TryAddWithoutValidation(":path", "/public/gmo_id/PA01344409/signed_in_status");
                request.Headers.TryAddWithoutValidation(":scheme", "https");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Sigin(HttpClient httpClient, string url, string referer, StringContent formdata, string shop_id)
        {
            string siginin_path = "/public/shops/" + shop_id + "/customers/signin";
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", referer);
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                request.Content = formdata;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string Get_Customer(HttpClient httpClient, string url, string shop_id, string sigin_token)
        {
            string get_path = "/public/shops/" + shop_id + "/customer";
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,ko;q=0.8,ja;q=0.7");
                request.Headers.TryAddWithoutValidation("authorization", "JWT " + sigin_token);
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://arktz.shop-pro.jp/cart/");
                request.Headers.TryAddWithoutValidation("x-shop-domain", "arktz.shop-pro.jp");
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        public string BASE_Login(HttpClient httpClient, string url, FormUrlEncodedContent formdata)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Origin", "https://arktz.shop-pro.jp/");
                request.Headers.TryAddWithoutValidation("Referer", "https://arktz.shop-pro.jp/secure/?mode=myaccount_login&shop_id=PA01036757");
                request.Content = formdata;
                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return response;
            }
        }
    }


}
