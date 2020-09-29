using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOBOT
{
    class EN_Constants
    {
        //form titles
        public const string Dashboard = "DashBoard";
        public const string Setting = "Setting";
        public const string Proxy = "Proxy";
        public const string Profile = "Profile";
        public const string NewTask = "NewTask";
        public const string Statistics = "Statistics";
        public const string TaskBoard = "TaskBoard";
        public const string SiteType = "Select Site";
        public const string ProductDetails = "Product Details";
        public const string SelectProfile = "Select Profile";
        public const string SelectPayment = "Select Payment Method";
        public const string SelectProxy = "Select Proxy";
        public const string CreateTask = "Create Task";
        public const string CancelTask = "Cancel";
        public const string RetryingLimit = "Retrying Limits";
        public const string RefreshTime = "Refresh Time";
        public const string SleepTime = "Sleep Time";
        public const string AutoSize = "Auto";
        public const string ManualSize = "Manual";
        public const string LabelSize = "Size";
        public const string ProductURL = "Product URL";
        public const string SearchProduct = "Search Product";
        public const string Waring = "Warning";
        public const string Success = "Success";
        public const string Email_Empty = "Please Input Email";
        public const string Password_Empty = "Please Input Password";
        public const string CardNumber_Empty = "Please Input Amazon ID or CardNumber";
        public const string CardName_Empty = "Please Input Card Name";
        public const string CardCVV_Empty = "Please Input Card Security Code";
        public const string ProfileName_Empty = "Please Input Profile Name";
        public const string ProductURL_Empty = "Please Input Product URL";
        public const string Size_Empty = "Please Input Size";
        public const string GETINFO = "FOUND PRODUCT";
        public const string FAILEDINFO = "NOT FOUND PRODUCT";
        public const string ProfileName_Duplicate = "The Same Profile Name exist.";
        public const string CardToken_Empty = "Please input the token of Credit Card";

        public const string TaskNotify = "Task Created Successfully";
        public const string WishComment = "If you have any requests regarding your order, please fill in the form below";

        public const string ProxyPanel = "PROXY PANEL";
        public const string ProxyAdd = "Add";
        public const string ProxyClear = "Clear";
        public const string ProxyCount = "PROXY COUNTS";
        public const string ProxyList = "PROXY LIST";
        public const string ProxySave = "Save";
        public const string ProxyTest = "Test";

        public const string EditProxy = "PROXY";
        public const string EditProxyAction = "ACTION";
        public const string EditProxyStatus = "STATUS";

        public const string GroupAction = "ACTION";
        public const string GroupList = "GROUP LIST";
        public const string GroupName = "GROUP NAME";
        public const string ClearGroup = "Clear Group";

        public const string WebHook = "WebHook";
        public const string TestWebhook = "Test WebHook";
        public const string GroudSoundSetting = "Sound Setting";
        public const string ProxyGroupName_Empty = "Please input the proxy group name!";
        public const string SettingPanel = "Setting Panel";
        ///
        public const string ERROR_MESSAGE = "Error Message:";
        public const string ERROR_INVALID_LOGIN_EMAIL = "Please Input Email";
        public const string ERROR_INVALID_LOGIN_PASSWORD = "Please Input Password";
        public const string ERROR_ACCOUNT_NOT_EXIST = "Account does not exist";
        public const string ERROR_ACCOUNT_INCORRECT_PASSWORD = "Password is not correct";
        public const string ERROR_ACCOUNT_INVALID_KEY = "Does not match the registered key.";
        public const string ERROR_ACCOUNT_EXPIRED = "Usage period is expired.";
        public const string ERROR_AIOBOT_LOGIN_FAILED = "Failed to login";
        public const string ERROR_ACCOUNT_NOT_AUTHED = "Unauthenticated account";
        public const string ERROR_ACCOUNT_NOT_TRADER = "Since it is an unapproved account, we only support order testing";
        public const string ERROR_INVALID_SITE_LOGINID = "Please enter your login ID";
        public const string ERROR_INVALID_SITE_LOGINPASS = "Please Input Password";
        public const string ERROR_INVALID_SITE_GOODNO = "Please input the product number";
        public const string ERROR_INVALID_SIZE = "Please Select a size";
        public const string ERROR_INVALID_AMOUNT = "Please Input the quantity";
        public const string ERROR_NO_STOCK = "No Stock";
        public const string ERROR_NOT_PAYED = "This feature is only available to the paying user";
        public const string ERROR_INVALID_GOOD_INFO = "Please get the product information first";
        public const string ERROR_INVALID_CREDIT_CARD_INFO = "The card number is incorrect";
        public const string ERROR_GET_KEY_FAILED = "Key acquisition failed";
        public const string ERROR_MULTI_USE = "Multiple launches were not allowed";
        public const string ERROR_PRODUCT_NOT_EXIST = "Product is not released yet.";
        public const string IDLE = "IDLE";
        public const string TASKSTART = "Task Started";
        // Login
        public const string SITE_LOGIN_FAILED = "Login Failed";
        public const string SITE_LOGIN_STARTED = "Login Start";
        public const string SITE_LOGIN_RESTARTED = "***** Login is resumed。*****";
        public const string SITE_LOGIN_REQUEST_SUCCESS1 = "The login request was successful. (Stage-1)";
        public const string SITE_LOGIN_REQUEST_SUCCESS2 = "The login request was successful. (Stage-2)";
        public const string SITE_LOGIN_REQUEST_SUCCESS3 = "The login request was successful. (Stage-3)";
        public const string SITE_LOGIN_SUCCESS = "Login Success";

        // Logout
        public const string SITE_LOGOUT_FAILED = "Logout Failed";
        public const string SITE_LOGOUT_STARTED = "Logout Start";
        public const string SITE_LOGOUT_RESTARTED = "***** Logout resumed*****";
        public const string SITE_LOGOUT_REQUEST_SUCCESS1 = "The logout request was successful. (Stage-1)";
        public const string SITE_LOGOUT_REQUEST_SUCCESS2 = "The logout request was successful. (Stage-2)";
        public const string SITE_LOGOUT_SUCCESS = "Logout Success";
        public static string SITE_LOGOUT_REQUEST_SUCCESS = "Successful logout request";
        // Step1 - GetInfo
        public const string SITE_GETINFO_FAILED = "GET Product Failed";
        public const string SITE_GETINFO_STARTED = "GET Product has started";
        public const string SITE_GETINFO_RESTARTED = "***** GET Product has resumed*****";
        public const string SITE_GETINFO_REQUEST_SUCCESS = "Successful request for GET Product";
        public const string SITE_GETINFO_SUCCESS = "GET Product Success";
        public const string SITE_GET_STOCKOPT_SUCCESS = "「stockopt」Succeeded in。";
        public const string SITE_GET_STOCKOUT_SUCCESS = "「stockout」Succeeded in。";
        public const string SITE_GETSIZE_NODATA = "There is no size information";
        public const string SITE_GETSIZE_SUCCESS = "Successfully obtained size information";
        public const string SITE_GETSIZE_VARIATION = "Got VARIATION information";
        public const string SITE_GOOD_SOLDOUT = "The product has expired!";

        // Step2 - ConfirmBasket
        public const string SITE_CONFIRMBASKET_STARTED = "Adding Cart started";
        public const string SITE_CONFIRMBASKET_FAILED = "Adding Cart Failed";
        public const string SITE_CONFIRMBASKET_SUCCESS = "Adding Cart Success";
        public const string SITE_CONFIRMBASKET_RESTARTED = "***** Adding Cart resume *****";
        public const string SITE_CONFIRMBASKET_REQUEST_SUCCESS1 = "Successful request to add to shopping cart. (stage-1)";
        public const string SITE_CONFIRMBASKET_REQUEST_SUCCESS2 = "Successful request to add to shopping cart. (stage-2)";

        // Step3 - ResetQuantity
        public const string SITE_RESETQUANTITY_STARTED = "Quantity Change started.";
        public const string SITE_RESETQUANTITY_FAILED = "Quantity Change failed";
        public const string SITE_RESETQUANTITY_SUCCESS = "Quantity Change succeeded";
        public const string SITE_RESETQUANTITY_RESTARTED = "***** Quantity Change has resumed *****";
        public const string SITE_RESETQUANTITY_REQUEST_SUCCESS1 = "Successful request for quantity Change. (stage-1)";
        public const string SITE_RESETQUANTITY_REQUEST_SUCCESS2 = "Successful request for quantity Change. (stage-2)";

        // Step4 - GetOrdererInfo
        public static string SITE_GETORDERERINFO_STARTED = "Order Info started";
        public static string SITE_GETORDERERINFO_FAILED = "Order Info failed";
        public static string SITE_GETORDERERINFO_SUCCESS = "Order Info Success";
        public static string SITE_GETORDERERINFO_RESTARTED = "***** Order Info has resumed *****";
        public static string SITE_GETORDERERINFO_REQUEST_SUCCESS1 = "Order Info request was successful. (Step-1)";
        public static string SITE_GETORDERERINFO_REQUEST_SUCCESS2 = "Order Info request was successful. (Stage-2)";

        // Step4 - ConfirmCartin
        public const string SITE_CONFIRMCUTIN_STARTED = "Cart-in started";
        public const string SITE_CONFIRMCUTIN_FAILED = "Cartin Failed";
        public const string SITE_CONFIRMCUTIN_SUCCESS = "Cart-in succeeded";
        public const string SITE_CONFIRMCUTIN_RESTARTED = "***** Cart-in has been resumed *****";
        public const string SITE_CONFIRMCUTIN_REQUEST_SUCCESS1 = "The cart-in request was successful。(stage-1)";
        public const string SITE_CONFIRMCUTIN_REQUEST_SUCCESS2 = "The cart-in request was successful。(stage-2)";

        // Step5 - DeliverySettle
        public const string SITE_DELIVERYSETTLE_STARTED = "Delivery address setting has started";
        public const string SITE_DELIVERYSETTLE_FAILED = "Destination setting failed";
        public const string SITE_DELIVERYSETTLE_SUCCESS = "Delivery setting successful";
        public const string SITE_DELIVERYSETTLE_RESTARTED = "***** Delivery address settings have been resumed *****";
        public const string SITE_DELIVERYSETTLE_REQUEST_SUCCESS1 = "Successful request for delivery address setting。(stage-1)";
        public const string SITE_DELIVERYSETTLE_REQUEST_SUCCESS2 = "Successful request for delivery address setting。(stage-2)";

        // Step6 - PayConfirm
        public const string SITE_PAYCONFIRM_STARTED = "Payment Start";
        public const string SITE_PAYCONFIRM_FAILED = "Payment Failed";
        public const string SITE_PAYCONFIRM_SUCCESS = "Payment Successe";
        public const string SITE_PAYCONFIRM_RESTARTED = "***** Payment Resume *****";
        public const string SITE_PAYCONFIRM_REQUEST_SUCCESS1 = "Successful payment confirmation request。(stage-1)";
        public const string SITE_PAYCONFIRM_REQUEST_SUCCESS2 = "Successful payment confirmation request。(stage-2)";
        public const string SITE_PAYCONFIRM_REQUEST_SUCCESS = "Successful payment confirmation request";
        // Step7 - PayComplete
        public const string SITE_PAYCOMPLETE_STARTED = "Payment Completion Start";
        public const string SITE_PAYCOMPLETE_FAILED = "Payment Completion Failed";
        public const string SITE_PAYCOMPLETE_SUCCESS = "Payment Completion Success";
        public const string SITE_PAYCOMPLETE_RESTARTED = "***** Payment completion has resumed *****";
        public const string SITE_PAYCOMPLETE_REQUEST_SUCCESS1 = "Successful payment request (stage-1)";
        public const string SITE_PAYCOMPLETE_REQUEST_SUCCESS2 = "Successful payment request (stage-2)";
        public const string SITE_PAYCOMPLETE_REQUEST_SUCCESS3 = "Successful payment request (stage-3)";

        public static string SITE_PAYCOMPLETE_REQUEST_SUCCESS = "Successful payment request";
        // Step7 - CreditComplete
        public static string SITE_CREDITCOMPLETE_STARTED = "Credit Start";
        public static string SITE_CREDITCOMPLETE_FAILED = "Credit Failed";
        public static string SITE_CREDITCOMPLETE_SUCCESS = "Credit Success";
        public static string SITE_CREDITCOMPLETE_RESTARTED = "***** Credit Resume *****";
        public static string SITE_CREDITCOMPLETE_REQUEST_SUCCESS1 = "The request to complete credit payment was successful. (Stage-1)";
        public static string SITE_CREDITCOMPLETE_REQUEST_SUCCESS2 = "The request to complete credit payment was successful. (Stage-2)";
        // AutoSearch
        public const string SITE_AUTOSEARCH_FAILED = "Automatic search failed";
        public const string SITE_AUTOSEARCH_STARTED = "Automatic search has started";
        public const string SITE_AUTOSEARCH_RESTARTED = "***** Automatic search has resumed *****";
        public const string SITE_AUTOSEARCH_REQUEST_SUCCESS1 = "Successful automatic search request。(stage-1)";
        public const string SITE_AUTOSEARCH_REQUEST_SUCCESS2 = "Successful automatic search request。(stage-2)";
        public const string SITE_AUTOSEARCH_SUCCESS = "Successful automatic search";

        public const string SITE_ORDER_STARTED = "Order Started";
        public const string SITE_ORDER_STOPPED = "Order Suspended";
        public const string SITE_ORDER_FAILED = "Order Failed";
        public const string SITE_ORDER_SUCCESS = "Order successful";

        public const string MONITOR_AMOUNT_STARTED = "Balance monitoring has started";
        public const string MONITOR_AMOUNT_STOPPED = "Balance monitoring stopped";

        public const string AUTO_ORDER_STARTED = "Automatic order has started";
        public const string AUTO_ORDER_FAILED = "Automatic order Faild";
        public const string AUTO_ORDER_FINISHED = "Auto order was successful";

        // API
        public const string API_SET_KEY_LIST_SUCCESS = "Key setting was successful。";
        public const string API_SET_KEY_LIST_FAILURE = "Key setting failed";

        // Proxy Test
        public const string PROXY_TEST_FAILED = "Proxy test failed。";
    }



    class JP_Constants
    {
        //form titles
        public const string Success = "成功";
        public const string Dashboard = "ダッシュボード";
        public const string Setting = "設定";
        public const string Proxy = "プロキシ";
        public const string Profile = "プロフィール";
        public const string NewTask = "タスクセッティング";
        public const string Statistics = "ステータス";
        public const string TaskBoard = "タスクボード";

        public const string SiteType = "サイトを選択";
        public const string ProductDetails = "製品詳細";
        public const string SelectProfile = "プロファイルを選択";
        public const string SelectPayment = "お支払い方法を選択";
        public const string SelectProxy = "プロキシを選択";
        public const string CreateTask = "タスクを作成";
        public const string CancelTask = "キャンセル";
        public const string RetryingLimit = "再試行制限";
        public const string RefreshTime = "再試行時間";
        public const string SleepTime = "睡眠時間";
        public const string AutoSize = "自動";
        public const string ManualSize = "手動";
        public const string LabelSize = "サイズ";
        public const string ProductURL = "製品URL";
        public const string SearchProduct = "製品を検索";

        public const string TaskNotify = "タスクが正常に作成されました";
        public const string WishComment = "ご注文に関してご要望がございましたら、以下のフォームにご記入ください。";
        public const string ProxyAdd = "追加";
        public const string ProxyClear = "削除";
        public const string ProxyCount = "プロキシカウント";
        public const string ProxyList = "プロキシリスト";
        public const string ProxySave = "保存";
        public const string ProxyTest = "テスト";

        public const string EditProxy = "プロキシ";
        public const string EditProxyAction = "作用";
        public const string EditProxyStatus = "状態";
        public const string ProxyPanel = "プロキシパネル";
        public const string GroupAction = "作用";
        public const string GroupList = "グループリスト";
        public const string GroupName = "グループ名";
        public const string ClearGroup = "グループ削除";

        public const string SettingPanel = "設定パネル";
        public const string WebHook = "WebHook";
        public const string TestWebhook = "WebHookテスト";
        public const string GroudSoundSetting = "サウンド設定";

        public const string Warning = "警告";
        public const string Email_Empty = "メールアドレスを入力してください";
        public const string Password_Empty = "パスワードを入力してください";
        public const string CardNumber_Empty = "Amazon IDまたはCardNumberを入力してください";
        public const string CardName_Empty = "カード名を入力してください";
        public const string CardCVV_Empty = "カードのセキュリティコードを入力してください";      
        public const string ProfileName_Empty = "プロファイル名を入力してください";
        public const string ProductURL_Empty = "製品のURLを入力してください";
        public const string Size_Empty = "サイズを入力してください";
        public const string GETINFO = "見つかった製品";
        public const string FAILEDINFO = "製品が見つかりません";
        public const string CardToken_Empty = "クレジットカードのトークンを入力してください";
        public const string ProxyGroupName_Empty = "プロキシグループ名を入力してください";

        // Messages
        public const string ERROR_MESSAGE = "エラーメッセージ:";
        public const string ERROR_INVALID_LOGIN_EMAIL = "メールを入力ください。";
        public const string ERROR_INVALID_LOGIN_PASSWORD = "パスワードを入力ください。";
        public const string ERROR_ACCOUNT_NOT_EXIST = "アカウントが存在しません。";
        public const string ERROR_ACCOUNT_INCORRECT_PASSWORD = "パスワードが正確ではありません。";
        public const string ERROR_ACCOUNT_INVALID_KEY = "登録されたキーと一致しません。";
        public const string ERROR_ACCOUNT_EXPIRED = "使用期間が完了しました。";
        public const string ERROR_AIOBOT_LOGIN_FAILED = "ログインに失敗しました。";
        public const string ERROR_ACCOUNT_NOT_AUTHED = "認証されていないアカウントです。";
        public const string ERROR_ACCOUNT_NOT_TRADER = "承認されていないアカウントなので注文テストのみサポートします。";
        public const string ERROR_INVALID_SITE_LOGINID = "ログインIDを入力ください。";
        public const string ERROR_INVALID_SITE_LOGINPASS = "パスワードを入力ください。";
        public const string ERROR_INVALID_SITE_GOODNO = "商品番号を入力ください。";
        public const string ERROR_INVALID_SIZE = "サイズを選択してください。";
        public const string ERROR_INVALID_AMOUNT = "数量を入力してください。";
        public const string ERROR_NO_STOCK = "在庫がありません。";
        public const string ERROR_NOT_PAYED = "この機能は支払ったユーザーのみ使用できます。";
        public const string ERROR_INVALID_GOOD_INFO = "先に商品情報を取得してください。";
        public const string ERROR_INVALID_CREDIT_CARD_INFO = "カード番号に誤りがあります。";
        public const string ERROR_GET_KEY_FAILED = "キー取得に失敗しました。";
        public const string ERROR_MULTI_USE = "複数起動が許されませんでした。";

        public const string IDLE = "待機";
        public const string TASKSTART = "タスク開始";
        // Login
        public const string SITE_LOGIN_FAILED = "ログイン失敗";
        public const string SITE_LOGIN_STARTED = "ログインが始まりました。";
        public const string SITE_LOGIN_RESTARTED = "***** ログインが再開されました。*****";
        public const string SITE_LOGIN_REQUEST_SUCCESS1 = "ログイン要請に成功しました。(段階-1)";
        public const string SITE_LOGIN_REQUEST_SUCCESS2 = "ログイン要請に成功しました。(段階-2)";
        public const string SITE_LOGIN_REQUEST_SUCCESS3 = "ログイン要請に成功しました。(段階-3)";
        public const string SITE_LOGIN_SUCCESS = "ログイン成功";

        // Logout
        public const string SITE_LOGOUT_FAILED = "ログアウト失敗";
        public const string SITE_LOGOUT_STARTED = "ログアウトが始まりました。";
        public const string SITE_LOGOUT_RESTARTED = "***** ログアウトが再開されました。*****";
        public const string SITE_LOGOUT_REQUEST_SUCCESS1 = "ログアウト要請に成功しました。(段階-1)";
        public const string SITE_LOGOUT_REQUEST_SUCCESS2 = "ログアウト要請に成功しました。(段階-2)";
        public const string SITE_LOGOUT_SUCCESS = "ログアウト成功";
        public static string SITE_LOGOUT_REQUEST_SUCCESS = "ログアウト要請に成功しました。";
        // Step1 - GetInfo
        public const string SITE_GETINFO_FAILED = "情報取得失敗";
        public const string SITE_GETINFO_STARTED = "情報取得が始まりました。";
        public const string SITE_GETINFO_RESTARTED = "***** 情報取得が再開されました。*****";
        public const string SITE_GETINFO_REQUEST_SUCCESS = "情報取得要請に成功しました。";
        public const string SITE_GETINFO_SUCCESS = "情報取得成功";
        public const string SITE_GET_STOCKOPT_SUCCESS = "「stockopt」に成功しました。";
        public const string SITE_GET_STOCKOUT_SUCCESS = "「stockout」に成功しました。";
        public const string SITE_GETSIZE_NODATA = "サイズ情報がありません。";
        public const string SITE_GETSIZE_SUCCESS = "サイズ情報取得に成功しました。";
        public const string SITE_GETSIZE_VARIATION = "VARIATION情報を取得しました。";
        public const string SITE_GOOD_SOLDOUT = "商品期限が切れた!";

        // Step2 - ConfirmBasket
        public const string SITE_CONFIRMBASKET_STARTED = "買い物かごに入れるのが開始されました。";
        public const string SITE_CONFIRMBASKET_FAILED = "カート失敗の追加";
        public const string SITE_CONFIRMBASKET_SUCCESS = "カート成功の追加";
        public const string SITE_CONFIRMBASKET_RESTARTED = "***** 買い物かごに入れるのが再開されました。*****";
        public const string SITE_CONFIRMBASKET_REQUEST_SUCCESS1 = "買い物かごに入れるの要請に成功しました。(段階-1)";
        public const string SITE_CONFIRMBASKET_REQUEST_SUCCESS2 = "買い物かごに入れるの要請に成功しました。(段階-2)";

        // Step3 - ResetQuantity
        public const string SITE_RESETQUANTITY_STARTED = "数量調整が開始されました。";
        public const string SITE_RESETQUANTITY_FAILED = "数量調整失敗";
        public const string SITE_RESETQUANTITY_SUCCESS = "数量調整成功";
        public const string SITE_RESETQUANTITY_RESTARTED = "***** 数量調整が再開されました。*****";
        public const string SITE_RESETQUANTITY_REQUEST_SUCCESS1 = "数量調整要請に成功しました。(段階-1)";
        public const string SITE_RESETQUANTITY_REQUEST_SUCCESS2 = "数量調整要請に成功しました。(段階-2)";

        // Step4 - GetOrdererInfo
        public static string SITE_GETORDERERINFO_STARTED = "注文者情報取得が開始されました。";
        public static string SITE_GETORDERERINFO_FAILED = "注文者情報失敗。";
        public static string SITE_GETORDERERINFO_SUCCESS = "注文者情報成功。";
        public static string SITE_GETORDERERINFO_RESTARTED = "***** 注文者情報取得が再開されました。*****";
        public static string SITE_GETORDERERINFO_REQUEST_SUCCESS1 = "注文者情報取得要請に成功しました。(段階-1)";
        public static string SITE_GETORDERERINFO_REQUEST_SUCCESS2 = "注文者情報取得要請に成功しました。(段階-2)";


        // Step4 - ConfirmCutin
        public const string SITE_CONFIRMCUTIN_STARTED = "カートインが開始されました。";
        public const string SITE_CONFIRMCUTIN_FAILED = "カート失敗";
        public const string SITE_CONFIRMCUTIN_SUCCESS = "カート成功。";
        public const string SITE_CONFIRMCUTIN_RESTARTED = "***** カートインが再開されました。*****";
        public const string SITE_CONFIRMCUTIN_REQUEST_SUCCESS1 = "カートイン要請に成功しました。(段階-1)";
        public const string SITE_CONFIRMCUTIN_REQUEST_SUCCESS2 = "カートイン要請に成功しました。(段階-2)";

        // Step5 - DeliverySettle
        public const string SITE_DELIVERYSETTLE_STARTED = "お届け先設定が開始されました。";
        public const string SITE_DELIVERYSETTLE_FAILED = "配信設定失敗";
        public const string SITE_DELIVERYSETTLE_SUCCESS = "配信設定成功";
        public const string SITE_DELIVERYSETTLE_RESTARTED = "***** お届け先設定が再開されました。*****";
        public const string SITE_DELIVERYSETTLE_REQUEST_SUCCESS1 = "お届け先設定要請に成功しました。(段階-1)";
        public const string SITE_DELIVERYSETTLE_REQUEST_SUCCESS2 = "お届け先設定要請に成功しました。(段階-2)";

        // Step6 - PayConfirm
        public const string SITE_PAYCONFIRM_STARTED = "決済確認が開始されました。";
        public const string SITE_PAYCONFIRM_FAILED = "支払い確認失敗";
        public const string SITE_PAYCONFIRM_SUCCESS = "支払い確認成功";
        public const string SITE_PAYCONFIRM_RESTARTED = "***** 決済確認が再開されました。*****";
        public const string SITE_PAYCONFIRM_REQUEST_SUCCESS1 = "決済確認要請に成功しました。(段階-1)";
        public const string SITE_PAYCONFIRM_REQUEST_SUCCESS2 = "決済確認要請に成功しました。(段階-2)";
        public const string SITE_PAYCONFIRM_REQUEST_SUCCESS = "決済確認要請に成功しました";
        // Step7 - PayComplete
        public const string SITE_PAYCOMPLETE_STARTED = "決済完了が開始されました。";
        public const string SITE_PAYCOMPLETE_FAILED = "支払い失敗";
        public const string SITE_PAYCOMPLETE_SUCCESS = "支払い成功";
        public const string SITE_PAYCOMPLETE_RESTARTED = "***** 決済完了が再開されました。*****";
        public const string SITE_PAYCOMPLETE_REQUEST_SUCCESS1 = "決済完了要請に成功しました。(段階-1)";
        public const string SITE_PAYCOMPLETE_REQUEST_SUCCESS2 = "決済完了要請に成功しました。(段階-2)";
        public const string SITE_PAYCOMPLETE_REQUEST_SUCCESS3 = "決済完了要請に成功しました。(段階-3)";
        public static string SITE_PAYCOMPLETE_REQUEST_SUCCESS = "決済完了要請に成功しました。";
        // Step7 - CreditComplete
        public static string SITE_CREDITCOMPLETE_STARTED = "クレジット決済完了が開始されました。";
        public static string SITE_CREDITCOMPLETE_FAILED = "クレジット決済完了が失敗しました。";
        public static string SITE_CREDITCOMPLETE_SUCCESS = "クレジットカード成功";
        public static string SITE_CREDITCOMPLETE_RESTARTED = "***** クレジット決済完了が再開されました。*****";
        public static string SITE_CREDITCOMPLETE_REQUEST_SUCCESS1 = "クレジット決済完了要請に成功しました。(段階-1)";
        public static string SITE_CREDITCOMPLETE_REQUEST_SUCCESS2 = "クレジット決済完了要請に成功しました。(段階-2)";
        // AutoSearch
        public const string SITE_AUTOSEARCH_FAILED = "自動検索に失敗しました。";
        public const string SITE_AUTOSEARCH_STARTED = "自動検索が始まりました。";
        public const string SITE_AUTOSEARCH_RESTARTED = "***** 自動検索が再開されました。*****";
        public const string SITE_AUTOSEARCH_REQUEST_SUCCESS1 = "自動検索要請に成功しました。(段階-1)";
        public const string SITE_AUTOSEARCH_REQUEST_SUCCESS2 = "自動検索要請に成功しました。(段階-2)";
        public const string SITE_AUTOSEARCH_SUCCESS = "自動検索に成功しました。";

        public const string SITE_ORDER_STARTED = "注文が開始されました。";
        public const string SITE_ORDER_STOPPED = "注文が停止されました。";
        public const string SITE_ORDER_FAILED = "注文が失敗しました。";
        public const string SITE_ORDER_SUCCESS = "注文成功";

        public const string MONITOR_AMOUNT_STARTED = "残高監視が開始されました。";
        public const string MONITOR_AMOUNT_STOPPED = "残高監視が停止されました。";

        public const string AUTO_ORDER_STARTED = "自動注文が開始されました。";
        public const string AUTO_ORDER_FAILED = "自動注文が失敗しました。";
        public const string AUTO_ORDER_FINISHED = "自動注文が成功しました。";

        // API
        public const string API_SET_KEY_LIST_SUCCESS = "キー設定が成功しました。";
        public const string API_SET_KEY_LIST_FAILURE = "キー設定が失敗しました。";

        // Proxy Test
        public const string PROXY_TEST_FAILED = "プロキシテストに失敗しました。";
    }
}
