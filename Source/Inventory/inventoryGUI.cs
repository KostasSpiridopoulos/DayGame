using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DayGame
{
    public partial class InventoryGUI : Form
    {
        //voithaei thn sunarthsh WeaponOrArmorEquip na katalavei poio button patithike,
        //kai na kanei equip to item tou sugkekrimenou button,
        //o arithmos ypologizete sthn sunarthsh GetTheButtonNumber
        int ChestButtonPressed = -1;
        int BagButtonPressed = -1;

        public Button[] ChestButtonsArray; //array list me ta ChestButtons
        public Button[] BagButtonArray;
        public CheckBox[] CheckBoxArray;
        Inventory inv = new Inventory(); //ftiaxnoume ypothetiko Inventory
        public Item spathi = new Weapon("Spathi", "spathi dou takesi", 1, 15, 5);
        public Item panoplia = new Armor("Armor D", "Armor tou takesi", 1, 3, 5);
        public Armor armor = new Armor("Armor D", "Armor tou takesi", 1, 3, 5);
        public Spell spell = new Spell("SpellName", "This is a spell", 6, 0, 15);
        public Potion potion = new Potion("PotionName", "This is a potion", 6, 0, 15);

        public int[] ButtonToChest = new int[42];
        public int[] ButtonToBag = new int[8];
        public int DamageBuff = 0;
        public int ArmorBuff = 0;

        public InventoryGUI()
        {
            InitializeComponent();
            CheckBoxArray = new[] { armorcheckbox, weaponscheckbox, spellscheckbox, potionscheckbox };
            ChestButtonsArray = new[]
            {
                chestbutton1, chestbutton2, chestbutton3, chestbutton4, chestbutton5, chestbutton6, chestbutton7,
                chestbutton8, chestbutton9, chestbutton10, chestbutton11, chestbutton12, chestbutton13, chestbutton14,
                chestbutton15, chestbutton16, chestbutton17, chestbutton18, chestbutton19, chestbutton20, chestbutton21,
                chestbutton22, chestbutton23, chestbutton24, chestbutton25, chestbutton26, chestbutton27, chestbutton28,
                chestbutton29, chestbutton30, chestbutton31, chestbutton32, chestbutton33, chestbutton34, chestbutton35,
                chestbutton36, chestbutton37, chestbutton38, chestbutton39, chestbutton40, chestbutton41, chestbutton42
            };
            BagButtonArray = new[]
            {
                BagButton1,BagButton2,BagButton3,BagButton4,BagButton5,BagButton6,BagButton7,BagButton8
            };
            //edw oloklirwnete h diadikasia tou ArrayList me ola ta ChestButtons
            proswrinh_sunarthsh_prosthiki_antikeimenwn_se_inventory();
            

            foreach (CheckBox cb in CheckBoxArray)
            {
                cb.Checked = true;
            }

            InventorySpaceReload();

            for (int i = 0; i < 42; i++)
            {
                ChestButtonsArray[i].Click += GetTheButtonNumberChest;
                ChestButtonsArray[i].Click += Equip;
                //efoswn patithei kapoio koubi, apothikefse to koubi pou patithike, kai kane equip
            }

            for (int i = 0; i < 8; i++)
            {
                BagButtonArray[i].Click += GetTheButtonNumberBag;
                BagButtonArray[i].Click += UnequipConsumable;
                //efoswn patithei kapoio koubi, apothikefse to koubi pou patithike, kai kane equip
            }

            ArmorButton.Click += UnequipArmor;
            WeaponButton.Click += UnequipWeapon;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button43_Click(object sender, EventArgs e)
        {
        }

        private void chestbutton2_Click(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void inventoryGUI_Load(object sender, EventArgs e)
        {
        }


        private void Equip(object sender, EventArgs e)
        {
            //GIA THN WRA DOULEVEI MONO GIA WEAPONS KAI ARMOR//
            Button btn = sender as Button;
            if (ButtonToChest[ChestButtonPressed] != -1)
            //mono ean to button den einai keno(dhladh den exei item),ekteleitai o parakatw kodikas.
            //(epeidh an einai keno, dhladh xwris item, tote to item einai = me null, kai buggarei to programma)
            {
                EquipUnequipGUI Equip = new EquipUnequipGUI(inv.ChestSpace[ButtonToChest[ChestButtonPressed]], "Equip");
                if (Equip.ShowDialog(this) == DialogResult.OK)
                {
                    if ((inv.WeaponEquiped == null) && (inv.ChestSpace[ButtonToChest[ChestButtonPressed]].GetType() == typeof(Weapon)))
                        //ean to weaponbutton einai empty, kai to button pou
                        //pathses einai Weapon
                        {
                            //kwdikas gia na prosthetei to buff tou weapon
                            Weapon weapon = (Weapon)inv.ChestSpace[ButtonToChest[ChestButtonPressed]];
                            DamageBuff = DamageBuff + weapon.Damage;
                            DamageTextNumber.Text = DamageBuff.ToString();
                            //

                            inv.AddWeapon(inv.ChestSpace[ButtonToChest[ChestButtonPressed]], ButtonToChest[ChestButtonPressed]);//prosthese to Weapon pou foreses sto WeaponButton kai afairese to apo to inventory
                            
                            InventorySpaceReload();//allaxe tis allages sto GUI
                    }
                    else if ((inv.ArmorEquiped == null) && (inv.ChestSpace[ButtonToChest[ChestButtonPressed]].GetType() == typeof(Armor)))
                    //ean to armorbutton einai empty, kai to button pou pathses einai Armor
                    {
                        //kwdikas gia na prosthetei to buff tou Armor
                        Armor Armor = (Armor)inv.ChestSpace[ButtonToChest[ChestButtonPressed]];
                            ArmorBuff = ArmorBuff + Armor.Defence;
                            DefenceTextNumber.Text = ArmorBuff.ToString();
                            //



                            inv.AddArmor(inv.ChestSpace[ButtonToChest[ChestButtonPressed]], ButtonToChest[ChestButtonPressed]);//prosthese to armor pou foreses sto ArmorButton kai afairese to apo to inventory

                        InventorySpaceReload();//allaxe tis allages sto GUI
                    }
                    else if ((inv.BagNotFull()) && (inv.ChestSpace[ButtonToChest[ChestButtonPressed]].GetType() == typeof(Spell)))
                    //ean to bag einai empty, kai to button pou pathses exei einai NonConsumableItems
                    {



                        inv.AddToBagFromInventory(inv.ChestSpace[ButtonToChest[ChestButtonPressed]], ButtonToChest[ChestButtonPressed]);//prosthese to armor pou foreses sto ArmorButton kai afairese to apo to inventory

                        InventorySpaceReload();//allaxe tis allages sto GUI
                    }
                    else if ((inv.BagNotFull()) && (inv.ChestSpace[ButtonToChest[ChestButtonPressed]].GetType() == typeof(Potion)))
                    //ean to bag einai empty, kai to button pou pathses exei einai NonConsumableItems
                    {



                        inv.AddToBagFromInventory(inv.ChestSpace[ButtonToChest[ChestButtonPressed]], ButtonToChest[ChestButtonPressed]);//prosthese to armor pou foreses sto ArmorButton kai afairese to apo to inventory

                        InventorySpaceReload();//allaxe tis allages sto GUI
                    }
                    else
                        {
                            MessageBox.Show("Δεν Μπορεις να βάλεις το αντικείμενο");
                        }
                    }
                }
            }
        /*
        private void Equip(object sender, EventArgs e)
        {
            //GIA THN WRA DOULEVEI MONO GIA WEAPONS KAI ARMOR//
            Button btn = sender as Button;
            if (inv.ChestSpace[ChestButtonPressed - 1] != null)
            //mono ean to button den einai keno(dhladh den exei item),ekteleitai o parakatw kodikas.
            //(epeidh an einai keno, dhladh xwris item, tote to item einai = me null, kai buggarei to programma)
            {
                EquipUnequipGUI Equip = new EquipUnequipGUI(inv.ChestSpace[ChestButtonPressed - 1], "Equip");
                if (Equip.ShowDialog(this) == DialogResult.OK)
                {
                    if ((inv.WeaponEquiped == null) && (inv.ChestSpace[ChestButtonPressed - 1].GetType() == typeof(Weapon)))
                    //ean to weaponbutton einai empty, kai to button pou
                    //pathses exei kokkino xrwma(dhladh einai weapon)
                    {
                        //kwdikas gia na prosthetei to buff tou weapon
                        Weapon weapon = (Weapon)inv.ChestSpace[ChestButtonPressed - 1];
                        DamageBuff = DamageBuff + weapon.Damage;
                        DamageTextNumber.Text = DamageBuff.ToString();
                        //

                        inv.AddWeapon(inv.ChestSpace[ChestButtonPressed - 1], ChestButtonPressed - 1);//prosthese to Weapon pou foreses sto WeaponButton kai afairese to apo to inventory

                        InventorySpaceReload();//allaxe tis allages sto GUI
                    }
                    else if ((inv.ArmorEquiped == null) && (inv.ChestSpace[ChestButtonPressed - 1].GetType() == typeof(Armor)))
                    //ean to armorbutton einai empty, kai to button pou pathses exei ble xrwma(dhladh einai armor)
                    {
                        //kwdikas gia na prosthetei to buff tou Armor
                        Armor Armor = (Armor)inv.ChestSpace[ChestButtonPressed - 1];
                        ArmorBuff = ArmorBuff + Armor.Defence;
                        DefenceTextNumber.Text = ArmorBuff.ToString();
                        //



                        inv.AddArmor(inv.ChestSpace[ChestButtonPressed - 1], ChestButtonPressed - 1);//prosthese to armor pou foreses sto ArmorButton kai afairese to apo to inventory

                        InventorySpaceReload();//allaxe tis allages sto GUI
                    }
                    else
                    {
                        MessageBox.Show("Δεν Μπορεις να βάλεις το αντικείμενο");
                    }
                }
            }
        }*/

        private void GetTheButtonNumberChest(object sender, EventArgs e)
            //DINEI STO INT ChestButtonPressed to Chestbutton pou epilexthike(1-42), etsi wste h sunarthsh Equip na mporei na leitourghsei
        {
            Button btn = sender as Button;
            string GETCHESTBUTTON = btn.Name;

            string loadingonlythelettersofthebutton = Regex.Replace(GETCHESTBUTTON, "[^0-9]", "");

            GETCHESTBUTTON = loadingonlythelettersofthebutton;

            ChestButtonPressed = Int32.Parse(GETCHESTBUTTON)-1;
        }

        private void GetTheButtonNumberBag(object sender, EventArgs e)
        //DINEI STO INT ChestButtonPressed to Chestbutton pou epilexthike(1-42), etsi wste h sunarthsh Equip na mporei na leitourghsei
        {
            Button btn = sender as Button;
            string GETCHESTBUTTON = btn.Name;

            string loadingonlythelettersofthebutton = Regex.Replace(GETCHESTBUTTON, "[^0-9]", "");

            GETCHESTBUTTON = loadingonlythelettersofthebutton;

            BagButtonPressed = Int32.Parse(GETCHESTBUTTON) - 1;
        }

        private void UnequipWeapon(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if ((inv.WeaponEquiped!=null))
                //mono ean to button den einai keno(dhladh den exei item),ekteleitai
                //o parakatw kodikas.(epeidh an einai keno, dhladh xwris item, tote
                //to item einai = me null, kai buggarei to programma)
            {
                EquipUnequipGUI UnequipWeapon = new EquipUnequipGUI(inv.WeaponEquiped, "Unequip");
                if (UnequipWeapon.ShowDialog(this) == DialogResult.OK)
                {
                    inv.InventoryAddItem(inv.WeaponEquiped);
                    //kwdikas gia na afairei to buff tou weapon
                    Weapon weapon = (Weapon)inv.WeaponEquiped;
                    DamageBuff = DamageBuff - weapon.Damage;
                    DamageTextNumber.Text = DamageBuff.ToString();



                    inv.DeleteWeapon(inv.WeaponEquiped);
                    InventorySpaceReload();//kane reload to GUI
                    }
                }
        }

        private void UnequipConsumable(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if ((ButtonToBag[BagButtonPressed] != -1))
            //mono ean to button den einai keno(dhladh den exei item),ekteleitai
            //o parakatw kodikas.(epeidh an einai keno, dhladh xwris item, tote
            //to item einai = me null, kai buggarei to programma)
            {
                EquipUnequipGUI UnequipWeapon = new EquipUnequipGUI(inv.Bag[ButtonToBag[BagButtonPressed]], "Unequip");
                if (UnequipWeapon.ShowDialog(this) == DialogResult.OK)
                {
                    inv.InventoryAddItem(inv.Bag[ButtonToBag[BagButtonPressed]]);
                    inv.DeleteBagItem(inv.Bag[ButtonToBag[BagButtonPressed]], ButtonToBag[BagButtonPressed]);


                    InventorySpaceReload();//kane reload to GUI
                }
            }
        }


        private void UnequipArmor(object sender, EventArgs e)
        {
            //UNEQUIP, doulevei mono gia ARMORS
            Button btn = sender as Button;
            if ((inv.ArmorEquiped != null))
            //mono ean to button den einai keno(dhladh den exei item),
            //ekteleitai o parakatw kodikas.(epeidh an einai keno,
            //dhladh xwris item, tote to item einai = me null, kai buggarei to programma)
            {
                EquipUnequipGUI Unequip = new EquipUnequipGUI(inv.ArmorEquiped, "Unequip");
                if (Unequip.ShowDialog(this) == DialogResult.OK)
                {

                    inv.InventoryAddItem(inv.ArmorEquiped);
                    //kwdikas gia na afairei to buff tou weapon
                    Armor Armor = (Armor)inv.ArmorEquiped;
                    ArmorBuff = ArmorBuff - Armor.Defence;
                    DefenceTextNumber.Text = ArmorBuff.ToString();



                    inv.DeleteArmor(inv.ArmorEquiped);
                    InventorySpaceReload();//kane reload to GUI
                }
            
            }
        }

        void proswrinh_sunarthsh_prosthiki_antikeimenwn_se_inventory()
        {
            //prosthesi adikeimenwn sto inventory


            inv.InventoryAddItem(spathi);
            inv.InventoryAddItem(potion);
            inv.InventoryAddItem(panoplia);
            inv.InventoryAddItem(spell);
            inv.InventoryAddItem(potion);
            inv.InventoryAddItem(spathi);
            //prosthesi adikeimenwn sto inventory
        }

        void InventorySpaceReload()
        {
            //arxika kanei ola ta buttons me colour Default
            for (int i = 0; i < 42; i++)
            {
                ChestButtonsArray[i].BackColor = Color.FromKnownColor(KnownColor.Control);
                ButtonToChest[i] = -1;
            }
            for (int i = 0; i < 8; i++)
            {
                BagButtonArray[i].BackColor = Color.FromKnownColor(KnownColor.Control);
                ButtonToBag[i] = -1;
            }
            WeaponButton.BackColor= Color.FromKnownColor(KnownColor.Control);
            ArmorButton.BackColor= Color.FromKnownColor(KnownColor.Control);
            // sth sunexeia prosthetei to xrwma analoga me to Inventory
            int ButtonIndex = 0;
            for (int i = 0; i < 42; i++)
            {
                if ((inv.ChestSpace[i] != null))
                {
                    if (inv.ChestSpace[i] is Armor && armorcheckbox.Checked)
                    {
                        ChestButtonsArray[ButtonIndex].BackColor = Color.Blue;
                        ButtonToChest[ButtonIndex] = i;
                        ButtonIndex++;
                    }
                    else if (inv.ChestSpace[i] is Weapon && weaponscheckbox.Checked)
                    {
                        ChestButtonsArray[ButtonIndex].BackColor = Color.Red;
                        ButtonToChest[ButtonIndex] = i;
                        ButtonIndex++;
                    }
                    else if (inv.ChestSpace[i] is Spell && spellscheckbox.Checked)
                    {
                        ChestButtonsArray[ButtonIndex].BackColor = Color.Yellow;
                        ButtonToChest[ButtonIndex] = i;
                        ButtonIndex++;
                    }
                    else if (inv.ChestSpace[i] is Potion && potionscheckbox.Checked)
                    {
                        ChestButtonsArray[ButtonIndex].BackColor = Color.Green;
                        ButtonToChest[ButtonIndex] = i;
                        ButtonIndex++;
                    }

                    /*ChestButtonsArray[i].BackColor = inv.ChestSpace[i] switch
                    {
                        Armor _ => Color.Blue,
                        Weapon _ => Color.Red,
                        Spell _ => Color.Yellow,
                        Potion _ => Color.Green,
                        _ => ChestButtonsArray[i].BackColor
                    };*/
                }
            }
            int ButtonBagIndex = 0;
            for (int i=0; i<8; i++)
            {
                if ((inv.Bag[i] != null))
                {
                    if (inv.Bag[i] is Potion)
                    {
                        BagButtonArray[ButtonBagIndex].BackColor = Color.Green;
                        ButtonToBag[ButtonBagIndex] = i;
                        ButtonBagIndex++;
                    }
                    if (inv.Bag[i] is Spell)
                    {
                        BagButtonArray[ButtonBagIndex].BackColor = Color.Yellow;
                        ButtonToBag[ButtonBagIndex] = i;
                        ButtonBagIndex++;
                    }
                }
            }
            if (inv.WeaponEquiped != null)
            {
                WeaponButton.BackColor = Color.Red;
            }
            if (inv.ArmorEquiped!= null)
            {
                ArmorButton.BackColor = Color.Blue;
            }
            if (DamageTextNumber.Text == "0")
            {
                DamageTextNumber.ForeColor = Color.Red;
            }
            if (DefenceTextNumber.Text == "0")
            {
                DefenceTextNumber.ForeColor = Color.Red;
            }
            if (DamageTextNumber.Text != "0")
            {
                DamageTextNumber.ForeColor = Color.Black;
            }
            if (DefenceTextNumber.Text != "0")
            {
                DefenceTextNumber.ForeColor = Color.Black;
            }
        }

        private void chestbutton3_Click(object sender, EventArgs e)
        {
        }

        private void filter_checked_changed(object sender, EventArgs e)
        {
            InventorySpaceReload();
        }
    }
}
