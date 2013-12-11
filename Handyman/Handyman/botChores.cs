
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
        private int ntinkobj = 0;
        private int nsalvage = 0;
        DateTime tinkTime;

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
                    Util.WriteToChat("I am in Salvage function");
                    xdocEquipment = new XDocument();
                    xdocEquipment = XDocument.Load(equipmentFilename);

                    string Current;
                    int CurrentId;
                    int n = TrdObjects.Count;

                    for (int i = 0; i < n; i++)
                    {
                        Current = TrdObjects[i].Name;
                        CurrentId = TrdObjects[i].Id;
                        Util.WriteToChat("Item to be salvaged " + Current);
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
            try{
                msecondarysubRoutine = "";
                bSpellsinUse = false;
                checkBuffs();
                Util.WriteToChat("bspellsinuse: " + bSpellsinUse.ToString());
                if (!bSpellsinUse) { msecondarysubRoutine = "buffing"; buff(); }
                else { msecondarysubRoutine = ""; Util.WriteToChat("msecondarysubroutine: " + msecondarysubRoutine); checkFocus(); }
 
            }
            catch (Exception ex) { Util.LogError(ex); }


        }


         private void TinkWeapon()
        {
            Util.WriteToChat("I am in TinkWeapon");
            if (chatCmd == "Weapon")
            {
                try
                {
                    if (TrdObjects.Count > 1)
                    {

                        int nobj = TrdObjects.Count;
                        WorldObject obj;
                        string objClass;
                        int nobjID;
                         Util.WriteToChat("nobj " + nobj);
                        List<int> salvages = new List<int>();
                        for (int n = 0; n < nobj; n++)
                        {
                            obj = TrdObjects[n];
                            Util.WriteToChat("n = " + n + ", " + obj.Name);
                            nobjID = obj.Id;
                            objClass = obj.ObjectClass.ToString();
                     //       if (TrdObjects[n].Name.Contains("Salvaged"))
                            if(obj.Name.Contains("Salvage"))
                            {
                                nsalvage = nobjID;
                                salvages.Add(nobjID);
                                Util.WriteToChat("Added " + obj.Name + " to salvages");
                            }
                            else if (objClass.Contains("Melee") || objClass.Contains("Missile"))
                            {
                                ntinkobj = nobjID;
                                Util.WriteToChat("ntinkobj " + ntinkobj.ToString());
  
                            }
                        } // end of for int n

                        Util.WriteToChat("Objtotink " + ntinkobj.ToString() + ", salvage " + salvages[0].ToString());
                        DotheTink();
                    } // end of trdobjects.count
                }
                catch (Exception ex) { Util.LogError(ex); }
            }
        }

         private void TinkArmor()
         {
             Util.WriteToChat("I am in TinkArmor");
             if (chatCmd == "Armor")
             {
                 try
                 {
                     if (TrdObjects.Count > 1)
                     {

                         int nobj = TrdObjects.Count;
                         WorldObject obj;
                         string objClass;
                         int nobjID;
                         Util.WriteToChat("nobj " + nobj);
                         List<int> salvages = new List<int>();
                         for (int n = 0; n < nobj; n++)
                         {
                             obj = TrdObjects[n];
                             Util.WriteToChat("n = " + n + ", " + obj.Name);
                             nobjID = obj.Id;
                             objClass = obj.ObjectClass.ToString();
                             //       if (TrdObjects[n].Name.Contains("Salvaged"))
                             if (obj.Name.Contains("Salvage"))
                             {
                                 nsalvage = nobjID;
                                 salvages.Add(nobjID);
                                 Util.WriteToChat("Added " + obj.Name + " to salvages");
                             }
                             else if (objClass.Contains("Armor") || objClass.Contains("Clothing"))
                             {
                                 ntinkobj = nobjID;
                                 Util.WriteToChat("ntinkobj " + ntinkobj.ToString());

                             }
                         } // end of for int n

                         Util.WriteToChat("Objtotink " + ntinkobj.ToString() + ", salvage " + salvages[0].ToString());
                         tinkTime = DateTime.Now;

                          DotheTink();
                     } // end of trdobjects.count
                 }
                 catch (Exception ex) { Util.LogError(ex); }
             }
         }

         private void DotheTink()
         {
             try{
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
                 Double TimeElapsed = Convert.ToDouble(DateTime.Now - tinkTime);
                 Util.WriteToChat("Number of seconds: " + TimeElapsed);
                 if ((DateTime.Now - tinkTime).TotalSeconds == 5)
                 {
                     Util.WriteToChat("I am in function to tink");
                     Core.Actions.ApplyItem(ntinkobj, nsalvage);

                     if (bTinkSucceeded) { CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Tink); }
                     else { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Tink); }

                 }
                 else { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Tink); }


             }
             catch (Exception ex) { Util.LogError(ex); }

         }

 

        private void checkFocus()
        {
            try{
                Util.WriteToChat("I am in needFocus");
            checkEnhancements();
            Util.WriteToChat("ketmaninuse: " + bKetnaninUse.ToString());
            if (!bKetnaninUse)
            {
                msecondarysubRoutine = "GetFocus";
                mroutine = "drinkingbeers";
                string name = "Spit Ale"; drinkBeers(name);
             }
            else { checkBeers(); }
        }
            catch (Exception ex) { Util.LogError(ex); }
        }



        private void checkBeers()
        {
            try{
            Util.WriteToChat("I am in checkBeers and chatcmd: " + chatCmd);
            checkEnhancements();
            mroutine = "drinkingbeers";
            if (chatCmd == "Weapon" && !bZongoinUse )
            {msecondarysubRoutine = "GeneralBeers"; string name = "Zongo"; Util.WriteToChat("I am boing to drink Zongo's"); drinkBeers(name); }
            else if (chatCmd == "Item" && !bBrighteyesinUse) { msecondarysubRoutine = "GeneralBeers"; Util.WriteToChat("mSecondarysubroutine " + msecondarysubRoutine); string name = "Amber Ape"; drinkBeers(name); }
            else if (chatCmd == "Armor" && !bHunterinUse ) { msecondarysubRoutine = "GeneralBeers"; string name = "Hunter"; drinkBeers(name); }
            else
            {
                mroutine = "";
                msecondarysubRoutine = "EquiptheBot";
                msub = "remove";
                clearBotOutfit();
            }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void drinkBeers(string beername )
        {
            try
            {

                GetInventoryCraftbot();
                foreach (WorldObject obj in botInventory)
                {
                    if (obj.Name.Contains(beername))
                    {
                        beer = obj.Id;
                        break;
                    }
                }
                if (beer < 0) 
                { 
                    bDrankBeer = false;
                    Util.WriteToChat("I am on my way to useenhancements");
                    UseEnhance = DateTime.Now;
                    useEnhancement(); }
                else
                {
                    Globals.Host.Actions.InvokeChatParser("/r " + ", " + "I am out of beers.  If you do not wish to continue, please tell me, 'remove'.");
                    mroutine = "equippingbot";
                    msecondarysubRoutine = "EquiptheBot";
                    msub = "remove";
                    clearBotOutfit();
                 }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void ArmorTink()
        {
            Util.WriteToChat("I am in Armor Tinkering function");
            try
            {
               // EquipOutfit("Armor");
                //msub = "remove";
                //removeItems(toEquip);

            }
            catch (Exception ex) { Util.LogError(ex); }


        }




     }
}
