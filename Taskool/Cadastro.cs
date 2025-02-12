using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Taskool
{
    public partial class Cadastro : Form
    {
        Image img;
        TaskoolEntities context = new TaskoolEntities();
        public Cadastro()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = textBox1.Text;
            string email = textBox4.Text;
            string senha = textBox2.Text;
            string telefone = maskedTextBox2.Text;
            string data = maskedTextBox1.Text;
            string usuario = textBox3.Text;

            if (!email.Contains("@"))
            {
                MessageBox.Show("Insira um email válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
                byte[] foto = ms.ToArray();


                Usuario user = new Usuario();
                user.Nome = nome;
                user.Email = email;
                user.Senha = senha;
                user.Usuario1 = usuario;
                user.Telefone = telefone;
                user.Foto = foto;

                context.Usuario.Add(user);
                context.SaveChanges();

                Modelo.dado.atual = user;
                new Home().Show();
                Hide(); 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "|*.png;*.jpg;*PNG;*JPG";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string srq = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(srq);
                img = Image.FromFile(srq);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] nomes = textBox1.Text.Split(' ');
                string nome1 = nomes[0]; // Primeiro nome
                string nome2 = nomes[nomes.Length - 1]; // Último nome

                string data = maskedTextBox1.Text;
                string num = data.Substring(data.Length - 4);

                string gerado = $"{nome1}.{nome2}{num}";

                var verif = context.Usuario.FirstOrDefault(x => x.Usuario1 == gerado);

                if (verif == null)
                {
                    textBox3.Text = gerado;
                }
                else
                {
                    if (nomes.Length > 2 && nome2 != nome1)
                    {
                        nome2 = nomes[nomes.Length - 2]; 
                        gerado = $"{nome1}.{nome2}{num}";

                        verif = context.Usuario.FirstOrDefault(x => x.Usuario1 == gerado);

                        if (verif == null)
                        {
                            textBox3.Text = gerado;
                        }
                        else
                        {
                            int numR = new Random().Next(0, 1000); 
                            gerado = $"{nome1}.{nome2}{numR}";
                            textBox3.Text = gerado; 
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível gerar usuário", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar usuário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
