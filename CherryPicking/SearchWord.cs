using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CherryPicking
{
    public class SearchWord
    {
        public string TextToSearch { get; set; }
        public string Keyword { get; set; }

        public SearchWord(string textToSearch, string keyword)
        {
            TextToSearch = textToSearch;
            Keyword = keyword;
        }
        
        public (int Start, int Length) FindByForEach(int startingPosition)
        {
            string currentText = String.Empty;
            int currentPosition = 1;

            foreach(char letter in TextToSearch.Substring(startingPosition))
            {
                currentText = (currentText.Length >= Keyword.Length ? currentText.Substring(1) : currentText) + letter;
                
                if (string.Equals(currentText, Keyword, StringComparison.CurrentCultureIgnoreCase))
                {
                    return (startingPosition + currentPosition - Keyword.Length, Keyword.Length);
                }

                currentPosition++;
            }

            return (0, 0);
        }

        public (int Start, int Length) FindByFor(int startingPosition)
        {
            var newSource = TextToSearch.Substring(startingPosition).ToLower();

            for (int i = 1; i < newSource.Length - Keyword.Length; i++)
            {
                if (newSource.Substring(i, Keyword.Length).ToLower().Equals(Keyword))
                {
                    return (startingPosition + i, Keyword.Length);
                }
            }

            return (0, 0);
        }

        public (int Start, int Length) FindByRegularExpression(int startingPosition)
        {
            Regex regex = new Regex(Keyword, RegexOptions.IgnoreCase);
            var result = regex.Match(TextToSearch.Substring(startingPosition));

            if (result.Index == 0)
            {
                startingPosition = 0;
            }

            return (startingPosition + result.Index, result.Length);
        }
        
        public (int Start, int Length) FindByIndexOf(int startingPosition)
        {
            return (TextToSearch.IndexOf(Keyword, startingPosition, StringComparison.CurrentCultureIgnoreCase), Keyword.Length);
        }

        public (int Start, int Length) FindByZip(int startingPosition)
        {
            var newSource = TextToSearch.Substring(startingPosition).ToLower();
            var withoutKeyword = newSource.Replace(Keyword, " ");
            var highlightDifferences = newSource.Zip(withoutKeyword, (a, b) => (a - b)).ToArray();
            var firstDifferencePosition = Array.FindIndex(highlightDifferences, x => x != 0);
            
            if (firstDifferencePosition == -1)
            {
                return (0, 0);
            }

            return (startingPosition + firstDifferencePosition, Keyword.Length);
        }

        public (int Start, int Length) FindBySplit(int startingPosition)
        {
            var newSource = TextToSearch.Substring(startingPosition).ToLower();

            var result = newSource.Split(new string[] { Keyword }, StringSplitOptions.None);

            if (newSource.Length == result[0].Length)
            {
                return (0, 0);
            }

            return (startingPosition + result[0].Length, Keyword.Length);
        }

        public (int Start, int Length) FindByFSharp(int startingPosition)
        {
            var newF = new FSharpPortableLibrary.SearchWord();

            var fSharpList = newF.CherryPickAll(TextToSearch.ToLower(), Keyword, startingPosition);

            if (fSharpList.Length == 0)
            {
                return (0, 0);
            }

            return (fSharpList[0], Keyword.Length);
        }
    }
}
