
    using Decal.Adapter;
    using Decal.Adapter.Wrappers;
    using Decal.Filters;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.XPath;
    using System.Linq;
    using System.Xml.Linq;
    using VirindiViewService;
    using MyClasses.MetaViewWrappers;
    using System.Windows.Forms;

namespace Handyman
{

    [WireUpBaseEvents]

    [FriendlyName("Handyman")]
    public partial class PluginCore : PluginBase
    {

        protected override void Startup()
        {
            try
            {
                Globals.Init("Handyman", Host, Core);
                ViewInit();
                Instance = this;
                host = Host;
                InitEvents();
              //  Startup2();
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        public void InitEvents()
        {

            try
            {
                FileService fileservice = (FileService)Core.FileService;
                Core.CharacterFilter.LoginComplete += CharacterFilter_LoginComplete;	
                  

            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        protected override void Shutdown()
        {
            try
            {
               // DisposeOnShutdown();
                MVWireupHelper.WireupEnd(this);
                View.Dispose();
                EndEvents();
                ClearControls();
                ClearVariables();

                if (CraftbotMasterTimer != null)
                {
                    CraftbotMasterTimer.Stop();
                    CraftbotMasterTimer.Tick -= CraftbotMasterTimer_Tick;
                }
                Core.CharacterFilter.LoginComplete -= new EventHandler(CharacterFilter_LoginComplete);
               // ClearRuleRelatedComponents();

            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void ClearControls()
        {

        }

        private void EndEvents()
        {
            chkEnabled.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnabled_Change);
            chkArmorTink.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkArmorTink_Change);
            chkItemTink.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkItemTink_Change);
            chkWeaponTink.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkWeaponTink_Change);
            chkMagicItemTink.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkMagicItemTink_Change);
            chkSalvaging.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSalvaging_Change);
            chkSetUst.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSetUst_Change);
            chkSetFocusingStone.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSetFocusingStone_Change);
            chkAlchemy.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkAlchemy_Change);
            chkCooking.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkCooking_Change);
            chkFletching.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkFletching_Change);
            chkLockpick.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkLockpick_Change);
            chkTier4Rares.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkTier4Rares_Change);

            chkBuffsEnabled.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffsEnabled_Change);
            chkBuffOnStart.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffOnStart_Change);
            chkUseBeers.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseBeers_Change);
            chkUseRares.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseRares_Change);
            chkUseBuffBot.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseBuffBot_Change);

            chkLogOff.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkLogOff_Change);
            chkUseCharge.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseCharge_Change);
            chkEquip.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEquip_Change);
            chkUseWeb.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseWeb_Change);
            chkRareAllegChan.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkRareAllegChan_Change);
            chkRareTradeChan.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkRareTradeChan_Change);
            chkEnableMail.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnableMail_Change);
            chkEnterSpamMail.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnterSpamMail_Change);
            chkEnterSpamRare.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnterSpamRare_Change);

            chkCalcMajors.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkCalcMajors_Change);
            chkJourneymanPet.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkJourneymanPet_Change);
            chkArtisanPet.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkArtisanPet_Change);
            chkMasterPet.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkMasterPet_Change);
            chkSetBuffingWand.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffingWand_Change);

            Decal.Adapter.CoreManager.Current.ItemSelected -= new EventHandler<ItemSelectedEventArgs>(Current_ItemSelected);
            Core.ChatBoxMessage -= new EventHandler<Decal.Adapter.ChatTextInterceptEventArgs>(Core_ChatBoxMessage);
            Core.WorldFilter.EnterTrade -= new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);
            Core.WorldFilter.AcceptTrade -= new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);
            Core.WorldFilter.AddTradeItem -= new EventHandler<AddTradeItemEventArgs>(WorldFilter_AddTradeItem);

            CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);

            if (BuffingTimer != null) { BuffingTimer.Stop(); BuffingTimer = null; buffPending = false; }



        }

        private void ClearVariables()
        {
            traderId = 0;
            tradeeId = 0;
            botGuid = 0;
            requestorID = 0;
            requestorName = null;
            if (actionWindowsTimer != null) { actionWindowsTimer.Stop(); }
            objID = 0;
            mBotInventoryID = null;
            mCurrID = null;
            //ActionPendingQueue = null;
            //ActionsPending = null;
            chatCmd = null;
            trdObj = null;
            TrdObjects = null;
            trdObjID = 0;
            mBotInventoryID = null;
            botInventory = null;

            
  
        }

        [BaseEvent("LoginComplete", "CharacterFilter")]
        private void CharacterFilter_LoginComplete(object sender, EventArgs e)
        {
              Util.WriteToChat("Handyman Plugin now online.");
                    try
                    {
                        setupTime = DateTime.Now;
                        Util.WriteToChat("I am ready to go to setup lists.");
                        Initializepaths();

                        fillSettingsVariables();
                        SetUpXdocs();
                        setUpLists();
                        initStaticEquip();
                        CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_DressBot);

                    }
                    catch (Exception ex) { Util.LogError(ex); }
            }

        private void RenderFrame_DressBot(object sender, EventArgs e)
        {
            
            if (bEnabled && ((DateTime.Now - setupTime).TotalSeconds > 10))
            {
                try
                {
                    CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_DressBot);
                    msecondarysubRoutine = "PrepareforRest";
                    Util.WriteToChat("I am in routine to set up bot outfit on startup.");
                    chatCmd = "";
                    clearBotOutfit();

                    if (bBuffOnStart) { buff(); }

                    //   checkPlugin();
                }
                catch (Exception ex) { Util.LogError(ex); }
            }
        }

        private void checkPlugin()
        {
            try
            {
 
          }
        catch (Exception ex) { Util.LogError(ex); }
         }

        private void Current_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            try
            {
            
                objSelectedID = e.ItemGuid;
                foreach (Decal.Adapter.Wrappers.WorldObject obj in Core.WorldFilter.GetInventory())
                {
                    if (obj.Id == objSelectedID)
                    {
                        ObjectClass objClass = obj.ObjectClass;
                        string objName = obj.Name;
                        Util.WriteToChat("Object selected is of class " + objClass);
                         if (objName.Contains("Ust"))
                        {
                            myUst = obj.Id;
                            Skill = "salvaging";
                            //saveEquip(Skill, myUst);
                        }
                         else if (objName.Contains("Focusing"))
                        {
                            myFocusingStone = obj.Id;
                            Skill = "tinking_crafting";
                           // saveEquip(Skill, myFocusingStone);
                        }
                         else if (objClass.ToString().Contains("Wand"))
                        {
                            Skill = "buffing";

                        }
  
                        break;
                    }
                }


            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void CraftbotMasterTimer_Tick(Object Sender, EventArgs CraftBotMasterTimer_Tick)
        {

        }


        void Core_ChatBoxMessage(object sender, Decal.Adapter.ChatTextInterceptEventArgs e)
        {
            if (bEnabled)
            {
           //     Util.WriteToChat("Bot is enabled");
                try
                {
                    if (e.Text.Contains("You cast"))
                    {
                        if (e.Text.Contains("You cast Incantation of Creature")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Focus")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Willpower")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Life")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Mana")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Item")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Coordination")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Endurance")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Strength")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Armor Tinkering")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Weapon Tinkering")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Magic Item")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Item Tinkering")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Alchemy")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Cooking")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Lockpick")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Fletching")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Arcanum Salvaging")) { nbuffsCast++; }
                        if (e.Text.Contains("You cast Incantation of Arcane Enlightenment")) { nbuffsCast++; }

                        if (e.Text.Contains("You cast Ketnan's Eye")) { bDrankBeer = true; }
                        if (e.Text.Contains("You cast Brighteyes' Favor")) { bDrankBeer = true; }
                        if (e.Text.Contains("You cast Zongo's Fist")) { bDrankBeer = true; }
                        if (e.Text.Contains("You cast Hunter's Hardiness")) { bDrankBeer = true; }
                        if (e.Text.Contains("successfully applies")) { bTinkSucceeded = true; }
                         processChatBuffing();
                    }
                    if (chatCmd == null || chatCmd == "")
                    {
                        if ((e.Text.Contains("tells you")) && ((e.Text.Contains("Salvage")) || (e.Text.Contains("salvage"))))
                        {
                            chatCmd = "Salvage";
                            initiateSalvagingSequence();

                        }
                        if ((e.Text.Contains("tells you")) && ((e.Text.Contains("Weapon") || e.Text.Contains("weapon"))))
                        {
                            try{
                            chatCmd = "Weapon";
                            Util.WriteToChat("chatcmd = " + chatCmd);
                            initiateCraftingTinkingSequence();
                            //Core.Actions.TradeReset();
                            //Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);
                            }
                            catch (Exception ex) { Util.LogError(ex); }


                        }
                        if ((e.Text.Contains("tells you")) && ((e.Text.Contains("Armor") || e.Text.Contains("armor"))))
                        {
                            chatCmd = "Armor";
                            initiateCraftingTinkingSequence();
 
                            //Core.Actions.TradeReset();
                            //Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);

                        }
                        if ((e.Text.Contains("tells you")) && ((e.Text.Contains("Item") || e.Text.Contains("item"))))
                        {
                            chatCmd = "Item";
                            initiateCraftingTinkingSequence();
 
                            //Core.Actions.TradeReset();
                            //Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);

                        }
                        if ((e.Text.Contains("tells you")) && ((e.Text.Contains("Magic") || e.Text.Contains("magic"))))
                        {
                            chatCmd = "MagicItem";
                            initiateCraftingTinkingSequence();
 
                            //Core.Actions.TradeReset();
                            //Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);

                        }
                        if ((e.Text.Contains("tells you")) && ((e.Text.Contains("Dye") || e.Text.Contains("dye"))))
                        {
                            chatCmd = "Dye";
                            initiateCraftingTinkingSequence();
 
                            //Core.Actions.TradeReset();
                            //Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);

                        }
                        if ((e.Text.Contains("tells you")) && ((e.Text.Contains("Lockpick") || e.Text.Contains("lockpick"))))
                        {
                            chatCmd = "Lockpick";
                            initiateCraftingTinkingSequence();
 
                            //Core.Actions.TradeReset();
                            //Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);

                        }
                    }
                  
                }
                catch (Exception ex) { Util.LogError(ex); }

            }

        }

    }
}