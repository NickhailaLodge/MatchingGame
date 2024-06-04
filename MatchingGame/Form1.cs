using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        Random rnd = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        /// <summary>
        /// Assign each icon from the list of icons to a random square
        /// </summary>

        private void AssignIconsToSquares()
        {
            // The TableLayoutPanel has 16 labels,
            // and the icon list has 16 icons,
            // so an icon is pulled at random from the list
            // and added to each label

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = rnd.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        /// <summary>
        /// Every label's Click event is handled by this event handler
        /// </summary>
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                //if the clicked label is black, the player clicked
                //an icon that's already been revealed --
                //ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }
                clickedLabel.ForeColor = Color.Black;
            }
        }
    }
}
