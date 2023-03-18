# NpcChatMod

Experimental Mod for Stardew Valley
Rewrite NPC Dialogue in realtime based on interactions with OpenAI ChatGPT

## Pre-Requisites

* Get an API key from OpenAI
* Run the mod once so that it generates a config under ...\Stardew Valley\Mods\NpcChatMod
* Replace the OpenAiBearerToken with your token
* Restart the mod

## Details

* Uses the POST /v1/edits endpoint:
https://platform.openai.com/docs/api-reference/edits/create

## Config
```json
{
  "OpenAiUrl": "https://api.openai.com",
  "OpenAiBearerToken": "{bearer token from OpenAI}",
  "OpenAiModel": "text-davinci-edit-001",
  "OpenAiInstruction": "Act as {characterName} from Stardew Valley talking to a farmer. We are {insideOrOutside} in {location}. When: {timeOfDay} in {season}. People other than us nearby are: {characters}. Limit the text to 3-7 sentences.",
  "OpenAiTemperature": 0.5,
  "OpenAiTimeoutMillis": 5000,
  "OpenAiEdits": "/v1/edits",
  "OpenAiMaxBufferSize": 1024
  "DialogueCharacterCount": 185
}
```
