using System;

namespace MegaDesk_3_ToddRhoads
{
    class DeskQuote
    {
        Desk desk;
        String name;
        String date = DateTime.Now.ToString("MMMM dd, yyyy");
        int drawerNumber;
        String surfaceMaterial;
        int shippingDays;
        float surfaceArea;
        decimal finalDeskCost;

        public DeskQuote()
        {

        }

        private static double calculateShippingPrice(int noOfDays, double width, double depth)
        {
            double shippingPrice = 0;
            double surfaceArea = width * depth;

            return shippingPrice;
        }


        internal Desk Desk { get => desk; set => desk = value; }
        public string Name { get => name; set => name = value; }
        public string Date { get => date; set => date = value; }
        public int DrawerNumber { get => drawerNumber; set => drawerNumber = value; }
        public string SurfaceMaterial { get => surfaceMaterial; set => surfaceMaterial = value; }
        public int ShippingDays { get => shippingDays; set => shippingDays = value; }
        public float SurfaceArea { get => surfaceArea; set => surfaceArea = value; }
        public decimal FinalDeskCost { get => finalDeskCost; set => finalDeskCost = value; }

        internal void calcualateSurfaceArea()
        {
            this.SurfaceArea = this.desk.Width * this.desk.Depth;
        }

        internal void calculateQuote()
        {
            decimal baseCost = 200;
            decimal extraSurfaceAreaCost = 0;
            decimal materialTypeCost = 0;
            decimal shippingCost = 0;
            decimal totalCost = 0;
            decimal drawerCost;
            int[,] priceList = GetRushOrder(); 

            if (surfaceArea > 1000)
            {
                extraSurfaceAreaCost = (decimal)surfaceArea - 1000;
            }
            switch (SurfaceMaterial)
            {



                case "Pine":
                    materialTypeCost = 50;
                    break;
                case "Oak":
                    materialTypeCost = 200;
                    break;
                case "Laminate":
                    materialTypeCost = 100;
                    break;
                case "Rosewood":
                    materialTypeCost = 30;
                    break;
                case "Veneer":
                    materialTypeCost = 125;
                    break;
            }

            drawerCost = drawerNumber * 50;
            if (ShippingDays == 14)
            {
                shippingCost += 0;
            }

            else
            {
                if (surfaceArea < 1000)
                {
                    switch (shippingDays)
                    {
                        case 3:
                            shippingCost = priceList[0,0] ;
                            break;
                        case 5:
                            shippingCost += priceList[0, 1];
                            break;
                        case 7:
                            shippingCost += priceList[0, 2];
                            break;


                    }
                }
                else if (surfaceArea >= 1000 && surfaceArea <= 2000)
                {

                    switch (shippingDays)
                    {
                        case 3:
                            shippingCost += priceList[1, 0];
                            break;
                        case 5:
                            shippingCost += priceList[1, 1];
                            break;
                        case 7:
                            shippingCost += priceList[1, 2];
                            break;

                    }
                }
                else
                {
                    switch (shippingDays)
                    {
                        case 3:
                            shippingCost += priceList[2, 0];
                            break;
                        case 5:
                            shippingCost += priceList[2, 1];
                            break;
                        case 7:
                            shippingCost += priceList[2, 2];
                            break;


                    }
                }
            }



            totalCost = materialTypeCost + baseCost + shippingCost + extraSurfaceAreaCost + drawerCost;
            this.FinalDeskCost = totalCost;
            Console.WriteLine(this.FinalDeskCost);
        }

        int[,] GetRushOrder()
        {
            try
            {
                String[] lines = System.IO.File.ReadAllLines(@"rushOrderPrices.txt");


                int[,] array1 = new int[3, 3];

                int lineIndex = 0;
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        array1[i, j] = int.Parse(lines[lineIndex]);
                        lineIndex++;
                    }

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.WriteLine(array1[i, j]);
                    }

                }
                return array1;
            }

            catch {
                System.Windows.Forms.MessageBox.Show("There is a problem wi8th our server, please try again later.");
                return null;
            }
        }
    }
}
    





