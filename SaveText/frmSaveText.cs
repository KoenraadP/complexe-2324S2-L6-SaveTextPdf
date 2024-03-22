using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private void BtnSavePdf_Click(object sender, EventArgs e)
        {
            // PDF document aanmaken
            PdfDocument document = new PdfDocument();

            // lege pagina toevoegen aan document
            PdfPage page = document.AddPage();

            // XGraphics object maken om de tekst te kunnen
            // 'tekenen' op de pagina
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // lettertype instellen
            // deze constructor gebruikt lettertype en lettergrootte
            XFont font = new XFont("Verdana", 12);

            // tekst op pagina tekenen
            // xrect --> rechthoek waarin tekst geplaatst wordt
            // eerste twee parameters bij xrect --> marge links en boven
            // laatste twee parameters bij xrect --> marge rechts en onder
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(txtStory.Text,font,XBrushes.Black,
                new XRect(100,100,page.Width-200,page.Height-200));

            // waar en hoe op te slaan
            string path = @"C:\verhalen\" + txtTitle.Text + ".pdf";

            // opslaan
            document.Save(path);

            // testen of PDF kan geopend worden
            // met standaard PDF programma van PC
            Process.Start(path);
        }
    }
}
