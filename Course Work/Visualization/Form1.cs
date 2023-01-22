using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTMLCrawler;
using System.IO;

namespace Visualization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            int x = 10;
            int y = 10;

            string document = File.ReadAllText("../../../../HTMLCrawler/demo.html");
            string wholeText = "";
            string[] result = CustomString.SplitByString(document, "\r\n");
            List<string> tableElements = new List<string>();


            for (int i = 0; i < result.Length - 1; i++)
            {

                string row = result[i];
                result[i] += '>';
                row += '>';
                string firstTag, data;
                string lastTag = null;

           

                if (!CustomString.Contains(row, "href") && !CustomString.Contains(row, "src"))
                {

                    firstTag = HTMLCrawler.Program.GetFirstHtmlTag(row);
                    lastTag = HTMLCrawler.Program.GetLastHtmlTag(firstTag);
                    data = HTMLCrawler.Program.GetData(row, firstTag, lastTag);

                }
                else
                {
                    firstTag = HTMLCrawler.Program.GetSelfClosingTag(row);

                    if (CustomString.Contains(row, "href"))
                    {
                        lastTag = HTMLCrawler.Program.GetLastHtmlTag(firstTag);
                    }

                    data = HTMLCrawler.Program.GetDataOfSelfClosingTag(row);
                }


                if (firstTag == "<td>")
                {
                    tableElements.Add(data);
                }


                if (data != String.Empty)
                {
                    wholeText += $"{data}\r\n";
                }
            }
 string[] lines2 = wholeText.Split("\r\n");

            foreach(var line2 in lines2)
            {
                Font font = new Font("Arial", 20);
                Point point = new Point(x, y);
                Pen pen = new Pen(Color.Gray, 1);



                TextRenderer.DrawText(e.Graphics, line2, font, point, Color.Black);

                if (tableElements.Contains(line2))
                {
                    graphics.DrawRectangle(pen, x, y, 50, 40);
                }

                y += 55;
                

            } 
        }

            void Form1_Load(object sender, EventArgs e)
            {


            }
    }
}
