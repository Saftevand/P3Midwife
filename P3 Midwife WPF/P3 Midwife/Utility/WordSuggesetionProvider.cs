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
        private List<Words> sentenceSuggestion = new List<Words>();


        public void AddWordToUsedWords(string _word)
        {
            FrequentlyUsedWords.Add(_word);
        }

        public List<string> GetSuggestions(string filter)
        {
            if (filter.Length < 1)
            {
                return null;
            }
            char last = filter[filter.Length - 1];
            if (last == '.' || last == ',')
            {
                return null;
            }

            createSentence(filter);

            if (last == ' ' )
            {
                int lastIndex = filter.LastIndexOf(' ');
                int secLastIndex = filter.LastIndexOf(' ', lastIndex - 1) + 1;

                string word = filter.Substring(secLastIndex, lastIndex - secLastIndex);

                for (int i = 0; i < sentenceSuggestion.FindAll(x => x.word == word).Count(); i++)
                {
                    list.Add(sentenceSuggestion.Find(x => x.word == word).secWord);
                }
                return list;
            }

            string text = filter;
            if (!text.EndsWith(" "))
            {
                text = suggestWords(filter);
            }

            //list.AddRange(FrequentlyUsedWords.FindAll(s => s.StartsWith(text)));

            rankList(FrequentlyUsedWords.FindAll(s => s.StartsWith(text)));

            if (list.Count < 5)
            {
                list.AddRange(dic.FindAll(s => s.StartsWith(text) && s.Length < text.Length + 3));
            }

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
                if (words.Count-1 < i)
                {
                    break;
                }
                else
                    list.Add(words[i].word);
            }
        }

        private string suggestWords(string filter)
        {
            int beginIndex = 0, endIndex = 0;
            int index = 0;
            if (filter.Contains(' '))
            {
                index = filter.LastIndexOf(' ') + 1;

                int i = 0;
                while ((i = filter.IndexOf(' ', i)) != -1)
                {
                    if (FrequentlyUsedWords.Count == 0)
                    {
                        addWordToList(filter.Substring(0, i++));
                        beginIndex = i;
                    }
                    else
                    {
                        endIndex = i++ - beginIndex;
                        addWordToList(filter.Substring(beginIndex, endIndex));
                        beginIndex = i;
                    }
                }
            }
            return filter.Substring(index);
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
            int i = 0, beginIndex = 0, endIndex = 0;
            string first = null;
            string second = null;

            i = beginIndex = text.IndexOf(',') + 2;

            while ((i = text.IndexOf(' ', i)) != -1)
            {
                endIndex = i;

                if (first == null)
                {
                    first = text.Substring(beginIndex, endIndex-beginIndex);
                    beginIndex = i + 1;
                }
                else
                {
                    second = text.Substring(beginIndex, endIndex-beginIndex);
                    if (sentenceSuggestion.Any(x => x.word == first && x.secWord == second))
                    {
                        sentenceSuggestion.Find(x => x.word == first && x.secWord == second).Rank++;
                    }
                    else
                        sentenceSuggestion.Add(new Words(first, second, 1));
                    beginIndex = endIndex + 1;
                    first = null;
                }
                i++;
            }
        }
    }
}
