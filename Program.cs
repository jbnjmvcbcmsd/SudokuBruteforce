using System.Diagnostics;

namespace SudokuBruteforce
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sudokusolver = new SudokuSolver(SetSudoku());
            sudokusolver.Solve();
            Console.WriteLine(sudokusolver.ToString());
        }
        //Set your sudoku grid here (0 represents empty square)
        static int[,] SetSudoku()
        {
            int[,] sudoku =
            {
                { 0, 0, 0, 0, 0, 0, 0, 2, 0
                },
                { 0, 0, 0, 0, 0, 0, 0, 0, 5
                },
                { 0, 2, 0, 0, 0, 0, 0, 0, 0
                },
                { 0, 0, 1, 0, 0, 0, 0, 0, 0
                },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                { 0, 0, 0, 2, 0, 0, 0, 0, 0
                },
                { 0, 0, 0, 0, 1, 0, 0, 0, 0
                },
                { 0, 0, 0, 0, 0, 2, 0, 0, 0
                },
                { 0, 0, 0, 0, 0, 0, 5, 0, 0
                }
            };
            return sudoku;
        }

    }
}
