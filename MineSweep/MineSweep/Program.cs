using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweep
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid newGame = new Grid(10, 10, 10);
            Console.WriteLine(newGame.ToString());

            //Console.WriteLine(newGame.Mines);
            
            Console.ReadLine();
        }
    }

    class Grid
    {
        private Cell[,] grid;
        public Cell[,] GetCells()
        {
            return grid;
        }
        //public int Mines;

        public Grid(int x, int y, int mines)
        {
            grid = new Cell[x, y];
            for (int indexX = 0; indexX < x; indexX++)
            {
                for (int indexY = 0; indexY < y; indexY++)
                {
                    grid[indexX, indexY] = new Cell();
                }
            }
            addMines(grid, mines);

            for (int indexX = 0; indexX < x; indexX++)
            {
                for (int indexY = 0; indexY < y; indexY++)
                {
                    grid[indexX, indexY].AdjMines = adjacentMine(indexX, indexY);
                }
            }
            //Mines = mines;
            
        }
        // checking currrent cell for adjacent mines
        public int adjacentMine(int row, int column)
        {
            int nearByBomb = 0;
            // check only if there is no bomb in the cell
            if(grid[row, column].Mine != true)
            {
                // checking row starting from -1, 0, and 1 from the current cell
                for(int checkRow = -1; checkRow < 2; checkRow++)
                {
                    // checking column starting from -1, 0, and 1 from the current cell
                    for(int checkColumn = -1; checkColumn < 2; checkColumn++)
                    {
                        // attempt to find neighbor, throw out of bound exception when fail
                        try
                        {
                            if (grid[row + checkRow, column + checkColumn].Mine)
                            {
                                nearByBomb++;
                            }
                        }
                        catch (IndexOutOfRangeException) { }
                    }
                }
            }
            return nearByBomb;
        }

        public void addMines(Cell[,] Grid, int NumMines)
        {
            int x = Grid.GetLength(0);
            int y = Grid.GetLength(1);
            var ran = new Random();
            for (int i = 0; i < NumMines; i++)
            {
                int num1 = ran.Next(x);
                int num2 = ran.Next(y);
                while (Grid[num1, num2].Mine == true)
                {
                    num1 = ran.Next(x);
                    num2 = ran.Next(y);
                }
                Grid[num1,num2].Mine = true;
                
            }
            
        }
        
        // printing out the layout of the grid
        public override String ToString()
        {
            String summary = "";
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if(grid[i, j].Mine)
                    {
                        summary += "[B]";
                    }
                    else
                    {
                        summary += "[" + grid[i, j].AdjMines + "]";
                    }

                    //summary += GetCells()[i, j].ToString() + "\r\n";
                }
                summary += "\n";
            }
            return summary;
        }


    }

    public enum CellState { OPEN, CLOSED, FLAGGED };

    class Cell
    {


        public Cell()
        {
            State = CellState.OPEN;
            Mine = false;
            AdjMines = 0;
        }

        //is Cell contain a mine
        private Boolean mine;
        public Boolean Mine { get; set; }

        //how many adjacent cells contain mines
        private int adjMines;
        public int AdjMines { get; set; }
        

        //what is the current state of the cell
        private CellState state;
        public CellState State { get; set; }

       

        public void Close()
        {
            state = CellState.CLOSED;
        }

        public void Open()
        {
            state = CellState.OPEN;
        }
        public void Flagged()
        {
            state = CellState.FLAGGED;
        }

        override
        public String ToString()
        {
            return Mine + " " + State;
        }
    }
}
