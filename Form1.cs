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
    public partial class Form1 : Form
    {
        bool[,] lightGrid = new bool[5, 5];
        public Form1()
        {
        
            InitializeComponent();
          
        }
        


    
        private void Form1_Click(object sender, EventArgs e)
        {
            Control btnClicked = (Control)sender;
            TableLayoutPanelCellPosition clickedCellPos = tableLayoutPanel1.GetCellPosition(btnClicked);
            List<TableLayoutPanelCellPosition> cellsToInvert = getCellsToInvert(clickedCellPos);
            InvertCells(cellsToInvert);           
            if (CheckIfPlayerWins()==  false)
            {
                 MessageBox.Show("WINNNER!!!", "Puzzle");
            }
            
        }


        private List<TableLayoutPanelCellPosition> getCellsToInvert(TableLayoutPanelCellPosition clickedCellPos)
        {

            TableLayoutPanelCellPosition topCell;
            TableLayoutPanelCellPosition leftCell;
            TableLayoutPanelCellPosition rightCell;
            TableLayoutPanelCellPosition bottomCell;
            List<TableLayoutPanelCellPosition> adjCells = new List<TableLayoutPanelCellPosition>();

            if (clickedCellPos.Row > 0)
            {
                topCell = new TableLayoutPanelCellPosition(clickedCellPos.Column, clickedCellPos.Row - 1);
                adjCells.Add(topCell);
            }
            if (clickedCellPos.Column > 0)
            {
                leftCell = new TableLayoutPanelCellPosition(clickedCellPos.Column - 1, clickedCellPos.Row);
                adjCells.Add(leftCell);
            }
            if (clickedCellPos.Row < 4)
            {
                bottomCell = new TableLayoutPanelCellPosition(clickedCellPos.Column, clickedCellPos.Row + 1);
                adjCells.Add(bottomCell);
            }
            if (clickedCellPos.Column <4)
            {
                rightCell = new TableLayoutPanelCellPosition(clickedCellPos.Column + 1, clickedCellPos.Row);
                adjCells.Add(rightCell);
            }
            adjCells.Add(clickedCellPos);
            return adjCells;
        }

        private void InvertCells(List<TableLayoutPanelCellPosition> cellsToInvert)
        {
            foreach (TableLayoutPanelCellPosition cell in cellsToInvert)
            {
                Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(cell.Column, cell.Row);
                if(btn.BackColor == Color.Black)
                {
                    btn.BackColor = Color.Aqua;                    
                }
                else
                {
                    btn.BackColor = Color.Black;          

                }
                lightGrid[cell.Column, cell.Row] = btn.BackColor == Color.Black;
            }
        }

        private bool CheckIfPlayerWins()
        {
            for(int i = 0; i<5; i++)
            {
                for (int x = 0; x<5; x++)
                {
                    if(lightGrid[i,x] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void RandomStartLights()
        {

            Random rBoxes = new Random();
            int lightNoBoxes = rBoxes.Next(1, 6);
            for (int i = 0; i < lightNoBoxes; i++)
            {
                Random rd = new Random();               
                int xValue = rd.Next(0, 4);
                int yValue = rd.Next(0, 4);
                lightGrid[xValue, yValue] = true;
                Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(xValue, yValue);
                btn.BackColor = Color.Aqua;
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RandomStartLights();
        }
    }
}
