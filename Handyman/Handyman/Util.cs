
using System;
using System.Collections.Generic;
using System.Text;
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

            public void WriteToTrader(string message)
            {
                try
                {
                    Globals.Host.Actions.InvokeChatParser("/tell " + requestorName + ", " +  message);
                }
                catch (Exception ex) { Util.LogError(ex); }
            }





        

    }
}// end of util

