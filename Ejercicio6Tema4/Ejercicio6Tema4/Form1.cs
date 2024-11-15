using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio6Tema4
{//Permitir cancelar. gESTION COLORES BOTNES. Añadir en nueva linea. Y no regutar sobreescribir. Exc geberica
    public partial class Form1 : Form
    {

        string code = "0048";
        int attemps = 0;
        public Form1()
        {


            InitializeComponent();
            Form2 modal = new Form2();
            while (attemps < 3)
            {
                DialogResult dr = modal.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (modal.textBox1.Text != code)
                    {
                        attemps += 1;
                        if (attemps == 3)
                        {
                            MessageBox.Show("Invalid Passcode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            int x = 144;
            int y = 70;

            for (int i = 0; i < 12; i++)
            {
                Button btn = new Button();
                btn.Text = (i + 1).ToString();

                if (i == 9)
                {
                    btn.Text = "*";
                }
                else if (i == 10)
                {
                    btn.Text = "0";
                }
                else if (i == 11)
                {
                    btn.Text = "#";
                }

                if (i % 3 == 0)
                {
                    //cada 3 filas , por lo tanto muevo x y y , en cambio la otro solo la x 
                    x = 144;
                    y += 50;

                }
                else
                {
                    x += 40;
                }
                btn.Enabled = true;
                btn.Location = new Point(x, y);
                btn.Size = new Size(40, 40);
                btn.Click += new System.EventHandler(this.WriteNumber);
                btn.MouseEnter += new EventHandler(this.ChangeColor);
                btn.MouseLeave += new EventHandler(this.ResetColor);

                this.Controls.Add(btn);

            }
        }
        private void ChangeColor(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            if (btn.BackColor != Color.HotPink)
            {

                btn.BackColor = Color.Pink;
            }

        }

        private void ResetColor(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.BackColor != Color.HotPink)
            {
                btn.BackColor = Color.Thistle;
            }
        }
        private void WriteNumber(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.HotPink;
            textBoxNumber.Text += btn.Text;

        }

        private void ResetBtn(object sender, EventArgs e)
        {
            textBoxNumber.Text = "";
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(Button))
                {

                    control.BackColor = Color.Thistle;
                }
            }

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void Info(object sender, EventArgs e)
        {
            MessageBox.Show("The author is Nicki<3", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveNumber(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                OverwritePrompt = false,
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Save Number"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, true))
                    {
                        writer.Write($"\n{textBoxNumber.Text}\n");
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"Access error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show($"Directory not found: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"I/O error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
