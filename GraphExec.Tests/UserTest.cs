using GraphExec.Tests.Framework;
using GraphExec.Tests.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GraphExec.Tests
{
    [TestClass]
    public class UserTest : BaseTestClass
    {
        [TestMethod]
        public void Login_Default()
        {
            // Arrange
            var login = new LoginNode(new LoginInfo()
            {
                UserName = "Guest",
                Access = "Password" // Note: NOT recommended as a real password
            });

            // Act
            login.Execute();
            var user = login.Value;

            // Assert
            Assert.AreEqual("Guest", user.Name);
            Assert.AreEqual("Guest@email.com", user.Email);

            Assert.IsNotNull(user.Permissions);
            Assert.IsTrue(user.Permissions.Any());
            Assert.AreEqual(5, user.Permissions.Count());

            Assert.IsTrue(user.Permissions.Contains("PerformMath"));
            Assert.IsTrue(user.Permissions.Contains("PerformAdd"));
            Assert.IsTrue(user.Permissions.Contains("PerformSubtract"));
            Assert.IsTrue(user.Permissions.Contains("PerformMultiply"));
            Assert.IsTrue(user.Permissions.Contains("PerformDivide"));
        }

        [TestMethod]
        public void Register_Default()
        {
            // Arrange
            var register = new RegistrationNode(new RegistrationInfo()
            {
                UserName = "Guest",
                Email = "Guest@email.com",
                Access = "Crypt|c"
            });

            // Act
            register.Execute();
            var user = register.Value;

            // Assert
            Assert.AreEqual("Guest", user.Name);
            Assert.AreEqual("Guest@email.com", user.Email);

            Assert.IsNotNull(user.Permissions);
            Assert.IsTrue(user.Permissions.Any());
            Assert.AreEqual(5, user.Permissions.Count());

            Assert.IsTrue(user.Permissions.Contains("PerformMath"));
            Assert.IsTrue(user.Permissions.Contains("PerformAdd"));
            Assert.IsTrue(user.Permissions.Contains("PerformSubtract"));
            Assert.IsTrue(user.Permissions.Contains("PerformMultiply"));
            Assert.IsTrue(user.Permissions.Contains("PerformDivide"));
        }
    }
}
