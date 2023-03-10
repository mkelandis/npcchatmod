using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpcChatMod;

namespace NpcChatModTest {
    [TestClass]
    public class CamelCaseBreakerTest {

        [TestMethod]
        public void TestBreakCamelCaseAtHumps() {
            Assert.AreEqual("this is spaced", CamelCaseBreaker.BreakCamelCaseAtHumps("ThisIsSpaced"));
        }

    }
}
