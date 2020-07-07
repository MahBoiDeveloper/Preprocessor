using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class GridAnalysis : UserControl
    {
        ProjectForm parent;

        #region properties
        public GradationType ColorGradationType
        {
            get
            {
                if (rbAngle.Checked) return GradationType.Angle;
                else return GradationType.Square;
            }
        }
        private double? maxSqr = null;
        public double MinAllowAngle
        {
            get { return (double)nudMinAngle.Value; }
        }
        public double MaxAllowAngle
        {
            get { return (double)nudMaxAngle.Value; }
        }
        public double MinAllowSquare
        {
            get { return (double)nudSquare.Value; }
        }
        public double MaxSquare
        {
            get
            {
                if (!maxSqr.HasValue)
                {
                    MyFiniteElementModel currentModel = parent.currentFullModel.FiniteElementModels.Find(m => m.ModelName == parent.currentFullModel.currentGridName);
                    maxSqr = currentModel.FiniteElements.Max(f => Mathematics.GeronLaw(f.Nodes));
                }
                return maxSqr.Value;
            }
        }
        #endregion

        public GridAnalysis(ProjectForm parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void tbMinAngle_Scroll(object sender, EventArgs e)
        {
            nudMinAngle.Value = tbMinAngle.Value;
        }

        private void tbMaxAngle_Scroll(object sender, EventArgs e)
        {
            nudMaxAngle.Value = tbMaxAngle.Value;
        }

        private bool angleIsBad(double angle)
        {
            return (angle < MinAllowAngle || angle > MaxAllowAngle);
        }

        private void nudMaxAngle_ValueChanged(object sender, EventArgs e)
        {
            tbMaxAngle.Value = (int)nudMaxAngle.Value;
            parent.ReDrawAll(true);
        }

        private void nudMinAngle_ValueChanged(object sender, EventArgs e)
        {
            tbMinAngle.Value = (int)nudMinAngle.Value;
            parent.ReDrawAll(true);
        }

        private void RadioButton_Changed(object sender, EventArgs e)
        {
            parent.ReDrawAll(true);
        }

        private void nudSquare_ValueChanged(object sender, EventArgs e)
        {
            parent.ReDrawAll(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void GridAnalysis_Load(object sender, EventArgs e)
        {
            nudSquare.Value = (decimal)Mathematics.floor(MaxSquare, 0.1);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            MyFiniteElementModel currentModel = parent.currentFullModel.FiniteElementModels.Find(m => m.ModelName == parent.currentFullModel.currentGridName);
            StringBuilder stats = new StringBuilder();
            stats.AppendLine("Статистика по сетке: " + currentModel.ModelName + ".");
            stats.AppendLine();
            stats.AppendLine("Заданные параметры:");
            stats.AppendLine("Минимально допустимый угол: " + MinAllowAngle);
            stats.AppendLine("Максимально допустимый угол: " + MaxAllowAngle);
            stats.AppendLine("Минимально допустимая площадь: " + MinAllowSquare);
            stats.AppendLine();
            stats.AppendLine("Всего конечных элементов: " + currentModel.Nodes.Count);

            int badMinAngle = 0, badMaxAngle = 0, badSquare = 0, badAngles = 0;
            double minAngle = double.MaxValue, maxAngle = double.MinValue, minSquare = double.MaxValue, maxSquare = double.MinValue;;
            int minAngleElem = 0, maxAngleElem = 0, minSquareElem = 0, maxSquareElem = 0;

            foreach (MyFiniteElement elem in currentModel.FiniteElements)
            {
                double[] angles = Mathematics.getFEangles(elem);
                double max = angles.Max();
                double min = angles.Min();
                if (max > MaxAllowAngle || min < MinAllowAngle)
                {
                    badAngles++;
                    if (max > MaxAllowAngle)
                        badMaxAngle++;
                    else 
                        badMinAngle++;
                }
                if (max > maxAngle)
                {
                    maxAngle = max;
                    maxAngleElem = elem.Id;
                }
                if (min < minAngle)
                {
                    minAngle = min;
                    minAngleElem = elem.Id;
                }
                double sqr = Mathematics.GeronLaw(elem.Nodes);
                if (sqr < MinAllowSquare)
                {
                    badSquare++;
                    if (sqr < minSquare)
                    {
                        minSquare = sqr;
                        minSquareElem = elem.Id;
                    }
                }
                if (sqr > maxSquare)
                {
                    maxSquare = sqr;
                    maxSquareElem = elem.Id;
                }
            }
            int elemCount = currentModel.FiniteElements.Count;
            string percentBadAngles = String.Format(" ({0:##0.##}% от общего числа).", (1.0*badAngles / elemCount) * 100);
            string percentBadMinAngles = String.Format(" ({0:##0.##}% от общего числа).", (1.0*badMinAngle / elemCount) * 100);
            string percentBadMaxAngles = String.Format(" ({0:##0.##}% от общего числа).", (1.0*badMaxAngle / elemCount) * 100);
            string percentBadSquare = String.Format(" ({0:##0.##}% от общего числа).", (1.0*badSquare/elemCount)*100);

            stats.AppendLine("Из них содержат недопустимых углов: " + badAngles + percentBadAngles);
            stats.AppendLine("Элементов с углом меньше допустимого: " + badMinAngle + percentBadMinAngles);
            stats.AppendLine("Элементов с углом больше допустимого: " + badMaxAngle + percentBadMaxAngles);
            stats.AppendLine("Минимальный угол: " + minAngle.ToString("##0.##") + ", в элементе: " + minAngleElem + ".");
            stats.AppendLine("Максимальный угол: " + maxAngle.ToString("##0.##") + ", в элементе: " + maxAngleElem + ".");
            stats.AppendLine();
            stats.AppendLine("Элементов с площадью меньше допустимой: " + badSquare + percentBadSquare);
            stats.AppendLine("Минимальная площадь: " + minSquare.ToString("##0.00") + ", у элемента: " + minSquareElem);
            stats.AppendLine("Максимальная площадь: " + maxSquare.ToString("##0.00") + ", у элемента: " + maxSquareElem);

            GridAnalysisStatistics statForm = new GridAnalysisStatistics();
            statForm.StatText = stats.ToString();
            statForm.ShowDialog();
        }
    }
}