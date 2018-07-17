using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Mole_Shooter.Properties;


namespace Shooting_Game
{
    public partial class Form1 : Form
    {
        private SoundPlayer _soundPlayer1;
        private SoundPlayer _soundPlayer;

        public Form1()
        {
            InitializeComponent();
            _soundPlayer = new SoundPlayer(@"C:\Users\thispc\source\repos\Shooting Game\spin.wav");

            _soundPlayer1 = new SoundPlayer(@"C:\Users\thispc\source\repos\Shooting Game\Gun+Reload.wav");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void common_Click(object sender, EventArgs e)
        {
            _soundPlayer1.Play();

            button1.Enabled = true;
            common.Enabled = false;
            button2.Enabled = false;
            pictureBox1.ImageLocation = (@"C:\Users\thispc\source\repos\Shooting Game\Spin.png");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _soundPlayer.Play();

            button2.Enabled = true;
            button1.Enabled = false;
            common.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mole_Shooter.MoleShooter moleProgram = new Mole_Shooter.MoleShooter();
            moleProgram.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, " Do you really want to exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
