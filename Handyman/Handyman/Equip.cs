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
        int equipmentCount = 0;
        int equipmentWielded = 0;
        int item = 0;
        int myCount;
        List<int> toEquip = new List<int>();
        DateTime equipTime = DateTime.MinValue;
        List< WorldObject> botEquip = new List<WorldObject>();
        WorldObject focusingObj = null;


       private void clearBotOutfit()
       {
               try
               {
                   equipToWield = new List<int>();
                   botEquip = new List<WorldObject>();
                   equipToWield = new List<int>();
                  Util.WriteToChat("I am in function clearBotOutfit; secondarysubroutine = " + msecondarysubRoutine);
                   foreach (Decal.Adapter.Wrappers.WorldObject obj in Core.WorldFilter.GetInventory())
                   {
                       try
                       {

                           if (obj.Values(LongValueKey.Slot) == -1)
                           {
                             equipToWield.Add(obj.Id);
                             botEquip.Add(obj);
                           }
                           if (obj.Id == myFocusingStone) { focusingObj = obj; }
                       
                       }
                       catch (Exception ex) { Util.LogError(ex); }

                   } // endof foreach world object
                   Util.WriteToChat("equiptowield.count " + equipToWield.Count.ToString());
                   if (equipToWield.Count > 0)
                   {
                       equipmentWielded = 0;
                       msub = "remove";
                       equipItems();
                   }
                   else if (msecondarysubRoutine.Contains("PrepareforRest")) { Util.WriteToChat("I am going to add clothes."); prepareRestingBot(); }
                  }
               catch (Exception ex) { Util.LogError(ex); }

       }


       private void EquipOutfit()
       {
           try
           {
               equipToWield = new List<int>();
               equipmentWielded = 0;
               Util.WriteToChat("I am in function to equip outfit. Chatcmd: " + chatCmd);
               if(msecondarysubRoutine.Contains("EquiptheBot"))
               {
               IEnumerable<XElement> eq = null;
               xdocEquipment = new XDocument();
               xdocEquipment = XDocument.Load(equipmentFilename);

               GetInventoryCraftbot();
               botEquip = new List<WorldObject>();
               botEquip.AddRange(botInventory);

               switch (chatCmd)
               {
                  case "Salvage":
                       eq = xdocEquipment.Element("Equip").Descendants("Salvaging");
                       break;
                  case "Weapon":
                       try
                       {

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

               if (eq != null)
               {
                    foreach (XElement el in eq.Elements("ItemID"))
                   {
 
                       if (Convert.ToInt32(el.Value) < 0)
                       {
                           item = Convert.ToInt32(el.Value);
                           equipToWield.Add(item);
                       //    Util.WriteToChat(item.ToString());
                       }

                   }
               }
               }

                   if (equipToWield.Count > 0)
                   {
                       equipmentWielded = 0;
                       equipItems();
                       // Core.Actions.UseItem(myFocusingStone, 0);
 
                   }
                   else
                   {
                       return;
                   }
               
          }
           catch (Exception ex) { Util.LogError(ex); }
       }


       private void equipItems()
       {
           try
           {
               equipmentCount = equipToWield.Count;
 
                   CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);

           }
           catch (Exception ex) { Util.LogError(ex); }
       }

       private void RenderFrame_Equip(object sender, EventArgs e)
       {
           try
           {
               if(msecondarysubRoutine.Contains("EquipforRest")){doEquipforRest();}
               else
               {

               int id = 0;
               if ((DateTime.Now - equipTime).TotalSeconds >  30 * (equipmentWielded+1) && equipToWield.Count > equipmentWielded)
               {
                   if (equipmentCount > equipmentWielded) { id = equipToWield[equipmentWielded]; }
                

               //  Util.WriteToChat("I am in the function to equip. msub = " + msub + " id = " + id.ToString());
                   if(msub.Contains("equip")){doTheEquip(id);}
                   else if(msub.Contains("remove")){doTheRemove(id);}
                //   if (msub == "") { return; }
               }
               }
           }
           catch (Exception ex) { Util.LogError(ex); }

       }

       private void doTheEquip(int item)
        {
           try{
              // Util.WriteToChat("I am in id<0 and msub = " + msub);
               // if (msub == "equip") { Util.WriteToChat("I am ready to equipitem"); Core.Actions.AutoWield(item); }
                  if (msub == "equip") 
                  
                      // Util.WriteToChat("I am ready to equipitem");
                      if (item == myFocusingStone && focusingObj.Values(LongValueKey.Slot) != -1) { Core.Actions.UseItem(item, 0); }
                      else { Core.Actions.UseItem(item, 0); }
                      foreach (WorldObject obj in botEquip)
                      {
                          if ((obj.Id == item) && (obj.Values(LongValueKey.Slot) == -1)) 
                          {
                              CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);

                              equipmentWielded++; 
                              Util.WriteToChat("Equipmentwielded " + equipmentWielded.ToString());
                              if (equipmentWielded == equipmentCount )
                              {
                                   CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
                                   Util.WriteToChat("I have wielded all Material.");
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
                              CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);
                          }

                      }//endof foreach
                  }//EndOf msub = equip
           }
           catch (Exception ex) { Util.LogError(ex); }
       }

           private void doTheRemove(int item)
           {
               try{
                if (msub == "remove") 
                  {
                //    Util.WriteToChat("I am in function to remove item and msub: " + msub);
                    Core.Actions.MoveItem(item, Core.CharacterFilter.Id, 0, false);
                    foreach (WorldObject obj in botEquip)
                    {
                        if ((obj.Id == item) && (obj.Values(LongValueKey.Slot) != -1))
                        {
                            equipmentWielded++;
                            Util.WriteToChat("equipmentWielded: " + equipmentWielded.ToString() + "; equipToWield.Count: " + equipToWield.Count.ToString());
                            if (equipmentWielded == equipToWield.Count)
                            {
                                CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
                                Util.WriteToChat("I have removed all items and equipmentWielded = " + equipmentWielded.ToString());
                                msub = "";
                                equipmentWielded = 0;

                                equipToWield.Clear();
                                botEquip.Clear();
                                Util.WriteToChat("msecondarysubroutine " + msecondarysubRoutine + "; chatCmd " + chatCmd);
                                if (msecondarysubRoutine.Contains("EquiptheBot")) { msub = "equip"; EquipOutfit(); }
                                else if (msecondarysubRoutine.Contains("PrepareforRest")) { prepareRestingBot(); }
                             }
                        }
                    } //end foreach
                 
                } //end else if remove
           }
           catch (Exception ex) { Util.LogError(ex); }

         }

           private void doEquipforRest()
           {
               Core.Actions.UseItem(item, 0);
                if (oIdleOutfit.Values(LongValueKey.Slot) == -1)
                          {
                                  CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
                                    msecondarysubRoutine = "";
                                  msubRoutine = "";
                                  equipToWield.Clear();
                                  msub = "";
                                  mroutine = ""; 
                              }
                         
                          else
                          {
                              CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);
                          }

           }

       private void prepareRestingBot()
       {
          try 
          {
              Util.WriteToChat("I am in function prepareRestingBot");
              equipToWield.Clear();
               msub = "equip";
               chatCmd = "";
               item = idleOutfit;
               equipToWield.Add(item);
               equipmentWielded = 0;
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
    }

}

