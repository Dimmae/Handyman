
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using VirindiViewService;
using MyClasses.MetaViewWrappers;
using System.Windows.Forms;
using System.Drawing;
using Decal.Adapter;
using Decal.Adapter.Wrappers;
using System.Xml;



namespace Handyman
{
    public partial class PluginCore : PluginBase
    {
        private DateTime traderreplytime;
        public static class Util
        {
            public static void LogError(Exception ex)
            {
                try
                {
                 using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Decal Plugins\Handyman" +  " errors.txt", true))
                   // using (StreamWriter writer = new StreamWriter(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Decal Plugins\" + Globals.PluginName + " errors.txt", true))
                    {
                        writer.WriteLine("============================================================================");
                        writer.WriteLine(DateTime.Now.ToString());
                        writer.WriteLine("Error: " + ex.Message);
                        writer.WriteLine("Source: " + ex.Source);
                        writer.WriteLine("Stack: " + ex.StackTrace);
                        if (ex.InnerException != null)
                        {
                            writer.WriteLine("Inner: " + ex.InnerException.Message);
                            writer.WriteLine("Inner Stack: " + ex.InnerException.StackTrace);
                        }
                        writer.WriteLine("============================================================================");
                        writer.WriteLine("");
                        writer.Close();
                    }
                }
                catch
                {
                }
            }

            public static void WriteToChat(string message)
            {
                
                try
                {
                    Globals.Host.Actions.AddChatText("<{" + Globals.PluginName + "}>: " + message, 5);
                }
                catch (Exception ex) { LogError(ex); }
            }



        }

        public void WriteToFellow(string message)
        {
            try
            {
                Globals.Host.Actions.InvokeChatParser("/f "  + message);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        //public void WriteToTrader(string message)
        //{
        //    try
        //    {
        //        Util.WriteToChat("I am in function to write a message to client.");
        //        Globals.Host.Actions.InvokeChatParser("/tell " + requestorName + ", " + message);
        //    }
        //    catch (Exception ex) { Util.LogError(ex); }
        //}




        public void WriteToTrader(string message)
            {
                Util.WriteToChat("I am in message maker for teller");
                try
                {
                  //  botMess = message;
                  bMessGiven = false;
                  requestorName = requestorName.Trim();
                  //  requestorName = requestorName.Trim() + ", ";
                    Util.WriteToChat("I am in function to write to trader");
                //    Util.WriteToChat(botMess);
                    doMessage();
                  //  Globals.Host.Actions.InvokeChatParser("/tell " + tellWho + ", " + botMessage);
 
            //   //  Globals.Host.Actions.InvokeChatParser("/tell " + requestorName + ", " + botMessage);


                }
                catch (Exception ex) { Util.LogError(ex); }
            }

        private void doMessage()
        {
            if (!bMessGiven)
            {
                traderreplytime = DateTime.Now;

                CoreManager.Current.RenderFrame += new EventHandler<EventArgs>(RenderFrame_WriteReplytoTrader);
            }
            else { botMess = null; return; }

        }
        private void RenderFrame_WriteReplytoTrader(object sender, EventArgs e)
        {
            try
            {
              if ((DateTime.Now - traderreplytime).TotalSeconds > 1)
                {
                    CoreManager.Current.RenderFrame -= new EventHandler<EventArgs>(RenderFrame_WriteReplytoTrader);
                    Util.WriteToChat(requestorName);

                    string message = "/tell " + requestorName + ", " + botMess;
                    Util.WriteToChat(message);
                    try { Host.Actions.InvokeChatParser("/tell " + requestorName + ", " + botMess); }
                    catch (Exception ex) { Util.LogError(ex); }

                    doMessage();
                }
            }
            catch (Exception ex) { Util.LogError(ex); }


        }

    }
}// end of util

