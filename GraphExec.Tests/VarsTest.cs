using GraphExec.Tests.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GraphExec.Tests
{
    [TestClass]
    public class VarsTest : BaseTestClass
    {
        //  TESTS Action
        //

        [TestMethod]
        public void Vars_HandleNull_Default()
        {
            Vars.HandleNull(2, HandleNull);
        }

        [TestMethod]
        public void Vars_HandleNull_NullHandle()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Action handle = null;
                Vars.HandleNull(2, handle);
            });
        }

        [TestMethod]
        public void Vars_HandleNull_NullVar()
        {
            ExpectException<AssertFailedException>(() =>
            {
                Vars.HandleNull<object>(null, HandleNull);
            });
        }

        //  TESTS Action<Type>
        //

        [TestMethod]
        public void Vars_HandleNullTVar_Default()
        {
            Vars.HandleNull(2, HandleNullTVar);
        }

        [TestMethod]
        public void Vars_HandleNullTVar_NullHandle()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Action<Type> handle = null;
                Vars.HandleNull(2, handle);
            });
        }

        [TestMethod]
        public void Vars_HandleNullTVar_NullVar()
        {
            ExpectException<AssertFailedException>(ex => ex != null && ex.Message.Contains("HandleNullTVar"),
            () =>
            {
                Vars.HandleNull<object>(null, HandleNullTVar);
            });
        }

        //  TESTS Action<THandleInfo>
        //

        [TestMethod]
        public void Vars_HandleNullInfo_Default()
        {
            Vars.HandleNull(2, new Object(), HandleNullInfo);
        }

        [TestMethod]
        public void Vars_HandleNullInfo_NullHandle()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Action<object> handle = null;
                Vars.HandleNull(2, new Object(), handle);
            });
        }

        [TestMethod]
        public void Vars_HandleNullInfo_NullVar()
        {
            ExpectException<AssertFailedException>(ex => ex != null && ex.Message.Contains("HandleNullInfo"),
            () =>
            {
                Vars.HandleNull<object, object>(null, new Object(), HandleNullInfo);
            });
        }

        //  TESTS Action<Type, THandleInfo>
        //

        [TestMethod]
        public void Vars_HandleNullTVarInfo_Default()
        {
            Vars.HandleNull(2, new Object(), HandleNullTVarInfo);
        }

        [TestMethod]
        public void Vars_HandleNullTVarInfo_NullHandle()
        {
            ExpectException<ArgumentNullException>(() =>
            {
                Action<Type, object> handle = null;
                Vars.HandleNull(2, new Object(), handle);
            });
        }

        [TestMethod]
        public void Vars_HandleNullTVarInfo_NullVar()
        {
            ExpectException<AssertFailedException>(ex => ex != null && ex.Message.Contains("HandleNullTVarInfo"),
            () =>
            {
                Vars.HandleNull<object, object>(null, new Object(), HandleNullTVarInfo);
            });
        }

        //  TEST HELPER METHODS
        //

        private void HandleNull()
        {
            Assert.Fail("HandleNull called successfully");
        }

        private void HandleNullTVar(Type variable)
        {
            Assert.IsNotNull(variable);
            Assert.Fail("HandleNullTVar called successfully");
        }

        private void HandleNullInfo(object info)
        {
            Assert.IsNotNull(info);
            Assert.Fail("HandleNullInfo called successfully");
        }

        private void HandleNullTVarInfo(Type variable, object info)
        {
            Assert.IsNotNull(variable);
            Assert.IsNotNull(info);
            Assert.Fail("HandleNullTVarInfo called successfully");
        }
    }
}
