
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

            private DateTime BuffingTime;
            private DateTime DotheBuffTime;

  
        public void buff()
        {
            try
            {
                if(msecondarysubRoutine.Contains("Buffing"))
                {
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
                             bSpellsinUse = false;
                             checkBuffs();
                             if (!bSpellsinUse)
                             {
                                  initiateBuffingSequence();
                             }
                             else { return; }



                         }

                         catch (Exception ex) { Util.LogError(ex); }
                         //  Core.Actions.SetCombatMode(CombatState.Peace);
                     }
                }
                    
            }
                                        catch (Exception ex) { Util.LogError(ex); }

            }

                private void initiateBuffingSequence()
                {
                    if (msecondarysubRoutine.Contains("Buffing"))
                    {
                    try
                    {
                        if (spellsList != null)
                        { 
                            Util.WriteToChat("I am in initiateBuffingSequence.");
                            BuffingTime = DateTime.Now;
                            mroutine = "buffing";
                            buffPending = true;
                            msubRoutine = "buffing";
                            msecondarysubRoutine = "buffing";
                            Core.Actions.AutoWield(buffWand);
                            nbuffsCast = 0;
                            numBuffs = spellsList.Count();

                       //     Core.Actions.UseItem(buffWand, 0);
                            SubscribeBuffingEvents(); 
                         //   Util.WriteToChat("Number of spells in spells List " + numBuffs);
                        }
                        else { Util.WriteToChat("You must first create a spells list."); }
                        
                  
                    }
                    catch (Exception ex) { Util.LogError(ex); }
                }
                }

                public void SubscribeBuffingEvents()
                {
                    try
                    {
                       // Util.WriteToChat("I am in Subscribebuffingevents; nbuffsCast = " + nbuffsCast.ToString());
                        nSpellID = spellsList[nbuffsCast];
                        Core.Actions.SetCombatMode(CombatState.Magic);
                        bbuffCast = false;
 
                        CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Buff);

                        //BuffingTimer.Tick += BuffingTimer_Tick;
                        //BuffingTimer.Interval = 1000;
                        //BuffingTimer.Start();
                    }
                    catch (Exception ex) { Util.LogError(ex); }
                }

                //private void BuffingTimer_Tick(object sender, EventArgs BuffingTimer_Tick)
                //{
                //    try
                //    {
                //        Util.WriteToChat("I am in BuffingTimer_Tick; nbuffsCast = " + nbuffsCast.ToString());
                //        BuffingTime = DateTime.Now;
                //        CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_Buff);
                //     }



                //    catch (Exception ex) { Util.LogError(ex); }

                //}

                private void RenderFrame_Buff(object sender, EventArgs e)
                {
                    try
                    {

                        if ((DateTime.Now - BuffingTime).TotalMilliseconds > 500)
                        {

                            CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Buff);
                      //      Util.WriteToChat("I am in renderframe_Buff.");
                            Core.Actions.SetCombatMode(CombatState.Magic);

 

                            //CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_Buff);
                            DotheBuffTime = DateTime.Now;
                            CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_DoSpell);
                            //Core.Actions.CastSpell(nSpellID, Core.CharacterFilter.Id);

                            //  BuffingTimer.Stop();
                            //nSpellID = spellsList[nbuffsCast];
                           // Util.WriteToChat("I am in RenderFrame_Buff; nbuffsCast = " + nbuffsCast.ToString() + " and spellid: " + nSpellID.ToString());
                    //        //    waitForCast();
                        }
                    }

                    catch (Exception ex) { Util.LogError(ex); }

                }

                private void RenderFrame_DoSpell(object sender, EventArgs e)
                {
                    //code violation has to do with turning virindi on and it interfering at this point.  Next time buff need to look at the timers more closely.
                    if ((DateTime.Now - DotheBuffTime).TotalMilliseconds > 400)
                    {
                        try
                        {
                         //   CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_DoSpell);

                          //  Core.Actions.SetCombatMode(CombatState.Magic);
                            Core.Actions.CastSpell(nSpellID, Core.CharacterFilter.Id);
                        }


                        catch (Exception ex) { Util.LogError(ex); }
                    }

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
                            try
                            {
                                //if (!bSpellsinUse) { initiateBuffingSequence(); }
                                //else
                                //{
                                spellID = spellsList[i+3];
                                //   Util.WriteToChat(spellID.ToString());
                                if (Core.CharacterFilter.SpellBook.Contains(spellID))
                                {
                                    for (int k = 0; k < spellCount; k++)
                                    {
                                        if (Core.CharacterFilter.Enchantments[k].SpellId == spellID)
                                        {
                                            timeleft = Core.CharacterFilter.Enchantments[k].TimeRemaining;
                                            if (timeleft < 20)
                                            {
                                                bSpellsinUse = false;
                                                Util.WriteToChat("There are less than 20 minutes left; going to buff.");
                                                i = spellListCount;
                                                k = spellCount;
                                                msecondarysubRoutine = "Buffing";
                                                initiateBuffingSequence();
                                            }
                                            else
                                            {
                                                bSpellsinUse = true;
                                                Util.WriteToChat("There are at least 20 minutes left.");
                                                i = spellListCount;
                                                k = spellCount;
                                                checkEnhancements();
                                            }
                                        }

                                        else
                                        {
                                            //Enchantments no longer contains spell  
                                            Util.WriteToChat("This spell is not in list of enchantments.");
                                            bSpellsinUse = false;
                                            i = spellListCount;
                                            msecondarysubRoutine = "Buffing";
                                            break;
                                            
                                        }

                                    }//eof for int = k
                                } // eof if spellbook contains
                            } //eof second try
                            catch (Exception ex) { Util.LogError(ex); }
                        } // eof int i

                        if (!bSpellsinUse) { initiateBuffingSequence(); }
                        Util.WriteToChat("At the end of checkbuffs() bSpellsinUse: " + bSpellsinUse.ToString());
                        if (bSpellsinUse) { checkEnhancements(); }
                    }
                                          
                    catch (Exception ex) { Util.LogError(ex); }
 
                }
    }
}

