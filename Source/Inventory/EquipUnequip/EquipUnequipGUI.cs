using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DayGame
{

    public partial class EquipUnequipGUI : Form
    {
        public EquipUnequipGUI(Item item,string buttontext)
        {
            InitializeComponent();
            this.CenterToParent();//kwdikas gia na emfanizetai to minima sthn mesh tis othonis
            setup(item,buttontext);
        }

        public void setup(Item item,String buttontext)
        {
            itemnamelabel.Text = item.Name;
            itemdescriptionlabel.Text = item.Description;
            equnbutton.Text = buttontext;
            if (item.GetType() == typeof(Armor))
            {
                Armor armor = (Armor)item;
                statlabel.Text = "Defence:";
                statnumber.Text = armor.Defence.ToString();

                pictureBox1.BackColor = Color.Blue;
            }
            else if (item.GetType() == typeof(Weapon))
            {
                Weapon weapon = (Weapon)item;
                statlabel.Text = "Damage:";
                statnumber.Text = weapon.Damage.ToString();

                pictureBox1.BackColor = Color.Red;
            }
            else if (item.GetType() == typeof(Spell))
            {
                Spell spell = (Spell)item;
                statlabel.Text = "Damage:";
                statnumber.Text = spell.Damage.ToString();

                pictureBox1.BackColor = Color.Yellow;
            }
            else if (item.GetType() == typeof(Potion))
            {
                Potion potion = (Potion)item;
                statlabel.Text = "Regen:";
                statnumber.Text = potion.Hit_point_regain.ToString();

                pictureBox1.BackColor = Color.Green;
            }
        }
        private void itemnamelabel_Click(object sender, EventArgs e)
        {
        }
        private void EquipUnequipGUI_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void itemdescriptionlabel_Click(object sender, EventArgs e)
        {

        }
    }
}