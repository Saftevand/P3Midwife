using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3_Midwife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Tests
{
    [TestClass()]
    public class WordSuggesetionProviderTests
    {
        [TestMethod()]
        public void GetSuggestionsTest()
        {
            WordSuggesetionProvider provider = new WordSuggesetionProvider();
            provider.GetSuggestions("Hej mit navn dan hej mit navn dan hej mit navn ");
            Assert.IsTrue(provider.GetSuggestions("Hej mit navn dan hej mit navn dan hej mit navn mit ").Any(x => x == "navn"));
        }
    }
}