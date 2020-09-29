using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOBOT
{
    class Types
    {
    }

    public struct BotTask
    {
        public string id;
        public string product_url;
        public string product_name;
        public string size;      
        public string color_option;
        public string profilename;
        public string activeProxy;
        public string store;
        public int cardtype;
        public string wish;
        public string token;
        public string sku;
        public string variant;
        public string product_id;
        public string image_url;
    }

    public struct MyProxy
    {
        public string url;
        public string port;
        public string user;
        public string pass;
    }

    public struct AddTask
    {
        public string id;
        public string product_url;
        public string size_option;
        public string profilename;
        public string proxy;
        public int monitor;
        public int interval;
    }
    public enum Result
    {
        FAILURE = 0,
        RETRY = 1,
        SUCCESS = 2,
    }

    public struct Profile
    { 
        public string profilename;
        public string email;
        public string password;
        public string cardNumber;
        public string cardName;
        public string cardMonth;
        public string cardYear;
        public string cardCVV;
        public string cardType;
        public string store;
    }  

    public struct ProfileMCT
    {
        public string id;
        public string profilename;
        public string email;
        public string firstname;
        public string lastname;
        public string company;
        public string address1;
        public string address2;
        public string city;
        public string phone;
        public string province;
        public string postalcode;
        public string cardNumber;
        public string cardName;
        public string cardMonth;
        public string cardYear;
        public string cardCVV;
        public string cardType;
    }
    public enum MessageBoxType
    {
        ConfirmationWithYesNo = 0,
        ConfirmationWithYesNoCancel,
        Information,
        Error,
        Warning
    }

    public enum MessageBoxImage
    {
        Warning = 0,
        Question,
        Information,
        Error,
        None
    }
    public struct ProxyGroup
    {
        public string group_id;
        public string group_name;
        public int group_count;
        public List<MyProxy> proxylist;
    }

    public class TaskManagement
    {
        public static List<string> SeletedTasks = new List<string>();
        public static string webhook = "";
        public static bool success_notify = false;
        public static string success_sound = "";
        public static bool warning_notify = false;
        public static string warning_sound = "";
        public static bool user_MCT = false;
        public static bool user_AS = false;
        public static bool user_MORTAR = false;
        public static bool user_ZI = false;
        public static bool user_Zozo = false;
        public static bool user_FTC = false;
        public static bool user_ARK = false;
        public static bool user_ISB = false;
        public static List<BotTask> User_Tasks = new List<BotTask>();
        public static List<Profile> User_Profiles = new List<Profile>();
        public static List<ProfileMCT> User_Profiles_MCT = new List<ProfileMCT>();
        public static string GA_token = "";
        public static Dictionary<string, int> captcha_token_Mortar = new Dictionary<string, int>();
    }

    public enum CreditCardType
    {
        None = 0,
        Visa = 1,
        MasterCard = 2,
        JCB = 3,
        AmericanExpress = 4,
    }
}
