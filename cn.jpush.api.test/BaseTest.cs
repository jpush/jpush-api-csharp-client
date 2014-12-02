using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cn.jpush.api.test
{
  
    public class BaseTest
    {
        public static String APP_KEY = "997f28c1cea5a9f17d82079a";
        public static String MASTER_SECRET = "47d264a3c02a6a5a4a256a45";
      

         public const  String ALERT = "JPush Test - alert";
         public const  String MSG_CONTENT = "JPush Test - msgContent";

         public const String REGISTRATION_ID1 = "061c84f7bef";
         public const String REGISTRATION_ID2 = "0619cfd4989";

         public const String LONG_TEXT_1 = ""
            + "鏋佸厜鎺ㄩ�侊紝浣垮緱寮�鍙戣�呭彲浠ュ嵆鏃跺湴鍚戝叾搴旂敤绋嬪簭鐨勭敤鎴锋帹閫侀�氱煡鎴栬�呮秷鎭紝"
            + "涓庣敤鎴蜂繚鎸佷簰鍔紝浠庤�屾湁鏁堝湴鎻愰珮鐣欏瓨鐜囷紝鎻愬崌鐢ㄦ埛浣撻獙銆傚钩鍙版彁渚涙暣鍚堜簡Android鎺ㄩ�併�乮OS鎺ㄩ�佺殑缁熶竴鎺ㄩ�佹湇鍔°��";
         public const String LONG_TEXT_2 = ""
            + "閫氳繃鏋佸厜鎺ㄩ�佹湇鍔★紝涓诲姩銆佸強鏃跺湴鍚戞偍鐨勭敤鎴峰彂璧蜂氦浜掞紝鍚戝叾鎺ㄩ�佽亰澶╂秷鎭�佹棩绋嬫彁閱掋�佹椿鍔ㄩ鍛娿�佽繘搴︽彁绀恒�佸姩鎬佹洿鏂扮瓑銆�"
            + "绮惧噯鐨勭洰鏍囩敤鎴峰拰鏈変环鍊肩殑鎺ㄩ�佸唴瀹瑰彲浠ユ彁鍗囩敤鎴峰繝璇氬害锛屾彁楂樼暀瀛樼巼涓庢敹鍏ャ��"
            + "瀹㈡埛绔� SDK 閲囩敤鑷畾涔夌殑鍗忚淇濇寔闀胯繛鎺ワ紝鐢甸噺銆佹祦閲忔秷鑰楅兘寰堝皯銆� "
            + "鏈嶅姟绔厛杩涙妧鏈灦鏋勶紝楂樺苟鍙戝彲鎵╁睍鎬х殑浜戞湇鍔★紝缁忓彈杩囧嚑浜跨敤鎴风殑鑰冮獙锛�"
            + "瀹屽叏鐪佸幓搴旂敤寮�鍙戣�呰嚜宸辩淮鎶ら暱杩炴帴鐨勮澶囧拰浜哄姏鐨勬垚鏈姇鍏ャ��"
            + "绠�鍗曠殑SDK闆嗘垚鏂瑰紡锛屼娇寮�鍙戝晢鍙互蹇�熼儴缃诧紝鏇翠笓娉ㄤ富钀ヤ笟鍔°�傜伒娲荤殑鎺ㄩ�佸叆鎺ュ叆锛�"
            + "鍚屾椂鏀寔缃戠珯涓婄洿鎺ユ帹閫侊紝涔熸彁渚� 娑堟伅鎺ㄩ�佸拰閫佽揪缁熻鐨� API璋冪敤銆� "
            + "娓呮櫚鐨勭粺璁″浘琛紝鐩磋鐨勮窡韪帹閫佸甫鏉ョ殑鏁堟灉銆�"
            + "涓嬭浇骞堕泦鎴� SDK 鎺ュ叆鏋佸厜鎺ㄩ�佹湇鍔°�傛瀬鍏夋帹閫佹彁渚涗簡 Android锛宨OS锛學indows Phone浠ュ強 PhoneGap 鐨勫鎴风 SDK銆�"
            + "鍚屾椂涔熷紑鏀惧绉嶈瑷�瀹炵幇鐨勬湇鍔＄ SDK锛屾柟渚垮紑鍙戣�呰皟鐢� API 杩涜鎺ㄩ�併��"
            + "鏈� Wiki 鏄瀬鍏夋帹閫� (JPush) 浜у搧鐨勫紑鍙戣�呮枃妗ｇ綉绔欍��"
            + "鏋佸厜鎺ㄩ�佹墍鏈夋妧鏈枃妗ｉ兘鍦ㄦ湰 Wiki 閲岋紝娌℃湁鍒殑鎻愪緵娓犻亾銆傚悓鏃讹紝鎴戜滑涔熷湪涓嶆柇鍦拌ˉ鍏呫�佸畬鍠勬枃妗ｃ��"
            + "杩欎簺鏂囨。鍖呮嫭杩欐牱鍑犵绫诲瀷锛氬父瑙侀棶棰樸�佸叆闂ㄦ寚鍗椼�丄PI瀹氫箟銆佹暀绋嬬瓑銆�";

    }
}
