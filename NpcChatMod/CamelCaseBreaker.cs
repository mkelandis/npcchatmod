using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NpcChatMod {
    public class CamelCaseBreaker {
        public static string BreakCamelCaseAtHumps(string text) {
            return Regex.Replace(text, @"([^A-Z])([A-Z])", "$1 $2").ToLower();
        }
    }
}
