using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace NpcChatMod {
    public class TextBreaker {        

        readonly string SPLIT_REGEX = @"(?<=[.?!])";
        readonly int characterCount = 512;

        public TextBreaker(int characterCount) {
            this.characterCount = characterCount;
        }

        public string[] BreakAtPunctuation(string text) {

            List<string> paragraphs = new List<string>();

            // split at punctuation            
            string[] sentences = Regex.Split(text, SPLIT_REGEX);
            string currentParagraph = sentences[0].Trim();

            for (int i = 1; i < sentences.Length; i++) {

                var currentSentence = sentences[i];
                var currentSentenceTrimmed = String.Copy(sentences[i]).Trim();
                if (currentSentenceTrimmed.Length <= 0) {
                    continue;
                }

                var sentenceLength = currentSentenceTrimmed.Length;
                var paragraphLength = currentParagraph.Length;

                // advance to the next paragraph if we've run out of characters
                if ((sentenceLength + paragraphLength) > characterCount || currentSentence.StartsWith("\r\n")) {
                    paragraphs.Add(currentParagraph);
                    currentParagraph = currentSentenceTrimmed;
                } else {
                    currentParagraph = $"{currentParagraph} {currentSentenceTrimmed}";
                } 
            }

            paragraphs.Add(currentParagraph);
            return paragraphs.ToArray();
        }
    }    
}
