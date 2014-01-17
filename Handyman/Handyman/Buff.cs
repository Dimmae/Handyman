
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
                        Core.Actions.SetCombatMode(CombatState.Peace);

                        BuffingTimer.Stop(); 
                        buffPending = false; 
                        Util.WriteToChat("I have turned off buffing timer. nbuffsCast = " + nbuffsCast);
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
                        int spellListCount = spellsList.Count;
                        int timeleft;
                        Util.WriteToChat("Number of active enchantments: " + spellCount.ToString());
                        Util.WriteToChat("Number of spells in spell list: " + spellListCount.ToString());
                        for (int i = 0; i < spellListCount; i++)
                        {
                            Util.WriteToChat("I am in check buffs and bspellsinuse: " + bSpellsinUse.ToString());
                            if (!bSpellsinUse) { return; }
                            int spellid = spellsList[i];
                            for (int k = 0; k < spellCount; k++)
                            {
                                if (Core.CharacterFilter.Enchantments[i].SpellId == spellid)
                                {
                                    timeleft = Core.CharacterFilter.Enchantments[spellid].TimeRemaining;
                                    if (timeleft < 300) { bSpellsinUse = false; }
                                    else { bSpellsinUse = true; }
                                }
                                else { bSpellsinUse = false; }
                            }
                        }
                    }
                                          
                    catch (Exception ex) { Util.LogError(ex); }
 
                }
    }
}

