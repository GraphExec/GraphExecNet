using GraphExec.Tests.Framework;
using GraphExec.Tests.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphExec.Tests
{
    [TestClass]
    public class BehaviorNodeTest : BaseTestClass
    {
        [TestMethod]
        public void BaseBehaviorNode_Default()
        {
            // Arrange 1 + 2 - 3 * 4 / 2

            var head = new MathNode<OperationProvider<MultiplyOperationInfo>, MultiplyOperationInfo>(new OperandInfo()
            {
                LHS = 3,
                RHS = 4
            });

            var parent = new MathNode<OperationProvider<DivideOperationInfo>, DivideOperationInfo>(new OperandInfo()
            {
                LHS = 0d, // Uses excecution value from Head
                RHS = 2
            });

            var child = new MathNode<OperationProvider<AddOperationInfo>, AddOperationInfo>(new OperandInfo()
            {
                LHS = 1,
                RHS = 2
            });

            var formula = new MathNode<OperationProvider<SubtractOperationInfo>, SubtractOperationInfo>(new OperandInfo());
            // formula info uses values from Parent/Child
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
