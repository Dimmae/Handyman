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

    public partial class PluginCore : PluginBase
    {

        private void Initializepaths()
        {
            try{
            toonname = Core.CharacterFilter.Name;
            world = Core.CharacterFilter.Server;
            //Dir for the Document files for Handyman
            HandymanDir = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Decal Plugins\" + Globals.PluginName);
            //Directory for the current world in Handyman Directory
            currDir = String.Concat(HandymanDir + @"\" + world);
            //Directory for the toon in the current world
            toonDir = String.Concat(currDir + @"\" + toonname);

            chkDirsExist();

            setupFilenames();
            Util.WriteToChat("I have finished initializing paths");

            }
            catch (Exception ex) { Util.LogError(ex); }
 
        }


        private void setupFilenames()
        {
            equipmentFilename = toonDir + @"\equipment.xml";
            spellsFilename = HandymanDir + @"\spells.xml";
            settingsFilename = toonDir + @"\settings.xml";
        }


        private void SetUpXdocs()
        {
            try{
  
            if (!File.Exists(equipmentFilename))
            {
                     XDocument tempEquipDoc = new XDocument(new XElement("Equip"));
                    tempEquipDoc.Save(equipmentFilename);
                    tempEquipDoc = null;

            }
 
            if (!File.Exists(spellsFilename))
            {
                saveSpellsToXML();

            }
            if (!File.Exists(settingsFilename))
            {
                XDocument tempsettingsDoc = new XDocument(new XElement("Settings"));
                tempsettingsDoc.Save(settingsFilename);
                tempsettingsDoc = null;

            }
              
 
            }
           catch (Exception ex) { Util.LogError(ex); }

        }

        private void setUpLists()
        {
            try{
              //  Util.WriteToChat("I am in setuplists function");
            xdocSpells = new XDocument();
            xdocSpells = XDocument.Load(spellsFilename);
            IEnumerable<XElement> elements = xdocSpells.Element("Spells").Descendants("Spell8");
            spellsList = new List<int>();
                foreach (XElement el in elements)
                {
                    nSpellID = Convert.ToInt32(el.Value);
                    spellsList.Add(nSpellID);

                }

                botCmds = new List<string>();
                botCmds.Add("Salvage");
                botCmds.Add("Armor");
                botCmds.Add("Weapon");
                botCmds.Add("Item");
                botCmds.Add("MagicItem");
                botCmds.Add("Dye");
                botCmds.Add("Lockpick");
                botCmds.Add("Cooking");
                botCmds.Add("Alchemy");
            }
            catch (Exception ex) { Util.LogError(ex); }

        }


        // need to be certain above directories exist and create them if they don't
        public void chkDirsExist()
        {

            if (!Directory.Exists(HandymanDir))
            { DirectoryInfo dirInfo = Directory.CreateDirectory(HandymanDir); }
            if (!Directory.Exists(currDir))
            { DirectoryInfo dirInfo = Directory.CreateDirectory(currDir); }
            if (!Directory.Exists(toonDir))
            { DirectoryInfo dirInfo = Directory.CreateDirectory(toonDir); }
        }


        private void saveSpellsToXML()
        {
            try{
            xdocSpells = new XDocument(new XElement("Spells"));
            xdocSpells.Root.Add(new XElement("Spell8", 4530));
            xdocSpells.Root.Add(new XElement("Spell8", 4305));
            xdocSpells.Root.Add(new XElement("Spell8", 4329));
            xdocSpells.Root.Add(new XElement("Spell8", 4582));
            xdocSpells.Root.Add(new XElement("Spell8", 4602));
            xdocSpells.Root.Add(new XElement("Spell8", 4564));
            xdocSpells.Root.Add(new XElement("Spell8", 4297));
            xdocSpells.Root.Add(new XElement("Spell8", 4299));
            xdocSpells.Root.Add(new XElement("Spell8", 4325));
            xdocSpells.Root.Add(new XElement("Spell8", 4512));
            xdocSpells.Root.Add(new XElement("Spell8", 4640));
            xdocSpells.Root.Add(new XElement("Spell8", 4592));
            xdocSpells.Root.Add(new XElement("Spell8", 5068));
            xdocSpells.Root.Add(new XElement("Spell8", 4566));
            xdocSpells.Root.Add(new XElement("Spell8", 4506));
            xdocSpells.Root.Add(new XElement("Spell8", 4526));
            xdocSpells.Root.Add(new XElement("Spell8", 4586));
            xdocSpells.Root.Add(new XElement("Spell8", 4552));
            xdocSpells.Root.Add(new XElement("Spell8", 4499));
            xdocSpells.Root.Add(new XElement("Spell8", 4510));

            xdocSpells.Save(spellsFilename);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

            private void initStaticEquip()
            {
                //try{
                //                        int InventoryCount = 0;
                //    var inventorymatches = Core.WorldFilter.GetInventory().Where(x => x.Name == namecheck);
                //    if(inventorymatches.Count() > 0)
                //    {
                //        foreach(WorldObject inv in inventorymatches)
                //        {
                //            if(!inv.LongKeys.Contains((int)LongValueKey.StackCount)){InventoryCount++;}
                //            else{InventoryCount += inv.Values(LongValueKey.StackCount);}
                //        }

                try{
                    List<WorldObject> wo = new List<WorldObject>();
                    var tempa = Core.WorldFilter.GetInventory().Where(x=> x.Name == "Focusing Stone");
                    if (tempa.Count() > 0) { wo.AddRange(tempa); Util.WriteToChat("tempa has a count of " + wo.Count.ToString()); }
                    omyFocusingStone = wo[0];
                    myFocusingStone = omyFocusingStone.Id;
                    wo.Clear();

                    var tempb = Core.WorldFilter.GetInventory().Where(x=> x.Name == "Ust");
                    if (tempb.Count() > 0) { wo.AddRange(tempb); }
                    omyUst = wo[0];
                    myUst = omyUst.Id;
                    wo.Clear();

                    Util.WriteToChat("My focusing stone has an id of " + myFocusingStone.ToString());
                    List<WorldObject> temp = new List<WorldObject>();
                    if(File.Exists(equipmentFilename))
                    {
                        xdocEquipment = new XDocument();

                        xdocEquipment = XDocument.Load(equipmentFilename);

                        buffWand = Convert.ToInt32(xdocEquipment.Root.Element("Buffing").Element("BuffWand").Value);
                
                        idleOutfit = Convert.ToInt32(xdocEquipment.Root.Element("IdleWield").Element("ItemID").Value);
                        msubinven = "IdleClothes";
                        // GetInventory();
                      temp.AddRange(obotInventory);
                       foreach (WorldObject obj in temp)
                      {
                         if (obj.Id == idleOutfit)
                         {
                            oIdleOutfit = obj;
                            temp.Clear();
                             temp = null;
                            Util.WriteToChat("idleoutfit: " + oIdleOutfit.Name);
                             break;
                         }
                      }
                    }
                }
            catch (Exception ex) { Util.LogError(ex); }

           }


            private void GetInventory()
            {
                try
                {
                    inventoryTime = DateTime.Now;
                    msubinven = "GetInventory";
                    nBotInventoryID = new List<int>();
                    obotInventory = new List<WorldObject>();
                    bInventoryFinished = false;

                    foreach (Decal.Adapter.Wrappers.WorldObject obj in Core.WorldFilter.GetInventory())
                    {
                        try
                        {
                            objID = obj.Id;
                            nBotInventoryID.Add(objID);
                            obotInventory.Add(obj);
                        }

                        catch (Exception ex) { Util.LogError(ex); }


                    } // endof foreach world object
                    Util.WriteToChat("I have completed inventory and count is " + obotInventory.Count);
                    inventoryCount = nBotInventoryID.Count;
                    msubinven = "";
                    bInventoryFinished = true;
                    // Util.WriteToChat("Number of items in inventory: " + botInventory.Count);
                    // CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Inventory);

                }
                catch (Exception ex) { Util.LogError(ex); }
            }



        }


    }

