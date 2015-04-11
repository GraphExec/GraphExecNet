using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GraphExec.Tests.Framework
{
    public abstract class BaseTestClass
    {
        protected void ExpectException<TException>(Action test)
            where TException : Exception
        {
            try
            {
                test();

                Assert.Fail("Expected thrown exception {0}", new object[] { typeof(TException).Name });
            }
            catch (Exception ex)
            {
                var actual = ex.GetType().Name;
                var expected = typeof(TException).Name;

                if (!String.Equals(actual, expected, StringComparison.InvariantCulture))
                {
                    Assert.Fail("Expected exception {0} but caught {1}", new object[] { expected, actual });
                }
            }
        }

        protected void ExpectException<TException>(Func<Exception, bool> predicate, Action test)
            where TException : Exception
        {
            try
            {
                test();

                Assert.Fail("Expected thrown exception {0}", new object[] { typeof(TException).Name });
            }
            catch (Exception ex)
            {
                if (!predicate(ex))
                {
                    var actual = ex.GetType().Name;
                    var expected = typeof(TException).Name;

                    Assert.Fail("Expected exception {0} but caught {1}", new object[] { expected, actual });
                }
            }
        }
    }
}
