
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace voice_bot
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speaker = new SpeechSynthesizer();
        SpeechRecognitionEngine reciever = new SpeechRecognitionEngine();
        Choices list = new Choices();
        Dictionary<String, String> numbers = new Dictionary<string, string>();
        Boolean recording = false;
        String move = "";
        private void setup()
        {
            numbers["one"] = "1";
            numbers["two"] = "2";
            numbers["three"] = "3";
            numbers["four"] = "4";
            numbers["five"] = "5";
            numbers["six"] = "6";
            numbers["seven"] = "7";
            numbers["eight"] = "8";
           
            GrammarBuilder build = new GrammarBuilder();
            build.AppendDictation();
            // list.Add(new String[]{"restart","eh2","eh","b","c","d","e","f","g","h","1","2","3","4","5","6","7","8"});
            String[] places = new String[]{"eh one","eh two","eh three","eh four","eh five","eh six","eh seven","eh eight",
                                           "b one","b two","b three","b four","b five","b six","b seven","b eight",
                                           "c one","c two","c three","c four","c five","c six","c seven","c eight",
                                           "d one","d two","d three","d four","d five","d six","d seven","d eight",
                                           "e one","e two","e three","e four","e five","e six","e seven","e eight",
                                           "f one","f two","f three","f four","f five","f six","f seven","f eight",
                                           "g one","g two","g three","g four","g five","g six","g seven","g eight",
                                           "h one","h two","h three","h four","h five","h six","h seven","h eight"};
            String[] moves = new String[4098+4096];
            moves[4096] = "yes";
            moves[4097] = "no";
            moves[4098] = "agent";
            int index = 0;
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    moves[index] = places[i] + " to " + places[j];
                    index++;
                }
            }

            // list.Add(new String[] {"eh1 to b1","eh1 to c1","eh1 to d1"});
            list.Add(moves);
            Grammar gr = new Grammar(new GrammarBuilder(list));
            //Grammar gr = new Grammar(build);

            try
            {
                reciever.RequestRecognizerUpdate();
                reciever.LoadGrammar(gr);
                reciever.SpeechRecognized += reciever_SpeechRecognized;
                reciever.SetInputToDefaultAudioDevice();
                reciever.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }

            //speaker.SelectVoiceByHints(VoiceGender.Female);
            speaker.Speak("hello chess fuckers I am ready");
        }
        private String finalize(String phrase)
        {
            if (phrase.Equals("yes") || phrase.Equals("no"))
                return phrase;
            String first;
            String socend;
            String third;
            String fourth;
            String finalInstruction;
            int index;
            if (phrase[0].Equals('e') && phrase[1].Equals('h'))
            {
                first = "a";
                int counter = 0;
                int i = 3;
                while (phrase[i].Equals(' ') == false)
                {
                    i++;
                    counter++;
                }
                socend = numbers[phrase.Substring(3, counter)] + phrase.Substring(3 + counter, 4);
                index = 3 + counter + 4;
            }
            else
            {
                first = phrase.Substring(0, 1);
                int counter = 0;
                int i = 2;
                while (phrase[i].Equals(' ') == false)
                {
                    i++;
                    counter++;
                }
                socend = numbers[phrase.Substring(2, counter)] + phrase.Substring(2 + counter, 4);
                index = 2 + counter + 4;
            }


            if (phrase[index].Equals('e') && phrase[index + 1].Equals('h'))
            {
                third = "a";
                fourth = numbers[phrase.Substring(index + 3)];
            }
            else
            {
                third = phrase.Substring(index, 1);
                fourth = numbers[phrase.Substring(index + 2)];
            }



            finalInstruction = first + socend + third + fourth;
            return finalInstruction;
        }

        public Form1()
        {
            setup();
            InitializeComponent();
            button1.Enabled = true;
            button2.Enabled = false;
        }



        private void reciever_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            if (recording == false)
                return;

            String phrase = e.Result.Text;
            String final = finalize(phrase); //this is the final string instruction
            if (final.Equals("yes") || final.Equals("no"))
            {
                if (final.Equals("yes"))
                {
                    button1.Enabled = true;
                    button2.Enabled = false;
                    recording = false;
                    textBox2.Enabled = true;
                    textBox2.Text = "The final move is: " + move;
                    textBox2.Enabled = false;
              //      Console.WriteLine(move);
                }
                else
                {
                    textBox2.Enabled = true;
                    textBox2.Text = "Please Say again your desired move ";
                    textBox2.Enabled = false;
                }
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = final;
                textBox1.Enabled = false;

                textBox2.Enabled = true;
                textBox2.Text = "Are you sure you want " + final + " ?(Yes/NO)";
                textBox2.Enabled = false;

                move=final;
            }
            speaker.Speak(phrase);
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            recording = true;
            button2.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recording = false;
            button2.Enabled = false;
            button1.Enabled = true;
        }
    }
}
