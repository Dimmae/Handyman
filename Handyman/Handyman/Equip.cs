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
        List<int> toEquip = new List<int>();
        DateTime equipTime = DateTime.MinValue;
        List<WorldObject> botEquip = new List<WorldObject>();
        List<WorldObject> botRemove = new List<WorldObject>();
        WorldObject focusingObj = null;


       private void clearBotOutfit()
       {
               try
               {
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
                       equipItems();
                       equipTime = DateTime.Now;

                   }
                   else if (msecondarysubRoutine.Contains("PrepareforRest")) {  prepareRestingBot(); }
                  }
               catch (Exception ex) { Util.LogError(ex); }

       }


       private void EquipOutfit()
       {
           try
           {
               equipToWield = new List<int>();
               equipmentWielded = 0;
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
                             }

                   }
               }
               }

                   if (equipToWield.Count > 0)
                   {
                       equipmentWielded = 0;
                       equipmentCount = equipToWield.Count;
                       equipTime = DateTime.Now;


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
             //  Util.WriteToChat("I am in equipitems");
              // equipmentCount = equipToWield.Count;
                   CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);

           }
           catch (Exception ex) { Util.LogError(ex); }
       }

       private void RenderFrame_Equip(object sender, EventArgs e)
       {
           try
           {
               if (msecondarysubRoutine == "") {CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip); return;}

              if ((DateTime.Now - equipTime).TotalSeconds >  30 * (equipmentWielded+1)); //&& (equipToWield.Count > equipmentWielded || equipToRemove.Count > equipmentWielded) )
               {
                 if(msecondarysubRoutine.Contains("EquipforRest")){ doEquipforRest();}


                  else if (msub.Contains("equip")) {  doTheEquip(); }
                   else if(msub.Contains("remove")){doTheRemove();}
                }
           }
           catch (Exception ex) { Util.LogError(ex); }

       }

       private void doTheEquip()
        {
           try                      

           {
               CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);

               int item = 0;
               WorldObject obj;
                   if (msub == "equip")
                  {
                      if (equipmentCount > equipmentWielded) { item = equipToWield[equipmentWielded]; }
                  
                      if (item == myFocusingStone && focusingObj.Values(LongValueKey.Slot) != -1) { Core.Actions.UseItem(item, 0); }
                      else { Core.Actions.UseItem(item, 0); }
                       for(int i = 0;i<botEquip.Count;i++)
                      {
                         
                           obj = botEquip[i];
                          
                          if ((obj.Id == item) && (obj.Values(LongValueKey.Slot) == -1)) 
                          {
                               equipmentWielded++; 
                               if (equipmentWielded == equipmentCount )
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
                              else{ CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip);}

                          }
                      }//endof for i= 0
                      if (msub == "equip") { CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Equip); }
                   }//EndOf msub = equip
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
                       //     foreach (WorldObject obj in botRemove)
                       for (int i = 0; i < botRemove.Count; i++)
                       {

                           obj = botRemove[i];
                           if ((obj.Id == item) && (obj.Values(LongValueKey.Slot) != -1))
                           {
                               equipmentWielded++;
                               Util.WriteToChat("equipmentwield: " + equipmentWielded + "; equiptoremove.count " + equipToRemove.Count.ToString());
                               if (equipmentWielded == equipToRemove.Count)
                               {
                                   try{
                                //   Util.WriteToChat("I am in equipmentWielded == equiptoremove.count");
                                   CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Equip);
                                   msub = "";
                                   equipmentWielded = 0;

                                   equipToRemove.Clear();
                                   botRemove.Clear();
                                   Util.WriteToChat("msecondarysubroutine: " + msecondarysubRoutine);

                                   if (msecondarysubRoutine.Contains("EquiptheBot")) { msub = "equip"; EquipOutfit(); }
                                   else if (msecondarysubRoutine.Contains("PrepareforRest")) {  prepareRestingBot(); }
                                   Util.WriteToChat("I am on other side of if's in remove");
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
    }

}

