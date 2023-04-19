using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;
using static System.Net.WebRequestMethods;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        List<Process> runningProcesses = new List<Process>();
        List<string> matchingFiles = new List<string>();
        List<string> labels = new List<string>();
        List<CheckBox> specifiedCheckBoxes = new List<CheckBox>();




        public Form1()
        {
            InitializeComponent();

            specifiedCheckBoxes.Add(checkBox1);
            specifiedCheckBoxes.Add(checkBox2);
            specifiedCheckBoxes.Add(checkBox3);
            specifiedCheckBoxes.Add(checkBox4);
            specifiedCheckBoxes.Add(checkBox5);
            specifiedCheckBoxes.Add(checkBox6);
            specifiedCheckBoxes.Add(checkBox7);
            specifiedCheckBoxes.Add(checkBox8);
            specifiedCheckBoxes.Add(checkBox9);
            specifiedCheckBoxes.Add(checkBox10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string rootDirectory = folderBrowserDialog.SelectedPath;
                    string fileName = "BinanceTradingBot.exe"; // Replace with your file name
                    string configName = "BinanceTradingBot.exe.config";

                    string[] directoriesToSearch = Directory.GetDirectories(rootDirectory);

                    foreach (string directory in directoriesToSearch)
                    {
                        string fullPath = Path.Combine(directory, "BinanceTradingBot", "bin", "Debug", fileName);
                        if (System.IO.File.Exists(fullPath))
                        {
                            matchingFiles.Add(fullPath);
                        }

                        string configFilePath = Path.Combine(directory, "BinanceTradingBot", "bin", "Debug", configName);

                        // Load the config file as an XML document
                        XmlDocument configXml = new XmlDocument();
                        configXml.Load(configFilePath);

                        // Get the value of the "symbol" key from the appSettings section
                        XmlNode symbolNode = configXml.SelectSingleNode("//appSettings/add[@key='symbol']");
                        string symbolValue = symbolNode.Attributes["value"].Value;

                        // Update the label with the symbol value
                        labels.Add(symbolValue);
                    }



                    //if (matchingFiles.Count > 0)
                    //{
                    //    MessageBox.Show("Found " + matchingFiles.Count + " matching file(s):\n" + string.Join("\n", matchingFiles));
                    //}
                    //else
                    //{
                    //    MessageBox.Show("No matching file found.");
                    //}


                    if (matchingFiles.Count <= 0)
                    {
                        MessageBox.Show("Wrong folder used.");
                    }
                    else
                        for (int i = 0; i < labels.Count && i < 10; i++)
                        {
                            if (i == 0) // update label1
                                label1.Text = labels[i];
                            else if (i == 1) // update label2
                                label2.Text = labels[i];
                            else if (i == 2) // update label3
                                label3.Text = labels[i];
                            else if (i == 3) // update label4
                                label4.Text = labels[i];
                            else if (i == 4) // update label5
                                label5.Text = labels[i];
                            else if (i == 5) // update label6
                                label6.Text = labels[i];
                            else if (i == 6) // update label7
                                label7.Text = labels[i];
                            else if (i == 7) // update label8
                                label8.Text = labels[i];
                            else if (i == 8) // update label9
                                label9.Text = labels[i];
                            else if (i == 9) // update label10
                                label10.Text = labels[i];
                        }

                }
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Process process in runningProcesses)
            {
                process.Kill();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        // Run Button
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < matchingFiles.Count && i < specifiedCheckBoxes.Count; i++)
            {
                if (specifiedCheckBoxes[i].Checked)
                {
                    Process process = Process.Start(matchingFiles[i]);
                    runningProcesses.Add(process);
                }
            }

            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            // Get the trigger checkbox that was checked/unchecked
            CheckBox triggerCheckBox = (CheckBox)sender;

            // Loop through the other specified checkboxes and set their Checked property to be the same as the trigger checkbox
            foreach (CheckBox otherCheckBox in specifiedCheckBoxes)
            {
                if (otherCheckBox != triggerCheckBox) // Make sure not to check/uncheck the trigger checkbox again
                {
                    otherCheckBox.Checked = triggerCheckBox.Checked;
                }
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}