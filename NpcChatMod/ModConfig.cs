using System;


namespace NpcChatMod {
    class ModConfig {
        public string OpenAiUrl { get; set; } = "https://api.openai.com";
        public string OpenAiBearerToken { get; set; } = "{yourtoken}";
        public string OpenAiModel { get; set; } = "text-davinci-edit-001";
        public string OpenAiInstruction { get; set; } = "Act as {characterName} from Stardew Valley. The location is: {insideOrOutside} in {location}. The time of day is: {timeOfDay} in {season}. People other than us nearby: {characters}. Embellish the dialog. Limit the text to 3-7 sentences.";
        public double OpenAiTemperature { get; set; } = 0.5;
        public int OpenAiTimeoutMillis { get; set; } = 5000;
        public string OpenAiEdits { get; set; } = "/v1/edits";
        public long OpenAiMaxBufferSize { get; set; } = 1024;
        public int DialogueCharacterCount { get; set; } = 185;
    }
}
