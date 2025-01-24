using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBruteforce
{
    public class SudokuSolver
    {
        public int[,] Sudokutab { get; set; }
        public int[,] Occupied { get; }
        public List<Answer> Answers { get; set; }
        public SudokuSolver(int[,] sudoku)
        {
            Sudokutab = sudoku;
            Occupied = BuildOccupied(Sudokutab);
            Answers = new List<Answer>();
        }
        public bool IsRowValid(int row)
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
        public bool IsColumnValid(int column)
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
        public int WichCell(int row, int collumn)
        {
            int x = collumn/3 + 1;
            int y = row/3;
            return x + 3 * y;
        }
        public bool IsCellValid(int cell)
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
        public void Solve()
        {
            int i = 0;
            int j = 0;
            int k = 1;
            bool issudokuvalid = true;
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
                           //Console.Clear();
                           //ToString(Sudokutab);
                           //Answers.ForEach(x => Console.Write(x.Value));
                           //Thread.Sleep(50);
                            if (IsCellValid(WichCell(i, j)) && IsColumnValid(j) && IsRowValid(i))
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
                                    issudokuvalid = false;
                                    goto printanswer;
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
            printanswer:
            if (issudokuvalid)
            {
                Console.Clear();
                ToString(Sudokutab);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Brak rozwiązań");
            }
            
        }
        public void ToString(int[,] tab)
        {
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
            Console.Write(output);
        }
    }
}
