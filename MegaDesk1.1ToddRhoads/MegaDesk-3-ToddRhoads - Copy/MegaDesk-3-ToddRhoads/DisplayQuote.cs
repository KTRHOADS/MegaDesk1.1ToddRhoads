using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;




namespace MegaDesk_3_ToddRhoads
{
    public partial class DisplayQuote : Form
    {
        public DisplayQuote()
        {
           
            InitializeComponent();
            List<DeskQuote> deskQuotes = Program.DeskQuotes;
            String[] widths = new string[deskQuotes.Count];
            String[] depths = new string[deskQuotes.Count];
            String[] materials = new string[deskQuotes.Count];
            String[] surfaceAreas = new string[deskQuotes.Count];
            String[] shippingDays = new string[deskQuotes.Count];
            String[] finalCosts = new string[deskQuotes.Count];
            String[] names = new string[deskQuotes.Count];
            String[] dates = new string[deskQuotes.Count];




            for (int i = 0; i < deskQuotes.Count; i++)
            {
                names[i] = deskQuotes[i].Name;
                shippingDays[i] = deskQuotes[i].ShippingDays.ToString();
                widths[i] = deskQuotes[i].Desk.Width.ToString();
                depths[i] = deskQuotes[i].Desk.Depth.ToString();
                surfaceAreas[i] = deskQuotes[i].SurfaceArea.ToString();
                materials[i] = deskQuotes[i].SurfaceMaterial;
                finalCosts[i] = "$" + deskQuotes[i].FinalDeskCost.ToString();
                dates[i] = deskQuotes[i].Date;
                listView.Items.Add(new ListViewItem(new string[] { names[i], widths[i], depths[i], materials[i], shippingDays[i], finalCosts[i], dates[i] }));
               var tempDate = DateTime.ParseExact(deskQuotes[i].Date, "MMMM dd, yyyy", null);
                var dateOnly = tempDate.Date;

                deskQuotes[i].Date = dateOnly.ToString("MM/dd/yyyy") ;
                var deskLine = Newtonsoft.Json.JsonConvert.SerializeObject(deskQuotes[i].Desk);
               
                var deskQuoteLine = Newtonsoft.Json.JsonConvert.SerializeObject(deskQuotes[i]);

                using (StreamWriter outputFile = new StreamWriter("deskQuote.json"))
                {
                    outputFile.WriteLine(deskLine);
                    outputFile.WriteLine(deskQuoteLine);
                    
                }

            }



        }





        private void button1_Click(object sender, EventArgs e)
        {
            AddQuote addNewQuoteForm = new AddQuote();
            addNewQuoteForm.Tag = this;
            addNewQuoteForm.Show(this);
            Hide();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DisplayQuote_Load(object sender, EventArgs e)
        {
            

        
            }

        private void button2_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.Tag = this;
            mm.Show(this);
            Hide();
        }
    }
           
        }
    


