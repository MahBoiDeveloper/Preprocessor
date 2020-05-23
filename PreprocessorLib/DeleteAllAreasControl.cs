using System;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class DeleteAllAreasControl : UserControl
    {
        ProjectForm parent;
        public DeleteAllAreasControl()
        {
            InitializeComponent();
        }
        public DeleteAllAreasControl(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
         /*   foreach (MyLine line in this.parent.currentFullModel.geometryModel.Lines)
            {
                line.Areas.Clear();
            }*/
            foreach (MyStraightLine sline in this.parent.currentFullModel.geometryModel.StraightLines)
            {
                sline.Areas.Clear();
            }
            foreach (MyArc arc in this.parent.currentFullModel.geometryModel.Arcs)
            {
                arc.Areas.Clear();
            }
            this.parent.currentFullModel.geometryModel.Areas.Clear();
            for (int i = 0; i < 100; i++)
            {
                for(int j = 0; j < 4; j++) this.parent.currentFullModel.geometryModel.joinTable[i,j] = 0;
            }
            this.parent.currentFullModel.geometryModel.NumOfAreas = 0;
            this.parent.currentFullModel.geometryModel.NumOfAreaNodes = 0;
            this.parent.ReDrawAll();
            this.Dispose();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
