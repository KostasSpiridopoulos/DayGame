﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DayGame.Boss

{
    public class Boss
    {
        public string Name { get; set; }
        private int _image;
        public int Damage { get; set; }
        public int HitPoints { get; set; }
        public int Level { get; set; }

        public Boss(string Name, int image, int Hit_points, int Level, int Damage)
            {
                this.Name = Name;
                this._image = image;
                this.HitPoints = Hit_points;
                this.Level = Level;
                this.Damage = Damage;
            }

        //dummy method, to be changed
        public void BossDeath()
        {
            Console.WriteLine("The boss died");
        }
    }

    
    
}

