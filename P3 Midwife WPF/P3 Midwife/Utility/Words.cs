using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Words
    {
        public int Rank;
        public string word;
        public string secWord;

        public Words(int _rank, string _word)
        {
            this.Rank = _rank;
            this.word = _word;
        }

        public Words(string _word, string _secWord, int _rank)
        {
            this.Rank = _rank;
            this.word = _word;
            this.secWord = _secWord;
        }


    }
}
