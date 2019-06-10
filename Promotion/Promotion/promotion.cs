using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace Promotion
{
    class promotion
    {
        SpeechSynthesizer speaker = new SpeechSynthesizer();
        SpeechRecognitionEngine reciever = new SpeechRecognitionEngine();
        Choices list = new Choices();
        Dictionary<String, String> numbers = new Dictionary<string, string>();
        Dictionary<String, String> pieces = new Dictionary<string, string>();
        public bool recording = false;
        String move = "";
        Grammar gr;
        public SpeechSynthesizer getSpeaker()
        {
            return speaker;
        }
        public string listen()
        {
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
                if (move != "")
                    return move;
                return "";
            }
            /*
            catch
            {
                return "-1";
            }*/

            return move;
        }
        public void setup()
        {

            numbers["one"] = "1";
            numbers["two"] = "2";
            numbers["three"] = "3";
            numbers["four"] = "4";
            numbers["five"] = "5";
            numbers["six"] = "6";
            numbers["seven"] = "7";
            numbers["eight"] = "8";

            pieces["bishop"] = "b";
            pieces["queen"] = "q";
            pieces["rook"] = "r";
            pieces["knight"] = "n";
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
            String[] moves = new String[4102];
            moves[4096] = "yes";
            moves[4097] = "no";
            moves[4098] = "queen";
            moves[4099] = "knight";
            moves[4100] = "bishop";
            moves[4101] = "rook";

            //7etet promotions kda
            int index = 0;
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    moves[index] = places[i] + " to " + places[j];
                    index++;
                }
            }
            index = 4098;

            String[] promotions = new String[515];
            promotions[512] = "yes";
            promotions[513] = "no";
            promotions[514] = "voice";
            index = 0;
            String[] row2 = { "eh two", "b two", "c two", "d two", "e two", "f two", "g two", "h two" };
            String[] row1 = { "eh one", "b one", "c one", "d one", "e one", "f one", "g one", "h one" };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row2[i] + " to " + row1[j] + " queen";
                    index++;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row2[i] + " to " + row1[j] + " knight";
                    index++;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row2[i] + " to " + row1[j] + " rook";
                    index++;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row2[i] + " to " + row1[j] + " bishop";
                    index++;
                }
            }


            String[] row7 = { "eh seven", "b seven", "c seven", "d seven", "e seven", "f seven", "g seven", "h seven" };
            String[] row8 = { "eh eight", "b eight", "c eight", "d eight", "e eight", "f eight", "g eight", "h eight" };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row7[i] + " to " + row8[j] + " queen";
                    index++;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row7[i] + " to " + row8[j] + " knight";
                    index++;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row7[i] + " to " + row8[j] + " rook";
                    index++;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    promotions[index] = row7[i] + " to " + row8[j] + " bishop";
                    index++;
                }
            }

            // list.Add(new String[] {"eh1 to b1","eh1 to c1","eh1 to d1"});
            list.Add(promotions);
            gr = new Grammar(new GrammarBuilder(list));
            //Grammar gr = new Grammar(build);
            speaker.SelectVoiceByHints(VoiceGender.Male);
            speaker.Speak("hello dark horse I am ready");

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

        }
        private String finalize(String phrase)
        {
            if (phrase.Equals("yes") || phrase.Equals("no"))
                return phrase;
            if (phrase.Equals("bishop"))
                return "b";
            if (phrase.Equals("knight"))
                return "n";
            if (phrase.Equals("queen"))
                return "q";
            if (phrase.Equals("rook"))
                return "r";
            if (phrase.Equals("voice"))
                return "v";

            String first;
            String socend;
            String third;
            String fourth;
            string fifth = "";
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
                int i = index + 3;
                int counter = 0;
                while (i < phrase.Length && phrase[i].Equals(' ') == false)
                {
                    i++;
                    counter++;
                }
                fourth = numbers[phrase.Substring(index + 3, counter)];
                if ((index + 3 + counter) < phrase.Length)
                    fifth =pieces[ phrase.Substring(index + 4 + counter)];
            }
            else
            {
                third = phrase.Substring(index, 1);
                int i = index + 2;

                int counter = 0;
                while (i < phrase.Length && phrase[i].Equals(' ') == false)
                {
                    i++;
                    counter++;
                }
                fourth = numbers[phrase.Substring(index + 2, counter)];
                if ((index + 2 + counter) < phrase.Length)
                    fifth =pieces[ phrase.Substring(index + 3 + counter)];

                //fourth = numbers[phrase.Substring(index + 2)];
            }



            finalInstruction = first + socend + third + fourth + fifth;
            return finalInstruction;
        }

        private void reciever_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (recording == false)
                return;
            // move = "";
            String phrase = e.Result.Text;
            String final = finalize(phrase); //this is the final string instruction
            move = final;
            recording = false;
            Console.WriteLine(move);
            speaker.Speak(phrase); //for testing

        }
    }
}
