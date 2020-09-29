using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Threading;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Management;
using Newtonsoft.Json;
using System.Windows;
using MaterialDesignColors.Recommended;

namespace AIOBOT
{
    public enum UserRole
    {
        None = 0,
        Auth = 1,
        Trader = 2,
        Manager = 3,
        Admin = 4,

    }

    public enum LoginResult
    {
        InvalidEmail = 0,
        InvalidPass = 1,
        InvalidKey = 2,
        Expired = 3,
        Success = 10,
    }

    public enum MultiUse
    {
        Banned = 0,
        Active = 1,
    }

    public enum LoginError
    {
        None = 0,
        RequestFailed = 1,
        InvalidResponse = 2,
        InvalidPassword = 3,
    }

    public enum PayMethod
    {
        CreditCard = 1,
        CashOnDelivery = 2,
    }

    public enum PayMethodBilly
    {
        CreditCard = 1,
        CashOnDelivery = 2,
        AfterPay = 3,
    }

    public enum CreditCardTypeBilly
    {
        None = 0,
        Visa = 1,
        MasterCard = 2,
        JCB = 3,
        AmericanExpress = 4,
    }

    public enum OrderStep
    {
        None = 0,
        GetInfo = 1,
        CheckOut_Delivery = 2,
        ConfirmCutin = 4,
        DeliverySettle = 5,
        PayConfirm = 6,
        PayComplete = 7,
        CreditComplete = 8,
        GetOrdererInfo = 9,
        FoundProduct = 10,
        LoginFailed = 96,
        LoginSuccess = 97,
        Failed = 98,
        Finished = 99,
    }

    public enum ProductType
    {
        None = 0,
        OnlySize = 1,
        Selection = 2,
        ByRadio = 3,
    }

    public enum UsePointType
    {
        NoUse = 0,
        AllUse = 2,
        PartialUse = 3,
    }


    public struct LoginResponse
    {
        public LoginResult result;
        public UserRole role;
        public MultiUse? multi_use;
    }

    public struct CutinProduct
    {
        public string product_id;
        public string size;
        public int amount;
    }

    public struct NewItem
    {
        public int price_before_tax;
        public int tax;
        public int point;
        public string code;
        public string name;
        public string url;
        public string category;
        public string image;
        public int price;
        public int stock;
        public string release_date;
    }


    class Constant
    {
        // Product Info
        public static string g_strProductName_MCT = "AIOBOT_MCT";
        public static string g_strProductName_AS = "AIOBOT_AS";
        public static string g_strProductName_MORTAR = "AIOBOT_MORTAR";
        public static string g_strProductName_FTC = "AIOBOT_FTC";
        public static string g_strProductName_ARK = "AIOBOT_ARKTZ";
        public static string g_strAppGuid = "{8F6F0AC4-BAA1-45fd-A64F-20190508AB05}";
        public static bool g_bUseRealOrder = true;

        // Application Info
        public static string g_strApplicationName = "AIOBOT_CaliforniaStreet";

        // URLs
        //public static string g_strMainURL = "http://localhost:8000";
        public static string g_strMainURL = "http://54.238.208.47";
        public static string g_strAPIURL = g_strMainURL + "/public/";
        public static string g_strAPILoginURL = "api/login";
        public static string g_strAPIAddBuyHistoryURL = "api/addBuyHistory";
        public static string g_strAPIAddGoodInfoURL = "api/addGoodInfo";
        public static string g_strAPIGetKeyListURL = "api/getKeyList";
        public static string g_strAPISetKeyListURL = "api/setKeyList";


        //AS
        public static string g_strSiteAddtoCart_AS = "https://soph.shop-pro.jp/cart/proxy/basket/items/add?";
        public static string g_strCart_API_AS = "https://api.shop-pro.jp/public/shops/to_shop_id?fqdn=soph.shop-pro.jp";
        public static string g_strGetItem_AS = "https://api.shop-pro.jp/public/basket/items?shop_id=";
        public static string g_strGetDelivery_AS = "https://api.shop-pro.jp/public/basket/delivery_methods?shop_id=";
        public static string g_strCheckOut_AS = "https://api.shop-pro.jp/public/basket/checkout";
        public static string g_strPayment_AS = "https://api.shop-pro.jp/public/basket/payment_methods?adjustment_total_price=0&delivery_id=390918&delivery_total_charge=0&";
        public static string g_strSignIn_AS = "https://api.shop-pro.jp/public/gmo_id/";
        public static string g_strProductSearch_AS = "https://store.architectureandsneakers.com/?mode=srh&x=0&y=0&keyword=";
        public static string g_strBase_AS = "https://store.architectureandsneakers.com/";
        public static string g_strBase_Login_AS = "https://soph.shop-pro.jp/secure/?mode=myaccount_login";

        public string get_Charge_AS(string shopid, string perfecture_id)
        {
            return "https://api.shop-pro.jp/public/basket/delivery_methods?payment_id=650079&prefecture_id=" + perfecture_id + "&shop_id=" + shopid;
        }

        public string get_CustomerSetting_AS(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer_setting";
        }

        public string get_SignIn_AS(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customers/signin";
        }

        public string get_Customer(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer";
        }

        //FTC
        public static string g_strSiteAddtoCart_FTC= "https://ftcjapan.shop-pro.jp/cart/proxy/basket/items/add";
        public static string g_strCart_API_FTC = "https://api.shop-pro.jp/public/shops/to_shop_id?fqdn=ftcjapan.shop-pro.jp";
        public static string g_strGetItem_FTC = "https://api.shop-pro.jp/public/basket/items?shop_id=";
        public static string g_strGetDelivery_FTC = "https://api.shop-pro.jp/public/basket/delivery_methods?shop_id=";
        public static string g_strCheckOut_FTC = "https://api.shop-pro.jp/public/basket/checkout";
        public static string g_strPayment_FTC = "https://api.shop-pro.jp/public/basket/payment_methods?adjustment_total_price=0&delivery_id=64266&delivery_total_charge=550&";
        public static string g_strSignIn_FTC = "https://api.shop-pro.jp/public/gmo_id/";
        public static string g_strProductSearch_FTC = "https://ftcstore.jp/?mode=srh&keyword=";
        public static string g_strBase_FTC = "https://ftcstore.jp/";
        public static string g_strBase_Login_FTC = "https://ftcjapan.shop-pro.jp/secure/?mode=myaccount_login";

        public string get_Charge_FTC(string shopid, string perfecture_id)
        {
            return "https://api.shop-pro.jp/public/basket/delivery_methods?payment_id=650079&prefecture_id=" + perfecture_id + "&shop_id=" + shopid;
        }

        public string get_CustomerSetting_FTC(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer_setting";
        }
        public string get_SignIn_FTC(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customers/signin";
        }

        public string get_Customer_FTC(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer";
        }

        //ARK
        public static string g_strSiteAddtoCart_ARK = "https://arktz.shop-pro.jp/cart/proxy/basket/items/add";
        public static string g_strCart_API_ARK = "https://api.shop-pro.jp/public/shops/to_shop_id?fqdn=arktz.shop-pro.jp";
        public static string g_strGetItem_ARK = "https://api.shop-pro.jp/public/basket/items?shop_id=";
        public static string g_strGetDelivery_ARK = "https://api.shop-pro.jp/public/basket/delivery_methods?shop_id=";
        public static string g_strCheckOut_ARK = "https://api.shop-pro.jp/public/basket/checkout";
        public static string g_strPayment_ARK = "https://api.shop-pro.jp/public/basket/payment_methods?adjustment_total_price=0&delivery_id=416353&delivery_total_charge=770&";
        public static string g_strSignIn_ARK = "https://api.shop-pro.jp/public/gmo_id/";
        public static string g_strProductSearch_ARK = "https://arktz.shop-pro.jp/?mode=srh&sort=n&cid=&keyword=";
        public static string g_strBase_ARK = "https://arktz.shop-pro.jp/";
        public static string g_strBase_Login_ARK = "https://arktz.shop-pro.jp/secure/?mode=myaccount_login";

        public string get_Charge_ARK(string shopid, string perfecture_id)
        {
            return "https://api.shop-pro.jp/public/basket/delivery_methods?payment_id=650079&prefecture_id=" + perfecture_id + "&shop_id=" + shopid;
        }

        public string get_CustomerSetting_ARK(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer_setting";
        }
        public string get_SignIn_ARK(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customers/signin";
        }

        public string get_Customer_ARK(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer";
        }

        //Zingaro

        public static string g_strSiteAddtoCart_Zingaro = "https://shop-zingaro.shop-pro.jp/secure/?mode=cart_inn";
        public static string g_strRegi_Zingaro = "https://shop-zingaro.shop-pro.jp/secure/?mode=regi_bgn";
        public static string g_strCart_API_Zingaro = "https://api.shop-pro.jp/public/shops/to_shop_id?fqdn=shop-zingaro.shop-pro.jp";
        public static string g_strGetItem_Zingaro = "https://api.shop-pro.jp/public/basket/items?shop_id=";
        public static string g_strGetDelivery_Zingaro = "https://api.shop-pro.jp/public/basket/delivery_methods?shop_id=";
        public static string g_strCheckOut_Zingaro = "https://api.shop-pro.jp/public/basket/checkout";
        public static string g_strPayment_Zingaro = "https://api.shop-pro.jp/public/basket/payment_methods?adjustment_total_price=0&delivery_id=416353&delivery_total_charge=770&";
        public static string g_strSignIn_Zingaro = "https://api.shop-pro.jp/public/gmo_id/";
        public static string g_strProductSearch_Zingaro = "https://shop-zingaro.shop-pro.jp/?mode=srh&sort=n&cid=&keyword=";
        public static string g_strBase_Zingaro = "https://shop-zingaro.shop-pro.jp/";
        public static string g_strBase_Login_Zingaro = "https://arktz.shop-pro.jp/secure/?mode=myaccount_login";
        public static string g_strLogin_Zingaro = "https://shop-zingaro.shop-pro.jp/secure/?mode=user_login";
        public static string g_strCust_Zingaro = "https://shop-zingaro.shop-pro.jp/secure/?mode=cust_input_end";

        public string get_Charge_Zingaro(string shopid, string perfecture_id)
        {
            return "https://api.shop-pro.jp/public/basket/delivery_methods?payment_id=650079&prefecture_id=" + perfecture_id + "&shop_id=" + shopid;
        }

        public string get_CustomerSetting_Zingaro(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer_setting";
        }
        public string get_SignIn_Zingaro(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customers/signin";
        }

        public string get_Customer_Zingaro(string shopid)
        {
            return "https://api.shop-pro.jp/public/shops/" + shopid + "/customer";
        }


        //MCT
        public static string g_strMCTProducts = "https://mct.tokyo/products.json";
        public static string g_strMCTProduct1 = "https://mct.tokyo/products.json?limit=250";
        public static string g_strMCTProduct2 = "https://mct.tokyo/products.json?limit=250&page=2";
        public static string g_strMCTBASE = "https://mct.tokyo/products/";
        public static string g_strMCTAddtoCart = "https://mct.tokyo/cart/add";
        public static string g_strMCTCartInfo = "https://mct.tokyo/cart.js";
        public static string g_strMCTCART = "https://mct.tokyo/cart/";
        public static string g_strMCTCheckoutBase = "https://mct.tokyo";
        public static string g_strMCTCardToken = "https://deposit.us.shopifycs.com/sessions";
        public static string g_strMCTPayDialog = "https://mct.tokyo/3623944241/digital_wallets/dialog";

      

        public static string[] state_array = new string[]
        {
           "北海道","青森県","秋田県","岩手県","宮城県","山形県","福島県","茨城県","栃木県","群馬県","埼玉県","千葉県","神奈川県"
                                            ,"東京都"
                                            ,"山梨県"
                                            ,"新潟県"
                                            ,"長野県"
                                            ,"静岡県"
                                            ,"愛知県"
                                            ,"三重県"
                                            ,"岐阜県"
                                            ,"富山県"
                                            ,"石川県"
                                            ,"福井県"
                                            ,"大阪府"
                                            ,"京都府"
                                            ,"滋賀県"
                                            ,"奈良県"
                                            ,"和歌山県"
                                            ,"兵庫県"
                                            ,"岡山県"
                                            ,"広島県"
                                            ,"山口県"
                                            ,"鳥取県"
                                            ,"島根県"
                                            ,"香川県"
                                            ,"徳島県"
                                            ,"愛媛県"
                                            ,"高知県"
                                            ,"福岡県"
                                            ,"佐賀県"
                                            ,"長崎県"
                                            ,"熊本県"
                                            ,"大分県"
                                            ,"宮崎県"
                                            ,"鹿児島県"
                                            ,"沖縄県"
        };
    }

    public struct ProductColor
    {
        public string value;
        public string name;
    }

    public struct ProductSize
    {
        public string value;
        public string name;
        public bool sold_out;
    }

    public struct BasketProduct
    {
        public string rowcart;
        public string rowgoods;
        public string qty;
        public string price;
    }
    public class IniControl
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, String lpFileName);

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string cipherText)
        {
            byte[] data = System.Convert.FromBase64String(cipherText);
            return System.Text.UTF8Encoding.UTF8.GetString(data);
        }


        public static void WriteIntValue(string lpAppName, string lpKeyName, int nValue, string lpFileName)
        {
            WritePrivateProfileString(lpAppName, lpKeyName, nValue.ToString(), lpFileName);
        }

        public static void WriteBoolValue(string lpAppName, string lpKeyName, bool bValue, string lpFileName)
        {
            WritePrivateProfileString(lpAppName, lpKeyName, bValue ? "1" : "0", lpFileName);
        }

        public static void WriteStringValue(string lpAppName, string lpKeyName, string lpValue, string lpFileName, bool bForce = false)
        {
            if (bForce)
                WritePrivateProfileString(lpAppName, lpKeyName, lpValue, lpFileName);
            else
                WritePrivateProfileString(lpAppName, lpKeyName, Base64Encode(lpValue), lpFileName);
        }

        public static string GetStringValue(string lpAppName, string lpKeyName, string lpDefault, int nSize, string lpFileName, bool bForce = false)
        {
            StringBuilder sbBuffer = new StringBuilder(nSize);

            GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, sbBuffer, nSize, lpFileName);
            if (bForce) return sbBuffer.ToString();

            return Base64Decode(sbBuffer.ToString());
        }

        public static int GetIntValue(string lpAppName, string lpKeyName, int nDefault, string lpFileName)
        {
            return GetPrivateProfileInt(lpAppName, lpKeyName, nDefault, lpFileName);
        }

        public static bool GetBoolValue(string lpAppName, string lpKeyName, int nDefault, string lpFileName)
        {
            int nValue = GetPrivateProfileInt(lpAppName, lpKeyName, nDefault, lpFileName);

            return nValue == 1 ? true : false;
        }

        public static double GetDoubleValue(string lpAppName, string lpKeyName, double dDefault, string lpFileName)
        {
            double dResult = 0;

            int nSize = 0x10;
            StringBuilder sbBuffer = new StringBuilder(nSize);

            string lpDefault = dDefault.ToString();

            GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, sbBuffer, nSize, lpFileName);

            Double.TryParse(sbBuffer.ToString(), out dResult);

            return dResult;
        }
    }

    class API
    {
        public static string apiLogin(string email, string pass,string strMac,string strIP)
        {
            string json;
            if (strMac == "")
            {
                strMac = "aio-2020";
            }
            try
            {
                CGlobalVar.g_nLoginError = LoginError.None;
                var parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("email", email));
                parameters.Add(new KeyValuePair<string, string>("password", pass));
                parameters.Add(new KeyValuePair<string, string>("ip", strIP));
                parameters.Add(new KeyValuePair<string, string>("mac", strMac));
                parameters.Add(new KeyValuePair<string, string>("app", "AIOBOT"));
                FormUrlEncodedContent formdata = new FormUrlEncodedContent(parameters);
                HttpClient httpClient = new HttpClient();
                json = httpClient.PostAsync("https://aiobotjp.com/api/login", formdata).Result.Content.ReadAsStringAsync().Result;                
                return json;
            }
            catch
            {               
                return "error";
            }            
        }            
        
    }

    class CGlobalVar
    {
        public static string CONFIG_FILE_NAME = "AIOBOT_CaliforniaStreet.ini";
        public static string g_strConfigPath;
        public static bool g_bMultiUse = false;

        public static LoginError g_nLoginError = LoginError.None;
        
        public static string[] g_strPrefectureCodes = new string[] { "00101", "00202", "00203", "00204", "00305", "00306", "00307", "00408", "00409", "00410", "00411", "00412", "00413", "00414", "00415", "00516", "00517", "00618", "00619", "00620", "00621", "00722", "00723", "00724", "00825", "00826", "00827", "00828", "00829", "00830", "00931", "00932", "00933", "00934", "00935", "01036", "01037", "01038", "01039", "01140", "01141", "01142", "01143", "01144", "01145", "01146", "01247" };

        // AIOBOT Login Info
        public static bool g_bAIOBOT_AutoLogin = false;
        public static string g_strAIOBOT_Email = "";
        public static string g_strAIOBOT_Password = "";

        // Site Login Info
        public static string g_strSiteLoginID = "";
        public static string g_strSiteLoginPass = "";
        public static bool g_bSite_AutoLogin = false;

        // Setting Info
        public static int g_nRetryInterval = 100;
        public static int g_nRetryCount = 5;
        public static int g_nOrderSleepTime = 10;
        public static bool g_bUseProxy = false;
        public static string g_strProxyIP = "";
        public static string g_nProxyPort = "";
        public static string g_strProxyID = "";
        public static string g_strProxyPass = "";
        public static bool g_bUseTrayIcon = false;
        public static bool g_bUseStartTime = false;
        public static string g_strStartDay = "";
        public static string g_strStartTime = "";
        public static string g_strAutoGoodID = "";
        public static string g_strAutoSize1 = "";
        public static string g_strAutoSize2 = "";
        public static string g_strAutoSize3 = "";
        public static bool g_bUseNoSizeOrder = true;
        public static bool g_bUseAlarm = true;
        public static int g_nBeforeStart = 30;
        public static int g_nAutoOrderAmount = 1;
        public static int g_nAutoOrderCount = 1;
        public static bool g_bUseAutoOrderPoint = true;

        // AutoSearch
        public static bool g_bUseSearch = false;
        public static int g_nSearchInterval = 100;
        public static List<string> g_strSearchURL = new List<string>();
        public static List<string> g_strSearchKeyword = new List<string>();
        public static bool g_bUseRealOrder = false;

        // Address Info
        public static bool g_bUseLoginInfo = true;
        public static string g_strName1 = "";
        public static string g_strName2 = "";
        public static string g_strHuri1 = "";
        public static string g_strHuri2 = "";
        public static string g_strPostalCode1 = "";
        public static string g_strPostalCode2 = "";
        public static int g_nPrefecture = 0;
        public static string g_strAddress1 = "";
        public static string g_strAddress2 = "";
        public static string g_strAddress3 = "";
        public static string g_strMobile1 = "";
        public static string g_strMobile2 = "";
        public static string g_strMobile3 = "";
        public static string g_strDeliveryTime = "";
        public static string g_strHope = "";
        public static UsePointType g_nUsePointType = UsePointType.NoUse;
        public static int g_nPartialPoint = 0;

        public static string g_strToken = "";
        public static string g_strValidity = "";
        public static string g_strMaskedPan = "";

        public static void ReadConfig()
        {
            g_strConfigPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE_NAME);

            g_bAIOBOT_AutoLogin = IniControl.GetBoolValue("AIOBOT", "AutoLogin", 0, g_strConfigPath);
            g_strAIOBOT_Email = IniControl.GetStringValue("AIOBOT", "Email", "", 255, g_strConfigPath);
            g_strAIOBOT_Password = IniControl.GetStringValue("AIOBOT", "Password", "", 255, g_strConfigPath);

            g_bSite_AutoLogin = IniControl.GetBoolValue("Site", "AutoLogin", 0, g_strConfigPath);
            g_strSiteLoginID = IniControl.GetStringValue("Site", "LoginID", "", 255, g_strConfigPath);
            g_strSiteLoginPass = IniControl.GetStringValue("Site", "Password", "", 255, g_strConfigPath);

            g_nRetryInterval = IniControl.GetIntValue("Setting", "RetryInterval", 100, g_strConfigPath);
            g_nRetryCount = IniControl.GetIntValue("Setting", "RetryCount", 5, g_strConfigPath);
            g_nOrderSleepTime = IniControl.GetIntValue("Setting", "OrderSleepTime", 10, g_strConfigPath);
            g_bUseProxy = IniControl.GetBoolValue("Setting", "UseProxy", 0, g_strConfigPath);
            g_strProxyIP = IniControl.GetStringValue("Setting", "ProxyIP", "", 100, g_strConfigPath);
            g_strProxyID = IniControl.GetStringValue("Setting", "ProxyID", "", 100, g_strConfigPath);
            g_strProxyPass = IniControl.GetStringValue("Setting", "ProxyPass", "", 100, g_strConfigPath);
            g_bUseTrayIcon = IniControl.GetBoolValue("Setting", "UseTrayIcon", 0, g_strConfigPath);
            g_bUseStartTime = IniControl.GetBoolValue("Setting", "UseStartTime", 0, g_strConfigPath);
            g_strStartDay = IniControl.GetStringValue("Setting", "StartDay", "", 100, g_strConfigPath);
            g_strStartTime = IniControl.GetStringValue("Setting", "StartTime", "", 100, g_strConfigPath);
            g_strAutoGoodID = IniControl.GetStringValue("Setting", "AutoGoodID", "", 100, g_strConfigPath, true);
            g_strAutoSize1 = IniControl.GetStringValue("Setting", "AutoSize1", "", 100, g_strConfigPath);
            g_strAutoSize2 = IniControl.GetStringValue("Setting", "AutoSize2", "", 100, g_strConfigPath);
            g_strAutoSize3 = IniControl.GetStringValue("Setting", "AutoSize3", "", 100, g_strConfigPath);
            g_bUseNoSizeOrder = IniControl.GetBoolValue("Setting", "UseNoSizeOrder", 1, g_strConfigPath);
            g_bUseAlarm = IniControl.GetBoolValue("Setting", "UseAlarm", 1, g_strConfigPath);
            g_nBeforeStart = IniControl.GetIntValue("Setting", "BeforeStart", 30, g_strConfigPath);
            g_nAutoOrderAmount = IniControl.GetIntValue("Setting", "AutoOrderAmount", 1, g_strConfigPath);
            g_nAutoOrderCount = IniControl.GetIntValue("Setting", "AutoOrderCount", 1, g_strConfigPath);
            g_bUseAutoOrderPoint = IniControl.GetBoolValue("Setting", "UseAutoOrderPoint", 1, g_strConfigPath);

            g_strToken = IniControl.GetStringValue("Setting", "Token", "", 100, g_strConfigPath);
            g_strValidity = IniControl.GetStringValue("Setting", "Validity", "", 100, g_strConfigPath);
            g_strMaskedPan = IniControl.GetStringValue("Setting", "MaskedPan", "", 100, g_strConfigPath);

            g_bUseSearch = IniControl.GetBoolValue("Setting", "UseSearch", 0, g_strConfigPath);
            g_nSearchInterval = IniControl.GetIntValue("Setting", "SearchInterval", 100, g_strConfigPath);
            int nCount = IniControl.GetIntValue("Setting", "SearchURLCount", 0, g_strConfigPath);
            g_strSearchURL.Clear();
            for (int i = 1; i <= nCount; i++)
            {
                string strURL = IniControl.GetStringValue("Setting", "SearchURL" + i.ToString(), "", 100, g_strConfigPath, true);
                g_strSearchURL.Add(strURL);
            }
            nCount = IniControl.GetIntValue("Setting", "SearchKeywordCount", 0, g_strConfigPath);
            g_strSearchKeyword.Clear();
            for (int i = 1; i <= nCount; i++)
            {
                string strWord = IniControl.GetStringValue("Setting", "SearchKeyword" + i.ToString(), "", 100, g_strConfigPath, true);
                g_strSearchKeyword.Add(strWord);
            }
            g_bUseRealOrder = IniControl.GetBoolValue("Setting", "UseRealOrder", 0, g_strConfigPath);

            g_bUseLoginInfo = IniControl.GetBoolValue("Setting", "UseLoginInfo", 1, g_strConfigPath);
            g_strName1 = IniControl.GetStringValue("Setting", "Name1", "", 100, g_strConfigPath);
            g_strName2 = IniControl.GetStringValue("Setting", "Name2", "", 100, g_strConfigPath);
            g_strHuri1 = IniControl.GetStringValue("Setting", "Huri1", "", 100, g_strConfigPath);
            g_strHuri2 = IniControl.GetStringValue("Setting", "Huri2", "", 100, g_strConfigPath);
            g_strPostalCode1 = IniControl.GetStringValue("Setting", "PostalCode1", "", 100, g_strConfigPath);
            g_strPostalCode2 = IniControl.GetStringValue("Setting", "PostalCode2", "", 100, g_strConfigPath);
            g_nPrefecture = IniControl.GetIntValue("Setting", "Prefecture", 0, g_strConfigPath);
            g_strAddress1 = IniControl.GetStringValue("Setting", "Address1", "", 100, g_strConfigPath);
            g_strAddress2 = IniControl.GetStringValue("Setting", "Address2", "", 100, g_strConfigPath);
            g_strAddress3 = IniControl.GetStringValue("Setting", "Address3", "", 100, g_strConfigPath);
            g_strMobile1 = IniControl.GetStringValue("Setting", "Mobile1", "", 100, g_strConfigPath);
            g_strMobile2 = IniControl.GetStringValue("Setting", "Mobile2", "", 100, g_strConfigPath);
            g_strMobile3 = IniControl.GetStringValue("Setting", "Mobile3", "", 100, g_strConfigPath);
            g_strDeliveryTime = IniControl.GetStringValue("Setting", "DeliveryTime", "", 100, g_strConfigPath);
            g_strHope = IniControl.GetStringValue("Setting", "Hope", "", 100, g_strConfigPath);
            g_nUsePointType = (UsePointType)IniControl.GetIntValue("Setting", "UsePointType", (int)UsePointType.NoUse, g_strConfigPath);
            g_nPartialPoint = IniControl.GetIntValue("Setting", "PartialPoint", 0, g_strConfigPath);

            IniControl.WriteStringValue("Setting", "Token", g_strToken, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Validity", g_strValidity, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "MaskedPan", g_strMaskedPan, g_strConfigPath);
        }

        public static void WriteConfig()
        {
            IniControl.WriteBoolValue("AIOBOT", "AutoLogin", g_bAIOBOT_AutoLogin, g_strConfigPath);
            IniControl.WriteStringValue("AIOBOT", "Email", g_strAIOBOT_Email, g_strConfigPath);
            IniControl.WriteStringValue("AIOBOT", "Password", g_strAIOBOT_Password, g_strConfigPath);

            IniControl.WriteBoolValue("Site", "AutoLogin", g_bSite_AutoLogin, g_strConfigPath);
            IniControl.WriteStringValue("Site", "LoginID", g_strSiteLoginID, g_strConfigPath);
            IniControl.WriteStringValue("Site", "Password", g_strSiteLoginPass, g_strConfigPath);

            IniControl.WriteIntValue("Setting", "RetryInterval", g_nRetryInterval, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "RetryCount", g_nRetryCount, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "OrderSleepTime", g_nOrderSleepTime, g_strConfigPath);
            IniControl.WriteBoolValue("Setting", "UseProxy", g_bUseProxy, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "ProxyIP", g_strProxyIP, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "ProxyID", g_strProxyID, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "ProxyPass", g_strProxyPass, g_strConfigPath);
            IniControl.WriteBoolValue("Setting", "UseTrayIcon", g_bUseTrayIcon, g_strConfigPath);
            IniControl.WriteBoolValue("Setting", "UseStartTime", g_bUseStartTime, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "StartDay", g_strStartDay, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "StartTime", g_strStartTime, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "AutoGoodID", g_strAutoGoodID, g_strConfigPath, true);
            IniControl.WriteStringValue("Setting", "AutoSize1", g_strAutoSize1, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "AutoSize2", g_strAutoSize2, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "AutoSize3", g_strAutoSize3, g_strConfigPath);
            IniControl.WriteBoolValue("Setting", "UseNoSizeOrder", g_bUseNoSizeOrder, g_strConfigPath);
            IniControl.WriteBoolValue("Setting", "UseAlarm", g_bUseAlarm, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "BeforeStart", g_nBeforeStart, g_strConfigPath);

            IniControl.WriteBoolValue("Setting", "UseSearch", g_bUseSearch, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "SearchInterval", g_nSearchInterval, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "SearchURLCount", g_strSearchURL.Count, g_strConfigPath);
            for (int i = 1; i <= g_strSearchURL.Count; i++)
            {
                IniControl.WriteStringValue("Setting", "SearchURL" + i.ToString(), g_strSearchURL[i - 1], g_strConfigPath, true);
            }
            IniControl.WriteIntValue("Setting", "SearchKeywordCount", g_strSearchKeyword.Count, g_strConfigPath);
            for (int i = 1; i <= g_strSearchKeyword.Count; i++)
            {
                IniControl.WriteStringValue("Setting", "SearchKeyword" + i.ToString(), g_strSearchKeyword[i - 1], g_strConfigPath, true);
            }
            IniControl.WriteBoolValue("Setting", "UseRealOrder", g_bUseRealOrder, g_strConfigPath);

            IniControl.WriteBoolValue("Setting", "UseLoginInfo", g_bUseLoginInfo, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Name1", g_strName1, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Name2", g_strName2, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Huri1", g_strHuri1, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Huri2", g_strHuri2, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "PostalCode1", g_strPostalCode1, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "PostalCode2", g_strPostalCode2, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "Prefecture", g_nPrefecture, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Address1", g_strAddress1, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Address2", g_strAddress2, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Address3", g_strAddress3, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Mobile1", g_strMobile1, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Mobile2", g_strMobile2, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Mobile3", g_strMobile3, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "DeliveryTime", g_strDeliveryTime, g_strConfigPath);
            IniControl.WriteStringValue("Setting", "Hope", g_strHope, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "UsePointType", (int)g_nUsePointType, g_strConfigPath);
            IniControl.WriteIntValue("Setting", "PartialPoint", g_nPartialPoint, g_strConfigPath);
        }

        public static string EncodeURIComponent(Dictionary<string, object> rgData)
        {
            string _result = String.Join("&", rgData.Select((x) => String.Format("{0}={1}", x.Key, x.Value)));

            _result = System.Net.WebUtility.UrlEncode(_result)
                        .Replace("+", "%20").Replace("%21", "!")
                        .Replace("%27", "'").Replace("%28", "(")
                        .Replace("%29", ")").Replace("%26", "&")
                        .Replace("%3D", "=").Replace("%7E", "~").Replace("%25", "%");
            return _result;
        }

        public static bool isSubSequence(string str1,
                  string str2, int m, int n)
        {

            // Base Cases 
            if (m == 0)
                return true;
            if (n == 0)
                return false;

            // If last characters of two strings 
            // are matching 
            if (str1[m - 1] == str2[n - 1])
                return isSubSequence(str1, str2,
                                        m - 1, n - 1);

            // If last characters are not matching 
            return isSubSequence(str1, str2, m, n - 1);
        }

        public static string GetCpuInfo()
        {
            string cpuInfo = "";

            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                cpuInfo = managObj.Properties["processorID"].Value.ToString();
                break;
            }

            return cpuInfo;
        }

        public static string GetBOTKey()
        {
            int i, nSplit = 4;
            string strKey = "", strFull;
            string strMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            if (strMacAddress == null)
            {
                strMacAddress = GetCpuInfo();
            }
            if (strMacAddress == null)
            {
                MessageBox.Show(JP_Constants.ERROR_GET_KEY_FAILED);
                strMacAddress = "";
            }

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(strMacAddress);
            for (i = 0; i < inputBytes.Length; i++)
            {
                inputBytes[i] = (byte)((inputBytes[i] + 58) % 255);
            }

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < hash.Length; i++)
            {
                hash[i] = (byte)((hash[i] + 84) % 255);
                sb.Append(hash[i].ToString("X2"));
            }

            strFull = sb.ToString();
            for (i = 0; i < strFull.Length; i += nSplit)
            {
                strKey += strFull.Substring(i, nSplit) + "-";
            }

            return strKey.Substring(0, strKey.Length - 1);
        }

        public static string GetGlobalIPAddress()
        {
            HttpCommon http_request = new HttpCommon();

            http_request.setURL("https://search.naver.com/search.naver?sm=top_hty&fbm=1&ie=utf8&query=%EB%82%B4+ip");
            http_request.setSendMode(HTTP_SEND_MODE.HTTP_GET);

            if (!http_request.sendRequest(false, ""))
            {
                MessageBox.Show(JP_Constants.PROXY_TEST_FAILED);
                return GetLocalIPAddress();
            }

            string response = http_request.getResponseString();
            int nStartPos = response.IndexOf("ip_chk_box");
            if (nStartPos < 0)
            {
                MessageBox.Show(JP_Constants.PROXY_TEST_FAILED);
                return GetLocalIPAddress();
            }
            nStartPos = response.IndexOf("<em>", nStartPos);
            nStartPos += 4;
            int nEndPos = response.IndexOf("</em>", nStartPos);
            string strIp = response.Substring(nStartPos, nEndPos - nStartPos);

            return strIp;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string GetNowTimestamp()
        {
            string strTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds.ToString();
            return strTimestamp.Substring(0, strTimestamp.Length - 3);
        }
    }
}
