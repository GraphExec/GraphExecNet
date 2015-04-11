using GraphExec.Tests.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GraphExec.Tests
{
    [TestClass]
    public class ArgsTest : BaseTestClass
    {
        [TestMethod]
        public void Args_IsTrue_Default()
        {
            Args.IsTrue(() => 1 == 1);
        }

        [TestMethod]
        public void Args_IsFalse_Default()
        {
            Args.IsFalse(() => 1 < 1);
        }

        [TestMethod]
        public void Args_IsNotNull_Default()
        {
            var two = 2;

            Args.IsNotNull(() => two);
        }

        [TestMethod]
        public void Args_IsTrue_NullSelector()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Args.IsTrue(null);
            });
        }

        [TestMethod]
        public void Args_IsTrue_ResultIsFalse()
        {
            ExpectException<ArgumentException>(() =>
            {
                Args.IsTrue(() => false);
            });
        }

        [TestMethod]
        public void Args_IsFalse_NullSelector()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Args.IsFalse(null);
            });
        }

        [TestMethod]
        public void Args_IsFalse_ResultIsTrue()
        {
            ExpectException<ArgumentException>(() =>
            {
                Args.IsFalse(() => true);
            });
        }

        [TestMethod]
        public void Args_IsNotNull_NullSelector()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Args.IsNotNull<object>(null);
            });
        }

        [TestMethod]
        public void Args_IsNotNull_NullMember()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Args.IsNotNull<object>(() => null);
            });
        }

        [TestMethod]
        public void Args_IsNotNull_NullConstant()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Args.IsNotNull(() => (new Object()).GetType().Name);
            });
        }
    }
}
