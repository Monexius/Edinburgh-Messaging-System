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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareDev
{

    public partial class MainWindow : Window
    {
        public static string[] shortWord = new string[255]; // declare class variables
        public static string[] longWord = new string[255];
        public static int wordCount = 0;
        public static string id = "";
        public static string subject = "";
        public static string to = "";
        public static string text = "";
        public static int messageType = 1;
        public static bool proceed = true;
        public static string ID = "";
        public static string replace = "<URL Quarantined>";
        public static int hashTags = 0;
        GenerateFile generate = new GenerateFile(); // declare other classes 
        checkInfo check = new checkInfo();
        List<string> tag = new List<string>();
        CheckText checkText = new CheckText();
        public List<string> setTag // enable the transfer of getters and setters
        {
            set { tag = value; }
        }
        public int getHash
        {
            get { return hashTags; }
        }
        public int setHash
        {
            set { hashTags = value; }
        }
        public string getReplace
        {

            get { return replace;  }
        }
        public string getID
        {

            get { return id; }
        }
        public string getTo
        {

            get { return to; }
        }
        public string getSubject
        {

            get { return subject; }
        }
        public string getText
        {

            get { return text; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void messageBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void trendBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void subjectBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void idBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void toBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { // 1 = SMS, 2 = Email, 3 = Tweet
            var incident = new Dictionary<int, string>(); // declare SIR List
            incident.Add(1, "Theft");
            incident.Add(2, "Staff Attack");
            incident.Add(3, "ATM Theft");
            incident.Add(4, "Raid");
            incident.Add(5, "Customer Attack");
            incident.Add(6, "Staff Abuse");
            incident.Add(7, "Bomb Threat");
            incident.Add(8, "Terrorism");
            incident.Add(9, "Suspicious Incident");
            incident.Add(10, "Intelligence");
            incident.Add(11, "Cash Loss");

            generate.checkFiles(); // create directories in order to store user input/ messages
            id = idBox.Text;   // get text from user input and assign to variables
            to = toBox.Text;
            subject = subjectBox.Text;
            text = messageBox.Text;
            
            check.checkMessageType(id); // checks for the ID lengths
            messageType = check.getMessageType; // checks for the first character of the ID to determine its type

            if (messageType == 1)
            {
                generate.getWords(longWord, shortWord); // gets abbreviations from the file
                if (!check.validateCharCount(messageType, text)) // if the character count is not valid for this type of message
                {
                    proceed = false;
                    MessageBox.Show("Max Chars Reached"); // output warning
                }
                else
                {
                    if (check.checkID(id) == true) // if ID is correct
                    {
                        ID = id;
                        text = checkText.wordTranslate(text, longWord, shortWord); // edit body text with abbreviations
                        MessageBox.Show(text); // output the message to the user
                        if (ID == "" || text == "" || to == "")
                        {
                            MessageBox.Show("Please fill out the ID, Bodytext & To fields");
                        }
                        else
                        {
                            generate.smssFile(ID, text); // creates a json file with the body text within it and stores it
                        }
                    }
                }
            }
            if (messageType == 2)
            {
                generate.getWords(longWord, shortWord); // get abbreviations from file
                if (!check.validateCharCount(messageType, text))// if the character count is not valid for this type of message
                {
                    proceed = false;
                    MessageBox.Show("Max Chars Reached"); // output warning
                }
                else
                {
                    if (check.checkID(id) == true) // if the ID is correct
                    {
                        ID = id;
                        text = checkText.wordTranslate(text, longWord, shortWord); // add abbreviations to the text
                        text = checkText.hyperlinkCheck(text, replace);// remove any URLs found
                        if (ID == "" || text == "" || subject == "" || to == "")
                        {
                            MessageBox.Show("Please fill out the ID, body text, subject & To fields");
                        }
                        else
                        {
                            MessageBox.Show(subject + "\n" + text); // output text to user
                            generate.emailFile(ID, to, subject, text); // create a json file with the text contained within it.
                        }
                       
                    }
                }
            }
            if (messageType == 3)
            {
                generate.getWords(longWord, shortWord); // get abbreviations from file
                if (!check.validateCharCount(messageType, text)) // if the character count is not valid for this type of message
                {
                    proceed = false;
                    MessageBox.Show("Max Chars Reached"); // output warning
                }
                else
                {
                    if (check.checkID(id) == true)// if ID is correct
                    {
                        ID = id;
                        text = checkText.wordTranslate(text, longWord, shortWord); // add abbreviations to the body text
                        generate.checkHashTag(text); // checks for any hashtags and adds them to a list & text file
                        tag = generate.getTags; // gets the tags from a file
                        if (ID == null || text == null || to == null)
                        {
                            MessageBox.Show("Please fill out the ID, body text & To fields");
                        }
                        else
                        {
                            MessageBox.Show(subject + "\n" + text); // outputs body text to user
                            string box = "";
                            foreach (string item in tag) // foreach tag found in text file
                            {
                                box = box + item.ToString();
                            }
                            MessageBox.Show(box); // output all the tags
                            generate.tweetFile(ID, text); // generate file with tags within it
                            trendBox.Clear(); // remove tags in the trend box
                            foreach (string item in tag) // output all tags to the screen
                            {
                                trendBox.Text += item.ToString() + Environment.NewLine;
                            }
                        }
                     
                    }
                }
            }
        }
    }
}
