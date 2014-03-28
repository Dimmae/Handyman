
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
        int numTrdObjs = 0;

        int trdObjID = 0;
        int objID = 0;
        List<int> nBotInventoryID;
        List<WorldObject> obotInventory;
        List<WorldObject> lstWaitingforID;
        //List<Action> ActionPendingQueue = new List<Action>();
        //List<string> ActionsPending = new List<string>();
        //DateTime ActionBegin;
        bool subIDObjs = false;
        bool bFinishedPreparingforID = false;
        bool bInventoryFinished = false;
        DateTime TimeIdObj;

        private void WorldFilter_EnterTrade(object sender, EnterTradeEventArgs e)
        {
            try
            {
               //   Util.WriteToChat("Trader ID: " + e.TradeeId);
                
                  requestorID = e.TradeeId;
                  requestorName = Core.WorldFilter[e.TradeeId].Name;
                
                  Util.WriteToChat("Trader name: " + Core.WorldFilter[e.TradeeId].Name);
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
              //   botInventory = new List<WorldObject>();
              //   msubRoutine = "GetInventory";

                 Util.WriteToChat("Trade window started and trader is = " + requestorName);
                 Util.WriteToChat("chatCmd: " + chatCmd);
                
                if (chatCmd.Contains("None")) 
                {
                    botMess = "You must give me a command such as salvage, weapon, armor, before I can continue.";
                    WriteToTrader(botMess);
                    return;
                     
                }
                else
                {
                    Util.WriteToChat("I am in the else of chatcmd == null");
                     lstWaitingforID = new List<WorldObject>();
                lstTrdObjects = new List<WorldObject>();
                lstSalvages = new List<WorldObject>();
                numTrdObjs = 0;
                        Core.WorldFilter.EnterTrade -= new EventHandler<EnterTradeEventArgs>(WorldFilter_EnterTrade);
                    
                        Core.WorldFilter.AcceptTrade += new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);

                    }
                 //}
            }
              catch (Exception ex) { Util.LogError(ex); }
        }

         private void WorldFilter_AcceptTrade(object sender, AcceptTradeEventArgs e)
        {
           // Util.WriteToChat("I am in accepttrade and inventory is finished " + bInventoryFinished.ToString());

            try
            {
                TimeIdObj = DateTime.Now;
                    
                    Util.WriteToChat("I am in function to Accept trade");
                    Core.Actions.TradeAccept();
                    Core.WorldFilter.AcceptTrade -= new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);
                    Core.WorldFilter.AddTradeItem += new EventHandler<AddTradeItemEventArgs>(WorldFilter_AddTradeItem);


            }
            catch (Exception ex) { Util.LogError(ex); }

        }

        private void WorldFilter_AddTradeItem(object sender, AddTradeItemEventArgs e)
        {
            try{
                TimeIdObj = DateTime.Now;
            //    lstWaitingforID = new List<WorldObject>();
            oTrdObjAccepted = (Core.WorldFilter[e.ItemId]);
            lstWaitingforID.Add(oTrdObjAccepted);
            Core.Actions.RequestId(oTrdObjAccepted.Id);
            numTrdObjs ++;
            Util.WriteToChat("I have just added trade items and there are " + numTrdObjs.ToString() + " items being id'd");
            CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_DotheIDing);
            }
            catch (Exception ex) { Util.LogError(ex); }

        }
        
        private void RenderFrame_DotheIDing(object sender, EventArgs e)
        {
            try{
                if ((DateTime.Now - TimeIdObj).TotalSeconds > 5)
                {
                    Core.WorldFilter.AddTradeItem -= new EventHandler<AddTradeItemEventArgs>(WorldFilter_AddTradeItem);

                    CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_DotheIDing);
                    Util.WriteToChat("I am in function to move on to receiving id's; there are " + numTrdObjs.ToString() + " items waiting to be id'd");
                    Util.WriteToChat("The count in lstWaitingforid is " + lstWaitingforID.Count.ToString());
                    Core.WorldFilter.ChangeObject += TradeObject_ObjectIDReceived;
                }


            }
            catch (Exception ex) { Util.LogError(ex); }
  

        }


 
 
        private void TradeObject_ObjectIDReceived(object sender, ChangeObjectEventArgs e)
        {
            try
            {
                Util.WriteToChat("I am in TradeObject_ObjectIDReceived and msecondarysubroutine = " + msecondarysubRoutine);
                if (!msecondarysubRoutine.Contains("Tinking") &&  !msecondarysubRoutine.Contains("Crafting") && !msecondarysubRoutine.Contains("Salvage") && lstWaitingforID !=null && lstWaitingforID.Count > 0)
                {
                    Core.WorldFilter.ChangeObject -= TradeObject_ObjectIDReceived;

                    int tempnum = lstWaitingforID.Count;
                    WorldObject obj;
                    Util.WriteToChat("I am in function to report an id is received");
                    // if ((DateTime.Now - TimeIdObj).TotalSeconds > 10)

                    Util.WriteToChat("I am in objectidreceived and number of items in lstWaitingforId:  " + lstWaitingforID.Count.ToString());
                    if (tempnum > 0)
                    {
                        for (int x = 0; x < tempnum; x++)
                        {
                            // if (obj.Id == e.Changed.Id)
                            if (lstWaitingforID[x].HasIdData)
                            {
                                obj = lstWaitingforID[x];
                                lstTrdObjects.Add(obj);
                                lstWaitingforID.Remove(obj);

                                Util.WriteToChat(lstTrdObjects.Count + " items that have been id'd.");
                                Util.WriteToChat(lstWaitingforID.Count + " items that are waiting to be id'd.");

                                if (lstWaitingforID.Count == 0)
                                {
                                    lstWaitingforID.Clear();
                                    lstWaitingforID = null;
                                    Core.WorldFilter.ChangeObject -= TradeObject_ObjectIDReceived;
                                    Util.WriteToChat("Moving on to BotChores");
                                    unsubscribeTradeEvents();

                                }
                              //  else { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_DotheIDing); }

                            }
                        }
                    }
                    else
                    {
                        Util.WriteToChat(lstWaitingforID.Count.ToString() + " items waiting to be id'd");
                        Core.WorldFilter.ChangeObject -= TradeObject_ObjectIDReceived;
                       // Core.WorldFilter.EndTrade += new EventHandler<EndTradeEventArgs>(WorldFilter_EndTrade);
                        unsubscribeTradeEvents();

                    }
                    //TimeIdObj = DateTime.Now;
                    //CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_DotheIDing);

                }
            }
            catch (Exception ex) { Util.LogError(ex); }
 
    }

        private void moveToBotChores()
        {
            Util.WriteToChat("I am in botchores");
            switch (chatCmd)
            {
                case "Salvage":
                    Salvage();
                    break;
                case "Weapon":
                    Tink();
                    break;
                case "Armor":
                    Tink();
                    break;

            }

        }


         private void unsubscribeTradeEvents()
        {
           Core.WorldFilter.EnterTrade -= new EventHandler<EnterTradeEventArgs>(WorldFilter_EnterTrade);

            Core.WorldFilter.AcceptTrade -= new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);
            Core.WorldFilter.AddTradeItem -= new EventHandler<AddTradeItemEventArgs>(WorldFilter_AddTradeItem);
            CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_DotheIDing);
            Core.WorldFilter.ChangeObject -= TradeObject_ObjectIDReceived;
            moveToBotChores();




 
        }

    }
    }



//private void RenderFrame_WaitforInventory(object sender, EventArgs e)
//{
//     Util.WriteToChat("I am in wait for inventory and binventoryfinished = " + bInventoryFinished.ToString());
//    try
//    {
//      if (bInventoryFinished &&   mroutine.Contains("readyfortrade"))
//            {
//                Core.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_WaitforInventory);
//                Util.WriteToChat("I am in Wait for inventory and inventory has finished. I will now accept trade.");
//                Core.WorldFilter.AcceptTrade += new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade);
//            }
//        }


//    catch (Exception ex) { Util.LogError(ex); }
//}


//    DateTime PrepareforId;

//private void IDTradeObjects()
// {

//     if (!bFinishedPreparingforID) { Core.RenderFrame += new EventHandler<EventArgs>(RenderFrame_PrepareforID); }
//}

//private void RenderFrame_PrepareforID(object sender, EventArgs e)
//{
//     try
//    {

//       Core.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_PrepareforID);

//      //  Util.WriteToChat(obotInventory.Count.ToString() + " items in inventory prior to trade.");
//        foreach (Decal.Adapter.Wrappers.WorldObject obj in Core.WorldFilter.GetInventory())
//        {
//             ////Need to find the current inventory objects and create a list of their ids mCurrID
//            if (!obotInventory.Contains(obj))
//            {
//               Globals.Host.Actions.RequestId(obj.Id);
//               lstWaitingforID.Add(obj);
//               // lstTrdObjects.Add(obj);


//            }
//            Util.WriteToChat("I have just left function to sort inventory into objects waiting ID.  lstwaitngforid = " + lstWaitingforID.Count.ToString());

//        } // endof foreach world object
//      //  bFinishedPreparingforID = true;
//      //  Util.WriteToChat(lstWaitingforID.Count + "items to be id'd");
//     //   if (bFinishedPreparingforID) { Core.WorldFilter.ChangeObject += TradeObject_ObjectIDReceived; }

//    }
//    catch (Exception ex) { Util.LogError(ex); }

//}
//private void SetupForId()
//{

//    if (bInventoryFinished)
//    {
//      //  Core.RenderFrame -= SetupForId;
//        lstWaitingforID = new List<WorldObject>();
//        bFinishedPreparingforID = false;
//        IDTradeObjects();


//        //Util.WriteToChat("I have finished checking for new item. Count of new items is " + lstTrdObjects.Count);
//        //moveToBotChores();

//    }

//}
