using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;
using static System.Net.WebRequestMethods;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        List<Process> runningProcesses = new List<Process>();

        public Form1()
        {
            InitializeComponent();
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

                    List<string> matchingFiles = new List<string>();

                    string[] directoriesToSearch = Directory.GetDirectories(rootDirectory);

                    foreach (string directory in directoriesToSearch)
                    {
                        string fullPath = Path.Combine(directory, "BinanceTradingBot", "bin", "Debug", fileName);
                        if (System.IO.File.Exists(fullPath))
                        {
                            matchingFiles.Add(fullPath);
                        }
                    }

                    if (matchingFiles.Count > 0)
                    {
                        foreach (string matchingFile in matchingFiles)
                        {
                            Process process = Process.Start(matchingFile);
                            runningProcesses.Add(process);
                        }
                        MessageBox.Show("Found " + matchingFiles.Count + " matching file(s):\n" + string.Join("\n", matchingFiles));
                    }
                    else
                    {
                        MessageBox.Show("No matching file found.");
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
            List<string> labels = new List<string>();
            foreach (Process process in runningProcesses)
            {
                try
                {
                    // Get the main module for the process
                    ProcessModule mainModule = process.MainModule;

                    // Get the path of the config file
                    string configFilePath = Path.Combine(Path.GetDirectoryName(mainModule.FileName), mainModule.ModuleName + ".config");

                    // Load the config file as an XML document
                    XmlDocument configXml = new XmlDocument();
                    configXml.Load(configFilePath);

                    // Get the value of the "symbol" key from the appSettings section
                    XmlNode symbolNode = configXml.SelectSingleNode("//appSettings/add[@key='symbol']");
                    string symbolValue = symbolNode.Attributes["value"].Value;

                    // Update the label with the symbol value
                    labels.Add(symbolValue);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur while trying to get the symbol value for this process
                    MessageBox.Show("Error retrieving symbol value for process " + process.Id + ": " + ex.Message);
                }
            }
            // update labels iteratively
            for (int i = 0; i < labels.Count; i++)
            {
                if (i == 0) // update label1
                    label1.Text = labels[i];
                else if (i == 1) // update label2
                    label2.Text = labels[i];
                // add more if statements for additional labels
            }
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
    }
}