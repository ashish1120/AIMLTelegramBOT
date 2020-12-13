using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AIMLbot;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace aimilconsole
{
    public class Rootobject
    {
        public bool ok { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public int update_id { get; set; }
        public Message message { get; set; }
    }

    public class Message
    {
        public int message_id { get; set; }
        public From from { get; set; }
        public Chat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public New_Chat_Participant new_chat_participant { get; set; }
        public New_Chat_Member new_chat_member { get; set; }
        public New_Chat_Members[] new_chat_members { get; set; }
        public Left_Chat_Participant left_chat_participant { get; set; }
        public Left_Chat_Member left_chat_member { get; set; }
        public Reply_To_Message reply_to_message { get; set; }
    }

    public class From
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string language_code { get; set; }
    }

    public class Chat
    {
        public long id { get; set; }
        public string title { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }

    public class New_Chat_Participant
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }

    public class New_Chat_Member
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }

    public class Left_Chat_Participant
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
    }

    public class Left_Chat_Member
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
    }

    public class Reply_To_Message
    {
        public int message_id { get; set; }
        public From1 from { get; set; }
        public Chat1 chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
    }

    public class From1
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class Chat1
    {
        public long id { get; set; }
        public string title { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }

    public class New_Chat_Members
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }
    public class A
    {
        public string message_text,sendmessage;
        public int update_id;
        
        private string urlString = "http://api.telegram.org/bot1113002139:AAER2cf-x9qAKpu1UQjvD7h-QeqacQ1Wtss/";
        public void a()
        {
            var webclient = new WebClient();
            string urljson = webclient.DownloadString(urlString+ "getUpdates?chat_id=@help_ashish&offset=-1");
            var resultcollection = new JavaScriptSerializer().Deserialize<Rootobject>(urljson);
            // count = resultcollection.result.Count();
            message_text = resultcollection.result[0].message.text;
            update_id = resultcollection.result[0].update_id;

        }
        public void b()
        {
            WebClient webclient = new WebClient();
            webclient.DownloadString(urlString+ "sendMessage?chat_id=@help_ashish&text="+ sendmessage + "&parse_mode=html");

        }
    }

    class Program
    {
        private static int limit = 0;
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;
            try
            {
                Bot AI = new Bot(); // This defines the object "AI" To hold the bot's infomation
                AI.loadSettings();
                AI.loadAIMLFromFiles();
                AI.isAcceptingUserInput = false;
                User myUser = new User("Username", AI);
                AI.isAcceptingUserInput = true;
            Start:
                A ab = new A();
                ab.a();
                if (limit <= ab.update_id)
                {
                    if (ab.message_text.ToLower().Trim()!= "department"&&  ab.message_text.ToLower().Trim()!= "help"&& ab.message_text.ToLower().Trim() != "token"&& ab.message_text.ToLower().Trim() != "counter")
                    {
                        Console.WriteLine("user said:" + ab.message_text);
                        Request r = new Request(ab.message_text, myUser, AI); // 

                        Console.WriteLine("Robot: " + AI.Chat(r).Output);
                        ab.sendmessage = "Robot: " + AI.Chat(r).Output;
                        ab.b();
                        limit = ab.update_id + 1;

                    }
                }
                goto Start;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
      }
