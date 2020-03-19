using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOutPuzzleGame
{
    public partial class PuzzleForm : Form
    {
        readonly Color lightsOn = Color.Aqua;
        readonly Color lightsOff = Color.Black;

        Random rd = new Random();
        Random rBoxes = new Random();
        bool[,] lightGrid = new bool[5, 5];
        public PuzzleForm()
        {        
            InitializeComponent();          
        }

        /// <summary>
        /// By providing the selected cell, this method will provide a list of all adjacent cells
        /// </summary>
        /// <param name="clickedCellPos"></param>
        /// <returns>List of TableLayoutPanelCellPosition with all adjacent cells including the selected one</returns>
        private List<TableLayoutPanelCellPosition> getCellsToInvert(TableLayoutPanelCellPosition clickedCellPos)
        {
            TableLayoutPanelCellPosition topCell;
            TableLayoutPanelCellPosition leftCell;
            TableLayoutPanelCellPosition rightCell;
            TableLayoutPanelCellPosition bottomCell;
            List<TableLayoutPanelCellPosition> adjCells = new List<TableLayoutPanelCellPosition>();

            //If there is a cell above, get cell posItion
            if (clickedCellPos.Row > 0)
            {   
                topCell = new TableLayoutPanelCellPosition(clickedCellPos.Column, clickedCellPos.Row - 1);
                adjCells.Add(topCell);
            }
            //If there is a cell on the left side, get cell position
            if (clickedCellPos.Column > 0)
            {
                leftCell = new TableLayoutPanelCellPosition(clickedCellPos.Column - 1, clickedCellPos.Row);
                adjCells.Add(leftCell);
            }
            //if  there is a cell below, get cell position
            if (clickedCellPos.Row < 4)
            {
                bottomCell = new TableLayoutPanelCellPosition(clickedCellPos.Column, clickedCellPos.Row + 1);
                adjCells.Add(bottomCell);
            }
            //if there is a cell on the right, get cell position
            if (clickedCellPos.Column <4)
            {
                rightCell = new TableLayoutPanelCellPosition(clickedCellPos.Column + 1, clickedCellPos.Row);
                adjCells.Add(rightCell);
            }
            adjCells.Add(clickedCellPos);
            return adjCells;
        }

        /// <summary>
        /// This method will invert the boolean of the cell and color
        /// </summary>
        /// <param name="cellsToInvert"></param>
        private void InvertCells(List<TableLayoutPanelCellPosition> cellsToInvert)
        {
            foreach (TableLayoutPanelCellPosition cell in cellsToInvert)
            {
                Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(cell.Column, cell.Row);
                if(btn.BackColor == lightsOff)
                {
                    btn.BackColor =lightsOn;                   
                }
                else
                {
                    btn.BackColor = lightsOff; 
                }
                lightGrid[cell.Column, cell.Row] = btn.BackColor != lightsOff;
               
            }
        }

        /// <summary>
        /// Loops through boolean matrix to check if all false, the player wins
        /// </summary>
        /// <returns>return false if the player wins or true if the player wins</returns>
        private bool CheckIfPlayerWins()
        {
            for (int i = 0; i<5; i++)
            {
                for (int x = 0; x<5; x++)
                {
                    if(lightGrid[i,x] == true)
                    {
                        //if true exit nested loop with true
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Generates random number from 1 till 6 . This number will decide how many lights will be turned on 
        /// initially. Then generat 2 random numbers from 0 till 4 to decide the turned on cell postion
        /// </summary>
        private void RandomStartLights()
        {          
            int lightNoBoxes = rBoxes.Next(1, 6);
            for (int i = 0; i <= lightNoBoxes; i++)
            {                       
                int xValue = rd.Next(0, 4);
                int yValue = rd.Next(0, 4);
                lightGrid[xValue, yValue] = true;
                Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(xValue, yValue);
                btn.BackColor = lightsOn;               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RandomStartLights();
            //EasyWinStartLights();
        }

        /// <summary>
        /// To turn on lights so that the player can easily win. This was done for testing purposes
        /// </summary>
        private void EasyWinStartLights()
        {
            lightGrid[3, 2] = true;
            lightGrid[2, 1] = true;
            lightGrid[2, 2] = true;
            lightGrid[2, 3] = true;
            lightGrid[1, 2] = true;
            Button btn1 = (Button)tableLayoutPanel1.GetControlFromPosition(3, 2);
            btn1.BackColor = lightsOn;
            Button btn2 = (Button)tableLayoutPanel1.GetControlFromPosition(2, 1);
            btn2.BackColor = lightsOn;
            Button btn3 = (Button)tableLayoutPanel1.GetControlFromPosition(2, 2);
            btn3.BackColor = lightsOn;
            Button btn4 = (Button)tableLayoutPanel1.GetControlFromPosition(2, 3);
            btn4.BackColor = lightsOn;
            Button btn5 = (Button)tableLayoutPanel1.GetControlFromPosition(1, 2);
            btn5.BackColor = lightsOn;
        }

        /// <summary>
        /// All the buttons have this click event to handle player interaction for every button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
              Control btnClicked = (Control)sender;
            TableLayoutPanelCellPosition clickedCellPos = tableLayoutPanel1.GetCellPosition(btnClicked);
            if(clickedCellPos.Column >= 0 && clickedCellPos.Column <= 5 && clickedCellPos.Row >= 0 && clickedCellPos.Row <= 5)
            {          
                List<TableLayoutPanelCellPosition> cellsToInvert = getCellsToInvert(clickedCellPos);
                InvertCells(cellsToInvert);           
                if (CheckIfPlayerWins() ==  false)
                {
                     MessageBox.Show("WINNNER!!!", "Lights Out Puzzle");
                     RandomStartLights();
                }
            }
      
        }
    }
}
