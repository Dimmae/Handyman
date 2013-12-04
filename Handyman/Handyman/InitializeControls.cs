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
        private void chkEnabled_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bEnabled = e.Checked;
                Util.WriteToChat("Enabled = " + bEnabled);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkArmorTink_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bArmorTink = e.Checked;
                Util.WriteToChat("ArmorTink = " + bEnabled);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkItemTink_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bItemTink = e.Checked;
                Util.WriteToChat("bItemTink = " + bItemTink);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkWeaponTink_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bWeaponTink = e.Checked;
                Util.WriteToChat("WeaponTink = " + bWeaponTink);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkMagicItemTink_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bMagicItemTink = e.Checked;
                Util.WriteToChat("MagicItemTink = " + bMagicItemTink);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkSalvaging_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bSalvaging = e.Checked;
                Util.WriteToChat("Salvaging = " + bSalvaging);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkAlchemy_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bAlchemy = e.Checked;
                Util.WriteToChat("Alchemy = " + bAlchemy);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkCooking_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bCooking = e.Checked;
                Util.WriteToChat("Cooking = " + bCooking);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkFletching_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bFletching = e.Checked;
                Util.WriteToChat("Fletching = " + bFletching);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkLockpick_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bLockpick = e.Checked;
                Util.WriteToChat("Lockpick = " + bLockpick);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkTier4Rares_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bTier4Rares = e.Checked;
                Util.WriteToChat("Tier4Rares = " + bTier4Rares);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

         private void chkBuffsEnabled_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bBuffsEnabled = e.Checked;
                Util.WriteToChat("Buffs Enabled = " + bBuffsEnabled);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkBuffOnStart_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bBuffOnStart = e.Checked;
                Util.WriteToChat("Buff on start = " + bBuffOnStart);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkUseBeers_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bUseBeers = e.Checked;
                Util.WriteToChat("Use Beers = " + bUseBeers);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkUseRares_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bUseRares = e.Checked;
                Util.WriteToChat("Use Rares = " + bUseRares);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        private void chkUseBuffBot_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bUseBuffBot = e.Checked;
                Util.WriteToChat("Use Buff Bot = " + bUseBuffBot);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkLogOff_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bLogOff = e.Checked;
                Util.WriteToChat("LogOff = " + bLogOff);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkUseWeb_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bUseWeb = e.Checked;
                Util.WriteToChat("Use Web = " + bUseWeb);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkEquip_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bEquip = e.Checked;
                Util.WriteToChat("Equip items = " + bEquip);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkUseCharge_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bUseCharge = e.Checked;
                Util.WriteToChat("Use Charge = " + bUseCharge);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkRareAllegChan_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bRareAllegChan = e.Checked;
                Util.WriteToChat("Rare Allegance Channel = " + bRareAllegChan);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }


        private void chkRareTradeChan_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bRareTradeChan = e.Checked;
                Util.WriteToChat("Rare Trade Channel = " + bRareTradeChan);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkEnableMail_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bEnableMail = e.Checked;
                Util.WriteToChat("Enable Mail = " + bEnableMail);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkEnterSpamMail_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bEnterSpamMail = e.Checked;
                Util.WriteToChat("Spam Mail = " + bEnterSpamMail);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

        private void chkEnterSpamRare_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bEnterSpamRare = e.Checked;
                Util.WriteToChat("Spam Rare = " + bEnterSpamRare);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }





        private void chkCalcMajors_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bCalcMajors = e.Checked;
                Util.WriteToChat("Calculate Majors = " + bCalcMajors);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkJourneymanPet_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bJourneymanPet = e.Checked;
                Util.WriteToChat("Journeyman Pet = " + bJourneymanPet);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }
        private void chkArtisanPet_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bArtisanPet = e.Checked;
                Util.WriteToChat("Artisan Pet = " + bMasterPet);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

         private void chkMasterPet_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
        {
            try
            {
                bMasterPet = e.Checked;
                Util.WriteToChat("Master Pet = " + bMasterPet);

                SaveSettings();
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

         private void chkBuffingWand_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
         {
             try
             {

           //      saveEquip(Skill, objSelectedID);

             }
             catch (Exception ex) { Util.LogError(ex); }


         }

         private void chkSetUst_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
         {
             try
             {

                // saveEquip(Skill, objSelectedID);

             }
             catch (Exception ex) { Util.LogError(ex); }


         }
         private void chkSetFocusingStone_Change(object sender, MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs e)
         {
             try
             {

                // saveEquip(Skill, objSelectedID);

             }
             catch (Exception ex) { Util.LogError(ex); }


         }



        private void SaveSettings()
        {
             try
            {
                Util.WriteToChat("I am saving settings.");
                XDocument xdocGeneralSet = new XDocument(new XElement("Settings"));
                xdocGeneralSet.Element("Settings").Add(new XElement("Setting",
                         new XElement("Enabled", bEnabled),
                         new XElement("ArmorTink", bArmorTink),
                         new XElement("ItemTink", bItemTink),
                         new XElement("WeaponTink", bWeaponTink),
                         new XElement("MagicItemTink", bMagicItemTink),
                         new XElement("Salvaging", bSalvaging),
                         new XElement("Alchemy", bAlchemy),
                         new XElement("Cooking", bCooking),
                         new XElement("Fletching", bFletching),
                         new XElement("Lockpick", bLockpick),
                         new XElement("Tier4Rares", bTier4Rares),
                          new XElement("BuffsEnabled", bBuffsEnabled),
                         new XElement("BuffOnStart", bBuffOnStart),
                         new XElement("UseBeers", bUseBeers),
                         new XElement("UseRares", bUseRares),
                         new XElement("UseBuffBot", bUseBuffBot),
                        new XElement("LogOff", bLogOff),
                         new XElement("UseCharge", bUseCharge),
                         new XElement("Equip", bEquip),
                         new XElement("UseWeb", bUseWeb),
                          new XElement("RareAllegChan", bRareAllegChan),
                         new XElement("RareTradeChan", bRareTradeChan),
                         new XElement("EnableMail", bEnableMail),
                         new XElement("EnterSpamMail", bEnterSpamMail),
                         new XElement("EnterSpamRare", bEnterSpamRare),
                         new XElement("CalcMajors", bCalcMajors),
                         new XElement("JourneymanPet", bJourneymanPet),
                         new XElement("ArtisanPet", bArtisanPet),
                         new XElement("MasterPet", bMasterPet)));
               xdocGeneralSet.Save(settingsFilename);
               xdocGeneralSet = null;

            }
            catch (Exception ex) { Util.LogError(ex); }

        }

        //public void setUpSettingsList()
        //{
        //    //  if (xdocGenSettings != null)
        //    try
        //    {
        //        XDocument xdocSettings = XDocument.Load(settingsFilename); 
        //        IEnumerable<XElement> elements = xdocSettings.Element("Settings").Elements("Setting");
        //        lSettingsList = new List<XElement>();
        //        foreach (XElement el in elements.Descendants())
        //        {
        //            lSettingsList.Add(el);
        //        }


        //        fillSettingsVariables();

        //    }

        //    catch (Exception ex) { Util.LogError(ex); }

        //}

        private void fillSettingsVariables()
        {
            try
            {
                XDocument xdocSettings = new XDocument();
                 xdocSettings = XDocument.Load(settingsFilename);

                XElement el = xdocSettings.Root.Element("Setting");

                bEnabled = Convert.ToBoolean(el.Element("Enabled").Value);
                bArmorTink = Convert.ToBoolean(el.Element("ArmorTink").Value);
                bItemTink = Convert.ToBoolean(el.Element("ItemTink").Value);
                bWeaponTink = Convert.ToBoolean(el.Element("WeaponTink").Value);
                bMagicItemTink = Convert.ToBoolean(el.Element("MagicItemTink").Value);
                bSalvaging = Convert.ToBoolean(el.Element("Salvaging").Value);
                bAlchemy = Convert.ToBoolean(el.Element("Alchemy").Value);
                bCooking = Convert.ToBoolean(el.Element("Cooking").Value);
                bLockpick = Convert.ToBoolean(el.Element("Lockpick").Value);
                bFletching = Convert.ToBoolean(el.Element("Fletching").Value);
                bTier4Rares = Convert.ToBoolean(el.Element("Tier4Rares").Value);
                bBuffsEnabled = Convert.ToBoolean(el.Element("BuffsEnabled").Value);
                bBuffOnStart = Convert.ToBoolean(el.Element("BuffOnStart").Value);
                bUseRares = Convert.ToBoolean(el.Element("UseRares").Value);
                bUseBeers = Convert.ToBoolean(el.Element("UseBeers").Value);
                bUseBuffBot = Convert.ToBoolean(el.Element("UseBuffBot").Value);
                bLogOff = Convert.ToBoolean(el.Element("LogOff").Value);
                bUseCharge = Convert.ToBoolean(el.Element("UseCharge").Value);
                bEquip = Convert.ToBoolean(el.Element("Equip").Value);
                bUseWeb = Convert.ToBoolean(el.Element("UseWeb").Value);
                bRareAllegChan = Convert.ToBoolean(el.Element("RareAllegChan").Value);
                bRareTradeChan = Convert.ToBoolean(el.Element("RareTradeChan").Value);
                bEnableMail = Convert.ToBoolean(el.Element("EnableMail").Value);
                bEnterSpamMail = Convert.ToBoolean(el.Element("EnterSpamMail").Value);
                bEnterSpamRare = Convert.ToBoolean(el.Element("EnterSpamRare").Value);
                bCalcMajors = Convert.ToBoolean(el.Element("CalcMajors").Value);
                bJourneymanPet = Convert.ToBoolean(el.Element("JourneymanPet").Value);
                bArtisanPet = Convert.ToBoolean(el.Element("ArtisanPet").Value);
                bMasterPet = Convert.ToBoolean(el.Element("MasterPet").Value);
             

                Util.WriteToChat("I will now fill the checkboxes.");

                chkEnabled.Checked = bEnabled;
                chkArmorTink.Checked = bArmorTink;
                chkItemTink.Checked = bItemTink;
                chkMagicItemTink.Checked = bMagicItemTink;
                chkWeaponTink.Checked = bWeaponTink;
                chkSalvaging.Checked = bSalvaging;
                chkAlchemy.Checked = bAlchemy;
                chkCooking.Checked = bCooking;
                chkLockpick.Checked = bLockpick;
                chkFletching.Checked = bFletching;
                chkTier4Rares.Checked = bTier4Rares;
                chkBuffsEnabled.Checked = bBuffsEnabled;
                chkBuffOnStart.Checked = bBuffOnStart;
                chkUseBeers.Checked = bUseBeers;
                chkUseRares.Checked = bUseRares;
                chkUseBuffBot.Checked = bUseBuffBot;
                chkLogOff.Checked = bLogOff;
                chkUseCharge.Checked = bUseCharge;
                chkEquip.Checked = bEquip;
                chkUseWeb.Checked = bUseWeb;
                chkRareAllegChan.Checked = bRareAllegChan;
                chkRareTradeChan.Checked = bRareTradeChan;
                chkEnableMail.Checked = bEnableMail;
                chkEnterSpamMail.Checked = bEnterSpamMail;
                chkEnterSpamRare.Checked = bEnterSpamRare;

                
            }
            catch (Exception ex) { Util.LogError(ex); }


        }




    }


}