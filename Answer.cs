using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBruteforce
{
    public class Answer
    {
        public int X { get; }
        public int Y { get; }
        public int Value {  get; }
        public Answer(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }
}
