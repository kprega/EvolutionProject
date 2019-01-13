using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Playground
    {
        public readonly byte CellSize = 10;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public bool[,] CurrentState { get; set; }  
        private bool[,] NextState { get; set; }

        //public Bacteria[,] bacterias { get; set;}

        public Playground(int x, int y)
        {
            this.Width = x;
            this.Height = y;
            CurrentState = new bool[x, y];
            NextState = new bool[x, y];

            //bacterias = new Bacteria[x,y];
            //for (int i = 0; i < w - 1; i++)
            //{
            //    for (int j = 0; j < h; j++)
            //    {
            //        bacterias[i, j] = new Bacteria();
            //    }
            //}
        }

        public void StepForward()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    var neighboursAlive = GetNumberOfNeighboursAlive(x, y);
                    NextState[x, y] = neighboursAlive == 3 || (CurrentState[x, y] && neighboursAlive == 2);
                }
            }
            CurrentState = (bool[,])NextState.Clone();
        }

        private int GetNumberOfNeighboursAlive(int x, int y)
        {
            var result = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    //skipping fields:
                    if ((i == 0 && j == 0)                         //current field
                        || (x + j < 0 || x + j > this.Width - 1)   //too far left or right
                        || (y + i < 0 || y + i > this.Height - 1)) //too far up or down
                        continue;
                    if (CurrentState[x + j, y + i]) result++;
                }
            }
            return result;
        }

    }
}
