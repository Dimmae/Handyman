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
    using WindowsTimer = System.Windows.Forms.Timer;



namespace Handyman
{
    public partial class PluginCore
    {
        WindowsTimer enhanceTimer = new WindowsTimer();

        private bool bRareinUse;
        private bool bSmithyinUse;  //armor tinking by 250
        private bool bImbuerinUse;  //magic item
        private bool bTinkerinUse;  //Item Tinking
        private bool bArtistinUse; //Weapon Tinking
        private bool bAlchemistinUse;
        private bool bCookinginUse;
        private bool bLockpickinUse;
        private bool bFletchinginUse;
        private bool bLugianinUse;  //Strength by 250
        private bool bUrsuininUse;  //Endurance by 250
        private bool bWayfarerinUse; //Coordination by 250
        private bool bMagusinUse; //Focus by 250
        private bool bBrighteyesinUse = false; //Coord + 50
        private bool bZongoinUse = false; //Strength + 50
        private bool bHunterinUse = false; //Endurance + 50
        private bool bKetnaninUse = false; //Focus beer + 50
        private bool bSpellsinUse = false; //adja's blessing
        private bool bMasterTinker = false; //all 5 tinking pieces + 19
        private bool bJourneyTinker = false; //4 tinking pieces
        private bool bApprenticeTinker = false; //3 tinking pieces
        private bool bNoviceTinker = false; //2 tinking pieces
        private bool charmedSmith; //5% chance of Imbue 
        private int ciandraFortune = 0; //25% extra salvage material;
        private int tinkerCount = 0; //number of times tinked
        private int tinkWork = 0;
        private int tinkTinked = 0;
        private double ChanceofSuccess = 0.0;
        private double salvageWork;
        private int skill;
        private int num = 0;
        private int num2 = 0;
        private int num3 = 0;
        private int num4 = 0;
        private int num5 = 0;
        private int num6 = 0;
        private int num7 = 0;
        private int num8 = 0;
        private int beer;
        private bool bDrankBeer = false;


        Decal.Filters.FileService fs = CoreManager.Current.FileService as Decal.Filters.FileService;

        string spellName = null;
        int spellID = 0;
        DateTime UseEnhance = DateTime.MinValue;



        public void checkEnhancements()
        {
            try
            {
                int spellCount = Core.CharacterFilter.Enchantments.Count;
              //  Util.WriteToChat("Number of active enchantments: " + spellCount.ToString());
                for (int i = 0; i < spellCount; i++)
                {
                    int spellid = Core.CharacterFilter.Enchantments[i].SpellId;
                    int timeleft = Core.CharacterFilter.Enchantments[i].TimeRemaining;
                    //    Util.WriteToChat(timeleft.ToString());
                    switch (spellid)
                    {
                        case 3530:
                            bKetnaninUse = true;
                            if (timeleft < 300) { bKetnaninUse = false; }
                            break;
                        case 3864:
                            bZongoinUse = true;
                            if (timeleft < 300) { bZongoinUse = false; }
                            break;
                        case 3863:
                            bHunterinUse = true;
                            if (timeleft < 300) { bHunterinUse = false; }
                            break;
                        case 3533:
                            bBrighteyesinUse = true;
                            if (timeleft < 300) { bBrighteyesinUse = false; }
                            break;
                        case 4899:
                            bMasterTinker = true;
                            break;
                        case 4794:
                            bJourneyTinker = true;
                            break;
                        case 4793:
                            bApprenticeTinker = true;
                            break;
                        case 4792:
                            bNoviceTinker = true;
                            break;
                            
                        //case 4530:
                        //     bSpellsinUse = true;
                        //     if (timeleft < 300) { bSpellsinUse = false; }
                        //     break;
                        default:
                            break;
                    }
                }
              //  Util.WriteToChat("Ketman: " + bKetnaninUse.ToString() + "; Zongo: " + bZongoinUse.ToString() + "; Hunter: " + bHunterinUse.ToString());
              //  Util.WriteToChat("bMasterTinker: " + bMasterTinker.ToString());
            }
            catch (Exception ex) { Util.LogError(ex); }

        }




        public void useEnhancement()
        {
            //  Util.WriteToChat("I am in useEnhancement");
            UseEnhance = DateTime.Now;
            Core.RenderFrame += new EventHandler<EventArgs>(RenderFrame_doEnhance);

        }

        private void RenderFrame_doEnhance(object sender, EventArgs e)
        {
            try
            {
                //  Util.WriteToChat("I am in renderframe_doenhance");
                if (bDrankBeer)
                {
                  //  Util.WriteToChat("msecondarysubroutine " + msecondarysubRoutine);
                    Core.RenderFrame -= RenderFrame_doEnhance;
                    if (msecondarysubRoutine.Contains("GetFocus")) { msecondarysubRoutine = "GeneralBeers"; checkBeers(); }
                    else if (msecondarysubRoutine.Contains("GeneralBeers")) { msecondarysubRoutine = "EquiptheBot"; clearBotOutfit(); }
                    //   moveToBotChores();

                }
                else if (((DateTime.Now - UseEnhance).TotalSeconds > 10) && !bDrankBeer)
                {
                    UseBeers();
                }

            }
            catch (Exception ex) { Util.LogError(ex); }

        }

        private void UseBeers()
        {
            try
            {

                Core.Actions.UseItem(beer, 0);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        //public void ChanceofSuccess()
        //{

        //}

        public void getChanceofSuccess()
        {
          //  Util.WriteToChat("I have just entered chance of success.");
            try
            {
                // Core.CharFilterAttributeType
                //    if(Core.CharacterFilter.Enchantments[
                //         if (bCalcMajors)
                //        {
                //            num += 30;
                //            num2 += 30;
                //            num3 += 30;
                //            num4 += 30;
                //            num5 += 0x19;
                //            num6 += 0x19;
                //            num7 += 0x19;
                //            num8 += 0x19;
                //        }
                //        if (bArtisanPet)
                //        {
                //            num5 += 30;
                //            num6 += 30;
                //            num7 += 30;
                //            num8 += 30;
                //        }
                //        else if (bMasterPet)
                //        {
                //            num5 += 20;
                //            num6 += 20;
                //            num7 += 20;
                //            num8 += 20;
                //        }
                //        else if (bJourneymanPet)
                //        {
                //            num5 += 10;
                //            num6 += 10;
                //            num7 += 10;
                //            num8 += 10;
                //        }
                //        if (TrdObjects!=null)
                //        //??    bool flag = false;
                //            foreach (WorldObject obj2 in TrdObjects)
                //            {
                //                if (obj2.Name.Equals("Smithy's Crystal") || bSmithyinUse)
                //                {
                //                    num += 250;
                //                    bSmithyinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Tinker's Crystal") || bTinkerinUse)
                //                {
                //                    num2 += 250;
                //                    bTinkerinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Imbuer's Crystal") || bImbuerinUse)
                //                {
                //                    num3 += 250;
                //                    bImbuerinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Artist's Crystal") || bArtistinUse)
                //                {
                //                    num4 += 250;
                //                    bArtistinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Alchemist's Crystal") || bAlchemistinUse)
                //                {
                //                    num6 += 250;
                //                    bAlchemistinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Chef's Crystal") || bCookinginUse)
                //                {
                //                    num7 += 250;
                //                    bCookinginUse = true;
                //                }
                //                else if (obj2.Name.Equals("Fletcher's Crystal") || bFletchinginUse)
                //                {
                //                    num8 += 250;
                //                    bFletchinginUse = true;
                //                }
                //                else if (obj2.Name.Equals("Thief's Crystal") || bLockpickinUse)
                //                {
                //                    num5 += 250;
                //                    bLockpickinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Lugian's Pearl") || bLugianinUse)
                //                {
                //                    num4 += 0x7d;
                //                    bLugianinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Ursuin's Pearl") || bUrsuininUse)
                //                {
                //                    num += 0x7d;
                //                    bUrsuininUse = true;
                //                }
                //                else if (obj2.Name.Equals("Wayfarer's Pearl") || bWayfarerinUse)
                //                {
                //                    num2 += 0x7d;
                //                    num6 += 0x53;
                //                    num7 += 0x53;
                //                    num8 += 0x53;
                //                    num5 += 0x53;
                //                    bWayfarerinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Magus's Pearl") || bMagusinUse)
                //                {
                //                    num += 0x7d;
                //                    num2 += 0x7d;
                //                    num3 += 250;
                //                    num4 += 250;
                //                    num6 += 0x53;
                //                    num7 += 0x53;
                //                    num8 += 0x53;
                //                    num5 += 0x53;
                //                    bMagusinUse = true;
                //                }
                //                //else if (obj2.Name.Equals("Tusker Spit Ale") || bTuskerSpitinUse)
                //                //{
                //                //    num += 5;
                //                //    num2 += 5;
                //                //    num3 += 10;
                //                //    num4 += 5;
                //                //    num6 += 3;
                //                //    num7 += 3;
                //                //    num8 += 3;
                //                //    num5 += 3;
                //                //    bTuskerSpitinUse = true;
                //                //}
                //                //else if (obj2.Name.Equals("Amber Ape") || bAmberApeinUse)
                //                //{
                //                //    num2 += 5;
                //                //    num6 += 3;
                //                //    num7 += 3;
                //                //    num8 += 3;
                //                //    num5 += 3;
                //                //    bAmberApeinUse = true;
                //                //}
                //                else if (obj2.Name.Equals("Apothecary Zongo's Stout") || bZongoinUse)
                //                {
                //                    num4 += 5;
                //                    bZongoinUse = true;
                //                }
                //                else if (obj2.Name.Equals("Hunter's Stock Amber") || bHunterinUse)
                //                {
                //                    num += 5;
                //                    bHunterinUse = true;
                //                }
                //            }

                //             if ((chatCmd.Equals("Armor") || chatCmd.Equals("Item")) || (chatCmd.Equals("MagicItem") || chatCmd.Equals("Weapon")) && TinkObject != null && Salvages !=null)
                //             {
                //                 if (Salvages != null)
                //                 {
                //                     int numSalvages = Salvages.Count;
                //                     if (numSalvages > 0)
                //                     {
                //                         foreach (WorldObject salvage in Salvages)
                //                         {

                //                       //      string str = salvage.Name;
                //                             work = (double)salvage.Values(DoubleValueKey.SalvageWorkmanship);
                //                         }

                //                     }
                //                 }

                Util.WriteToChat("otinkobj: " + otinkobj.Name);
                tinkWork = (int)otinkobj.Values(LongValueKey.Workmanship);
                tinkerCount = (int)otinkobj.Values(LongValueKey.NumberTimesTinkered);
                 salvageWork = (double)osalvageobj.Values(DoubleValueKey.SalvageWorkmanship);
                 Util.WriteToChat("tinkerCount: " + tinkerCount.ToString() + ", tinkWork: " + tinkWork.ToString() + ", salvageWork: " + salvageWork.ToString() );

                getEffectiveSkill();
                Util.WriteToChat("Effective skill: " + skill.ToString());
               // ChanceofSuccess = calculateDifficulty(tinkWork, salvageWork, tinkerCount, skill);
                calculateDifficulty(tinkWork, salvageWork, tinkerCount, skill);
                //             }

            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private void getEffectiveSkill()
        {
            try{
            //   Util.WriteToChat("master tinking skill: " + bMasterTinker.ToString());
            switch (chatCmd)
            {
                case "Armor":
                    skill = base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering];  // +0x19; //+ num;
                    break;
                case "Weapon":
                    skill = base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering];// + 0x19 + num;
                    break;

            }
            if (bMasterTinker) { skill = skill + 19; }
            }
            catch (Exception ex) { Util.LogError(ex); }
 
        }

        //       public double calculateDifficulty(bool imbue, bool charmedSmith, int salvageType, int itemWork, double salvageQuality, int tinkerNum, int skill)
        public void calculateDifficulty(int tinkWork, double work, int tinkerCount, int skill)
        {
          //  Util.WriteToChat("I have just entered calculate difficulty");
            try
            {
                //if (((itemWork <= 0) || (itemWork > 10)) || ((tinkerNum < 0) || (tinkerNum > 10)))
                //{
                //    clsMessageWriter.Write_Split_Message(debugFile, string.Concat(new object[] { "Out of bounds! Returning a negative tinker value. Debug: itemWork:", itemWork, " tinkerNum: ", tinkerNum }));
                //    return -1.0;
                //}
                double num = 0.0;
                double num4 = 0.0;
                switch (tinkerCount)
                {
                    case 0:
                        num = 1.0;
                        break;

                    case 1:
                        num = 1.0;
                        break;

                    case 2:
                        num = 1.1;
                        break;

                    case 3:
                        num = 1.3;
                        break;

                    case 4:
                        num = 1.6;
                        break;

                    case 5:
                        num = 2.0;
                        break;

                    case 6:
                        num = 2.5;
                        break;

                    case 7:
                        num = 3.0;
                        break;

                    case 8:
                        num = 3.5;
                        break;

                    case 9:
                        num = 4.0;
                        break;

                    case 10:
                        num = 4.5;
                        break;

                    default:
                        //  clsMessageWriter.Write_Split_Message(debugFile, "Default case found for tinker success!");
                        num = 1.0;

                        break;
                }
                int num2 = 0;
                if (salvageWork < (double)tinkWork)
                {
                    num2 = 1;
                }
                else
                {
                    num2 = 2;
                }
                //double num3 = Math.Floor((double)((((5 * salvageType) + ((2 * tinkwork) * salvageType)) - (((salvageQuality * num2) * salvageType) / 5.0)) * num));
                double num3 = Math.Floor((double)(((2 * tinkWork) - (salvageWork * num2)) / 5.0) * num);
                num4 = Math.Floor((double)((1.0 - (1.0 / (1.0 + Math.Exp(0.03 * (skill - num3))))) * 100.0));
                Util.WriteToChat("num: " + num.ToString() + ", num2: " + num2.ToString() + ", num3: " + num3.ToString());
                ChanceofSuccess = num4;
            }
            catch (Exception ex) { Util.LogError(ex); }

        }
    }
}


        //            //                switch (str)
        //            //               {
        //            //               case "Alabaster":
        //            //               tinkerCount++;
        //            //               return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Armoredillo_Hide:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Bronze:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Ceramic:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Marble:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Peridot:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Reed_Shark_Hide:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Steel:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

        //            //    case (int)Salvages.Wool:
        //            //        tinkerCount++;
        //            //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case (int)Salvages.Yellow_Topaz:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case (int)Salvages.Zircon:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case (int)Salvages.Copper:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 15, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Ebony:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Gold:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 10, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Linen:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Moonstone:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Pine:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Porcelain:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Satin:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Silk:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Silver:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 15, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Teak:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case (int)Salvages.Agate:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Azurite:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Black_Opal:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Bloodstone:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Carnelian:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Citrine:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Fire_Opal:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Green_Garnet:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Hematite:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Lapis_Lazuli:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Lavender_Jade:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Malachite:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Opal:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Red_Jade:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Rose_Quartz:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Smokey_Quartz:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Sunstone:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case (int)Salvages.Aquamarine:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Black_Garnet:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Brass:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Emerald:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Granite:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Imperial_Topaz:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Iron:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Jet:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Mahogany:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Oak:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 10, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Red_Garnet:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.Velvet:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case (int)Salvages.White_Sapphire:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);
                    //}


                    ////string key = str;
                    ////if (key != null)
                    ////{
                    ////    int num16;
                    ////    if (_dictionaryCmds4 == null)
                    //    {
                    //        Dictionary<string, int> dictionary1 = new Dictionary<string, int>(0x34);
                    //        dictionary1.Add("Salvaged Alabaster", 0);
                    //        dictionary1.Add("Salvaged Armoredillo Hide", 1);
                    //        dictionary1.Add("Salvaged Bronze", 2);
                    //        dictionary1.Add("Salvaged Ceramic", 3);
                    //        dictionary1.Add("Salvaged Marble", 4);
                    //        dictionary1.Add("Salvaged Peridot", 5);
                    //        dictionary1.Add("Salvaged Reedshark Hide", 6);
                    //        dictionary1.Add("Salvaged Steel", 7);
                    //        dictionary1.Add("Salvaged Wool", 8);
                    //        dictionary1.Add("Salvaged Yellow Topaz", 9);
                    //        dictionary1.Add("Salvaged Zircon", 10);
                    //        dictionary1.Add("Salvaged Copper", 11);
                    //        dictionary1.Add("Salvaged Ebony", 12);
                    //        dictionary1.Add("Salvaged Gold", 13);
                    //        dictionary1.Add("Salvaged Linen", 14);
                    //        dictionary1.Add("Salvaged Moonstone", 15);
                    //        dictionary1.Add("Salvaged Pine", 0x10);
                    //        dictionary1.Add("Salvaged Porcelain", 0x11);
                    //        dictionary1.Add("Salvaged Satin", 0x12);
                    //        dictionary1.Add("Salvaged Silk", 0x13);
                    //        dictionary1.Add("Salvaged Silver", 20);
                    //        dictionary1.Add("Salvaged Teak", 0x15);
                    //        dictionary1.Add("Salvaged Agate", 0x16);
                    //        dictionary1.Add("Salvaged Azurite", 0x17);
                    //        dictionary1.Add("Salvaged Black Opal", 0x18);
                    //        dictionary1.Add("Salvaged Bloodstone", 0x19);
                    //        dictionary1.Add("Salvaged Carnelian", 0x1a);
                    //        dictionary1.Add("Salvaged Citrine", 0x1b);
                    //        dictionary1.Add("Salvaged Fire Opal", 0x1c);
                    //        dictionary1.Add("Salvaged Green Garnet", 0x1d);
                    //        dictionary1.Add("Salvaged Hematite", 30);
                    //        dictionary1.Add("Salvaged Lapis Lazuli", 0x1f);
                    //        dictionary1.Add("Salvaged Lavender Jade", 0x20);
                    //        dictionary1.Add("Salvaged Malachite", 0x21);
                    //        dictionary1.Add("Salvaged Opal", 0x22);
                    //        dictionary1.Add("Salvaged Red Jade", 0x23);
                    //        dictionary1.Add("Salvaged Rose Quartz", 0x24);
                    //        dictionary1.Add("Salvaged Smoky Quartz", 0x25);
                    //        dictionary1.Add("Salvaged Sunstone", 0x26);
                    //        dictionary1.Add("Salvaged Aquamarine", 0x27);
                    //        dictionary1.Add("Salvaged Black Garnet", 40);
                    //        dictionary1.Add("Salvaged Brass", 0x29);
                    //        dictionary1.Add("Salvaged Emerald", 0x2a);
                    //        dictionary1.Add("Salvaged Granite", 0x2b);
                    //        dictionary1.Add("Salvaged Imperial Topaz", 0x2c);
                    //        dictionary1.Add("Salvaged Iron", 0x2d);
                    //        dictionary1.Add("Salvaged Jet", 0x2e);
                    //        dictionary1.Add("Salvaged Mahogany", 0x2f);
                    //        dictionary1.Add("Salvaged Oak", 0x30);
                    //        dictionary1.Add("Salvaged Red Garnet", 0x31);
                    //        dictionary1.Add("Salvaged Velvet", 50);
                    //        dictionary1.Add("Salvaged White Sapphire", 0x33);
                    //        _dictionaryCmds4 = dictionary1;
                    //    }
                    //    if (_dictionaryCmds4.TryGetValue(key, out num16))
                    //    {

                    //switch (num16)
                    //{
                    //    case 0:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 1:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 2:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 3:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 4:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 5:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 6:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 7:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 8:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 9:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 10:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ArmorTinkering] + 0x19) + num);

                    //    case 11:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 15, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 12:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 13:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 10, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 14:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 15:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 0x10:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 0x11:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 0x12:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 0x13:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 20:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 15, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 0x15:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.ItemTinkering] + 0x19) + num2);

                    //    case 0x16:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x17:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x18:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x19:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x1a:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x1b:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x1c:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x1d:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 30:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x1f:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x20:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x21:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x22:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x23:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x24:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x25:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 0x19, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x26:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.MagicItemTinkering] + 50) + num3);

                    //    case 0x27:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 40:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x29:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x2a:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x2b:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x2c:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x2d:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x2e:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x2f:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 12, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x30:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 10, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x31:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 50:
                    //        tinkerCount++;
                    //        return calculateDifficulty(false, charmedSmith, 11, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);

                    //    case 0x33:
                    //        tinkerCount++;
                    //        return calculateDifficulty(true, charmedSmith, 20, tinkeringTarget.Values(LongValueKey.Workmanship), salvage.getWork(), tinkerCount, (base.Core.CharacterFilter.EffectiveSkill[CharFilterSkillType.WeaponTinkering] + 0x19) + num4);
                    //}
                    //    }
                    //}
        //            return 0.0;
        //        }
        //        int num10 = 100;
        //        int num11 = 0;
        //        foreach (string str2 in _SkillChecks)
        //        {
        //            if (str2 != null)
        //            {
        //                int length = str2.LastIndexOf('-');
        //                string s = str2.Substring(0, length);
        //                string str4 = str2.Substring(length + 1);
        //                if (str4.Equals("Armor Tinkering") && !Settings.Default.useArmorTinkering)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Item Tinkering") && !Settings.Default.useItemTinerking)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Magic Item Tinkering") && !Settings.Default.useMagicItemTinkering)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Weapon Tinkering") && !Settings.Default.useWeaponTinkering)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Alchemy") && !Settings.Default.useAlchemy)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Cooking") && !Settings.Default.useCooking)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Fletching") && !Settings.Default.useFletching)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Lockpick") && !Settings.Default.useLockpick)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                else if (str4.Equals("Rare Tier 4") && !Settings.Default.useTier4Rares)
        //                {
        //                    e(str4);
        //                    resetCraftingState();
        //                }
        //                int num13 = int.Parse(s);
        //                num11 = 0;
        //                if (str4.Equals("Alchemy"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Alchemy].Buffed + num6;
        //                }
        //                else if (str4.Equals("Arcane Lore"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.ArcaneLore].Buffed;
        //                }
        //                else if (str4.Equals("Assess Creature"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.AssessCreature].Buffed;
        //                }
        //                else if (str4.Equals("Assess Person"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.AssessPerson].Buffed;
        //                }
        //                else if (curSkill.Equals("Axe"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Axe].Buffed;
        //                }
        //                else if (str4.Equals("Bow"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Bow].Buffed;
        //                }
        //                else if (str4.Equals("Cooking"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Cooking].Buffed + num7;
        //                }
        //                else if (str4.Equals("Creature Enchantment"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.CreatureEnchantment].Buffed;
        //                }
        //                else if (str4.Equals("Crossbow"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Crossbow].Buffed;
        //                }
        //                else if (str4.Equals("Dagger"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Dagger].Buffed;
        //                }
        //                else if (str4.Equals("Deception"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Deception].Buffed;
        //                }
        //                else if (str4.Equals("Fletching"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Fletching].Buffed + num8;
        //                }
        //                else if (str4.Equals("Healing"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Healing].Buffed;
        //                }
        //                else if (str4.Equals("Item Enchantment"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.ItemEnchantment].Buffed;
        //                }
        //                else if (str4.Equals("Jump"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Jump].Buffed;
        //                }
        //                else if (str4.Equals("Leadership"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Leadership].Buffed;
        //                }
        //                else if (str4.Equals("Life Magic"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.LifeMagic].Buffed;
        //                }
        //                else if (str4.Equals("Lockpick"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Lockpick].Buffed + num5;
        //                }
        //                else if (str4.Equals("Loyalty"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Loyalty].Buffed;
        //                }
        //                else if (str4.Equals("Mace"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Mace].Buffed;
        //                }
        //                else if (str4.Equals("Magic Defense"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.MagicDefense].Buffed;
        //                }
        //                else if (str4.Equals("Mana Conversion"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.ManaConversion].Buffed;
        //                }
        //                else if (str4.Equals("Melee Defense"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.MeleeDefense].Buffed;
        //                }
        //                else if (str4.Equals("Missile Defense"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.MissileDefense].Buffed;
        //                }
        //                else if (str4.Equals("Run"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Run].Buffed;
        //                }
        //                else if (str4.Equals("Salvaging"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Salvaging].Buffed;
        //                }
        //                else if (str4.Equals("Spear"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Spear].Buffed;
        //                }
        //                else if (str4.Equals("Staff"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Staff].Buffed;
        //                }
        //                else if (str4.Equals("Sword"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Sword].Buffed;
        //                }
        //                else if (str4.Equals("Thrown Weapons"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.ThrownWeapons].Buffed;
        //                }
        //                else if (str4.Equals("Unarmed Combat"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.Unarmed].Buffed;
        //                }
        //                else if (str4.Equals("War Magic"))
        //                {
        //                    num11 = base.Core.CharacterFilter.Skills[CharFilterSkillType.WarMagic].Buffed;
        //                }
        //                else
        //                {
        //                    num11 = 0;
        //                }
        //                int num14 = (int)Math.Floor((double)((1.0 - (1.0 / (1.0 + Math.Exp(0.03 * (num11 - num13))))) * 100.0));
        //                if (num14 < num10)
        //                {
        //                    num10 = num14;
        //                }
        //            }
        //        }
        //        return num10;
        //    }
        //    catch (Exception exception)
        //    {
        //        clsMessageWriter.Write_Error(errorLogFile, exception);
        //        return 0.0;
        //    }
        //    return 0.0;
        //}

 

    

    
//Commented out code
//                private void TimeLeft(int spId)
//{
//   int timer = 0;
//   foreach(int i in Core.CharacterFilter.Enchantments.Count)
//   {
//      if(Core.CharacterFilter.Enchantments[i].SpellId == spId)
//      {
//         if(Core.CharacterFilter.Enchantments[i].TimeRemaining > Timer)
//         {
//            Timer = Core.CharacterFilter.Enchantments[i].TimeRemaining;
//         }
//      }
//   }
//}
        //      for (int i = 0;i<spellCount;i++)
          ////  foreach (int spellId in Core.CharacterFilter.SpellBook)
          //  {

          //     // Spell s = fs.SpellTable.GetById(spellId);
          //      spellID = s.Id;
          //       spellName = s.Name;
          //       if (spellID == 3530) { bKetnaninUse = true; }  //focus  -2142578610  TuskerSpitAle
          //       if (spellID == 3864) { bZongoinUse = true; }  //strength  -2147193651  Strength
          //       if (spellID == 3863) { bHunterinUse = true; } //endurance  -2147193657  Endurance
          //       if (spellID == 3533) { bBrighteyesinUse = true; }  //coord  AmberApe -2142578611
          //       //    if (spellName.Contains("Ketnan's Blessing")) { bKetnaninUse = true; }  //focus  -2142578610  TuskerSpitAle
          //   //    if (spellName.Contains("Brighteyes")) { bBrighteyesinUse = true; }  //coord  AmberApe -2142578611
          //   //    if (spellName.Contains("Hunter")) { bHunterinUse = true; } //endurance  -2147193657  Endurance
          //   //    if (spellName.Contains("Zongo")) { bZongoinUse = true; }  //strength  -2147193651  Strength
          //   //    if (spellName.Contains("Adja") || spellName.Contains("Incantation")) { bSpellsinUse = true; }
          //   ////    if(chatCmd != "Salvage"){ CoreManager.Current.Actions.UseItem(-2142578610, 0); }
          //       Util.WriteToChat("Ketman: " + bKetnaninUse.ToString() + "; Zongo: " + bZongoinUse.ToString() + "; Spells: " + bSpellsinUse.ToString());
          //  }



//    if (imbue)
//    {
//        num4 = Math.Ceiling((double)(num4 / 3.0));
//        if (charmedSmith)
//        {
//            num4 += 5.0;
//        }
//        if (num4 > 38.0)
//        {
//            num4 = 38.0;
//        }
//    }

