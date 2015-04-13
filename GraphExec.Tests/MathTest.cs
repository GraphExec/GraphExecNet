using GraphExec.Tests.Framework;
using GraphExec.Tests.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphExec.Tests
{
    [TestClass]
    public class MathTest : BaseTestClass
    {
        /// <summary>
        /// BaseBehaviorNode_Default()
        /// 
        /// Test the BehaviorNode and BehaviorProvider using an implementation of basic arithmetic
        /// </summary>
        [TestMethod]
        public void Math_Default()
        {
            // Arrange 1 + 2 - 3 * 4 / 2

            var head = new MathNode<MultiplyProvider, MultiplyOperationInfo>(new OperandInfo()
            {
                LHS = 3,
                RHS = 4
            });

            var parent = new MathNode<DivideProvider, DivideOperationInfo>(new OperandInfo()
            {
                LHS = 0d, // Uses excecution value from Head
                RHS = 2
            });

            var child = new MathNode<AddProvider, AddOperationInfo>(new OperandInfo()
            {
                LHS = 1,
                RHS = 2
            });

            var formula = new MathNode<SubtractProvider, SubtractOperationInfo>(new OperandInfo());
            // Formula Info uses values from Parent/Child
            formula.Head = head;
            formula.Parent = parent;
            formula.Child = child;

            // Act
            formula.Execute();

            // Assert = -3
            Assert.AreEqual(-3, formula.Value);
        }
    }
}
