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

## Config Example
```json
{
  "OpenAiUrl": "https://api.openai.com",
  "OpenAiBearerToken": "{bearer token from OpenAI}",
  "OpenAiModel": "text-davinci-edit-001",
  "OpenAiInstruction": "Act as {characterName} from Stardew Valley. The location is: {insideOrOutside} in {location}. The time of day is: {timeOfDay} in {season}. People other than us nearby: {characters}. Embellish the dialog. Limit the text to 3-7 sentences.",
  "OpenAiTemperature": 0.5,
  "OpenAiTimeoutMillis": 5000,
  "OpenAiEdits": "/v1/edits",
  "OpenAiMaxBufferSize": 1024
  "DialogueCharacterCount": 185
}
```
