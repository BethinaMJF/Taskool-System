using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taskool
{
    public partial class Form1 : Form
    {
        public Image img;
        public string nomeHm;
        TaskoolEntities context = new TaskoolEntities();
        public Form1()
        {
            InitializeComponent();

            MessageBox.Show("Status conexão com banco de dados: " + context.Database.Exists().ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "|*.png;*.jpg;*PNG;*JPG";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string srq = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(srq);
                img = Image.FromFile(srq);

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(Control.IsKeyLocked(Keys.CapsLock))
            {
                toolTip1.Active = true;
                toolTip1.Show("Caps Lock  ativada", textBox1);
            }
            else
            {
                toolTip1.Active = false;    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if(pictureBox1.Image == null)
            {
                MessageBox.Show("Insira sua credencial", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, img.RawFormat);

                var users = context.Usuario
                    .Where(x => x.Senha == textBox2.Text &&
                                (x.Usuario1 == textBox1.Text || x.Email == textBox1.Text))
                    .ToList(); // Carrega os dados na memória

                var user = users.FirstOrDefault(x => x.Foto.SequenceEqual(ms.ToArray())); // Compara a foto na memória
                if (user == null)
                {
                    MessageBox.Show("Imagem ou usuário não reconhecio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SystemSounds.Beep.Play();
                    
                    string nameip = Dns.GetHostName();
                    IPAddress[] IP = Dns.GetHostAddresses(nameip); 

                    string caminho = $"C:\\USER_LOGS\\{textBox1.Text}.txt";

                    if (!Directory.Exists(caminho))
                    {
                        Directory.CreateDirectory($"C:\\USER_LOGS\\");
                    }
                    StreamWriter tx = new StreamWriter(caminho);
                    tx.Write(DateTime.Now.ToString() + "; ");
                    tx.Write(textBox1.Text + "; ");
                    tx.Write(IP[1].ToString());
                    tx.Write("");
                    tx.Close();
                }
                else
                {
                  
                    Modelo.dado.atual = user;
                    new Home().Show();
                    Hide();
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cadastro cadastro = new Cadastro();
            cadastro.ShowDialog();
            Hide();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1.Show("Duplo click para selecionar imagem", pictureBox1, 2000);

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
