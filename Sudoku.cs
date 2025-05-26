using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBruteforce
{
    public class SudokuSolver
    {
        int[,] Sudokutab { get; set; }
        int[,] Occupied { get; }
        List<Answer> Answers { get; set; }
        bool Validation { get; set; } = true;
        
        public SudokuSolver(int[,] sudoku)
        {
            Sudokutab = sudoku;
            Occupied = BuildOccupied(Sudokutab);
            Answers = new List<Answer>();
        }
        //Checks if numbers are 0-9
        void IsInputValid(int[,] input)
        {
            if(input.GetLength(0) != 9 || input.GetLength(1) != 9)
            {
                Validation = false; 
                return;
            }
            for(int i = 0; i < input.GetLength(0); i++)
            {
                for(int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i,j] < 0 || input[i,j] > 9)
                    {
                        Validation = false;
                        return;
                    }
                }
            }
            Validation = true;
        }
        //Checks if there is no duplicates in row
        bool IsRowValid(int row)
        {
            var nums = new HashSet<int>();
            for (int i = 0; i < Sudokutab.GetLength(1); i++)
            {
                if (Sudokutab[row, i] != 0)
                {
                    if (nums.Add(Sudokutab[row, i]))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //Checks if there is no duplicates in collumn
        bool IsCollumnValid(int column)
        {
            var nums = new HashSet<int>();
            for (int i = 0; i < Sudokutab.GetLength(0); i++)
            {
                if (Sudokutab[i, column] != 0)
                {
                    if (nums.Add(Sudokutab[i, column]))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //Finds number of 1 of 9 cells
        int WhichCell(int row, int collumn)
        {
            int x = collumn/3 + 1;
            int y = row/3;
            return x + 3 * y;
        }
        //Checks if there is no duplicates in cell
        bool IsCellValid(int cell)
        {
            cell -= 1;
            int centerx = 3*(cell / 3) + 1;
            int centery = 3*(cell % 3) + 1;
            var nums = new HashSet<int>();
            for(int i = centerx - 1; i <= centerx + 1; i++)
            {
                for(int j = centery - 1; j <= centery + 1; j++)
                {
                    if(Sudokutab[i, j] != 0)
                    {
                        if (nums.Add(Sudokutab[i, j]))
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        //Checks your input and builds grid to determine which number Solve() can ignore
        int[,] BuildOccupied(int[,] tab)
        {
            int[,] occupied = new int[tab.GetLength(1),tab.GetLength(0)];

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if(tab[i, j] != 0)
                    {
                        occupied[i, j] = 1;
                    }
                }
            }
            return occupied;
        }
        //Solves your sudoku
        public void Solve()
        {
            IsInputValid(Sudokutab);
            if (!Validation) { return; }
            int i = 0;
            int j = 0;
            int k = 1;
            previous:
            while (i < Sudokutab.GetLength(0))
            {
                while(j < Sudokutab.GetLength(1))
                {
                    while( k < 10)
                    {
                        if(Occupied[i, j] == 0)
                        {
                            Sudokutab[i, j] = k;
                            if (IsCellValid(WhichCell(i, j)) && IsCollumnValid(j) && IsRowValid(i))
                            {
                                Answers.Add(new Answer(j, i, k));
                                break;
                            }
                            else if(k == 9)
                            {
                                ktencase:
                                Sudokutab[i, j] = 0;
                                try
                                {
                                    i = Answers[Answers.Count - 1].Y;
                                    j = Answers[Answers.Count - 1].X;
                                    k = Answers[Answers.Count - 1].Value + 1;
                                    Answers.RemoveAt(Answers.Count - 1);
                                }
                                catch(Exception x)
                                {
                                    Validation = false;
                                    return;
                                }
                                if(k > 9)
                                {
                                    goto ktencase;
                                }
                                goto previous;
                            }
                            
                        }
                        else
                        {
                            break;
                        }
                        k++;
                    }
                    k = 1;
                    j++;
                }
                i++;
                j = 0;
            }
        }
        //Returns solved sudoku if exists
        public override string ToString()
        {
            if (!Validation)
            {
                return "No Answers";
            }
            int[,] tab = Sudokutab;
            string output = "";
            for(int i = 0; i < tab.GetLength(0); i++)
            {
                for( int j = 0; j < tab.GetLength(1); j++)
                {
                    if(tab[i, j] != 0)
                    {
                        output += tab[i, j] + " ";
                    }
                    else
                    {
                        output += "  ";
                    }
                    
                }
                output += "\n";
            }
            return output;
        }
    }
}
