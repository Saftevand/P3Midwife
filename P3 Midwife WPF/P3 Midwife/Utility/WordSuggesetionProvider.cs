using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    class WordSuggesetionProvider
    {
        private List<string> FrequentlyUsedWords = new List<string>();
        private List<string> list = new List<string>();
        private List<string> dic = Filemanagement.DanishWordList;
        private List<Words> sentenceSuggestion = new List<Words>(5);


        public void AddWordToUsedWords(string _word)
        {
            FrequentlyUsedWords.Add(_word);
        }

        public List<string> GetSuggestions(string filter)
        {
            list.Clear();
            string text = filter;

            if (filter.Length < 1)
            {
                return list;
            }
            char last = filter[filter.Length - 1];

            if (last == '.' || last == ',')
            {
                return list;
            }
            if (filter.Count(x => x == ' ') > 1)
            {
                text = suggestWords(filter);
            }
            if (text.Length < 3)
            {
                return list;
            }
            if (last == ' ')
            {
                addWordToList(text.Remove(text.Length - 1));
                if (filter.Count(x => x == ' ') > 2)
                {
                    createSentence(filter);
                }

                string word = text.Remove(text.Length - 1);

                for (int i = 0; i < sentenceSuggestion.FindAll(x => x.word == word).Count(); i++)
                {
                    list.Add(sentenceSuggestion.Find(x => x.word == word).secWord);
                }
                return list;
            }

            rankList(FrequentlyUsedWords.FindAll(s => s.StartsWith(text)));

            if (list.Count < 5)
            {
                list.AddRange(dic.FindAll(s => s.StartsWith(text)));
            }

            list.Remove(text);

            return list;
        }

        private void rankList(List<string> usedWords)
        {
            List<Words> words = new List<Words>();

            foreach (string item in usedWords)
            {
                if (words.Exists(x => x.word == item))
                    words.Find(x => x.word == item).Rank++;
                else
                    words.Add(new Words(1, item));
            }

            words = words.OrderByDescending(x => x.Rank).ToList();

            for (int i = 0; i < 5; i++)
            {
                if (words.Count - 1 < i)
                {
                    break;
                }
                else
                    list.Add(words[i].word);
            }
        }

        private string suggestWords(string filter)
        {
            int index = 0;
            string subString;
            index = filter.LastIndexOf(' ', filter.Length - 2);
            subString = filter.Substring(index + 1);
            return subString;
        }

        private void addWordToList(string text)
        {
            text = text.ToLower();

            text = missSpelling(text);
            if (text != null)
            {
                FrequentlyUsedWords.Add(text);
            }
        }

        private string missSpelling(string word)
        {
            if (dic.Any(s => s.Contains(word)))
            {
                return word;
            }
            return null;
        }

        private void createSentence(string text)
        {
            int beginIndex = 0, endIndex = 0, lenght = text.Length;
            string first = null;
            string second = null;

            endIndex = text.LastIndexOf(' ', lenght - 2);
            beginIndex = text.LastIndexOf(' ', endIndex - 2);

            first = text.Substring(beginIndex + 1, endIndex - beginIndex - 1);

            beginIndex = endIndex;
            endIndex = text.LastIndexOf(' ');

            second = text.Substring(beginIndex + 1, endIndex - beginIndex - 1);

            if (sentenceSuggestion.Any(x => x.word == first && x.secWord == second))
            {
                sentenceSuggestion.Find(x => x.word == first && x.secWord == second).Rank++;
            }
            else
                sentenceSuggestion.Add(new Words(first, second, 1));
        }
    }
}
