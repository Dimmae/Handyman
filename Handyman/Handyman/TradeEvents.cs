
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
using VirindiViewService.Controls;
using MyClasses.MetaViewWrappers;
using System.Windows;
using System.Windows.Forms;
using WindowsTimer = System.Windows.Forms.Timer;


namespace Handyman
{
    public partial class PluginCore
    {
        int traderId = 0;
        int tradeeId = 0;
        string requestorName;
        int requestorID;
        int botGuid = CoreManager.Current.CharacterFilter.Id;
        WorldObject trdObj = null;
        int trdObjID = 0;
        int objID = 0;
       List<int> mBotInventoryID;
        List<WorldObject> botInventory;
        WorldObject TinkObject;
        List<int> mCurrID;
        List<WorldObject> TrdObjects;
        List<WorldObject> Salvages;
        //List<Action> ActionPendingQueue = new List<Action>();
        //List<string> ActionsPending = new List<string>();
        //DateTime ActionBegin;
        bool subIDObjs = false;



        private void WorldFilter_EnterTrade(object sender, EnterTradeEventArgs e)
        {
            try
            {
               //   Util.WriteToChat("Trader ID: " + e.TradeeId);
                
                  requestorID = e.TradeeId;
                  requestorName = Core.WorldFilter[e.TradeeId].Name;
                //  Util.WriteToChat("Trader name: " + Core.WorldFilter[e.TradeeId].Name);
                //   if (((base.Core.WorldFilter[A_1.ItemId].ObjectClass.Equals((ObjectClass)2) || base.Core.WorldFilter[A_1.ItemId].ObjectClass.Equals((ObjectClass)3)) || (base.Core.WorldFilter[A_1.ItemId].ObjectClass.Equals((ObjectClass)4) || base.Core.WorldFilter[A_1.ItemId].ObjectClass.Equals((ObjectClass)1))) || (base.Core.WorldFilter[A_1.ItemId].ObjectClass.Equals((ObjectClass)9) || base.Core.WorldFilter[A_1.ItemId].ObjectClass.Equals((ObjectClass)0x1f)))
                // from Magnus code
                // Thx Magnus for your code which really helped


                // Magnus writes, This is a little trick.
                // When someone initiates the trade, they will have both the trader and tradee ID's be accurate.
                // When someone initiates a trade with you, you will have them as both ID's.
                // This will prevent our mule from auto-muling back to us.
                //if (e.TradeeId == botGuid)   //??? Must play with this
                //     traderId = e.TraderId;
                //else if (e.TraderId == botGuid)
                //    traderId = e.TradeeId;

                GetInventoryCraftbot();
                if (requestorID == botGuid) { Util.WriteToChat("Traderid = Krafie"); }
                Util.WriteToChat("Trade window started and trader is = " + requestorName);
                Util.WriteToChat("Inventory currently is " + botInventory.Count);
                Util.WriteToChat("chatCmd: " + chatCmd);
                 if ((chatCmd == null) || (chatCmd == ""))
                {
                    WriteToTrader("You must give me a command such as salvage, weapon, armor, before I can continue.");
                    Util.WriteToChat("I should have just given a tell to tradee");
                    chatCmd = "";
                }
                else
                {
                    if (mroutine.Contains("readyfortrade"))
                    {
                        Core.WorldFilter.AcceptTrade += new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);
                        Core.WorldFilter.EnterTrade -= new EventHandler<EnterTradeEventArgs>(WorldFilter_EnterTrade);
                    }
                }

            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void GetInventoryCraftbot()
        {
            try
            {
                //loop for checking each obj in the current inventory
                msubRoutine = "GetInventory";
                //Need to find the current inventory objects and create a list of their ids mCurrID
                botInventory = new List<WorldObject>();
                foreach (Decal.Adapter.Wrappers.WorldObject obj in Core.WorldFilter.GetInventory())
                {
                    try
                    {
                        objID = obj.Id;
                        //     string sobjID = objID.ToString();
                        //   mBotInventoryID.Add(objID);
                        botInventory.Add(obj);
                    }

                    catch (Exception ex) { Util.LogError(ex); }


                } // endof foreach world object
                msubRoutine = "";
                Util.WriteToChat("Number of items in inventory: " + botInventory.Count);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        DateTime IdObjs = DateTime.MinValue;


 
        private void WorldFilter_AcceptTrade(object sender, AcceptTradeEventArgs e)
        {

            try
            {
                Core.WorldFilter.AcceptTrade -= new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);


                Core.Actions.TradeAccept();

                IdObjs = DateTime.Now;
                Core.RenderFrame += new EventHandler<EventArgs>(SetupForId);
            }
            catch (Exception ex) { Util.LogError(ex); }

        }

        private void SetupForId(object sender, EventArgs e)
        {

            if ((DateTime.Now - IdObjs).TotalSeconds > 1)
            {
                Core.RenderFrame -= SetupForId;
                Util.WriteToChat("I have finished checking for new item. Count of new items is " + TrdObjects.Count);
                moveToBotChores();

            }
            else
            {
                IDTradeObjects();
            }
 

        }

        private void moveToBotChores()
        {
            switch (chatCmd)
            {
                case "Salvage":
                    Salvage();
                    break;
                case "Weapon":
                    TinkWeapon();
                    break;
                case "Armor":
                    TinkArmor();
                    break;

            }

        }

        private void IDTradeObjects()
        {
            TrdObjects = new List<WorldObject>();
            Salvages = new List<WorldObject>();

             try
            {
                foreach (Decal.Adapter.Wrappers.WorldObject obj in Core.WorldFilter.GetInventory())
                {
                    ////Need to find the current inventory objects and create a list of their ids mCurrID
                    if (!botInventory.Contains(obj))
                    {
                        TrdObjects.Add(obj);
                        if(obj.Name.Contains("Salvage")){Salvages.Add(obj);}
                        else{TinkObject = obj;}

                    }

                } // endof foreach world object

            }
            catch (Exception ex) { Util.LogError(ex); }

        }


        private void WorldFilter_AddTradeItem(object sender, AddTradeItemEventArgs e)
        {

        }




 
    }
}