using CalculatorApp.Core;

/// <summary>
/// Represents an arithmetic operation.
/// </summary>
/// <param name="operator">The operator used in the equation.</param>
/// <param name="x">The first component of the equation.</param>
/// <param name="y">The second component of the equation.</param>
/// <param name="result">The result of the equation.</param>
public readonly record struct Operation(ArithmeticOperators @operator, int x, int y, float result);