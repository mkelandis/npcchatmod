using StardewValley;
using System.Linq;

namespace NpcChatMod {

    public class GameInfo {
        public string season {
            get {
                return Game1.currentSeason;
            }
        }
        public string timeOfDay {
            get {
                return getDisplayTime(Game1.timeOfDay);
            }
        }
        public string dayOfMonth {
            get {
                return $"{Game1.dayOfMonth}";
            }
        }
        public string location {
            get {
                return getLocation(Game1.currentLocation.NameOrUniqueName);
            }
        }
        public string insideOrOutside {
            get {
                return Game1.currentLocation.IsOutdoors ? "Outside" : "Inside";
            }
        }
        public string getCharacters(string currentCharacter) {
            if (Game1.currentLocation.characters.ToArray().Length > 1) {
                return Game1.currentLocation.characters.Select(Npc => Npc.Name).Where(name => !name.Equals(currentCharacter)).Aggregate((s, sx) => s + ", " + sx);
            } else {
                return "";
            }
        }

        public string getDisplayTime(int timeOfDay) {
            var hours26 = timeOfDay / 100;
            var hours = hours26 > 24 ? hours26 - 24 : hours26 > 12 ? hours26 - 12 : hours26;
            var mins = timeOfDay % 100;
            var ampm = hours26 < 12 || hours26 >= 24 ? "am" : "pm";
            return $"{hours}:{mins:00} {ampm}";
        }

        public string getLocation(string location) {
            return CamelCaseBreaker.BreakCamelCaseAtHumps(location);    
        }
    }
}
