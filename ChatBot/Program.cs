using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIMLbot;

namespace ChatBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application. Application.Run(new Chat("104","105","115"));
        /// </summary>
        /// 

        public static string userid = "104";
        public static string picpath = "";
        public static string username = "";
        public static string usertype = "";
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Startup());
        }
    }

    public class ChatBot
    {
        const string UserId = "szabist";
        private Bot AimlBot;
        private User myUser;

        public ChatBot()
        {
            AimlBot = new Bot();
            myUser = new User(UserId, AimlBot);
            Initialize();
        }

        // Loads all the AIML files in the \AIML folder         
        public void Initialize()
        {
            AimlBot.loadSettings();
            AimlBot.isAcceptingUserInput = false;
            AimlBot.loadAIMLFromFiles();
            AimlBot.isAcceptingUserInput = true;
        }

        // Given an input string, finds a response using AIMLbot lib
        public String getOutput(String input)
        {
            Request r = new Request(input, myUser, AimlBot);
            Result res = AimlBot.Chat(r);
            return (res.Output);
        }
    }
}
