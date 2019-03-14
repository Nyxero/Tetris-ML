using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tetris_ML
{
    public class Tetris
    {
        private Random rnd = new Random();
        private int Columns { get; set; }
        private static int Rows { get; set; }
        private short[,] PlayGround { get; set; } 
        private short[,] CurrentBlock { get; set; }
        public int Score { get; private set; } 

        public Tetris(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
            PlayGround = new short[Columns, Rows];
            CurrentBlock = SpawnBlock(GetBlockType());
        }

        private bool IsBlockOnGround
        {
            get
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (CurrentBlock[j, i] == 1)
                        {
                            if (i == Rows - 1 || PlayGround[j, i + 1] == 1)
                                return true;
                        }
                    }
                }
                return false;
            }
        }
        private bool IsGameOver
        {
            get
            {
                for (int i = 0; i < Columns; i++)
                {
                    if (PlayGround[i, 1] == 1)
                        return true;
                }
                return false;
            }
        }

        private TetrisBlock GetBlockType()
        {
            int typeNr = rnd.Next(0, 6);
            TetrisBlock blockType;

            switch (typeNr)
            {
                case 0:
                    blockType = TetrisBlock.I;
                    break;
                case 1:
                    blockType = TetrisBlock.J;
                    break;
                case 2:
                    blockType = TetrisBlock.L;
                    break;
                case 3:
                    blockType = TetrisBlock.O;
                    break;
                case 4:
                    blockType = TetrisBlock.S;
                    break;
                case 5:
                    blockType = TetrisBlock.T;
                    break;
                case 6:
                    blockType = TetrisBlock.Z;
                    break;
                default:
                    blockType = TetrisBlock.I;
                    break;
            }

            return blockType;
        }
        private List<TetrisBlock> GetBlockType(int numbers)
        {
            List<TetrisBlock> result = new List<TetrisBlock>();
            for (int i = 0; i < numbers; i++)
            {
                result.Add(GetBlockType());
            }
            return result;
        }
        private short[,] SpawnBlock(TetrisBlock blockType)
        {
            short[,] result = new short[Columns, Rows];
            switch (blockType)
            {
                case TetrisBlock.I:
                    result[3, 1] = 1;
                    result[4, 1] = 1;
                    result[5, 1] = 1;
                    result[6, 1] = 1;
                    break;
                case TetrisBlock.J:
                    result[3, 1] = 1;
                    result[3, 2] = 1;
                    result[4, 2] = 1;
                    result[5, 2] = 1;
                    break;
                case TetrisBlock.L:
                    result[5, 1] = 1;
                    result[3, 2] = 1;
                    result[4, 2] = 1;
                    result[5, 2] = 1;
                    break;
                case TetrisBlock.O:
                    result[4, 1] = 1;
                    result[5, 1] = 1;
                    result[4, 2] = 1;
                    result[5, 2] = 1;
                    break;
                case TetrisBlock.S:
                    result[3, 2] = 1;
                    result[4, 2] = 1;
                    result[4, 1] = 1;
                    result[5, 1] = 1;
                    break;
                case TetrisBlock.T:
                    result[3, 2] = 1;
                    result[4, 1] = 1;
                    result[4, 2] = 1;
                    result[5, 2] = 1;
                    break;
                case TetrisBlock.Z:
                    result[3, 1] = 1;
                    result[4, 1] = 1;
                    result[4, 2] = 1;
                    result[5, 2] = 1;
                    break;
            }
            return result;
        }
        private void MoveDown()
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (CurrentBlock[j, i] == 1)
                    {
                        CurrentBlock[j, i] = 0;
                        CurrentBlock[j, i + 1] = 1;
                    }

                }
            }
        }
        private void MoveLeft()
        {
            // TODO: Move all Parts of a block to the left side (blocks will be deformed). 
            for (int i = Rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (CurrentBlock[j, i] == 1 && j-1 >= 0 && CurrentBlock[j-1, i] != 1)
                    {
                        CurrentBlock[j, i] = 0;
                        CurrentBlock[j-1, i] = 1;
                    }

                }
            }
        }
        private void MoveRight()
        {
            // TODO: Move all Parts of a block to the right side (blocks will be deformed). 
            for (int i = Rows - 1; i >= 0; i--)
            {
                for (int j = Columns-1; j >= 0; j--)
                {
                    if (CurrentBlock[j, i] == 1 && j + 1 <= Columns-1 && CurrentBlock[j + 1, i] != 1)
                    {
                        CurrentBlock[j, i] = 0;
                        CurrentBlock[j + 1, i] = 1;
                    }

                }
            }
        }
        private void Draw()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (CurrentBlock[j, i] == 1 || PlayGround[j, i] == 1)
                        Console.Write("XX");
                    else
                        Console.Write("__");
                }
                Console.WriteLine();
            }
        }
        private void SetBlock()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (CurrentBlock[j, i] == 1)
                        PlayGround[j, i] = 1;
                }
            }
        }

        public void Start()
        {
            while (!IsGameOver)
            {
                Console.Clear();
                Draw();

                if (IsBlockOnGround)
                {
                    SetBlock();
                    CurrentBlock = SpawnBlock(GetBlockType());
                }
                MoveDown();
                Thread.Sleep(250);
            }
            Console.WriteLine("GameOver!");
            Console.Read();
        }
    }
}
