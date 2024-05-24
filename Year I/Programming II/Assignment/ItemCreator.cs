using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class itemCreatorForm : Form
    {
        public Shop _shop;
        
        public itemCreatorForm()
        {
            InitializeComponent();
        }

        private void ItemCreator_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ItemCreatorClass itemCreator = new ItemCreatorClass();

            itemCreator.CreateNewItem(comboBox1.Text, textBox1.Text, textBox2.Text, int.Parse(textBox3.Text), int.Parse(textBox4.Text), int.Parse(textBox5.Text), int.Parse(textBox6.Text));
            itemCreator.updateShop(_shop);

            this.Close();
        }
    }
}
