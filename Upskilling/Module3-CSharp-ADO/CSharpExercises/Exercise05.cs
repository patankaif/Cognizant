using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 5: Perform Conditional Logic for Grade Calculation
    /// </summary>
    public static class Exercise05
    {
        private static string GetGradeWithIfElse(int score)
        {
            if (score >= 90) return "A";
            else if (score >= 80) return "B";
            else if (score >= 70) return "C";
            else if (score >= 60) return "D";
            else return "F";
        }

        // Pattern matching with switch expression (more concise)
        private static string GetGradeWithSwitch(int score) => score switch
        {
            >= 90 and <= 100 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            >= 0  => "F",
            _     => "Invalid score"
        };

        public static void Run()
        {
            int[] sampleScores = { 95, 82, 76, 64, 40, 105, -5 };

            foreach (var score in sampleScores)
            {
                string gradeIfElse = score is >= 0 and <= 100 ? GetGradeWithIfElse(score) : "Invalid score";
                string gradeSwitch = GetGradeWithSwitch(score);

                Console.WriteLine($"Score: {score,4} | If-Else Grade: {gradeIfElse,-13} | Switch Grade: {gradeSwitch}");
            }
        }
    }
}
