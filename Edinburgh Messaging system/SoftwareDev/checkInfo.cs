using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareDev
{
    class checkInfo
    {
        public static string text = "";
        public static int messageType = 0;
         // variables that any class can have access to
        public int getMessageType
        {
            get { return messageType; }
        }
        public int setMessageType
        {
            set { messageType = value; }
        }
        public int getText
        {
            get { return messageType; }
        }
        public int setText
        {
            set { messageType = value; }
        }

        public int checkWordCount(string strSource) // check how many words within given text
        {

            int wordCount = 0;
            int index = 0;
      
                while (index < strSource.Length)
                {
                    // check if current char is part of a word
                    while (index < strSource.Length && !char.IsWhiteSpace(strSource[index]))
                        index++;
  
                    wordCount++;

                    // skip whitespace until next word
                    while (index < strSource.Length && char.IsWhiteSpace(strSource[index]) )
                        index++;
                }

            return wordCount;
        }
        public int checkCharCount(string strSource) // check how many characters are found within a text
        {
            int wordCount = 0;
            int index = 0;
            while (index < strSource.Length) // for each character in string
            {
                    index++;
                wordCount++; // add to character count
            }
            return wordCount;
        }

        public bool validateCharCount(int messageType, string text) // check if ammount of characters are valid
        {
            int charCount = checkCharCount(text);
            switch (messageType) // check the message type
            {
                case 1:
                    if (charCount < 141) // if SMS body text characters are less than 140
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 2:
                    if (charCount < 1029) // if Email body text character count is less than 1029
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 3:
                    if (charCount < 141) // if Tweet body text character count is less than 141
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
                    break;
            }

        }
        public void checkMessageType(string text) // checks for message type by looking at the ID
        {
            string sub = "";
            if (text.Length != 0)
            {
                sub = text.Substring(0, 1); // check first character
            }
            if (sub == "S")
            {
                messageType = 1;
                Console.WriteLine("SMS"); // if the message type is a SMS
            }
            if (sub == "E")
            {
                messageType = 2;
                Console.WriteLine("Email"); // if the message type is a Email
            }
            if (sub == "T")
            {
                messageType = 3;
                Console.WriteLine("Tweet");// if the message type is a Tweet
            }
            else
            {   
                MessageBox.Show("Incorrect Message Type"); // otherwise return error
            }
        }

        public bool checkID(string text) // checks the length of the ID
        {
            string su = text;
            if (su.Length > 10 || su.Length < 10) // if the Id is out of range
            {
                MessageBox.Show("ID is out of range"); // return warning
                return false;
            }
            else
            {
                string sub = text.Substring(1, 9);
                char[] numbers = sub.ToCharArray(); // count characters

                if (numbers.Length > 8 || numbers.Length < 8) // if ID is within range
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (char.IsNumber(numbers[i])) // if the characters are numbers
                        {
                            return true; // ID is acceptable
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Keep to 9 figure number after Element"); // otherwise return warning
                    return false;
                }
                return false;
            }
        }
    }

   
}
