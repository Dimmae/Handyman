using System;
using System.Collections.Generic;
using System.Text;
using VirindiViewService;
using MyClasses.MetaViewWrappers;
using System.Drawing;
using Decal.Adapter;
using Decal.Adapter.Wrappers;

namespace Handyman
{
    public partial class PluginCore
    {
        MyClasses.MetaViewWrappers.IView View;

        MyClasses.MetaViewWrappers.ICheckBox chkEnabled;
        MyClasses.MetaViewWrappers.ICheckBox chkArmorTink;
        MyClasses.MetaViewWrappers.ICheckBox chkItemTink;
        MyClasses.MetaViewWrappers.ICheckBox chkMagicItemTink;
        MyClasses.MetaViewWrappers.ICheckBox chkWeaponTink;
        MyClasses.MetaViewWrappers.ICheckBox chkSalvaging;
        //MyClasses.MetaViewWrappers.ICheckBox chkSetUst;
        //MyClasses.MetaViewWrappers.ICheckBox chkSetFocusingStone;
        MyClasses.MetaViewWrappers.ICheckBox chkAlchemy;
        MyClasses.MetaViewWrappers.ICheckBox chkCooking;
        MyClasses.MetaViewWrappers.ICheckBox chkFletching;
        MyClasses.MetaViewWrappers.ICheckBox chkLockpick;
       // MyClasses.MetaViewWrappers.ICheckBox chkTier4Rares;

        MyClasses.MetaViewWrappers.ICheckBox chkBuffsEnabled;
        MyClasses.MetaViewWrappers.ICheckBox chkBuffOnStart;
        MyClasses.MetaViewWrappers.ICheckBox chkUseBeers;
        MyClasses.MetaViewWrappers.ICheckBox chkUseRares;
        MyClasses.MetaViewWrappers.ICheckBox chkUseBuffBot;
        MyClasses.MetaViewWrappers.ICheckBox chkSetBuffingWand;
        MyClasses.MetaViewWrappers.ICheckBox chkUseLevelVIIBuffs;
        MyClasses.MetaViewWrappers.ICheckBox chkUseLevelVIIIBuffs;

        MyClasses.MetaViewWrappers.ICheckBox chkLogOff;
        //MyClasses.MetaViewWrappers.ICheckBox chkUseCharge;
        //MyClasses.MetaViewWrappers.ICheckBox chkEquip;
        //MyClasses.MetaViewWrappers.ICheckBox chkUseWeb;
        MyClasses.MetaViewWrappers.ICheckBox chkRareAllegChan;
        MyClasses.MetaViewWrappers.ICheckBox chkRareTradeChan;
        MyClasses.MetaViewWrappers.ICheckBox chkEnableMail;
        MyClasses.MetaViewWrappers.ICheckBox chkEnterSpamMail;
        MyClasses.MetaViewWrappers.ICheckBox chkEnterSpamRare;

        //MyClasses.MetaViewWrappers.ICheckBox chkCalcMajors;
        //MyClasses.MetaViewWrappers.ICheckBox chkJourneymanPet;
        //MyClasses.MetaViewWrappers.ICheckBox chkMasterPet;
        //MyClasses.MetaViewWrappers.ICheckBox chkArtisanPet;

        MyClasses.MetaViewWrappers.IStaticText skillCheckMajorInfo;
        MyClasses.MetaViewWrappers.IStaticText returnHelp;

        MyClasses.MetaViewWrappers.IList listMessages;
        MyClasses.MetaViewWrappers.IList listReturn;

     //   MyClasses.MetaViewWrappers.IButton btnCheckVersion;
        MyClasses.MetaViewWrappers.IButton btnClearMessages;
        MyClasses.MetaViewWrappers.IButton btnAddReturn;
        MyClasses.MetaViewWrappers.IButton btnRemoveReturn;

        MyClasses.MetaViewWrappers.ITextBox txtReturnTo;


        void ViewInit()
        {
            try
            {

                //Create view here
                View = MyClasses.MetaViewWrappers.ViewSystemSelector.CreateViewResource(PluginCore.host, "Handyman.Views.MainView.xml");
                VirindiViewService.ViewProperties myprop = new ViewProperties();


                chkEnabled = (MyClasses.MetaViewWrappers.ICheckBox)View["chkEnabled"];
                chkArmorTink = (MyClasses.MetaViewWrappers.ICheckBox)View["chkArmorTink"];
                chkItemTink = (MyClasses.MetaViewWrappers.ICheckBox)View["chkItemTink"];
                chkMagicItemTink = (MyClasses.MetaViewWrappers.ICheckBox)View["chkMagicItemTink"];
                chkWeaponTink = (MyClasses.MetaViewWrappers.ICheckBox)View["chkWeaponTink"];
                chkSalvaging = (MyClasses.MetaViewWrappers.ICheckBox)View["chkSalvaging"];
             //   chkSetUst = (MyClasses.MetaViewWrappers.ICheckBox)View["chkSetUst"];
                chkAlchemy = (MyClasses.MetaViewWrappers.ICheckBox)View["chkAlchemy"];
             //   chkSetFocusingStone = (MyClasses.MetaViewWrappers.ICheckBox)View["chkSetFocusingStone"];
                chkCooking = (MyClasses.MetaViewWrappers.ICheckBox)View["chkCooking"];
                chkFletching = (MyClasses.MetaViewWrappers.ICheckBox)View["chkFletching"];
                chkLockpick = (MyClasses.MetaViewWrappers.ICheckBox)View["chkLockpick"];
                //chkTier4Rares = (MyClasses.MetaViewWrappers.ICheckBox)View["chkTier4Rares"];

                chkBuffsEnabled = (MyClasses.MetaViewWrappers.ICheckBox)View["chkBuffsEnabled"];
                chkBuffOnStart = (MyClasses.MetaViewWrappers.ICheckBox)View["chkBuffOnStart"];
                chkUseBeers = (MyClasses.MetaViewWrappers.ICheckBox)View["chkUseBeers"];
                chkUseRares = (MyClasses.MetaViewWrappers.ICheckBox)View["chkUseRares"];
                chkUseBuffBot = (MyClasses.MetaViewWrappers.ICheckBox)View["chkUseBuffBot"];
                chkSetBuffingWand = (MyClasses.MetaViewWrappers.ICheckBox)View["chkSetBuffingWand"];
                chkUseLevelVIIBuffs = (MyClasses.MetaViewWrappers.ICheckBox)View["chkUseLevelVIIBuffs"];
                chkUseLevelVIIIBuffs = (MyClasses.MetaViewWrappers.ICheckBox)View["chkUseLevelVIIIBuffs"];

                chkLogOff = (MyClasses.MetaViewWrappers.ICheckBox)View["chkLogOff"];
                //chkUseCharge = (MyClasses.MetaViewWrappers.ICheckBox)View["chkUseCharge"];
                //chkEquip = (MyClasses.MetaViewWrappers.ICheckBox)View["chkEquip"];
                //chkUseWeb = (MyClasses.MetaViewWrappers.ICheckBox)View["chkUseWeb"];
                chkRareAllegChan = (MyClasses.MetaViewWrappers.ICheckBox)View["chkRareAllegChan"];
                chkRareTradeChan = (MyClasses.MetaViewWrappers.ICheckBox)View["chkRareTradeChan"];
                chkEnableMail = (MyClasses.MetaViewWrappers.ICheckBox)View["chkEnableMail"];
                chkEnterSpamMail = (MyClasses.MetaViewWrappers.ICheckBox)View["chkEnterSpamMail"];
                chkEnterSpamRare = (MyClasses.MetaViewWrappers.ICheckBox)View["chkEnterSpamRare"];

                //chkCalcMajors = (MyClasses.MetaViewWrappers.ICheckBox)View["chkCalcMajors"];
                //chkJourneymanPet = (MyClasses.MetaViewWrappers.ICheckBox)View["chkJourneymanPet"];
                //chkArtisanPet = (MyClasses.MetaViewWrappers.ICheckBox)View["chkArtisanPet"];
                //chkMasterPet = (MyClasses.MetaViewWrappers.ICheckBox)View["chkMasterPet"];

                listMessages = (MyClasses.MetaViewWrappers.IList)View["listMessages"];
                listReturn = (MyClasses.MetaViewWrappers.IList)View["listReturn"];

            //    btnCheckVersion = (MyClasses.MetaViewWrappers.IButton)View["btnCheckVersion"];
                btnClearMessages = (MyClasses.MetaViewWrappers.IButton)View["btnClearMessages"];
                btnAddReturn = (MyClasses.MetaViewWrappers.IButton)View["btnAddReturn"];
                btnRemoveReturn = (MyClasses.MetaViewWrappers.IButton)View["btnRemoveReturn"];

                txtReturnTo = (MyClasses.MetaViewWrappers.ITextBox)View["txtReturnTo"];

                chkEnabled.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnabled_Change);
                chkArmorTink.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkArmorTink_Change);
                chkItemTink.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkItemTink_Change);
                chkWeaponTink.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkWeaponTink_Change);
                chkMagicItemTink.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkMagicItemTink_Change);
                chkSalvaging.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSalvaging_Change);
                //chkSetUst.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSetUst_Change);
                //chkSetFocusingStone.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkSetFocusingStone_Change);
                chkAlchemy.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkAlchemy_Change);
                chkCooking.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkCooking_Change);
                chkFletching.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkFletching_Change);
                chkLockpick.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkLockpick_Change);
                //chkTier4Rares.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkTier4Rares_Change);

                chkBuffsEnabled.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffsEnabled_Change);
                chkBuffOnStart.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffOnStart_Change);
                chkUseBeers.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseBeers_Change);
                chkUseRares.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseRares_Change);
                chkUseBuffBot.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseBuffBot_Change);
                chkSetBuffingWand.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkBuffingWand_Change);
              //  chkUseLevelVIIBuffs.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseLevelVIIBuffs_Change);
             //   chkUseLevelVIIIBuffs.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseLevelVIIIBuffs_Change);

                
                chkLogOff.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkLogOff_Change);
                //chkUseCharge.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseCharge_Change);
                //chkEquip.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEquip_Change);
                //chkUseWeb.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkUseWeb_Change);
                chkRareAllegChan.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkRareAllegChan_Change);
                chkRareTradeChan.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkRareTradeChan_Change);
                chkEnableMail.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnableMail_Change);
                chkEnterSpamMail.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnterSpamMail_Change);
                chkEnterSpamRare.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkEnterSpamRare_Change);

                //chkCalcMajors.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkCalcMajors_Change);
                //chkJourneymanPet.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkJourneymanPet_Change);
                //chkArtisanPet.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkArtisanPet_Change);
                //chkMasterPet.Change += new EventHandler<MyClasses.MetaViewWrappers.MVCheckBoxChangeEventArgs>(chkMasterPet_Change);

            //    Decal.Adapter.CoreManager.Current.ItemSelected += new EventHandler<ItemSelectedEventArgs>(Current_ItemSelected);
                CraftbotMasterTimer.Tick += new EventHandler(CraftbotMasterTimer_Tick);
                Core.CharacterFilter.ActionComplete += new EventHandler(CharacterFilter_ActionComplete);
                Core.ChatBoxMessage += new EventHandler<Decal.Adapter.ChatTextInterceptEventArgs>(Core_ChatBoxMessage);
                Core.WorldFilter.EnterTrade += new EventHandler<Decal.Adapter.Wrappers.EnterTradeEventArgs>(WorldFilter_EnterTrade);
                Core.WorldFilter.AddTradeItem += new EventHandler<AddTradeItemEventArgs>(WorldFilter_AddTradeItem);
           //     Core.WindowMessage += new EventHandler<WindowMessageEventArgs>(Core_WindowMessage);


                //btnCheckVersion.Click += new EventHandler<MyClasses.MetaViewWrappers.MVControlEventArgs>(btnCheckVersion_Click);
                //btnClearMessages.Click += new EventHandler<MyClasses.MetaViewWrappers.MVControlEventArgs>(btnClearMessages_Click);
                //btnAddReturn.Click += new EventHandler<MyClasses.MetaViewWrappers.MVControlEventArgs>(btnAddReturn_Click);
                //btnRemoveReturn.Click += new EventHandler<MyClasses.MetaViewWrappers.MVControlEventArgs>(btnRemoveReturn_Click);

                //listMessages.Selected += new EventHandler<MVListSelectEventArgs>(listMessages_Selected);
                //listReturn.Selected += new EventHandler<MVListSelectEventArgs>(listReturn_Selected);


                //txtReturnTo.End += new EventHandler<MVTextBoxEndEventArgs>(txtReturnTo_End);

 


            }

            catch (Exception ex) { Util.LogError(ex); }

        }
    }
}
