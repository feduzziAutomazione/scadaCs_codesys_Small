using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodesysNetVars;

namespace scadaCs_codesys_Small
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Thread tRead;

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }
        ArrayList readValues;

        public void Read() 
        {
            SRCodesysNetVars sRead = new SRCodesysNetVars();
            sRead.CobID = 2;
            sRead.IPAdress = "192.168.250.3";
            sRead.Port = 1203;

            sRead.dataTypeCollection.Add(new CDataTypeCollection(DataTypes.lrealtype));
            

            while (true) 
            {
                try
                {
                    readValues = sRead.ReadValues();
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex + " " + sRead.CobID, sRead.CobID.ToString());
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label1.Text = readValues[0].ToString();
            }
            catch { label1.Text = "timeout"; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tRead = new Thread(new ThreadStart(Read));
            tRead.Start();

            Thread.Sleep(100);
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch
            { }
            try 
            {
                Environment.Exit(1);
            }
            catch { }
            }

    }
}
