
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
            //chkSetUst.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSetUst_Change);
            //chkSetFocusingStone.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSetFocusingStone_Change);
            chkAlchemy.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkAlchemy_Change);
            chkCooking.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkCooking_Change);
            chkFletching.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkFletching_Change);
            chkLockpick.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkLockpick_Change);
          //  chkTier4Rares.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkTier4Rares_Change);

            chkBuffsEnabled.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffsEnabled_Change);
            chkBuffOnStart.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffOnStart_Change);
            chkUseBeers.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseBeers_Change);
            chkUseRares.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseRares_Change);
            chkUseBuffBot.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseBuffBot_Change);

            chkLogOff.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkLogOff_Change);
            //chkUseCharge.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseCharge_Change);
            //chkEquip.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEquip_Change);
            //chkUseWeb.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseWeb_Change);
            chkRareAllegChan.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkRareAllegChan_Change);
            chkRareTradeChan.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkRareTradeChan_Change);
            chkEnableMail.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnableMail_Change);
            chkEnterSpamMail.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnterSpamMail_Change);
            chkEnterSpamRare.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnterSpamRare_Change);

            //chkCalcMajors.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkCalcMajors_Change);
            //chkJourneymanPet.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkJourneymanPet_Change);
            //chkArtisanPet.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkArtisanPet_Change);
            //chkMasterPet.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkMasterPet_Change);
            chkSetBuffingWand.Change -= new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffingWand_Change);

            Decal.Adapter.CoreManager.Current.ItemSelected -= new EventHandler<ItemSelectedEventArgs>(Current_ItemSelected);
            Core.ChatBoxMessage -= new EventHandler<Decal.Adapter.ChatTextInterceptEventArgs>(Core_ChatBoxMessage);
            Core.WorldFilter.EnterTrade -= new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);
            Core.WorldFilter.AcceptTrade -= new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);
            Core.WorldFilter.AddTradeItem -= new EventHandler<AddTradeItemEventArgs>(WorldFilter_AddTradeItem);

            CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
            Core.EchoFilter.ServerDispatch -= HandymanServerDispatch;
          //  WindowMessage += new EventHandler<WindowMessageEventArgs>(FilterCore_WindowMessage);


            MasterTimer.Stop();
            MasterTimer = null;
           // MasterTimer.Dispose();

            if (BuffingTimer != null) { BuffingTimer.Stop(); BuffingTimer = null; buffPending = false; }



        }

        private void ClearVariables()
        {
            traderId = 0;
            tradeeId = 0;
            botGuid = 0;
            requestorID = 0;
            requestorName = null;
            //if (actionWindowsTimer != null) { actionWindowsTimer.Stop(); }
            objID = 0;
            nBotInventoryID = null;
            mCurrID = null;
            //ActionPendingQueue = null;
            //ActionsPending = null;
            chatCmd = null;
            oTrdObj = null;
            lstTrdObjects = null;
            trdObjID = 0;
            obotInventory = null;

            
  
        }

        [BaseEvent("LoginComplete", "CharacterFilter")]
        private void CharacterFilter_LoginComplete(object sender, EventArgs e)
        {
              Util.WriteToChat("Handyman Plugin now online.");
                    try
                    {
                        MasterTimer.Interval = 1000;
                        MasterTimer.Start();
                        setupTime = DateTime.Now;
                      //  Util.WriteToChat("I am ready to go to setup lists.");
                        Initializepaths();

                        fillSettingsVariables();
                        SetUpXdocs();
                        Util.WriteToChat("I have just set up xdocs.");
                        setUpLists();
                        GetInventory();
                        CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_EquipLists);
 
                    }
                    catch (Exception ex) { Util.LogError(ex); }
            }

        private void RenderFrame_EquipLists(object sender, EventArgs e)
        {
            try{
            if ((DateTime.Now - setupTime).TotalSeconds > 1)
            {
                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_EquipLists);
                initStaticEquip();
                CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_DressBot);
                Core.EchoFilter.ServerDispatch += HandymanServerDispatch;

            }

            }
            catch (Exception ex) { Util.LogError(ex); }
 
        }
 

        private void RenderFrame_DressBot(object sender, EventArgs e)
        {
            
            if (bEnabled && ((DateTime.Now - setupTime).TotalSeconds > 2))
            {
                try
                {
                    CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_DressBot);
                    msecondarysubRoutine = "PrepareforRest";
                   // Util.WriteToChat("I am in routine to set up bot outfit on startup.");
                    chatCmd = "";
                    clearBotOutfit();

                    if (bBuffOnStart) { msecondarysubRoutine = "buffing"; buff(); }

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

 
    }
}