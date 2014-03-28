
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
        private string mHoldingRoutine;
        private string sContinueMessage = null;

        private WorldObject osalvage1;
        private WorldObject osalvage2;
        private WorldObject osalvage3;
        private WorldObject osalvage4;
        private WorldObject osalvage5;
        private WorldObject osalvage6;
        private WorldObject osalvage7;
        private WorldObject osalvage8;
        private WorldObject osalvage9;
        private WorldObject osalvage10;

        private WorldObject osalvage;



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
                    int n = lstTrdObjects.Count;

                    for (int i = 0; i < n; i++)
                    {
                        Current = lstTrdObjects[i].Name;
                        CurrentId = lstTrdObjects[i].Id;
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
             Util.WriteToChat("I am in function to initiate Crafting or Tinking");
            try
            {
                msecondarysubRoutine = "";
                bSpellsinUse = false;
                bEnhanced = false;
                checkBuffs();
           }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void Tink()
        {
            msecondarysubRoutine = "Tinking";
             Util.WriteToChat("I am in Tink and there are " + lstTrdObjects.Count.ToString() + " items in the trading list.");
            //if (chatCmd == "Weapon")
            //{
                try
                {
                    if (lstTrdObjects.Count > 1)
                    {

                        int nobj = lstTrdObjects.Count;
                        WorldObject obj;
                        string objClass;
                        int nobjID;
                        lstSalvages = new List<WorldObject>();
                        for (int n = 0; n < nobj; n++)
                        {
                            obj = lstTrdObjects[n];
                            //     Util.WriteToChat("n = " + n + ", " + obj.Name);
                            nobjID = obj.Id;
                            objClass = obj.ObjectClass.ToString();
                            //       if (lstTrdObjects[n].Name.Contains("Salvaged"))
                            if (obj.Name.Contains("Salvage"))
                            {
                                lstSalvages.Add(obj);
                           }
                            else if (obj.ObjectClass.ToString().Contains("Gem")) { oGemObj = obj; }
                            else
                            {
                               ntinkobjid = nobjID;
                               oTinkObj = obj;
 
                            }
                        } // end of for int n
                         lstSortedSalvage = new List<WorldObject>();
                        var sort = from WorldObject in lstSalvages
                                   orderby WorldObject.Values(DoubleValueKey.SalvageWorkmanship) ascending
                                   select WorldObject;
                        lstSortedSalvage.AddRange(sort);
                        nsalvageCount = lstSortedSalvage.Count;

                        DotheTink();
                    } // end of lstTrdObjects.count
                }
                catch (Exception ex) { Util.LogError(ex); }
            
        }




 

        private void DotheTink()
        {
            try
            {
               

 
                 Util.WriteToChat("I am in DotheTink");
                  if (lstSortedSalvage.Count > 0)
                  {
                      //for (int i = 0; i < nsalvageCount; i++)
                      //{
                      tinkTime = DateTime.Now;
                      Util.WriteToChat("I am ready to do the tink");
 
                      oSalvageObj = lstSortedSalvage[0];
                      nsalvageid = oSalvageObj.Id;
                      lstSortedSalvage.Remove(oSalvageObj);
                     CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Tink);

                  }
                  else { returnItem(); }
                    
              


            }
            catch (Exception ex) { Util.LogError(ex); }

        }

        private void RenderFrame_Tink(object sender, EventArgs e)
        {
            try
            {

                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Tink);
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
                else { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Tink); }

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

                    if (bTinkSucceeded)
                    {
                        try
                        {
                            if (lstSortedSalvage.Count > 0)
                            {
                                lstSalvages = new List<WorldObject>();
                                lstSalvages.AddRange(lstSortedSalvage);
                                lstSortedSalvage = new List<WorldObject>();
                                var sort = from WorldObject in lstSalvages
                                           orderby WorldObject.Values(DoubleValueKey.SalvageWorkmanship) ascending
                                           select WorldObject;
                                lstSortedSalvage.AddRange(sort);
                                nsalvageCount = lstSortedSalvage.Count;

                                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_TinkSucceed);
                                bTinkSucceeded = false;
                                Util.WriteToChat("Tink succeeded.");
                                tinkTime = DateTime.Now;
                                DotheTink();
                            }
                            else { returnItem(); }
                        }
                         catch (Exception ex) { Util.LogError(ex); }

                    }
                    else
                    {
                       DotheTink();
                    }
                }
            
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void returnItem()
        {
            returnTime = DateTime.Now;
 
            if (oTinkObj != null)
            {
                msecondarysubRoutine = "returningitem";
                CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_ReturnItem);
                CoreManager.Current.Actions.GiveItem(ntinkobjid, requestorID);
            }
        }

        private void returnSalvage()
        {
            returnTime = DateTime.Now;

            if ( oSalvageObj != null)
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
                         if(msecondarysubRoutine.Contains("item")){ oTinkObj = null; msecondarysubRoutine = "";}
                        else if(msecondarysubRoutine.Contains("salvage")){ oSalvageObj = null; msecondarysubRoutine = "";}
                        Util.WriteToChat("ItemReturned.");
                        bReturnItemCompleted = false;
                        nsalvageCount = lstSortedSalvage.Count;
                        if (nsalvageCount > 0)
                        {
                            for (int i = 0; i < nsalvageCount; i++)
                            {
                                bReturnItemCompleted = false;
                                oSalvageObj = lstSortedSalvage[i];
                                returnSalvage();
                            }
                        }
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





 


 
    }
}

//Code fragments
//  Double TimeElapsed = Convert.ToDouble(DateTime.Now - tinkTime);
//  Util.WriteToChat("Number of seconds: " + TimeElapsed);
//  Util.WriteToChat("DateTimenow: " + DateTime.Now.ToString() + "tinktime: " +  tinkTime.ToString());

//private void TinkWeapon()
//{
//    //  Util.WriteToChat("I am in TinkWeapon");
//    if (chatCmd == "Weapon")
//    {
//        try
//        {
//            if (lstTrdObjects.Count > 1)
//            {

//                int nobj = lstTrdObjects.Count;
//                WorldObject obj;
//                string objClass;
//                int nobjID;
//                salvages = new List<int>();
//                for (int n = 0; n < nobj; n++)
//                {
//                    obj = lstTrdObjects[n];
//               //     Util.WriteToChat("n = " + n + ", " + obj.Name);
//                    nobjID = obj.Id;
//                    objClass = obj.ObjectClass.ToString();
//                    //       if (lstTrdObjects[n].Name.Contains("Salvaged"))
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
//            } // end of lstTrdObjects.count
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
//            if (lstTrdObjects.Count > 1)
//            {

//                int nobj = lstTrdObjects.Count;
//                WorldObject obj;
//                string objClass;
//                int nobjID;
//              //  Util.WriteToChat("nobj " + nobj);
//                List<int> salvages = new List<int>();
//                for (int n = 0; n < nobj; n++)
//                {
//                    obj = lstTrdObjects[n];
//                  //  Util.WriteToChat("n = " + n + ", " + obj.Name);
//                    nobjID = obj.Id;
//                    objClass = obj.ObjectClass.ToString();
//                    //       if (lstTrdObjects[n].Name.Contains("Salvaged"))
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
//            } // end of lstTrdObjects.count
//        }
//        catch (Exception ex) { Util.LogError(ex); }
//    }
//}
