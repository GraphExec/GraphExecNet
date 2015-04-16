using GraphExec.Tests.Framework;
using GraphExec.Tests.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphExec.Tests
{
    [TestClass]
    public class MathTest : BaseTestClass
    {
        /// <summary>
        /// Test the BehaviorNode and BehaviorProvider using an implementation of basic arithmetic
        /// </summary>
        [TestMethod]
        public void BehaviorNode_Default()
        {
            // Arrange 1 + 2 - 3 * 4 / 2

            // Nodes [ [1 + 2] - [ [3 * 4] / 2] ]
            // parent = 3 * 4
            // left = 1 + 2
            // right = parent / 2
            // formula = left - right

            var parent = new BasicMathNode<MultiplyProvider, MultiplyOperationInfo>(new OperandInfo()
            {
                LHS = 3,
                RHS = 4
            });

            var left = new BasicMathNode<AddProvider, AddOperationInfo>(new OperandInfo()
            {
                LHS = 1,
                RHS = 2
            });

            var right = new BasicMathNode<DivideProvider, DivideOperationInfo>(new OperandInfo()
            {
                LHS = 0d, // Uses excecution value from Parent
                RHS = 2
            });

            var formula = new BasicMathNode<SubtractProvider, SubtractOperationInfo>(new OperandInfo());
            // Formula Info uses values from Right/Left nodes
            formula.Parent = parent;
            formula.Left = left;
            formula.Right = right;

            // Act
            formula.Execute();

            // Assert = -3
            Assert.AreEqual(-3, formula.Value);
        }

        /// <summary>
        /// Test the DataNode and DataProvider using an implementation of a basic geometric formula
        /// </summary>
        [TestMethod]
        public void DataNode_Default()
        {
            // Arrange ((PI) d^2) / 4

            // Nodes [ [ [PI] * [d^2] ] / 4 ]
            // parent = d^2
            // left = PI
            // right = left * parent
            // formula = right / 4

            const double POW = 2d;
            const double CONSTANT = 4d;
            var d = 3d;

            var parent = new BasicGeometryNode<PowerProvider, PowerOperationInfo>(new OperandInfo()
            {
                LHS = d,
                RHS = POW
            });

            var left = new PIDataNode();

            var right = new BasicGeometryNode<MultiplyProvider, MultiplyOperationInfo>(new OperandInfo());

            var formula = new BasicGeometryNode<DivideProvider, DivideOperationInfo>(new OperandInfo()
            {
                RHS = CONSTANT
            });

            formula.Parent = parent;
            formula.Left = left;
            formula.Right = right;

            // Act
            formula.Execute();

            // Assert = 7.0685834705770347865409476123789
            // see formula in Windows Calculator
            Assert.AreEqual(((System.Math.PI * System.Math.Pow(3,2)) / 4), formula.Value);
        }

        [TestMethod]
        public void Math_Long()
        {
            // Arrange ((PI) d^2) / 4 where d = 1 + 2 - 3 * 4 / 2

            // Nodes [ [ [PI] * [d^2] ] / 4 ]
            // parent = d^2
            // left = PI
            // right = left * parent
            // formula = right / 4

            const double POW = 2d;
            const double CONSTANT = 4d;

            var parentBasic = new BasicMathNode<MultiplyProvider, MultiplyOperationInfo>(new OperandInfo()
            {
                LHS = 3,
                RHS = 4
            });

            var leftBasic = new BasicMathNode<AddProvider, AddOperationInfo>(new OperandInfo()
            {
                LHS = 1,
                RHS = 2
            });

            var rightBasic = new BasicMathNode<DivideProvider, DivideOperationInfo>(new OperandInfo()
            {
                LHS = 0d, // Uses excecution value from Parent
                RHS = 2
            });

            var formulaBasic = new BasicMathNode<SubtractProvider, SubtractOperationInfo>(new OperandInfo());
            // Formula Info uses values from Right/Left nodes
            formulaBasic.Parent = parentBasic;
            formulaBasic.Left = leftBasic;
            formulaBasic.Right = rightBasic;

            var parent = new BasicGeometryNode<PowerProvider, PowerOperationInfo>(new OperandInfo()
            {
                RHS = POW
            });

            parent.Left = formulaBasic;

            var left = new PIDataNode();

            var right = new BasicGeometryNode<MultiplyProvider, MultiplyOperationInfo>(new OperandInfo());

            var formula = new BasicGeometryNode<DivideProvider, DivideOperationInfo>(new OperandInfo()
            {
                RHS = CONSTANT
            });

            formula.Parent = parent;
            formula.Left = left;
            formula.Right = right;

            // Act
            formula.Execute();

            // Assert = 7.0685834705770347865409476123789
            // [see formula in Windows Calculator]
            Assert.AreEqual(((System.Math.PI * System.Math.Pow((1 + 2 - 3 * 4 / 2), 2)) / 4), formula.Value);
        }
    }
}
