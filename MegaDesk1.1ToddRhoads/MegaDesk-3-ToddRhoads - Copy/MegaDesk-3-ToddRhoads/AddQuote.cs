using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MegaDesk_3_ToddRhoads
{
    public partial class AddQuote : Form
    {

        public AddQuote()
        {

            InitializeComponent();
            populateCombo();

        }

        public enum materials
        {
            Pine,
            Rosewood,
            Veneer,
            Oak,
            Laminate

        }

        private void populateCombo()
        {
            string[] enumList = Enum.GetNames(typeof(materials));
            List<String> materialList = new List<String>(enumList);
            MaterialCombo.DataSource = materialList;
        }

        private void CreateQuote_Click(object sender, EventArgs e)
        {


            try
            {
                DeskQuote deskQuote = new DeskQuote();
                Desk desk = new Desk();
                List<DeskQuote> deskQuotes = Program.DeskQuotes;
                deskQuote.SurfaceMaterial = MaterialCombo.SelectedItem.ToString();
                desk.Depth = float.Parse(DepthBox.Text);
                desk.Width = float.Parse(WidthBox.Text);

                deskQuote.Desk = desk;

                if (customerNameBox.Text == "")
                {
                    MessageBox.Show("Please Enter the Customer's name");
                    return;
                }

                deskQuote.Name = customerNameBox.Text;

                if (threeDayRadio.Checked)
                {
                    deskQuote.ShippingDays = 3;
                }

                else if (fiveDayRadio.Checked)
                {
                    deskQuote.ShippingDays = 5;
                }

                else if (sevenDayRadio.Checked)
                {
                    deskQuote.ShippingDays = 7;
                }

                else if (normalRadio.Checked)
                {
                    deskQuote.ShippingDays = 14;
                }


                if ((int.Parse(drawerNumberBox.Text) > 0 && int.Parse(drawerNumberBox.Text) < 8))
                {

                    deskQuote.DrawerNumber = int.Parse(drawerNumberBox.Text);
                }

                else
                {
                    MessageBox.Show("Please Enter a Drawer Number Between 1 & 7");
                    return;
                }



              

                if (desk.Width < 24 || desk.Width > 96 || desk.Depth < 12 || desk.Depth > 48)
                {
                    MessageBox.Show("Please enter a width between 24 and 96 inches and a depth between 12 and 48 inches.");

                }

                else
                {

                    deskQuote.calcualateSurfaceArea();
                    deskQuote.calculateQuote();


                    String width;
                    String depth;
                    String material;
                    String shippingDay;
                    String finalCost;
                    String name;
                    String date;
                    StringBuilder sb = new StringBuilder();





                    name = deskQuote.Name;
                    shippingDay = deskQuote.ShippingDays.ToString();
                    width = desk.Width.ToString();
                    depth = desk.Depth.ToString();
                    material = deskQuote.SurfaceMaterial;
                    finalCost = "$" + deskQuote.FinalDeskCost.ToString();
                    date = deskQuote.Date;
                    sb.AppendLine(name + " ," + width + " ," + depth + " ," + material + " ," + shippingDay + ", " + finalCost + "," + date);
                    String filePath = "quotes.txt";
                    File.AppendAllText(filePath, sb.ToString());

                   
                    deskQuotes.Add(deskQuote);
                    Program.DeskQuotes = deskQuotes;

                    DisplayQuote addNewViewQuoteForm = new DisplayQuote();
                    addNewViewQuoteForm.Tag = this;
                    addNewViewQuoteForm.Show(this);
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                float width = float.Parse(WidthBox.Text);
                float depth = float.Parse(DepthBox.Text);
                float surfaceArea = width * depth;

                if (width < 24 || width > 96 || depth < 12 || depth > 48)
                {
                    MessageBox.Show("Please enter a width between 24 and 96 inches and a depth between 12 and 48 inches.");
                    return;
                }

                else
                {

                    if (surfaceArea < 1000)
                    {
                        threeDayRadio.Text = "3 Days (+ $60)";
                        fiveDayRadio.Text = "5 Days (+ $40)";
                        sevenDayRadio.Text = "7 Days (+ $30)";
                        threeDayRadio.Visible = true;
                        fiveDayRadio.Visible = true;
                        sevenDayRadio.Visible = true;
                    }

                    else if (surfaceArea >= 1000 && surfaceArea < 2000)
                    {
                        threeDayRadio.Text = "3 Days (+ $70)";
                        fiveDayRadio.Text = "5 Days (+ $50)";
                        sevenDayRadio.Text = "7 Days (+ $35)";
                        threeDayRadio.Visible = true;
                        fiveDayRadio.Visible = true;
                        sevenDayRadio.Visible = true;
                    }

                    else
                    {
                        threeDayRadio.Text = "3 Days (+ $80)";
                        fiveDayRadio.Text = " 5 Days (+ $60)";
                        sevenDayRadio.Text = "7 Days (+ $40)";
                        threeDayRadio.Visible = true;
                        fiveDayRadio.Visible = true;
                        sevenDayRadio.Visible = true;
                    }

                }


            }




            catch
            {
                MessageBox.Show("To see shipping options, you must enter a width and depth.");

            }



        }

    }
    }
