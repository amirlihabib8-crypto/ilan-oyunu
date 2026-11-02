//suni zekadan istifade etmsiem amma alqoritmi men demisem istiqamet duymelri ve birbasa klavyatura duymelrine baglanib
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ilan_oyunu
{
    public partial class Form1 : Form
    {
        List<Panel> yemPanelleri = new List<Panel>();
        List<Panel> ilanGovde = new List<Panel>();
        Panel aktivYem;
        Random rnd = new Random();

        int yonX = 14;
        int yonY = 0;
        int xal = 0;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Yem panellərini siyahıya əlavə edirik
            yemPanelleri.Add(panel2);
            yemPanelleri.Add(panel3);
            yemPanelleri.Add(panel4);
            yemPanelleri.Add(panel5);
            yemPanelleri.Add(panel6);

            foreach (var p in yemPanelleri)
            {
                p.BackColor = Color.White;
            }

            // 2. Dizayndakı uzun panel1-i gizlədirik (artıq onu 14x14-lərə böləcəyik)
            panel1.Visible = false;
            // ilani baslangicda uzun verdim ondada donmurdu deye boldum
            // 3. İlanın başlanğıc gövdəsini 14x14-lük 3 kiçik paneldən yaradırıq
            int basX = 100;
            int basY = 250;

            for (int i = 0; i < 3; i++)
            {
                Panel p = new Panel();
                p.Size = new Size(14, 14);
                p.BackColor = Color.Green; // İlanın rəngi yaşıl
                p.Location = new Point(basX - (i * 14), basY); // Bir-birinin arxasında düzülürlər

                this.Controls.Add(p);
                ilanGovde.Add(p);
            }

            // İlk random yemi seçirik
            YeniYemSec();

            // Timer-i işə salırıq
            timer1.Start();
        }

        private void YeniYemSec()
        {
            if (yemPanelleri.Count == 0) return;

            if (aktivYem != null)
            {
                aktivYem.BackColor = Color.White;
            }

            int index = rnd.Next(0, yemPanelleri.Count);
            aktivYem = yemPanelleri[index];
            aktivYem.BackColor = Color.Gray;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int yeniX = ilanGovde[0].Location.X + yonX;
            int yeniY = ilanGovde[0].Location.Y + yonY;

            // Sərhəd (divar) yoxlaması
            if (yeniX < 0 || yeniX + ilanGovde[0].Width > this.ClientSize.Width ||
                yeniY < 0 || yeniY + ilanGovde[0].Height > this.ClientSize.Height)
            {
                timer1.Stop();
                MessageBox.Show("Oyunu uduzdunuz!", "Diqqət", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Restart();
                return;
            }

            // Yem yeyildi mi?
            if (ilanGovde[0].Bounds.IntersectsWith(aktivYem.Bounds))
            {
                xal += 10;
                this.Text = "İlan Oyunu - Xal: " + xal;

                // Yem yeyildikdə yeni 14x14 parça əlavə olunub ilan uzanır
                Panel yeniHisse = new Panel();
                yeniHisse.Size = new Size(14, 14);
                yeniHisse.BackColor = Color.Green;
                yeniHisse.Location = new Point(yeniX, yeniY);

                this.Controls.Add(yeniHisse);
                ilanGovde.Insert(0, yeniHisse);

                YeniYemSec();
            }
            else
            {
                Panel sonHisse = ilanGovde[ilanGovde.Count - 1];
                ilanGovde.RemoveAt(ilanGovde.Count - 1);

                sonHisse.Location = new Point(yeniX, yeniY);
                ilanGovde.Insert(0, sonHisse);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && yonX == 0)
            {
                yonX = -14;
                yonY = 0;
            }
            else if (e.KeyCode == Keys.Right && yonX == 0)
            {
                yonX = 14;
                yonY = 0;
            }
            else if (e.KeyCode == Keys.Up && yonY == 0)
            {
                yonX = 0;
                yonY = -14;
            }
            else if (e.KeyCode == Keys.Down && yonY == 0)
            {
                yonX = 0;
                yonY = 14;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}