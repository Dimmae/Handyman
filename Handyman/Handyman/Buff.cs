
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

namespace Handyman
{
    public partial class PluginCore
    {
        
           public bool buffaction = false;
            public bool buffPending = false;
            public DateTime StartBuffAction = DateTime.MinValue;
            public int nSpellID = 0;
            public int Retries = 0;
            public int TimeSinceLastBuffing = 0;
            public bool bWillBuffed = false;
            public bool bWeapBuffed = false;
            public bool bStrengthBuffed = false;
            public bool bWeapTinkBuffed = false;
            public bool bManaCBuffed = false;
            public bool bMagicTinkBuffed = false;
            public bool bLockpickBuffed = false;
            public bool bLifeBuffed = false;
            public bool bItemTinkBuffed = false;
            public bool bItemBuffed = false;
            public bool bFocusBuffed = false;
            public bool bFletchingBuffed = false;
            public bool bEndurBuffed = false;
            public bool bCreatureBuffed = false;
            public bool bCoordBuffed = false;
            public bool bCookingBuffed = false;
            public bool bArmorTinkBuffed = false;
            public bool bAlchemyBuffed = false;
            public bool bArcaneBuffed = false;

  
        public void buff()
        {
            try
            {
               //if (bUseBuffBot)
                //{
                //    // buffBot();
                //}
                //else if (inCraft || inTrade)
                //{
                //    _queBuff = true;
                //}
                //else
                //{
                  //  base.Core.Filter<FileService>();
                     if (buffWand == 0)
                     {
                         Util.WriteToChat("Please go to settings file and setup wand for buffing.");
                     }
                     //else if (_buffList.Count <= 0)
                     //{
                     //    Util.WriteToChat("Error: No buffs defined!");
                     //}
                     else
                     {
                         try
                         {
                             bSpellsinUse = true;
                             checkBuffs();
                             if (!bSpellsinUse)
                             {
                                 mroutine = "buffing";
                                 buffPending = true;
                                 msubRoutine = "buffing";
                                 msecondarysubRoutine = "buffing";
                                 Core.Actions.AutoWield(buffWand);
                                 nbuffsCast = 0;
                                 initiateBuffingSequence();
                             }
                             else { return; }



                         }

                         catch (Exception ex) { Util.LogError(ex); }
                         //  Core.Actions.SetCombatMode(CombatState.Peace);
                     }
                    
            }
                                        catch (Exception ex) { Util.LogError(ex); }

            }

                private void initiateBuffingSequence()
                {
                    try
                    {
                        if (spellsList != null)
                        { numBuffs = spellsList.Count-1; SubscribeBuffingEvents(); Util.WriteToChat("Number of spells in spells List " + numBuffs);}
                        else { Util.WriteToChat("You must first create a spells list."); }
                        
                  
                    }
                    catch (Exception ex) { Util.LogError(ex); }
                }

                public void SubscribeBuffingEvents()
                {
                    try
                    {
                        BuffingTimer.Tick += BuffingTimer_Tick;
                        BuffingTimer.Interval = 1000;
                        BuffingTimer.Start();
                    }
                    catch (Exception ex) { Util.LogError(ex); }
                }

                private void BuffingTimer_Tick(object sender, EventArgs BuffingTimer_Tick)
                {
                    try
                    {
                        nSpellID = spellsList[nbuffsCast];
                        Core.Actions.SetCombatMode(CombatState.Magic);
                        Core.Actions.CastSpell(nSpellID, Core.CharacterFilter.Id);
                        waitForCast();
                    }

                    catch (Exception ex) { Util.LogError(ex); }

                }

                private void CharacterFilter_ActionComplete(object sender, EventArgs e)
                {

                }


                private void waitForCast()
                {
                    try{
                   //     Util.WriteToChat("I am in waitforcast");
                       }
                       catch (Exception ex) { Util.LogError(ex); }
 
                }

                private void processChatBuffing()
                {
                    if (bDrankBeer) { return; }
                    if (bTinkSucceeded) { return; }
                  //  if (bContinue) { return; }
                    if(msubRoutine.Contains("buffing") || msecondarysubRoutine.Contains("buffing"))
                    {
                    if (buffPending && nbuffsCast < numBuffs ) { Util.WriteToChat("I just cast " + nSpellID + ", " + "nbuffscast = " + nbuffsCast);  }
                    else 
                    { 
                        BuffingTimer.Stop(); 
                        buffPending = false; 
                        Util.WriteToChat("I have turned off buffing timer. nbuffsCast = " + nbuffsCast);
                        Core.Actions.SetCombatMode(CombatState.Peace);
                        msubRoutine = "";
                        msecondarysubRoutine = "";
                        mroutine = "";
                        checkFocus();

                    }
                    }

                }

                private void checkBuffs()
                {
                    try
                    {
                        int spellCount = Core.CharacterFilter.Enchantments.Count;
                        Util.WriteToChat("Number of active enchantments: " + spellCount.ToString());
                        for (int i = 0; i < spellCount; i++)
                        {
                            if (!bSpellsinUse) { return; }
                            int spellid = Core.CharacterFilter.Enchantments[i].SpellId;
                            int timeleft = Core.CharacterFilter.Enchantments[i].TimeRemaining;
                            bSpellsinUse = true;
                            //    Util.WriteToChat(timeleft.ToString());
                            switch (spellid)
                            {
                                case 4530:
                                    bCreatureBuffed = true;
                                    if (timeleft < 300) { bCreatureBuffed = false; bSpellsinUse = false; }
                                    Util.WriteToChat("timeleft: " + timeleft + ", bcreaturebuffed = " + bCreatureBuffed.ToString());
                                    break;
                                case 4305:
                                    bFocusBuffed = true;
                                    if (timeleft < 300) { bFocusBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4329:
                                    bWillBuffed = true;
                                    if (timeleft < 300) { bWillBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4582:
                                    bLifeBuffed = true;
                                    if (timeleft < 300) { bLifeBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4602:
                                    bManaCBuffed = true;
                                    if (timeleft < 300) { bManaCBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4564:
                                    bItemBuffed = true;
                                    if (timeleft < 300) { bItemBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4297:
                                    bCoordBuffed = true;
                                    if (timeleft < 300) { bCoordBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4299:
                                    bEndurBuffed = true;
                                    if (timeleft < 300) { bEndurBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4325:
                                    bStrengthBuffed = true;
                                    if (timeleft < 300) { bStrengthBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4512:
                                    bArmorTinkBuffed = true;
                                    if (timeleft < 300) { bArmorTinkBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4640:
                                    bWeapTinkBuffed = true;
                                    if (timeleft < 300) { bWeapTinkBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4592:
                                    bMagicTinkBuffed = true;
                                    if (timeleft < 300) { bMagicTinkBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4566:
                                    bItemTinkBuffed = true;
                                    if (timeleft < 300) { bItemTinkBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 5068:
                                    bItemTinkBuffed = true;
                                    if (timeleft < 300) { bItemTinkBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4506:
                                    bAlchemyBuffed = true;
                                    if (timeleft < 300) { bAlchemyBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4526:
                                    bCookingBuffed = true;
                                    if (timeleft < 300) { bCookingBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4586:
                                    bLockpickBuffed = true;
                                    if (timeleft < 300) { bLockpickBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4552:
                                    bFletchingBuffed = true;
                                    if (timeleft < 300) { bFletchingBuffed = false; bSpellsinUse = false; }
                                    break;
                                case 4510:
                                    bArcaneBuffed = true;
                                    if (timeleft < 300) { bArcaneBuffed = false; bSpellsinUse = false; }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception ex) { Util.LogError(ex); }

               }

                }
           
    }



