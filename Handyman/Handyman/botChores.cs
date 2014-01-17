
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
        private int ntinkobjid = 0;
        private int nsalvageid = 0;
        private WorldObject osalvageobj;
        private WorldObject otinkobj;
        private string mHoldingRoutine;
        DateTime tinkTime;
        DateTime tinkSucceed;
        DateTime timeContinue;
        DateTime returnTime;




        private void initiateSalvagingSequence()
        {
            msub = "equip";
            EquipOutfit();
            //Core.Actions.TradeReset();

            //Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);

        }


        private void Salvage()
        {
            try
            {
                if (chatCmd == "Salvage")
                {
                    //  Util.WriteToChat("I am in Salvage function");
                    xdocEquipment = new XDocument();
                    xdocEquipment = XDocument.Load(equipmentFilename);

                    string Current;
                    int CurrentId;
                    int n = TrdObjects.Count;

                    for (int i = 0; i < n; i++)
                    {
                        Current = TrdObjects[i].Name;
                        CurrentId = TrdObjects[i].Id;
                       // Util.WriteToChat("Item to be salvaged " + Current);
                        Core.Actions.UseItem(myUst, 0);
                        Core.Actions.SalvagePanelAdd(CurrentId);
                        //   Core.Actions.SalvagePanelSalvage();

                    }
                    msub = "remove";
                    EquipOutfit();
                    chatCmd = "";
                }
            }
            catch (Exception ex) { Util.LogError(ex); }

        }

        private void initiateCraftingTinkingSequence()
        {
            // Util.WriteToChat("I am in function to initiate Crafting or Tinking");
            try
            {
                msecondarysubRoutine = "";
                bSpellsinUse = true;
                checkBuffs();
             //   Util.WriteToChat("bspellsinuse: " + bSpellsinUse.ToString());
                if (!bSpellsinUse) { msecondarysubRoutine = "buffing"; buff(); }
                else { msecondarysubRoutine = ""; Util.WriteToChat("msecondarysubroutine: " + msecondarysubRoutine); checkFocus(); }

            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void Tink()
        {
            //  Util.WriteToChat("I am in TinkWeapon");
            //if (chatCmd == "Weapon")
            //{
                try
                {
                    if (TrdObjects.Count > 1)
                    {

                        int nobj = TrdObjects.Count;
                        WorldObject obj;
                        string objClass;
                        int nobjID;
                        salvages = new List<int>();
                        for (int n = 0; n < nobj; n++)
                        {
                            obj = TrdObjects[n];
                            //     Util.WriteToChat("n = " + n + ", " + obj.Name);
                            nobjID = obj.Id;
                            objClass = obj.ObjectClass.ToString();
                            //       if (TrdObjects[n].Name.Contains("Salvaged"))
                            if (obj.Name.Contains("Salvage"))
                            {
                                nsalvageid = nobjID;
                                salvages.Add(nobjID);
                                osalvageobj = obj;
                                //    Util.WriteToChat("Added " + obj.Name + " to salvages");
                            }
                            else 
                            {
                               ntinkobjid = nobjID;
                               otinkobj = obj;
 
                            }
                        } // end of for int n

                        //   Util.WriteToChat("Objtotink " + otinkobj.ToString() + ", salvage " + salvages[0].ToString());
                        DotheTink();
                    } // end of trdobjects.count
                }
                catch (Exception ex) { Util.LogError(ex); }
            
        }




        //private void TinkWeapon()
        //{
        //    //  Util.WriteToChat("I am in TinkWeapon");
        //    if (chatCmd == "Weapon")
        //    {
        //        try
        //        {
        //            if (TrdObjects.Count > 1)
        //            {

        //                int nobj = TrdObjects.Count;
        //                WorldObject obj;
        //                string objClass;
        //                int nobjID;
        //                salvages = new List<int>();
        //                for (int n = 0; n < nobj; n++)
        //                {
        //                    obj = TrdObjects[n];
        //               //     Util.WriteToChat("n = " + n + ", " + obj.Name);
        //                    nobjID = obj.Id;
        //                    objClass = obj.ObjectClass.ToString();
        //                    //       if (TrdObjects[n].Name.Contains("Salvaged"))
        //                    if (obj.Name.Contains("Salvage"))
        //                    {
        //                        nsalvageid = nobjID;
        //                        salvages.Add(nobjID);
        //                        osalvageobj = obj;
        //                    //    Util.WriteToChat("Added " + obj.Name + " to salvages");
        //                    }
        //                    else if (objClass.Contains("Melee") || objClass.Contains("Missile"))
        //                    {

        //                    }
        //                } // end of for int n

        //             //   Util.WriteToChat("Objtotink " + otinkobj.ToString() + ", salvage " + salvages[0].ToString());
        //                DotheTink();
        //            } // end of trdobjects.count
        //        }
        //        catch (Exception ex) { Util.LogError(ex); }
        //    }
        //}

        //private void TinkArmor()
        //{
        //    //  Util.WriteToChat("I am in TinkArmor");
        //    if (chatCmd == "Armor")
        //    {
        //        try
        //        {
        //            if (TrdObjects.Count > 1)
        //            {

        //                int nobj = TrdObjects.Count;
        //                WorldObject obj;
        //                string objClass;
        //                int nobjID;
        //              //  Util.WriteToChat("nobj " + nobj);
        //                List<int> salvages = new List<int>();
        //                for (int n = 0; n < nobj; n++)
        //                {
        //                    obj = TrdObjects[n];
        //                  //  Util.WriteToChat("n = " + n + ", " + obj.Name);
        //                    nobjID = obj.Id;
        //                    objClass = obj.ObjectClass.ToString();
        //                    //       if (TrdObjects[n].Name.Contains("Salvaged"))
        //                    if (obj.Name.Contains("Salvage"))
        //                    {
        //                        nsalvageid = nobjID;
        //                        salvages.Add(nobjID);
        //                        osalvageobj = obj;
        //                      //  Util.WriteToChat("Added " + obj.Name + " to salvages");
        //                    }
        //                    else if (objClass.Contains("Armor") || objClass.Contains("Clothing"))
        //                    {
        //                        ntinkobjid = nobjID;
        //                        otinkobj = obj;
        //                     //   Util.WriteToChat("otinkobj " + otinkobj.ToString());

        //                    }
        //                } // end of for int n

        //             //   Util.WriteToChat("Objtotink " + otinkobj.ToString() + ", salvage " + salvages[0].ToString());
        //                tinkTime = DateTime.Now;
        //                DotheTink();
        //            } // end of trdobjects.count
        //        }
        //        catch (Exception ex) { Util.LogError(ex); }
        //    }
        //}

        private void DotheTink()
        {
            try
            {
 
                  Util.WriteToChat("I am in DotheTink");
                   CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Tink);


            }
            catch (Exception ex) { Util.LogError(ex); }

        }

        private void RenderFrame_Tink(object sender, EventArgs e)
        {
            try
            {

                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Tink);
                //  Double TimeElapsed = Convert.ToDouble(DateTime.Now - tinkTime);
                //  Util.WriteToChat("Number of seconds: " + TimeElapsed);
                //  Util.WriteToChat("DateTimenow: " + DateTime.Now.ToString() + "tinktime: " +  tinkTime.ToString());
                if ((DateTime.Now - tinkTime).TotalSeconds > 5)
                {
                    try
                    {
                        //     Util.WriteToChat("I am in function to tink");
                        //  Util.WriteToChat(ntinkobj.ToString() + "; " + nsalvage.ToString());
                        Core.Actions.ApplyItem(nsalvageid, ntinkobjid);
                        didTinkSucceed();
                    }
                    catch (Exception ex) { Util.LogError(ex); }


                }
                //   else { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Tink); }
                else { DotheTink(); }

            }
            catch (Exception ex) { Util.LogError(ex); }

        }
        private void didTinkSucceed()
        {
            tinkSucceed = DateTime.Now;
            CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_TinkSucceed);

        }


        private void RenderFrame_TinkSucceed(object sender, EventArgs e)
        {
            try
            {
                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_TinkSucceed);
                //     Util.WriteToChat("I am in function to check if tink succeeded");
                if ((DateTime.Now - tinkSucceed).TotalSeconds > 15)
                {

                    if (bTinkSucceeded) 
                    { 
                        CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Tink); 
                        Util.WriteToChat("Tink succeeded.");
                        returnItem(); 
                    }
                    //   else { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Tink); }
                    else { DotheTink(); }
                }
                else
                { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_TinkSucceed); }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void returnItem()
        {
            returnTime = DateTime.Now;
 
            if (otinkobj != null)
            {
                msecondarysubRoutine = "returningitem";
                CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_ReturnItem);
                CoreManager.Current.Actions.GiveItem(ntinkobjid, requestorID);
            }
        }

        private void returnSalvage()
        {
            returnTime = DateTime.Now;

            if ( osalvageobj != null)
            {
                msecondarysubRoutine = "returningsalvage";
                CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_ReturnItem);
                CoreManager.Current.Actions.GiveItem(nsalvageid, requestorID);
            }
        }

        private void RenderFrame_ReturnItem(object sender, EventArgs e)
        {
            try
            {
                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_ReturnItem);
                //     Util.WriteToChat("I am in function to check if tink succeeded");
                if (bReturnItemCompleted || ((DateTime.Now - returnTime).TotalMilliseconds>300))
                {

                    if (bReturnItemCompleted)
                    {

                        CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_ReturnItem);
                         if(msecondarysubRoutine.Contains("item")){ otinkobj = null; msecondarysubRoutine = "";}
                        else if(msecondarysubRoutine.Contains("salvage")){ osalvageobj = null; msecondarysubRoutine = "";}
                        Util.WriteToChat("ItemReturned.");
                        bReturnItemCompleted = false;
                        
                        if (osalvageobj != null) {returnSalvage(); }
                        else
                        {
                            mroutine = "readyfortrade";
                            Util.WriteToChat("I am turning on trade again.");
                            Core.WorldFilter.AcceptTrade += new EventHandler<AcceptTradeEventArgs>(WorldFilter_AcceptTrade); 
                        }
                        
                    }
                    else { returnItem(); }
                }
                else
                { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_ReturnItem); }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }





        private void checkFocus()
        {
            try
            {
                //  Util.WriteToChat("I am in needFocus");
                checkEnhancements();
              //  Util.WriteToChat("ketmaninuse: " + bKetnaninUse.ToString());
                if (!bKetnaninUse)
                {
                    //  Util.WriteToChat("I am in !bketmaninuse");
                    msecondarysubRoutine = "GetFocus";
                    mroutine = "drinkingbeers";
                    string name = "Spit Ale";
                    drinkBeers(name);

                }
                else { checkBeers(); }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }



        private void checkBeers()
        {
            try
            {
                //  Util.WriteToChat("I am in checkBeers and chatcmd: " + chatCmd);
                checkEnhancements();
                mroutine = "drinkingbeers";

                switch (chatCmd)
                {
                    case "Weapon":
                        if (!bZongoinUse)
                        {
                            msecondarysubRoutine = "GeneralBeers";
                            string name = "Zongo";
                           // Util.WriteToChat("I am boing to drink Zongo's");
                            drinkBeers(name);

                        }
                        break;
                    case "Armor":
                        if (!bHunterinUse)
                        {
                            msecondarysubRoutine = "GeneralBeers";
                            string name = "Hunter";
                           // Util.WriteToChat("I am gboing to drink Hunter's");
                            drinkBeers(name);

                        }
                        break;

                    case "Item":
                        if (!bBrighteyesinUse)
                        {
                            msecondarysubRoutine = "GeneralBeers";
                            string name = "Amber Ape";
                          //  Util.WriteToChat("I am boing to drink AmberApe");
                            drinkBeers(name);


                        }
                        break;

                    default:
                        return;
                        break;
                }

                mroutine = "";
                msecondarysubRoutine = "EquiptheBot";
                msub = "remove";
                clearBotOutfit();




            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void drinkBeers(string beername)
        {
            try
            {
                try
                {
                    // GetInventoryCraftbot()
                   // Util.WriteToChat("Number of items: " + botInventory.Count());
                }

                catch (Exception ex) { Util.LogError(ex); }
                try
                {
                    foreach (WorldObject obj in botInventory)
                    {
                        // Util.WriteToChat(obj.Name);
                        if (obj.Name.Contains(beername))
                        {
                            beer = obj.Id;
                           // Util.WriteToChat(beername);
                            break;
                        }
                    }
                }
                catch (Exception ex) { Util.LogError(ex); }

                //  Util.WriteToChat("Beer id: " + beer);
                if (beer < 0)
                {
                    bDrankBeer = false;
                   // Util.WriteToChat("I am on my way to useenhancements");
                    UseEnhance = DateTime.Now;
                    useEnhancement();
                }
                else
                {
                    
                  //  Util.WriteToChat("I am in drinkbeers and beername = " + beername + "; msecondarysubroutine = " + msecondarysubRoutine);
                    mHoldingRoutine = msecondarysubRoutine;
                    timeContinue = DateTime.Now;
                    CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_RequestContinue);
                }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void RenderFrame_RequestContinue(object sender, EventArgs e)
        {
            if ((DateTime.Now - timeContinue).TotalSeconds > 10)
            {
                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_RequestContinue);

                Globals.Host.Actions.InvokeChatParser("/r " + ", " + "I am out of beers.  If you wish me to continue anyway, please tell me, 'continue'.");
                timeContinue = DateTime.Now;
                CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Continue);

            }
        }



        private void RenderFrame_Continue(object sender, EventArgs e)
        {
            try
            {
                if (bContinue || (DateTime.Now - timeContinue).TotalSeconds > 30)
                {
                    CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Continue);
                   // Util.WriteToChat("bContinue = " + bContinue + "; and mholdingroutine = " + mHoldingRoutine);
                    if (bContinue && mHoldingRoutine.Contains("GetFocus"))
                    {
                        checkBeers();
                    }
                    else if (bContinue && mHoldingRoutine.Contains("GeneralBeers"))
                    {
                        mroutine = "equippingbot";
                        msecondarysubRoutine = "EquiptheBot";
                        msub = "remove";
                        clearBotOutfit();
                    }
                    else
                    {
                        Globals.Host.Actions.InvokeChatParser("/r " + ", " + "I am discontinuing this session.");
                        chatCmd = "";
                        CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_DressBot);
                    }
                }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }
    }
}
