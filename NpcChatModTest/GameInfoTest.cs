using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpcChatMod;

namespace NpcChatModTest {
    [TestClass]
    public class GameInfoTest {

        [TestMethod]
        public void TestTimeTranslation() {

            GameInfo gameInfo = new GameInfo();

            var displayTime = gameInfo.getDisplayTime(600);
            Assert.AreEqual("6:00 am", displayTime);

            displayTime = gameInfo.getDisplayTime(1250);
            Assert.AreEqual("12:50 pm", displayTime);

            displayTime = gameInfo.getDisplayTime(1300);
            Assert.AreEqual("1:00 pm", displayTime);

            displayTime = gameInfo.getDisplayTime(2600);
            Assert.AreEqual("2:00 am", displayTime);
        }

    }
}
