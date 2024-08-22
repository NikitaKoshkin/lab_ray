﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenTK.Compute.OpenCL.CLGL;

using static System.Net.Mime.MediaTypeNames;


namespace Pacman.Objects
{

    internal class Map
    {
        public bool IsWall(int x, int y)
        {
            if (map[x, y] != 1)
                return true;
            return false;
        }


        public int[,] map;
        public int coins = 0;
        public Map(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            int width = bitmap.Width;
            int height = bitmap.Height;
            map = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color clr = bitmap.GetPixel(i, j);
                    if (clr == Color.FromArgb(65, 75, 235))
                    {
                        map[i, j] = 1;
                    }
                    else if (clr == Color.FromArgb(0, 0, 0))
                    {
                        map[i, j] = 0;
                    }
                    else if (clr == Color.FromArgb(255, 184, 151))
                    {
                        map[i, j] = 2; coins++;
                    }
                }
            }
        }
    }
}

