using Newtonsoft.Json;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace NpcChatMod {

    public class AiEditRequest {
        public string model { get; set; }
        public string input { get; set; }
        public string instruction { get; set; }
        public double temperature { get; set; }
        public AiEditRequest(string model, string input, string instruction, double temperature) {
            this.model = model;
            this.input = input;
            this.instruction = instruction;
            this.temperature = temperature;
        }
    }
    public class AiEditResponseChoice {
        public string text { get; set; }

    }
    public class AiEditResponse {

        public AiEditResponseChoice[] choices { get; set; }
    }

    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod {

        private HttpClient Client;
        private ModConfig Config;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper) {

            this.Config = this.Helper.ReadConfig<ModConfig>();
            this.Client = new HttpClient();
            Client.BaseAddress = new Uri(this.Config.OpenAiUrl);
            Client.MaxResponseContentBufferSize = this.Config.OpenAiMaxBufferSize;
            Client.Timeout = TimeSpan.FromMilliseconds(this.Config.OpenAiTimeoutMillis);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Config.OpenAiBearerToken);

            helper.Events.Display.MenuChanged += this.OnMenuChanged;
        }


        /*****************
        ** Private methods
        ******************/

        private void OnMenuChanged(object sender, MenuChangedEventArgs e) {

            if (e.NewMenu is DialogueBox dialogue) {

                var newStack = new Stack<string>(dialogue.characterDialoguesBrokenUp.Count + 10);

                foreach (var dialogueStackItem in dialogue.characterDialoguesBrokenUp) {
                    var editedItem = this.getAIEdit(dialogue.characterDialogue.speaker.name, dialogueStackItem);
                    this.Monitor.Log($"Edited Item: {dialogueStackItem} --> {editedItem}", LogLevel.Debug);

                    var chunks = this.breakText(editedItem);
                    for (int i = chunks.Length - 1; i >= 0; i--) {
                        var chunk = chunks[i];
                        newStack.Push(chunk);
                    }
                }

                dialogue.characterDialoguesBrokenUp.Clear();
                dialogue.characterDialoguesBrokenUp = newStack;
            }
        }

        private string getAIEdit(string characterName, string input) {

            var model = this.Config.OpenAiModel;
            var instruction = this.Config.OpenAiInstruction.Replace("{characterName}", characterName);
            var payload = new AiEditRequest(model, input, instruction, this.Config.OpenAiTemperature);
            var payloadString = JsonConvert.SerializeObject(payload);
            var payloadContent = new StringContent(payloadString, System.Text.Encoding.UTF8, "application/json");

            try {
                HttpResponseMessage response = this.Client.PostAsync(this.Config.OpenAiEdits, payloadContent).GetAwaiter().GetResult();  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                if (response.IsSuccessStatusCode) {
                    // Parse the response body.
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var body = JsonConvert.DeserializeObject<AiEditResponse>(content);
                    if (body.choices.Length >= 1) {
                        var aiEdit = Regex.Replace(body.choices[0].text, @"\t|\r", "");
                        return aiEdit;
                    }
                }
                this.Monitor.Log($"API Error: {response.StatusCode} ({response.ReasonPhrase} {payloadContent})", LogLevel.Error);
            } catch (Exception e) {
                this.Monitor.Log($"API Exception: {e.Message} ({e})", LogLevel.Error);
            }
            return input;
        }

        string[] breakText(string input) {
            return input.Split(new[] { "\n\n" }, 25, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}