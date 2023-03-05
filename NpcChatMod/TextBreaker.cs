using System;
using System.Text.RegularExpressions;


namespace NpcChatMod {
    public class TextBreaker {        

        const string SPLIT_REGEX = @"(?<=[.?!])";

        int characterCount = 512;

        public TextBreaker(int characterCount) {
            this.characterCount = characterCount;
        }

        public string[] breakAtPunctuation(string text) {

            // skip if we dont need to break it up!
            if (text.Length < characterCount) {
                return new string[] { text };
            }

            int paragraphCount = (text.Length / characterCount) + 1;
            int paragraphIdx = 0;
            string[] paragraphs = new string[paragraphCount];

            // split at punctuation            
            string[] sentences = Regex.Split(text, SPLIT_REGEX);

            paragraphs[paragraphIdx] = sentences[0];
            for (int i = 1; i < sentences.Length; i++) {

                // do we need this?...
                sentences[i] = sentences[i].Trim();
                if (sentences[i].Length <= 0) {
                    continue;
                }

                var sentenceLength = sentences[i].Length;
                var paragraphLength = paragraphs[paragraphIdx].Length;

                Console.WriteLine($"Paragraph Idx: {paragraphIdx}, sentence length: {sentenceLength}, paragraphLength: {paragraphLength}");

                // advance to the next paragraph if we've run out of characters
                if ((sentenceLength + paragraphLength) > characterCount) {
                    paragraphIdx++;
                    paragraphs[paragraphIdx] = sentences[i];
                } else {
                    paragraphs[paragraphIdx] = $"{paragraphs[paragraphIdx]} {sentences[i]}";
                } 

                Console.WriteLine($"Paragraph: --{paragraphs[paragraphIdx]}--");
            }

            paragraphs.
            return paragraphs;
        }

    }
    
}
