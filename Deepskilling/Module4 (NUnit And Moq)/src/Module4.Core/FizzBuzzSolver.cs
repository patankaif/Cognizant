namespace Module4.Core;

public class FizzBuzzSolver
{
    public string Convert(int number)
    {
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));

        if (number % 15 == 0) return "FizzBuzz";
        if (number % 3 == 0) return "Fizz";
        if (number % 5 == 0) return "Buzz";
        return number.ToString();
    }
}
