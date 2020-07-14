using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareDev
{
    class GenerateFile
    {
        public static string mainPath = Directory.GetCurrentDirectory(); // assign a path for each directory
        public static string smsFile = mainPath + "\\SMS";
        public static string emailsFile = mainPath + "\\Emails";
        public static string tweetsFile = mainPath + "\\Tweets";
        public static string hashTagsFile = mainPath + "\\HashTags";
        public static string csvFile = mainPath + "\\textwords.csv";
        public static MainWindow main = new MainWindow();
        List<string> tags = new List<string>();

        public List<string> getTags
        {
            get { return tags; }
        }
     
        public void checkFiles() // if a directory does not exist, create one
        {
            if(!Directory.Exists(smsFile))
                Directory.CreateDirectory(smsFile);
            if (!Directory.Exists(emailsFile))
                Directory.CreateDirectory(emailsFile);
            if (!Directory.Exists(tweetsFile))
                Directory.CreateDirectory(tweetsFile);
            if (!Directory.Exists(hashTagsFile))
                Directory.CreateDirectory(hashTagsFile);
        }
        public void emailFile(string ID, string to, string subject, string text) // create a email.json file in its directory
        {
            JObject content = new JObject( // assign these attributes to the json file
                          new JProperty(ID),
                           new JProperty(to),
                          new JProperty(subject),
                          new JProperty(text));
            using (StreamWriter file = File.CreateText(emailsFile + "\\" + main.getID + ".json")) // get directory
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, content); // create file
            }
        }

        public void tweetFile(string ID, string text)
        {
            JObject content = new JObject(
                         new JProperty(ID), 
                         new JProperty(text));
            using (StreamWriter file = File.CreateText(tweetsFile + "\\" + main.getID + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, content);
            }
        }

        public void smssFile(string ID, string text)
        {
            JObject content = new JObject(
                         new JProperty(ID),
                         new JProperty(text));
            using (StreamWriter file = File.CreateText(smsFile + "\\" + main.getID + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, content);
            }
        }

        public string checkHashTag(string strSource)
        {
            tags.Clear();
            int hashTags = main.getHash;
            string n = Environment.NewLine; // new line variable
            string[] test = Regex.Split(strSource, " "); // split all the words in the text
            if (!File.Exists(hashTagsFile + "\\HashTags.txt")) // if a hashtags file does not exist, create one
            {
                StreamWriter file = new StreamWriter(hashTagsFile + "\\HashTags.txt");
                file.Close(); // used to create a file
            }
            using (StreamReader sr = new StreamReader(hashTagsFile + "\\HashTags.txt")) // read from hashtag file
            {
                string tag = "";
                while ((tag = sr.ReadLine()) != null)// for every line in file
                {
                    tags.Add(tag);// add to list
                    main.setHash = hashTags++; 
                }
                sr.Close();
                File.WriteAllText(hashTagsFile + "\\HashTags.txt", String.Empty);
            }
            StreamWriter write = new StreamWriter(hashTagsFile + "\\HashTags.txt"); // write to file
            for (int i = 0; i < test.Length; i++) // checks for all characters
            {
                if (test[i].Contains("#")) // if a word contains a hashtag
                {
                    tags.Add(test[i]); // add it to the list
                }
            }
            foreach (string item in tags) // write all found hashtags from the list to the hashtags text file
            {
                write.WriteLine(item);
            }
            main.setTag = getTags;
            write.Close();  //close file
           
            return strSource; // return the text source
        }

        public void getWords(string[] longWord, string[] shortWord)
        {
            StreamReader sr = new StreamReader(csvFile); // get abbreviations from .csv file
            string line = "";
            int counter = 0;
            line = "";
            while ((line = sr.ReadLine()) != null) // read line by line
            {
                string[] entries = line.Split(','); // split on comma
                shortWord[counter] = entries[0]; // assign the short word to its variable
                longWord[counter] = entries[1]; // assign the word description to its variable
                counter++;
            }
            foreach (var item in longWord)
            {
                Console.WriteLine(item); // writes all words to console
            }
        }
    }

  
}
