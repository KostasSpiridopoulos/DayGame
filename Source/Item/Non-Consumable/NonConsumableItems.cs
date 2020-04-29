﻿using System;
using System.Drawing;

namespace DayGame
{
    public class NonConsumableItems : Item
    {
        public NonConsumableItems(string name, string description, int image, int price) : base(name, description, image, price)
        {
        }

        public void EquipToCharacter()
        {
            Console.WriteLine("Equipping to character");
        }
    }
}
