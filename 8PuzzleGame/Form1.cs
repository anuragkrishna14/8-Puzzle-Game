using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8PuzzleGame
{
    public partial class Form1 : Form
    {
             
        string[] homeVal = new string[9];
        string[] curVal = new string[9];
        bool isSolved;
        string posOfBlank="0", myPosition="0";
        string labelName;
        //bool runOrNot;
        int moves = 0;

        public void setHomeVal()
        {
            for(int i = 0; i<9 ; i++)
            {
                homeVal[i] = Convert.ToString(i + 1);
            }
            homeVal[8] = "";
        }

        public void setCurVal()
        {
            curVal[0] = label1.Text;
            curVal[1] = label2.Text;
            curVal[2] = label3.Text;
            curVal[3] = label4.Text;
            curVal[4] = label5.Text;
            curVal[5] = label6.Text;
            curVal[6] = label7.Text;
            curVal[7] = label8.Text;
            curVal[8] = label9.Text;
        }

        public void updateCurVal()
        {
            label1.Text = curVal[0];
            label2.Text = curVal[1];
            label3.Text = curVal[2];
            label4.Text = curVal[3];
            label5.Text = curVal[4];
            label6.Text = curVal[5];
            label7.Text = curVal[6];
            label8.Text = curVal[7];
            label9.Text = curVal[8];
        }

        public void checkEqual()
        {
            int counter = 0, i;
            for(i=0; i<9; i++)
            {
                if(homeVal[i]==curVal[i])
                {
                    counter++;
                }
            }
            if(counter == 9)
            {
                isSolved = true;
            }
            else
            {
                isSolved = false;
            }
        }

        public Label getLabelName()
        {
            string name = labelName;
            switch (name)
            {
                case "lbl1":
                    return label1;
                //break;
                case "lbl2":
                    return label2;
                //break;
                case "lbl3":
                    return label3;
                //break;
                case "lbl4":
                    return label4;
                //break;
                case "lbl5":
                    return label5;
                //break;
                case "lbl6":
                    return label6;
                //break;
                case "lbl7":
                    return label7;
                //break;
                case "lbl8":
                    return label8;
                //break;
                default:
                    return label9;
                //break;
            }
        }


        public void getPosOfBlank()
        {
            for(int i = 0; i<9; i++)
            {
                if(curVal[i]=="")
                {
                    posOfBlank = homeVal[i];
                    if(homeVal[i]=="")
                    {
                        posOfBlank = "9";
                    }
                }
            }
        }

        
        public void getMyPosition(Label clicked)
        {
            for(int i = 0; i<9; i++)
            {
                if(curVal[i]==clicked.Text)
                {
                    myPosition = homeVal[i];
                    if (homeVal[i] == "")
                    {
                        myPosition = "9";
                    }
                }
            }
        }

        public bool swapable()
        {
            int pob, mp;
            pob = Convert.ToInt32(myPosition);
            mp = Convert.ToInt32(posOfBlank);
            if (Math.Abs(pob - mp) == 1 || Math.Abs(pob - mp) == 3)
            {
                if (pob == 1 || pob == 2 || pob == 5 || pob == 8 || pob == 9)
                {
                    return true;
                }
                if ((pob == 3 || pob == 6) && (mp - pob != 1))
                {
                    return true;
                }
                if ((pob == 4 || pob == 7) && (pob - mp != 1))
                {
                    return true;
                }
            }
            return false;
        }

        public void swapText(Label click)
        {
            string temp;
            temp = click.Text;
            getPosOfBlank();
            int blankPos = Convert.ToInt32(posOfBlank);
            getMyPosition(click);
            int myPos = Convert.ToInt32(myPosition);            
            curVal[myPos - 1] = "";
            curVal[blankPos - 1] = click.Text;
            updateCurVal();
        }

        public void runGame()
        {
            if (label10.Text == "")
            {
                lblBlock.Width = 452;
                lblBlock.Height = 0;
                //runOrNot = false;

                setHomeVal();
                setCurVal();
                //checkEqual();
                label10.Text = "";
                getPosOfBlank();
                getMyPosition(getLabelName());
                if (swapable())
                {
                    swapText(getLabelName());
                    moves++;
                    label11.Text = moves.ToString();
                }
                checkEqual();
            }
            if (isSolved)
            {
                label10.Text = "Solved";
                //lblBlock.Width = 455;
                //lblBlock.Height = 450;
                afterSolved();
            }
        }

        public void reshuffle()
        {
            label11.Text = "0";
            moves = 0;
            label10.Text = "";
            string[] rndNum = new string[9];
            string r;
            int i, j, nine = 0;
            bool flag = false;
            i = 0;
            do
            {
                Random rnd = new Random();
                r = Convert.ToString(rnd.Next(1, 10));
                for (j = 0; j <= i; j++)
                {
                    if (rndNum[j] == r)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true)
                {
                    flag = false;
                }
                else
                {
                    rndNum[i] = r;
                    if (rndNum[i] == "9")
                    {
                        nine = i;
                    }
                    i = i + 1;
                }
            } while (i < 9);

            rndNum[nine] = "";

            label1.Text = Convert.ToString(rndNum[0]);
            label2.Text = Convert.ToString(rndNum[1]);
            label3.Text = Convert.ToString(rndNum[2]);
            label4.Text = Convert.ToString(rndNum[3]);
            label5.Text = Convert.ToString(rndNum[4]);
            label6.Text = Convert.ToString(rndNum[5]);
            label7.Text = Convert.ToString(rndNum[6]);
            label8.Text = Convert.ToString(rndNum[7]);
            label9.Text = Convert.ToString(rndNum[8]);
            bool noShuffleAgain = isSolvable();
            if (!noShuffleAgain)
            {
                reshuffle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = homeVal[0];
            label2.Text = homeVal[1];
            label3.Text = homeVal[2];
            label4.Text = homeVal[3];
            label5.Text = homeVal[4];
            label6.Text = homeVal[5];
            label7.Text = homeVal[6];
            label8.Text = homeVal[7];
            label9.Text = homeVal[8];
            label10.Text = "Solved";
            label11.Text = "0";
            moves = 0;
            label10.Visible = false;
            lblBlock.Width = 452;
            lblBlock.Height = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reshuffle();
            label10.Visible = false;
            lblBlock.Width = 452;
            lblBlock.Height = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reshuffle();
            setHomeVal();
            setCurVal();
            if (isSolved)
            {
                label10.Text = "Solved";
            }
            else
            {
                label10.Text = "";
            }
            runGame();
            label11.Text = "0";
        }          


        private void label1_Click(object sender, EventArgs e)
        {
            labelName = "lbl1";
            runGame();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            labelName = "lbl2";
            runGame();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            labelName = "lbl3";
            runGame();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            labelName = "lbl4";
            runGame();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            labelName = "lbl5";
            runGame();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            labelName = "lbl6";
            runGame();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            labelName = "lbl7";
            runGame();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            labelName = "lbl8";
            runGame();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            labelName = "lbl9";
            runGame();
        }

        bool isSolvable()
        {
            int myValue, afterValue, counter = 0;
            string[] val = new string[9];
            val[0] = label1.Text;
            val[1] = label2.Text;
            val[2] = label3.Text;
            val[3] = label4.Text;
            val[4] = label5.Text;
            val[5] = label6.Text;
            val[6] = label7.Text;
            val[7] = label8.Text;
            val[8] = label9.Text;

            for (int i = 0; i<9; i++)
            {
                if (val[i] == "")
                {
                    continue;
                }
                myValue = Convert.ToInt32(val[i]);
                for (int j=i+1; j<9; j++)
                {
                    if (val[j] == "")
                    {
                        continue;
                    }
                    afterValue = Convert.ToInt32(val[j]);
                    if (afterValue < myValue)
                    {
                        counter = counter + 1;
                    }
                }
            }
            if (counter % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        int timerCounter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timerCounter++;
            lblBlock.Height = lblBlock.Height + 5;
            if (timerCounter == 90)
            {
                //lblBlock.Width = 455;
                lblBlock.Height = 450;
                timerCounter = 0;
                timer1.Enabled = false;
                //lblBlock.Text = "SOLVED";
                label10.Visible = true;
            }
        }

        void afterSolved()
        {
            timer1.Enabled = true;

        }

    }
}
