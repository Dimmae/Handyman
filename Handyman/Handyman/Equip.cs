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
    using System.Xml;
    using System.Xml.XPath;
    using System.Linq;
    using System.Xml.Linq;
    using VirindiViewService;
    using MyClasses.MetaViewWrappers;
    using VirindiViewService.Controls;
    using System.Windows.Forms;
    using WindowsTimer = System.Windows.Forms.Timer;




namespace Handyman
{
    public partial class PluginCore : PluginBase
    {

        List<int> equipToWield = new List<int>();
        List<int> equipToRemove = new List<int>();
        int equipmentCount = 0;
        int equipmentWielded = 0;
        int item = 0;
        int myCount;
        int equipmentWieldedHolding = 0;
        List<int> toEquip = new List<int>();
        DateTime equipTime = DateTime.MinValue;
        List<WorldObject> botEquip = new List<WorldObject>();
        List<WorldObject> botRemove = new List<WorldObject>();
        WorldObject focusingObj = null;
        Boolean bEquipmentWielded = false;
        Boolean bItemNotAvailable = false;


       private void clearBotOutfit()
       {
           try
           {
               if (msecondarysubRoutine.Contains("EquiptheBot") && !bEnhanced) { return; }
                  equipToRemove = new List<int>();
                   botRemove = new List<WorldObject>();


                   foreach (Decal.Adapter.Wrappers.WorldObject obj in Core.WorldFilter.GetInventory())
                   {
                       try
                       {

                           if (obj.Values(LongValueKey.Slot) == -1)
                           {
                               equipToRemove.Add(obj.Id);
                               botRemove.Add(obj);
                           }
                           if (obj.Id == myFocusingStone) { focusingObj = obj; }

                       }
                       catch (Exception ex) { Util.LogError(ex); }

                   } // endof foreach world object
                   if (equipToRemove.Count > 0)
                   {
                       equipmentWielded = 0;
                       msub = "remove";
                       equipmentCount = equipToRemove.Count;
                       equipTime = DateTime.Now;
                       equipItems();

                   }
                   else if (msecondarysubRoutine.Contains("PrepareforRest")) { prepareRestingBot(); }
               
           }
           catch (Exception ex) { Util.LogError(ex); }

       }


       private void EquipOutfit()
       {
           try
           {
              // Util.WriteToChat("I am in equipoutfit.  msub: " + msub + " chatCmd: " + chatCmd + " msecondarysubroutine: " + msecondarysubRoutine);
               equipToWield = new List<int>();
               equipmentWielded = 0;
               if(msecondarysubRoutine.Contains("EquiptheBot"))
               {
               IEnumerable<XElement> eq = null;
               xdocEquipment = new XDocument();
               xdocEquipment = XDocument.Load(equipmentFilename);

              //GetInventoryCraftbot();
               botEquip = new List<WorldObject>();
               botEquip.AddRange(obotInventory);
             //  Util.WriteToChat(botEquip.Count + " number of items in botEquip.");
               switch (chatCmd)
               {
                  case "Salvage":
                       eq = xdocEquipment.Element("Equip").Descendants("Salvaging");
                       break;
                  case "Weapon":
                       try
                       {
                          // Util.WriteToChat("I am in case Weapon.");
                           eq = xdocEquipment.Element("Equip").Descendants("Weapon_Tinkering");
                       }
                       catch (Exception ex) { Util.LogError(ex); }
                       break;

                  case "Armor":
                       eq = xdocEquipment.Element("Equip").Descendants("Armor_Tinkering");
                       break;
                  case "Item":
                       eq = xdocEquipment.Element("Equip").Descendants("Item_Tinkering");
                       break;
                  case "MagicItem":
                       eq = xdocEquipment.Element("Equip").Descendants("MagicItem_Tinkering");
                      break;
                  case "Dye":
                       eq = xdocEquipment.Element("Equip").Descendants("Dyeing");
                      break;
                  case "Lockpick":
                       eq = xdocEquipment.Element("Equip").Descendants("Lockpicking");
                       break;
                   default:
                       eq = null;

                       break;
               }

            try{
               if (eq != null)
               {
                   int id =0;
                   bItemNotAvailable = false;
                    foreach (XElement el in eq.Elements("ItemID"))
                   {
                       id =  Convert.ToInt32(el.Value);
                       if (id < 0 && nBotInventoryID.Contains(id))
                       {
                          equipToWield.Add(id);
                        }
                       else
                       {
                          botMess = "Some items for me to equip are not available in packs.";
                          WriteToFellow(botMess);
                         // bItemNotAvailable = true;
                          break;
                       }

                   }
               
                   // Util.WriteToChat("equiptoWield has " + equipToWield.Count.ToString());

                   if(bItemNotAvailable)
                   {
                       WriteToFellow("Some of my armor or other equipment is no longer in my inventory.");
                    //msecondarysubRoutine.Contains("EquiptheBot");
                    //mHoldingRoutine = msecondarysubRoutine;
                    //timeContinue = DateTime.Now;
                    //sContinueMessage = "Some of my tinking armor is not in my inventory.";
                    //CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_RequestContinue);
                   }
                   continueTheEquip();
                  }//eof if (eq !=null
                } catch (Exception ex) { Util.LogError(ex); }
               }
              } catch (Exception ex) { Util.LogError(ex); }

       }


               private void continueTheEquip()
               {
                 try{
                   if (equipToWield.Count > 0)
                   {
                       equipmentWielded = 0;
                       bEquipmentWielded = false;
                       equipmentCount = equipToWield.Count;
                       equipTime = DateTime.Now;
                     //  Util.WriteToChat("Equipmentcount " + equipmentCount.ToString());

                       equipItems();
                       // Core.Actions.UseItem(myFocusingStone, 0);
 
                   }
                   else
                   {
                       return;
                   }
               
          }catch (Exception ex) { Util.LogError(ex); }
       }


       private void equipItems()
       {
           try
           {
                   CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);

           }
           catch (Exception ex) { Util.LogError(ex); }
       }

       private void RenderFrame_Equip(object sender, EventArgs e)
       {
           try
           {
               if (msecondarysubRoutine == "") {CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip); return;}

              if ((DateTime.Now - equipTime).TotalMilliseconds >  600 ) //&& (equipToWield.Count > equipmentWielded || equipToRemove.Count > equipmentWielded) )
               {
 //                  Util.WriteToChat(equipmentCount.ToString() + " items to equip and number of pieces reportedly wielded " + equipmentWielded.ToString());

                  CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);

                 if(msecondarysubRoutine.Contains("EquipforRest")){ doEquipforRest();}


                  else if (msub.Contains("equip")) {  doTheEquip(); }
                   else if(msub.Contains("remove")){doTheRemove();}
              }
           } catch (Exception ex) { Util.LogError(ex); }
         
       }

       private void doTheEquip()
        {
           try                      

           {
             //  Util.WriteToChat("i am in doTheEquip");
               equipTime = DateTime.Now;
               CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
               int item = 0;
                 if (msub == "equip")
               {
                   if ((equipmentCount > equipmentWielded) || (omyFocusingStone.Values(LongValueKey.Slot) != -1))
                   {
                       if (equipmentCount > equipmentWielded) 
                       {item = equipToWield[equipmentWielded];
                       Core.Actions.UseItem(item, 0);
                       equipmentWielded++;
                       
                       }
                      // Util.WriteToChat("I am in DotheEquip and if equipmentcount > equipmentwielded; equipmentWielded = " + equipmentWielded.ToString() );
                       else if (omyFocusingStone.Values(LongValueKey.Slot) != -1) 
                       { 
                           item = myFocusingStone;
                           Core.Actions.UseItem(item, 0);

                       }
                     //  Util.WriteToChat(item.ToString());
                       


                       //   if (item == myFocusingStone && focusingObj.Values(LongValueKey.Slot) != -1) { Core.Actions.UseItem(item, 0); }
                       //   else { Core.Actions.UseItem(item, 0); }

                       if (equipmentWielded == equipmentCount &&  (omyFocusingStone.Values(LongValueKey.Slot) == -1)
)
                       {
                           CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
                           
                           msecondarysubRoutine = "";
                           msubRoutine = "";
                           equipToWield.Clear();
                           botEquip.Clear();
                           msub = "";
                           if (chatCmd.Length > 0) { mroutine = "readyfortrade"; }
                           else { mroutine = ""; }

                       }
                       else
                       {
                          // Util.WriteToChat(equipmentWielded.ToString() + " has been wielded. and the number to be wielded is " + equipmentCount.ToString());
                           equipTime = DateTime.Now;
                           CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);
                           //  doTheEquip();
                       }



                    }//EndOf msub = equip
               }
           }
           catch (Exception ex) { Util.LogError(ex); }
       }

       private void equipmentHasBeenWielded()
       {
          try{
           //   Util.WriteToChat("I am in equipmenthasbeenwielded");
              if (bEquipmentWielded)
              {
                  
                  bEquipmentWielded = false;
                  if (equipmentWielded == equipmentCount)
                  {
                      CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
                      msecondarysubRoutine = "";
                      msubRoutine = "";
                      equipToWield.Clear();
                      botEquip.Clear();
                      msub = "";
                      if (chatCmd.Length > 0) { mroutine = "readyfortrade"; }
                      else { mroutine = ""; }

                  }
                  else 
                  { 
                    // Util.WriteToChat(equipmentWielded.ToString() + " has been wielded. and the number to be wielded is " + equipmentCount.ToString());
                       equipTime = DateTime.Now; 
                      CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip); 
                    //  doTheEquip();
                  }
              }
              else
              {

                  //}//endof for i= 0
                  equipTime = DateTime.Now;
                  CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);
                 // doTheEquip();
              }




          }
          catch (Exception ex) { Util.LogError(ex); }


       }
           private void doTheRemove()
           {
               try{
                   CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);

                   int item = 0;
                   WorldObject obj;
                   if (msub == "remove")
                   {
                      if (equipmentCount > equipmentWielded) { item = equipToRemove[equipmentWielded]; }


                       Core.Actions.MoveItem(item, Core.CharacterFilter.Id, 0, false);
                       for (int i = 0; i < botRemove.Count; i++)
                       {

                           obj = botRemove[i];
                           if ((obj.Id == item) && (obj.Values(LongValueKey.Slot) != -1))
                           {
                               equipmentWielded++;
                             //  Util.WriteToChat("equipmentwield: " + equipmentWielded + "; equiptoremove.count " + equipToRemove.Count.ToString());
                               if (equipmentWielded == equipToRemove.Count)
                               {
                                   try{
                                //   Util.WriteToChat("I am in equipmentWielded == equiptoremove.count");
                                   CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
                                   msub = "";
                                   equipmentWielded = 0;

                                   equipToRemove.Clear();
                                   botRemove.Clear();
                                 //  Util.WriteToChat("msecondarysubroutine: " + msecondarysubRoutine);

                                   if (msecondarysubRoutine.Contains("EquiptheBot")) { msub = "equip"; Util.WriteToChat("I have removed outfit and now moving to equip outfit"); EquipOutfit(); }
                                   else if (msecondarysubRoutine.Contains("PrepareforRest")) {  prepareRestingBot(); }
                                   Util.WriteToChat("I am on other side of if's in remove and msecondarysubRoutine = " + msecondarysubRoutine);
                                   }

                                   catch (Exception ex) { Util.LogError(ex); }

                               }
                               else
                               {
                                   CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);
                               }

                           }
                       }//end of for int i
                       if(msub == "remove"){CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);}
 
                   }//end of if msub == remove
                   

           }
           catch (Exception ex) { Util.LogError(ex); }

         }

           private void doEquipforRest()
           {
               try{
                 //  Util.WriteToChat("I am in doequpforreat");
                   CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
               Core.Actions.UseItem(item, 0);
                if (oIdleOutfit.Values(LongValueKey.Slot) == -1)
                          {

                              msecondarysubRoutine = "";
                                  msubRoutine = "";
                                  equipToWield.Clear();
                                  msub = "";
                                  mroutine = "";
                                  return;
                              }
                         
                          else
                          {

                              CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);
                          }
               }
               catch (Exception ex) { Util.LogError(ex); }

           }

       private void prepareRestingBot()
       {
          try 
          {
            //  Util.WriteToChat("I am in prepare resting bot.");
              equipToWield.Clear();
              equipToWield = new List<int>();
              botEquip.Clear();
              botEquip = new List<WorldObject>();
               msub = "equip";
               chatCmd = "";
               item = idleOutfit;
               botEquip.Add(oIdleOutfit);
               equipToWield.Add(item);
               equipmentWielded = 0;
               equipTime = DateTime.Now;

               msecondarysubRoutine = "EquipforRest";
               equipItems();
           }

          catch (Exception ex) { Util.LogError(ex); }
       }




        //private void saveEquip(string skill, int guid)
        //{
        //    if (skill == "buffing") { buffWand = guid; saveEquipToXML(); }
        //    else if (skill == "salvaging") { myUst = guid; saveEquipToXML(); }
        //    else if (skill == "tinking_crafting") { myFocusingStone = guid; saveEquipToXML(); }
        //}


        //private void saveEquipToXML()
        //{
        //    XDocument xdocEquipment = new XDocument(new XElement("Equip"));
        //    xdocEquipment.Element("Equip").Add(new XElement("Buffing",
        //         new XElement("BuffWand", buffWand)));
        //    xdocEquipment.Element("Equip").Add(new XElement("Salvaging",
        //        new XElement("MyUst", myUst)));
        //    xdocEquipment.Element("Equip").Add(new XElement("Tinking_Crafting",
        //       new XElement("MyFocusingStone", myFocusingStone)));

        //    xdocEquipment.Save(equipmentFilename);                
 
        //  }

        // Util.WriteToChat("bEquipmentWielded " + bEquipmentWielded.ToString());
        //if (bEquipmentWielded) 
        //{ 
        //    equipmentWielded++;
        //    bEquipmentWielded = false;
        //}
        //  Util.WriteToChat("equipmentwielded: " + equipmentWielded.ToString() + " and equipmentcount: " + equipmentCount.ToString());

    }

}

