

using System;
using System.Drawing;
using System.Windows.Forms;
using Mole_Shooter.Properties;
using System.Media;

namespace Mole_Shooter
{
    public partial class MoleShooter : Form
    {
      
        const int FrameNum = 8; 
        const int SplatNum = 3;

        bool splat = false;

       
        int _gameFrame = 0;

        int _splatTime = 0;

      
        int _hits = 0;
        int _misses = 0;
        int _totalShots = 0;
        double _averageHits = 0;

#if My_Debug
        int _cursX = 0;
        int _cursY = 0;
#endif
     
       private CMole _mole;

     
       private CSplat _splat;

    
       private CSign _sign;

       private CScoreFrame _scoreframe;

    
       Random rnd = new Random();

      
        public MoleShooter()
        {
         
            InitializeComponent();

        
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);

            _mole = new CMole() { Left = 300, Top = 450 };

         
            _scoreframe = new CScoreFrame() { Left = 10, Top = 10 };
            _sign = new CSign() { Left = 580, Top = 192 };


            _splat = new CSplat();

           
            Bitmap b = new Bitmap(Resources.Site);
            this.Cursor = CustomeCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

        }

       
        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if (_gameFrame >= FrameNum)
            {
                UpdateMole();
                _gameFrame = 0;
            }

            if(splat)
            {
                if(_splatTime >= SplatNum)
                {
                    splat = false;
                    _splatTime = 0;
                    UpdateMole();
                }
                _splatTime++;
            }

            _gameFrame++;
            this.Refresh();
        }

        
        private void UpdateMole()
        {
            _mole.Update(rnd.Next(Resources.mole.Width, this.Width - Resources.mole.Width),
                         rnd.Next(this.Height / 2, this.Height - Resources.mole.Height * 2)
                         );
        }

       
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
           
            if (splat == true)
            {
                _splat.DrawImage(dc);
            }
            
            else
            {
                _mole.DrawImage(dc);
            }
            
            _sign.DrawImage(dc);
            _scoreframe.DrawImage(dc);

#if My_Debug
            TextFormatFlags flag = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _fontt = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, "X = " + _cursX.ToString() + ":" + "Y=" + _cursY.ToString(),
                _fontt, new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flag);
#endif

          
            TextFormatFlags flags = TextFormatFlags.Left;
            Font _font = new System.Drawing.Font("Stencil", 14, FontStyle.Regular);
            TextRenderer.DrawText(e.Graphics, "Shots :" + _totalShots.ToString(), _font, new Rectangle(40, 62, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Misses :" + _hits.ToString(), _font, new Rectangle(40, 82, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Hits :" + _misses.ToString(), _font, new Rectangle(40, 102, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Avg :" + _averageHits.ToString("F0")+ "%", _font, new Rectangle(40, 122, 120, 20), SystemColors.ControlText, flags);
            base.OnPaint(e);
         }

        
        private void MoleShooter_MouseMove(object sender, MouseEventArgs e)
        {

#if My_Debug
            _cursX = e.X;
            _cursY = e.Y;
#endif
            this.Refresh();
        }

       
        private void MoleShooter_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X > 660 && e.X < 704 && e.Y > 242 && e.Y < 256)
            {
                timerGameLoop.Start();
            }

            else if (e.X > 661 && e.X < 696 && e.Y > 259 && e.Y < 271)
            {
                timerGameLoop.Stop();
            }

            else if (e.X > 661 && e.X < 703 && e.Y > 277 && e.Y < 289)
            {
                splat = false;
                timerGameLoop.Stop();
                _hits = 0;
                _misses = 0;
                _totalShots = 0;
                _averageHits = 0;
                _mole.Left = 300;
                _mole.Top = 450;
             }

            else if (e.X > 660 && e.X < 694 && e.Y > 295 && e.Y < 306)
            {
                timerGameLoop.Stop();
                DialogResult result = MessageBox.Show(this, " Do you really want to exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
           else
            {

                if(_mole.Hit(e.X,e.Y))
                {
                    splat = true;
                    _splat.Left = _mole.Left - Resources.Splat.Width / 3;
                    _splat.Top = _mole.Top - Resources.Splat.Height / 3;
                    _hits++;
                }
                else { _misses++; }
                
                _totalShots = _hits + _misses;
                _averageHits = (double)_hits / (double)_totalShots * 100.0;
            }
            // Call FireGun() method to create Gun sound
            FireGun();
        }

        public void FireGun()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.gun_sound);
            simpleSound.Play();
        }

        private void MoleShooter_Load(object sender, EventArgs e)
        {

        }
    }
}
