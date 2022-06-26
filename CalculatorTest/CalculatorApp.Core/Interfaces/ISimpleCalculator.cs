namespace CalculatorApp.Core.Interfaces
{
    public interface ISimpleCalculator
    {
        int Add(int start, int amount);
        int Subtract(int start, int amount);
        int Multiply(int start, int amount);
        float Divide(int start, int by);
    }
}
