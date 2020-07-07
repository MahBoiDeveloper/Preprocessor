using System;

namespace ELW.Library.Math.Calculators.Standard
{
    internal sealed class CalculatorAddition : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 2)
                throw new ArgumentException("It is binary operation. Parameters count should be equal to 2.", "parameters");
            
            return parameters[0] + parameters[1];
        }

        #endregion
    }

    internal sealed class CalculatorConditional : IOperationCalculator
    {
        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 3)
                throw new ArgumentException("It is ternary operation. Parameters count should be equal to 3.", "parameters");
            
            return (parameters[0] >= 0 ? parameters[1] : parameters[2]);
        }

        #endregion
    }

    internal sealed class CalculatorCosinus : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is function with 1 parameter. Parameters count should be equal to 1.", "parameters");
            
            return System.Math.Cos(parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorDivision : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 2)
                throw new ArgumentException("It is binary operation. Parameters count should be equal to 2.", "parameters");
            
            return parameters[0] / parameters[1];
        }

        #endregion
    }

    internal sealed class CalculatorMultiplication : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 2)
                throw new ArgumentException("It is binary operation. Parameters count should be equal to 2.", "parameters");
            
            return parameters[0] * parameters[1];
        }

        #endregion
    }

    internal sealed class CalculatorNegation : IOperationCalculator
    {
        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is unary operation. Parameters count should be equal to 1.", "parameters");
            
            return (-parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorPositivation : IOperationCalculator
    {
        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is unary operation. Parameters count should be equal to 1.", "parameters");
            
            return (parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorPowering : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 2)
                throw new ArgumentException("It is binary operation. Parameters count should be equal to 2.", "parameters");
            
            return System.Math.Pow(parameters[0], parameters[1]);
        }

        #endregion
    }

    internal sealed class CalculatorSinus : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is function with 1 parameter. Parameters count should be equal to 1.", "parameters");
            
            return System.Math.Sin(parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorSubtraction : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 2)
                throw new ArgumentException("It is binary operation. Parameters count should be equal to 2.", "parameters");
            
            return parameters[0] - parameters[1];
        }

        #endregion
    }

    internal sealed class CalculatorExponentiation : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is function with 1 parameter. Parameters count should be equal to 1.", "parameters");
            
            return System.Math.Exp(parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorSinh : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is function with 1 parameter. Parameters count should be equal to 1.", "parameters");
            
            return System.Math.Sinh(parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorCosh : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is function with 1 parameter. Parameters count should be equal to 1.", "parameters");
            
            return System.Math.Cosh(parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorAbsolution : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is function with 1 parameter. Parameters count should be equal to 1.", "parameters");
            
            return System.Math.Abs(parameters[0]);
        }

        #endregion
    }

    internal sealed class CalculatorLogarithm : IOperationCalculator
    {

        #region IOperationCalculator Members

        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (parameters.Length != 1)
                throw new ArgumentException("It is function with 1 parameter. Parameters count should be equal to 1.", "parameters");
            
            return System.Math.Log(parameters[0]);
        }

        #endregion
    }
}