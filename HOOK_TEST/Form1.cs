using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HOOK_TEST
{
    
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gHook;
        Translation t;
        StreamWriter sw;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            gHook=new GlobalKeyboardHook();
            gHook.KeyDown+=new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            String date = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            sw = new StreamWriter("TestFile_" + date + ".txt");
            sw.WriteLine(DateTime.Now);
            timer1.Start();
            textBox1.Text = DateTime.Now.ToString();
            gHook.hook();
        }
        public void gHook_KeyDown(object sender, KeyEventArgs e) 
        {
            t = new Translation();
            textBox1.Text += t.translater(e.KeyValue).ToString();
            sw.Write(t.translater(e.KeyValue).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String date = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            sw = new StreamWriter("TestFile_" + date + ".txt");
            sw.WriteLine(DateTime.Now);
            textBox1.Text = DateTime.Now.ToString();
            gHook.hook();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gHook.unhook();
            sw.Close();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
            sw.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sw.Close();
            String date = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            sw = new StreamWriter("TestFile_" + date + ".txt");
            sw.WriteLine(DateTime.Now);
        }
    }
}