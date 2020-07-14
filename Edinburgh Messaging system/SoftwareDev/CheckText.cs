using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoftwareDev
{
    class CheckText
    {
        public string hyperlinkCheck(string strSource, string replace)
        {

            string[] test = Regex.Split(strSource, " "); // splits words up by space
            foreach (string entry in test) // for every word found
            {
                int index = 0;
                if (entry.Contains("www.") || entry.Contains("https")) // if it contains these characters
                {
                    index = strSource.IndexOf(entry); // get index of that word
                    strSource = strSource.Insert(index + entry.Length, replace);// add long word after index
                    strSource = strSource.Remove(index, entry.Length); // remove the URL
             
                }
            }
            return strSource;
        }

        public string wordTranslate(string strSource, string[] longWord, string[] shortWord)
        {
            for (int i = 0; i < longWord.Length; i++)
            {
                if (strSource.Contains(shortWord[i])) // if a word was found within the text
                {
                    int index = strSource.IndexOf(shortWord[i]) + shortWord[i].Length; // Index after the word
                    strSource = strSource.Insert(index, "" + longWord[i]);// add long word after index
                    strSource = strSource.Insert((index), "<"); // encapsulate the word
                    strSource = strSource.Insert((index + longWord[i].Length + 2), ">"); // encapsulate the word
                }

            }

            return strSource;
        }

    }
}
