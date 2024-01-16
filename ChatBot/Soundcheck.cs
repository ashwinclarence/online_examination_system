using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Speech;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace ChatBot
{
    public partial class Soundcheck : Form
    {
        public Soundcheck()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = 0;     // -10...10
            synthesizer.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Child);
            synthesizer.Speak(textBox1.Text);
        }
    }
}
