using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Decal.Filters;

using Decal.Interop.Net;
using System;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using WindowsTimer = System.Windows.Forms.Timer;
using System.Drawing;

namespace Handyman
{
    public partial class PluginCore : PluginBase
    {
        WindowsTimer MasterTimer = new WindowsTimer();
        string HandymanDir = null;
        DirectoryInfo dinfo = null;

        string settingsFilename;
        string equipmentFilename;
        string spellsFilename;

        string msubRoutine;
        string msecondarysubRoutine;
        string mroutine;
        string msub;
        string msubinven;
        int inventoryCount;

        // private System.Windows.Forms.WindowsTimer MasterWindowsTimer = new System.Windows.Forms.WindowsTimer();
        public static string Log;
        public string chatCmd = "None";
        private string toonname;
        private string world;
        private string currDir;
        private string toonDir;

        public static PluginCore Instance;
        static PluginHost host;


        //public WindowsTimer buffWindowsTimer;
        //public WindowsTimer actionWindowsTimer;
        //public WindowsTimer tradeWindowsTimer;

        public List<string> chatCmds = new List<string>();




        XDocument xdocEquipment = new XDocument();
        XDocument xdocSpells = new XDocument();
       List<Int32> spellsList = null;
       List<string> botCmds = null;
//       List<WorldObject> objTrade = new List<WorldObject>();
       List<int> objTradeID = null;
       List<int> salvages = null;
       List<int> mCurrID;
       List<WorldObject> lstTrdObjects;
       List<WorldObject> lstSalvages;
       List<WorldObject> lstSortedSalvage;
       WorldObject oSalvageObj = null;
       WorldObject oTrdObjAccepted = null;
        int nTrdObjAccepted = 0;
       WorldObject oTrdObj = null;
       WorldObject oGemObj = null;
       WorldObject oTinkObj = null;
       private int ntinkobjid = 0;
       private int nsalvageid = 0;
       private int nsalvageCount = 0;

        //Settings variables
       bool bdrinkbeeranyway;
        bool bContinue;
        bool bEnabled;
        bool bArmorTink;
        bool bWeaponTink;
        bool bItemTink;
        bool bMagicItemTink;
        bool bSalvaging;
        bool bAlchemy;
        bool bFletching;
        bool bLockpick;
        bool bCooking;
        bool bTier4Rares;
        bool bBuffsEnabled;
        bool bBuffOnStart;
        bool bUseBeers;
        bool bUseRares;
        bool bUseBuffBot;
        bool bLogOff;
        bool bUseCharge;
        bool bEquip;
        bool bUseWeb;
        bool bRareAllegChan;
        bool bRareTradeChan;
        bool bEnableMail;
        bool bEnterSpamMail;
        bool bEnterSpamRare;
        bool bCalcMajors;
        bool bJourneymanPet;
        bool bArtisanPet;
        bool bMasterPet;
        bool bTinkSucceeded;
        bool bReturnItemCompleted;
        bool bBrill;
        bool bbuffCast;
 
        int buffWand = 0;
        int objSelectedID = 0;
        int numBuffs = 0;
        int nbuffsCast = 0;
        int myUst = 0;
        int myFocusingStone = 0;
        int idleOutfit = 0;
        WorldObject omyFocusingStone;
        WorldObject omyUst;
        WorldObject oIdleOutfit;

        DateTime setupTime = DateTime.MinValue;
        DateTime inventoryTime = DateTime.MinValue;
        DateTime tinkTime;
        DateTime tinkSucceed;
        DateTime timeContinue;
        DateTime returnTime;
        DateTime idTrdObjs;


 

        string Skill = null;


        System.Windows.Forms.Timer CraftbotMasterTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer BuffingTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TradeWindowTimer = new System.Windows.Forms.Timer();



    }

}