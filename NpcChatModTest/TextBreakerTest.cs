using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpcChatMod;
using System;

namespace NpcChatModTest {
    [TestClass]
    public class TextBreakerTest {

        [TestMethod]
        public void TestSingleParagraph() {

            TextBreaker breaker = new TextBreaker(100);
            var input = "The quick brown fox jumped over the fence. He was a little short and knocked over the slat.";
            var paragraphs = breaker.breakAtPunctuation(input);

            Assert.AreEqual(1, paragraphs.Length);
            Assert.AreEqual(input, paragraphs[0]);

        }

        [TestMethod]
        public void TestDoubleParagraph() {

            TextBreaker breaker = new TextBreaker(50);
            var input = "The quick brown fox jumped over the fence. He was a little short and knocked over the slat.";
            var paragraphs = breaker.breakAtPunctuation(input);

            Assert.AreEqual(2, paragraphs.Length);
            Assert.AreEqual("The quick brown fox jumped over the fence.", paragraphs[0]);
            Assert.AreEqual("He was a little short and knocked over the slat.", paragraphs[1]);
        }

        [TestMethod]
        public void TestSentenceExceedsCharacterCount_IgnoresInFavorOfCompleteSentences() {

            TextBreaker breaker = new TextBreaker(10);
            var input = "The quick brown fox jumped over the fence. He was a little short and knocked over the slat.";
            var paragraphs = breaker.breakAtPunctuation(input);

            foreach(var paragraph in paragraphs) {
                Console.WriteLine($"Paragraph Returned: --{paragraph}--");
            }

            Assert.AreEqual(2, paragraphs.Length);
            Assert.AreEqual("The quick brown fox jumped over the fence.", paragraphs[0]);
            Assert.AreEqual("He was a little short and knocked over the slat.", paragraphs[1]);
        }

    }
}
