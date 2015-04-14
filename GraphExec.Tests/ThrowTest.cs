using GraphExec.Tests.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GraphExec.Tests
{
    [TestClass]
    public class ThrowTest : BaseTestClass
    {
        [TestMethod]
        public void Throw_Default()
        {
            object robot = new Object();
            Throw.Exception<NullReferenceException>(() => robot == null);

            Assert.IsNotNull(robot);
        }

        [TestMethod]
        public void Throw_ThrowConditionMet()
        {
            this.ExpectException<NullReferenceException>(() =>
            {
                object robot = null;
                Throw.Exception<NullReferenceException>(() => robot == null);
            });
        }

        [TestMethod]
        public void Throw_NullMessage_ThrowConditionMet()
        {
            this.ExpectException<NullReferenceException>(() =>
            {
                object robot = null;
                Throw.Exception<NullReferenceException>(() => robot == null, null);
            });
        }
    }
}
