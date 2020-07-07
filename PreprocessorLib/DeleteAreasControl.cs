using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class DeleteAreasControl : UserControl
    {
        ProjectForm parent;
        public DeleteAreasControl()
        {
            InitializeComponent();
        }
        public DeleteAreasControl(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        public void SetCursor()
        {
            this.number.Select();
        }

        public void OK()
        {
            if (this.number.TextLength == 0)
            {
                this.errorMessage2.Visible = true;
                return;
            }
            this.errorAreaDoesNotExist.Visible = false;

            bool areaExists = false;
            foreach (MyArea area in this.parent.currentFullModel.geometryModel.Areas)
            {
                if (area.Id == Convert.ToInt32(this.number.Text))
                {
                    foreach (MyStraightLine sline in this.parent.currentFullModel.geometryModel.StraightLines)
                    {
                        foreach (int a in sline.Areas)
                        {
                            if (a == area.Id) sline.Areas.Remove(a);
                            break;
                        }
                    }
                    foreach (MyArc arc in this.parent.currentFullModel.geometryModel.Arcs)
                    {
                        foreach (int a in arc.Areas)
                        {
                            if (a == area.Id) arc.Areas.Remove(a);
                            break;
                        }
                    }

                    // обновляем массив joinTable
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (this.parent.currentFullModel.geometryModel.joinTable[i, j] == Convert.ToInt32(this.number.Text))
                            {
                                this.parent.currentFullModel.geometryModel.joinTable[i, j] = 0;
                            }
                        }
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        this.parent.currentFullModel.geometryModel.joinTable[Convert.ToInt32(this.number.Text) - 1, j] = 0;
                    }
                    
                    this.parent.currentFullModel.geometryModel.Areas.Remove(area);
                    this.reMakeAreas();

                    areaExists = true;

                    this.number.TextChanged -= new System.EventHandler(this.number_TextChanged);
                    this.number.Text = "";
                    this.number.TextChanged += new System.EventHandler(this.number_TextChanged);

                    this.number.Select();

                    break;
                }
            }
            if (!areaExists) this.errorAreaDoesNotExist.Visible = true;
  
        }

        private void reMakeAreas()
        {
            List<MyArea> tempAreas = new List<MyArea>();

            foreach (MyArea area in this.parent.currentFullModel.geometryModel.Areas)
            {
                tempAreas.Add(new MyArea(area));                
            }            

            AddAreasControl control = new AddAreasControl(this.parent);
            this.deleteAllAreas();

            foreach (MyArea area in tempAreas)
            {
                control.number.Text = (this.parent.currentFullModel.geometryModel.NumOfAreas + 1).ToString();
                control.line1.Text = area.Lines[0].Id.ToString();
                control.line2.Text = area.Lines[1].Id.ToString();
                control.line3.Text = area.Lines[2].Id.ToString();
                control.line4.Text = area.Lines[3].Id.ToString();
                control.OK();          
            }
            control.Dispose();
        }

        private void deleteAllAreas()
        {
            foreach (MyLine line in this.parent.currentFullModel.geometryModel.Lines)
            {
                line.Areas.Clear();
            }
            this.parent.currentFullModel.geometryModel.Areas.Clear();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 4; j++) this.parent.currentFullModel.geometryModel.joinTable[i, j] = 0;
            }
            this.parent.currentFullModel.geometryModel.NumOfAreas = 0;
            this.parent.currentFullModel.geometryModel.NumOfAreaNodes = 0;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();   
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void number_TextChanged(object sender, EventArgs e)
        {
            if (this.number.TextLength == 0) return;
            try
            {
                Convert.ToInt32(this.number.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.number.Select(0, this.number.TextLength);
            }
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }
    }
}
