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

namespace SaveText
{
    public partial class frmSaveText : Form
    {
        public frmSaveText()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // map die we willen gebruiken
            string path = @"C:\verhalen\";

            // controleren of map al bestaat
            // als de map nog niet bestaat, deze aanmaken
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            // titel + extensie toevoegen aan path
            path += txtTitle.Text + ".txt";

            // messagebox om te vragen of je het zeker wil opslaan
            DialogResult result = MessageBox.Show("Wil je het bestand bewaren?",
                "Bevestigen", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            // als er op 'Ja' geklikt wordt
            if (result == DialogResult.Yes)
            {
                // inhoud van grote textbox in file 'schrijven'
                // path = waar moet het bestand bewaard worden
                // txtStory.Text --> de tekst die in het bestand moet komen
                File.WriteAllText(path, txtStory.Text);
            }            
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // venster openen om bestanden te kiezen
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            // volledige pad + bestandsnaam opslaan in variabele
            // bvb: "C:\verhalen\kleinduimpje.txt"
            string path = ofd.FileName;

            // volledige inhoud van bestand in onderste
            // textbox plaatsen
            txtStory.Text = File.ReadAllText(path);

            // naam van bestand ZONDER EXTENSIE
            // in textbox bovenaan plaatsen
            txtTitle.Text = Path.GetFileNameWithoutExtension(path);
        }
    }
}
