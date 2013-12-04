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
        string HandymanDir = null;
        DirectoryInfo dinfo = null;

        string settingsFilename;
        string equipmentFilename;
        string spellsFilename;

        string msubRoutine;
        string msecondarysubRoutine;
        string mroutine;
        string msub;

        // private System.Windows.Forms.WindowsTimer MasterWindowsTimer = new System.Windows.Forms.WindowsTimer();
        public static string Log;
        public string chatCmd = "";
        private string toonname;
        private string world;
        private string currDir;
        private string toonDir;

        public static PluginCore Instance;
        static PluginHost host;


        public WindowsTimer buffWindowsTimer;
        public WindowsTimer actionWindowsTimer;
        public WindowsTimer tradeWindowsTimer;

        public List<string> chatCmds = new List<string>();




        XDocument xdocEquipment = new XDocument();
        XDocument xdocSpells = new XDocument();
       List<Int32> spellsList = null;
       List<string> botCmds = null;
       List<WorldObject> objTrade = new List<WorldObject>();
       List<int> objTradeID = null;
        //Settings variables
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
 
        int buffWand = 0;
        int objSelectedID = 0;
        int numBuffs = 0;
        int nbuffsCast = 0;
        int myUst = 0;
        int myFocusingStone = 0;
        int idleOutfit = 0;
        WorldObject oIdleOutfit;

        DateTime setupTime = DateTime.MinValue;

 

        string Skill = null;

        System.Windows.Forms.Timer CraftbotMasterTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer BuffingTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TradeWindowTimer = new System.Windows.Forms.Timer();



    }

}