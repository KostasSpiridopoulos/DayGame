﻿using System.Drawing;

namespace DayGame
{
    public class Item
    {
        protected string name;
        protected string description;
        protected int image;
        protected int price;

        public Item(string name, string description, int image, int price)
        {
            this.name = name;
            this.description = description;
            this.image = image;
            this.price = price;
        }

        public int Image => image;
    }
}
