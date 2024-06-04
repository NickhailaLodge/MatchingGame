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

        // firstClicked points to the first label control that player clicks,
        // but it will be null if the player hasn't clicked a label yet
        Label firstClicked = null;

        // secondClicked points to the second label control that the player clicks
        Label secondClicked = null;

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

                // if the firstClicked is null, this is the first icon in the pair that the player clicked
                // so set firstClicked to the label that the player clicked, change its color to black and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // if the player gets this far, the timer isn't running and firstClicked isn't null
                //so this must be the second icon the player clicked set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //check to see if the player won
                CheckForWinner();

                // if the player clicked two matching icons, keep them black and reset firstClicked and secondClicked
                //so the player can click another icon
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;

                    return;
                }

                // if the player gets this far, the player clicked two different icons, so start the timer
                // which will wait three quarters of a second and then hide the icons
                timer1.Start();
            }
        }
        
        //this timer is started when the player clicks two icons that don't match
        // so it counts three quarters of a second and then turns itself off and hides both icons.
        private void timer1_Tick(object sender, EventArgs e)
        {
            // stop the timer
            timer1.Stop();

            //hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //Reset firstClicked and secondClicked
            // so the next time a label is clicked, the program knows it is the first click
            firstClicked = null;
            secondClicked = null;
        }
        
        // check every icon to see if it is matched by comparing its foreground color to its background color
        //if all of the icons are matched. the player wins
        private void CheckForWinner()
        {
            // Go through all of the labels in thhe tablelayoutpanel,
            // checking each one to see if its icon is matched

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // if the loop didn't return, it didn't find any unmatched icons
            // that means the user won.
            // show a message and close the form
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}
