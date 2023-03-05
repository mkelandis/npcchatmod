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
            var paragraphs = breaker.BreakAtPunctuation(input);

            Assert.AreEqual(1, paragraphs.Length);
            Assert.AreEqual(input, paragraphs[0]);

        }

        [TestMethod]
        public void TestDoubleParagraph() {

            TextBreaker breaker = new TextBreaker(50);
            var input = "The quick brown fox jumped over the fence. He was a little short and knocked over the slat.";
            var paragraphs = breaker.BreakAtPunctuation(input);

            Assert.AreEqual(2, paragraphs.Length);
            Assert.AreEqual("The quick brown fox jumped over the fence.", paragraphs[0]);
            Assert.AreEqual("He was a little short and knocked over the slat.", paragraphs[1]);
        }

        [TestMethod]
        public void TestSentenceExceedsCharacterCount_IgnoresInFavorOfCompleteSentences() {

            TextBreaker breaker = new TextBreaker(10);
            var input = "The quick brown fox jumped over the fence. He was a little short and knocked over the slat.";
            var paragraphs = breaker.BreakAtPunctuation(input);

            foreach(var paragraph in paragraphs) {
                Console.WriteLine($"Paragraph Returned: --{paragraph}--");
            }

            Assert.AreEqual(2, paragraphs.Length);
            Assert.AreEqual("The quick brown fox jumped over the fence.", paragraphs[0]);
            Assert.AreEqual("He was a little short and knocked over the slat.", paragraphs[1]);
        }

        [TestMethod]
        public void TestPunctuation() {

            TextBreaker breaker = new TextBreaker(5);
            var input = "The quick brown fox jumped over the fence? He was a little short and knocked over the slat!  What a putz.";
            var paragraphs = breaker.BreakAtPunctuation(input);

            Assert.AreEqual(3, paragraphs.Length);
            Assert.AreEqual("The quick brown fox jumped over the fence?", paragraphs[0]);
            Assert.AreEqual("He was a little short and knocked over the slat!", paragraphs[1]);
            Assert.AreEqual("What a putz.", paragraphs[2]);
        }

        [TestMethod]
        public void TestWithNewlines() {
            var input = "Ahoy there! It's nice to see young folk movin' in to the valley. It's not very common these days.\r\n\r\nI'm Willy, the local fisherman. I've lived here all my life.\r\n\r\nI've got to go now. I've got some fish to catch.";

            TextBreaker breaker = new TextBreaker(383);
            var paragraphs = breaker.BreakAtPunctuation(input);

            Assert.AreEqual(3, paragraphs.Length);
            Assert.AreEqual("Ahoy there! It's nice to see young folk movin' in to the valley. It's not very common these days.", paragraphs[0]);
            Assert.AreEqual("I'm Willy, the local fisherman. I've lived here all my life.", paragraphs[1]);
            Assert.AreEqual("I've got to go now. I've got some fish to catch.", paragraphs[2]);
        }

    }
}
