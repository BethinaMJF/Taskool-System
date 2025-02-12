using System;
using System.Drawing;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Windows.Forms;
using Taskool.Modelo;
using WMPLib;

namespace Taskool
{
    public partial class Home : Form
    {
        string nomeP;
        bool isplayer = false;

        private WindowsMediaPlayer player;
        private IWMPPlaylist playlist;
        public Home()
        {
            InitializeComponent();
            nomeP = dado.atual.Nome;
            pictureBox1.Image = Image.FromStream( new MemoryStream( dado.atual.Foto));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm");
        }

        private void Home_Load(object sender, EventArgs e)
        {
            int hora = DateTime.Now.Hour;

            if (hora >= 12 && hora <= 17)
            {
                label2.Text = $"Boa tarde, {nomeP}!";
            }
            if (hora >= 18 && hora <= 23)
            {
                label2.Text = $"Boa noite, {nomeP}!";
            }
            if (hora >= 0 && hora <= 4)
            {
                label2.Text = $"Boa madrugada, {nomeP}!";
            }
            if (hora >= 4 && hora <= 11)
            {
                label2.Text = $"Bom dia, {nomeP}!";
            }


            string fraseAutor = File.ReadAllText(Directory.GetCurrentDirectory() + "\\json1.json");

            var json = JsonSerializer.Deserialize<JsonElement[]>(fraseAutor);
            int index = new Random().Next(0, json.Length);
            label3.Text = json[index].GetProperty("mensagem").GetString();
            label4.Text = json[index].GetProperty("autor").GetString();

            player = new WindowsMediaPlayer();
            playlist = player.newPlaylist("", "");
            player.currentPlaylist = playlist;

            string[] musicas = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Musicas\\", "*.mp3");

            foreach (string x in musicas)
            {
                playlist.appendItem(player.newMedia(x));
            }
            label5.Text = player.currentMedia.name.ToString();
            player.PlayStateChange += Player_PlayStateChange;

        }

        private void Player_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPPlayState.wmppsPlaying && player.currentMedia != null)
            {
                label5.Text = player.currentMedia.name;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (isplayer)
            {
                player.controls.pause();
                button7.Text = "Play";
                isplayer = false;

            }
            else
            {
                player.controls.play();
                button7.Text = "Pause";
                isplayer = true;

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = true;
            contextMenuStrip1.Show(Cursor.Position);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            string hexT = "#" + maskedTextBox1.Text;

            if (hexT.Length >= 5)
            {
                try
                {
                    panel10.BackColor = ColorTranslator.FromHtml(hexT);
                    Color cor = panel10.BackColor;
                    int r = cor.R;
                    int g = cor.G;
                    int b = cor.B;
                    maskedTextBox2.Text = $"{r},{g},{b}";

                }
                catch (Exception)
                {

                    panel10.BackColor = Color.White;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel10.BackColor = colorDialog1.Color;
                int r = colorDialog1.Color.R;
                int g = colorDialog1.Color.G;
                int b = colorDialog1.Color.B;
                maskedTextBox2.Text = $"{r},{g},{b}";

                var hex = panel10.BackColor;
                maskedTextBox1.Text = ColorTranslator.ToHtml(hex);
            }
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            string[] rgbT = maskedTextBox2.Text.Split(',');

            if(rgbT.Length > 0 &&
                int.TryParse(rgbT[0], out int r) &&
                int.TryParse(rgbT[1], out int g) &&
                int.TryParse(rgbT[2], out int b)

                )
            {
                try
                {
                    panel10.BackColor = Color.FromArgb(r, g, b);
                    Color cor = panel10.BackColor;
                    
                    maskedTextBox1.Text = ColorTranslator.ToHtml(cor) ;


                }
                catch (Exception)
                {

                    panel10.BackColor = Color.White;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel10.Visible = false;
            this.BackColor = panel10.BackColor;  
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.controls.stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel10.Visible = true;

        }

 
    }


}
