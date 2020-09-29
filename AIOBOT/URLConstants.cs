using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOBOT
{
    class URLConstants
    {
        public const string LOGIN_MORTAR = "https://mortartokyo.stores.jp/login";
        public const string BASE_MORTAR = "https://mortartokyo.stores.jp/";
        public const string ADDTOCART_MORTAR = "https://mortartokyo.com/api/v1/cart/items";
        public const string CHECKOUT_MORTAR = "https://mortartokyo.stores.jp/checkout";
        public const string PREVIEW_ORDER_MORTAR = "https://mortartokyo.stores.jp/api/v1/cart/preview_order";
    }

    class LogIn_Mortar
    {
        public string email { get; set; }
        public string password { get; set; }
        public override string ToString()
        {
            return $"{email}: {password}";
        }
    }

    class Cart_Item_Mortar
    {
        public string id { get; set; }
        public string variation { get; set; }
        public string quantity { get; set; }
        public string frequency { get; set; }
    }

    class Cart_Item_NoFreq_Mortar
    {
        public string id { get; set; }
        public string variation { get; set; }
        public int quantity { get; set; }
    }

    class CartItems_NoFreq_Mortar
    {
        public List<Cart_Item_NoFreq_Mortar> items { get; set; }
    }

    class Cart_Items_Mortar
    {
        public List<Cart_Item_Mortar> items { get; set; }
    }

    class Payment_Item_Mortar
    {
        public string id { get; set; }
        public string variation { get; set; }
        public string quantity { get; set; }
        public string frequency { get; set; }
        public string isDigit { get; set; }
        public string isMybookItem { get; set; }
        public string isSubscription { get; set; }
        public string isLimitedReferer { get; set; }
        public string isTicket { get; set; }
        public string isPreorder { get; set; }
    }
    class Payment_Customer_Mortar
    {
        public string email { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string tel { get; set; }
        public string zip { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string prefecture { get; set; }
        public string gift_flg { get; set; }
        public string wrapping_fee { get; set; }
        public string receipt_flg { get; set; }
        public string receipt_address { get; set; }
        public string note { get; set; }
        public string password { get; set; }
        public string signup_terms { get; set; }
        public string address_change { get; set; }
        public string use_registered_cc { get; set; }
        public string country_target { get; set; }
        public string store_newsletter { get; set; }
        public string operation_announcement { get; set; }
        public List<string> payment_method = new List<string>();
        public string address1 { get; set; }
        public CC_Mortar cc { get; set; }
    }
    class CC_Mortar
    {
        public string company { get; set; }
        public string security_code { get; set; }
        public string register { get; set; }
    }
    class Payment_Mortar
    {
        public List<Payment_Item_Mortar> items { get; set; }
        public string g_recaptcha_response { get; set; }
        public Payment_Customer_Mortar customer { get; set; }
        public string locale { get; set; }
    }
}
