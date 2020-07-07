using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ModelComponents;
using FrontalMethod;
using PreprocessorUtils;
using System.Drawing;

namespace PreprocessorLib
{
    public partial class ProjectForm : Form
    {
        public Visualizer visualizer;
        int numOfGrids = 0; // общее число вариантов сеток КЭ
        int numOfRuppertGrids = 0; // число сеток после Рапперта
        int numOfFrontalMethodGrids = 0; // число сеток после фронтального метода
        int modelsCount = 0;
        int mouseX, mouseY;
        public UndoRedo UndoManager;
        private PictureBox backupBox;
        PointF viewPosition = new PointF();
        float viewScale = 1.0f;
        ProgressDots progress;

        public bool applyingForce = false;
        private int UndoStackSize = 20;
        public FullModel currentFullModel;
        public string FullProjectFileName;
        public bool modelDiffersFromSaved = false;

        AddPointsControl addPointControl;
        AddStraightLinesControl addStraightLineControl;
        EditPointsControl editPointsControl;
        EditNodesControl editNodesControl;
        DeletePointsControl deletePointsControl;
        DeleteLinesControl deleteLinesControl;
        AddCirclesControl addCirclesControl;
        AddArcsControl addArcsControl;
        AddAreasControl addAreasControl;
        DeleteAreasControl deleteAreasControl;
        DeleteAllAreasControl deleteAllAreasControl;
        DeleteCirclesControl deleteCirclesControl;
        AddBoundsControl addBoundsControl;
        DeleteForces deleteforces;
        DeleteBounds deleteBoundsControl;
        GridAnalysis gridAnalysisControl;


        DivideIntoFE divideIntoFEForm;
        Forces forcesForm;
        AddMaterials addMaterialsForm;
        RuppertForm ruppertForm;
        StdOptimisation StdOptForm;
        Regularization RegulizeForm;
        EditMaterials viewMaterialsForm;
        SetMaterials setMaterialsForm;
        ExportForm exportForm;
        Delauney delauneyTriangulateForm;
        Form[] modalForms;
        public static double toch { get; set; }

        public ProjectForm()
        {
            InitializeComponent();
            ProjectForm.toch = 0.05;
            UndoManager = new UndoRedo(this, UndoStackSize);
            pixelPerUnit = startPixelPerUnit;
            pixelPerNewton = startPixelPerNewton;
            currentFullModel = new FullModel();
            constructionArea.MouseWheel += new MouseEventHandler(constructionArea_MouseWheel);
        }



        public void StartProgress(string text)
        {
            ParentForm.MdiChildren.ToList().ForEach(f => f.Enabled = false);
            progress = new ProgressDots(text);
            progress.Show();
        }

        public void EndProgress()
        {
            progress.Close();
            ParentForm.MdiChildren.ToList().ForEach(f => f.Enabled = true);
        }

        private MyPoint minXPoint()
        {
            return currentFullModel.geometryModel.Points.First(p => p.X == currentFullModel.geometryModel.Points.Max(pt => pt.X));

        }

        public void Undo()
        {
            if (!applyingForce)
            {
                UndoManager.UndoCommand();
                ReDrawAll();
            }
        }

        public void Redo()
        {
            if (!applyingForce)
            {
                UndoManager.RedoCommand();
                ReDrawAll();
            }
        }



        private MyPoint maxYPoint()
        {
            return currentFullModel.geometryModel.Points.First(p => p.Y == currentFullModel.geometryModel.Points.Max(pt => pt.Y));
        }

        public void MakeListOfGrids() // функция составляет список сеток  - дерево - и выводит их в объект gridVariants
        {
            int index;
            this.gridVariants.Nodes[0].Nodes.Clear();
            this.numOfGrids = 0;
            this.numOfRuppertGrids = 0;
            this.numOfFrontalMethodGrids = 0;
            foreach (MyFiniteElementModel m in this.currentFullModel.FiniteElementModels)
            {
                switch (m.type)
                {
                    case MyFiniteElementModel.GridType.Normal:
                        this.gridVariants.Nodes[0].Nodes.Add(m.ModelName, m.ModelName);
                        this.numOfGrids++;
                        break;
                    case MyFiniteElementModel.GridType.Ruppert:
                        if (this.numOfRuppertGrids == 0)
                        {
                            this.gridVariants.Nodes[0].Nodes.Add("Результаты Рапперта:"); // добавляем узел-родитель результатов рапперта
                        }
                        index = GetTreeNodeIndex("Результаты Рапперта:"); // с помощью этой модной функции получаем индекс родительсокого узла
                        this.gridVariants.Nodes[0].Nodes[index].Nodes.Add(m.ModelName, m.ModelName); // добавляем вариант сетки в дерево
                        this.numOfGrids++;
                        this.numOfRuppertGrids++;
                        break;
                    case MyFiniteElementModel.GridType.FrontalMethod:
                        if (this.numOfFrontalMethodGrids == 0)
                        {
                            this.gridVariants.Nodes[0].Nodes.Add("Результаты Фронтального метода:"); // добавляем узел-родитель результатов фронтального метода
                        }
                        index = GetTreeNodeIndex("Результаты Фронтального метода:"); // с помощью этой модной функции получаем индекс родительсокого узла
                        this.gridVariants.Nodes[0].Nodes[index].Nodes.Add(m.ModelName, m.ModelName); // добавляем вариант сетки в дерево
                        this.numOfGrids++;
                        this.numOfFrontalMethodGrids++;
                        break;
                    case MyFiniteElementModel.GridType.Delauney:
                        TreeNode delauneyNode;
                        if (!gridVariants.Nodes[0].Nodes.ContainsKey("Delauney"))
                            delauneyNode = gridVariants.Nodes[0].Nodes.Add("Delauney", "Результаты построения по Делоне:");
                        else delauneyNode = gridVariants.Nodes[0].Nodes["Delauney"];
                        delauneyNode.Nodes.Add(m.ModelName, m.ModelName);
                        numOfGrids++;
                        break;
                }

            }
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                if (currentFullModel.currentGridName == string.Empty)
                    currentFullModel.currentGridName = this.currentFullModel.FiniteElementModels[0].ModelName;
                TreeNode node = gridVariants.Nodes.Find(currentFullModel.currentGridName, true)[0];
                gridVariants.SelectedNode = node;
                while (node.Parent != null) { node.Parent.Expand(); node = node.Parent; }
                this.currentGridNameBox.Text = this.currentFullModel.currentGridName;
            }
        }

        public void UpdateObjectNumberInControl()
        {
            if (this.activeControl != null && this.activeControl.IsHandleCreated)
            {
                if (this.activeControl == this.addArcsControl) this.addArcsControl.number.Text = (this.currentFullModel.geometryModel.NumOfLines + 1).ToString();
                if (this.activeControl == this.addAreasControl) this.addAreasControl.number.Text = (this.currentFullModel.geometryModel.NumOfAreas + 1).ToString();
                if (this.activeControl == this.addCirclesControl) this.addCirclesControl.number.Text = (this.currentFullModel.geometryModel.NumOfCircles + 1).ToString();
                if (this.activeControl == this.addPointControl) this.addPointControl.number.Text = (this.currentFullModel.geometryModel.NumOfPoints + 1).ToString();
                if (this.activeControl == this.addStraightLineControl) this.addStraightLineControl.number.Text = (this.currentFullModel.geometryModel.NumOfLines + 1).ToString();
                if (this.activeControl == this.deleteAreasControl) this.deleteAreasControl.number.Text = "";
                if (this.activeControl == this.deleteCirclesControl) this.deleteCirclesControl.number.Text = "";
                if (this.activeControl == this.deleteLinesControl) this.deleteLinesControl.number.Text = "";
                if (this.activeControl == this.deletePointsControl) this.deletePointsControl.number.Text = "";
                if (this.activeControl == this.editNodesControl) this.editNodesControl.number.Text = "";
                if (this.activeControl == this.editPointsControl) this.editPointsControl.number.Text = "";
            }
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            this.scalePlus();
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            this.scaleMinus();
        }

        public void AddPointsClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя добавлять точки, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addPointControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.addPointControl != null && this.addPointControl.IsDisposed) || this.addPointControl == null)
            {
                addPointControl = new AddPointsControl(this);
                this.Controls.Add(addPointControl);
                this.addPointControl.SetCursor();
                this.activeControl = addPointControl;
            }
        }

        public void EditPointsClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя редактировать точки, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // устанавливаем "отображать только геометрию"
            this.showOnlyGeometry.Checked = true;
            this.showOnlyAreas.Checked = false;
            this.showOnlyFE.Checked = false;
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.editPointsControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.editPointsControl != null && this.editPointsControl.IsDisposed) || this.editPointsControl == null)
            {
                editPointsControl = new EditPointsControl(this);
                this.Controls.Add(editPointsControl);
                this.editPointsControl.SetCursor();
                this.activeControl = editPointsControl;
            }
        }

        public void EditNodesClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя редактировать узлы зон, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // устанавливаем "отображать только зоны"
            this.showOnlyGeometry.Checked = false;
            this.showOnlyAreas.Checked = true;
            this.showOnlyFE.Checked = false;
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.editNodesControl) this.disposeControl(activeControl);
            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.editNodesControl != null && this.editNodesControl.IsDisposed) || this.editNodesControl == null)
            {
                editNodesControl = new EditNodesControl(this);
                this.Controls.Add(editNodesControl);
                this.editNodesControl.SetCursor();
                this.activeControl = editNodesControl;
            }
        }

        public void DeletePointsClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя удалять точки, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.deletePointsControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.deletePointsControl != null && this.deletePointsControl.IsDisposed) || this.deletePointsControl == null)
            {
                deletePointsControl = new DeletePointsControl(this);
                this.Controls.Add(deletePointsControl);
                this.deletePointsControl.SetCursor();
                this.activeControl = deletePointsControl;
            }
        }

        public void AddStraightLinesClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя добавлять линии, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addStraightLineControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.addStraightLineControl != null && this.addStraightLineControl.IsDisposed) || this.addStraightLineControl == null)
            {
                addStraightLineControl = new AddStraightLinesControl(this);
                this.Controls.Add(addStraightLineControl);
                this.addStraightLineControl.SetCursor();
                this.activeControl = addStraightLineControl;
            }
        }

        public void DeleteLinesClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя удалять линии, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.deleteLinesControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.deleteLinesControl != null && this.deleteLinesControl.IsDisposed) || this.deleteLinesControl == null)
            {
                deleteLinesControl = new DeleteLinesControl(this);
                this.Controls.Add(deleteLinesControl);
                this.deleteLinesControl.SetCursor();
                this.activeControl = deleteLinesControl;
            }
        }

        public void DeleteCirclesClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя удалять окружности, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.deleteCirclesControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.deleteCirclesControl != null && this.deleteCirclesControl.IsDisposed) || this.deleteCirclesControl == null)
            {
                deleteCirclesControl = new DeleteCirclesControl(this);
                this.Controls.Add(deleteCirclesControl);
                this.deleteCirclesControl.SetCursor();
                this.activeControl = deleteCirclesControl;
            }
        }

        public void AddCirclesClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя добавлять окружности, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addCirclesControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.addCirclesControl != null && this.addCirclesControl.IsDisposed) || this.addCirclesControl == null)
            {
                addCirclesControl = new AddCirclesControl(this);
                this.Controls.Add(addCirclesControl);
                this.addCirclesControl.SetCursor();
                this.activeControl = addCirclesControl;
            }
        }

        public void AddArcsClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя добавлять дуги, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addArcsControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.addArcsControl != null && this.addArcsControl.IsDisposed) || this.addArcsControl == null)
            {
                addArcsControl = new AddArcsControl(this);
                this.Controls.Add(addArcsControl);
                this.addArcsControl.SetCursor();
                this.activeControl = addArcsControl;
            }
        }

        public void AddAreasClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя добавлять зоны, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addAreasControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.addAreasControl != null && this.addAreasControl.IsDisposed) || this.addAreasControl == null)
            {
                addAreasControl = new AddAreasControl(this);
                this.Controls.Add(addAreasControl);
                this.addAreasControl.SetCursor();
                this.activeControl = addAreasControl;
            }
        }

        public void DeleteAreasClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя удалять зоны, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.deleteAreasControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.deleteAreasControl != null && this.deleteAreasControl.IsDisposed) || this.deleteAreasControl == null)
            {
                deleteAreasControl = new DeleteAreasControl(this);
                this.Controls.Add(deleteAreasControl);
                this.deleteAreasControl.SetCursor();
                this.activeControl = deleteAreasControl;
            }
        }

        public void DeleteAllAreasClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count != 0)
            {
                MessageBox.Show("Нельзя удалять зоны, когда модель пластины уже разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.deleteAllAreasControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            // проверяем, не открыт ли этот контрол
            if ((this.deleteAllAreasControl != null && this.deleteAllAreasControl.IsDisposed) || this.deleteAllAreasControl == null)
            {
                deleteAllAreasControl = new DeleteAllAreasControl(this);
                this.Controls.Add(deleteAllAreasControl);
                this.activeControl = deleteAllAreasControl;
            }
        }

        public void CreateGridClick(MyFiniteElementModel.GridType type = MyFiniteElementModel.GridType.Normal)
        {
            if (this.currentFullModel.geometryModel.Areas.Count == 0)
            {
                MessageBox.Show("Незьзя разбить модель на КЭ, пока нет ни одной зоны!");
                return;
            }

            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addBoundsControl) this.disposeControl(activeControl);
            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            switch (type)
            {
                case MyFiniteElementModel.GridType.Normal:
                    divideIntoFEForm = new DivideIntoFE(this);
                    divideIntoFEForm.Show();
                    break;
                case MyFiniteElementModel.GridType.FrontalMethod:
                    FrontalMethodClick();
                    break;

                // и много-много других волшебных методов построения сетки КЭ
            }
        }

        public void AddMaterialsClick()
        {
            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            addMaterialsForm = new AddMaterials(this);
            addMaterialsForm.ShowDialog();
        }

        public void EditMaterialsClick()
        {
            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            viewMaterialsForm = new EditMaterials(this);
            viewMaterialsForm.ShowDialog();
        }

        public void SetMaterialsClick()
        {
            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            if (this.currentFullModel.FiniteElementModels.Count == 0)
            {
                MessageBox.Show("Незьзя назначить материалы, пока модель пластины не разбита на КЭ!");
                return;
            }
            setMaterialsForm = new SetMaterials(this);
            setMaterialsForm.ShowDialog();
        }

        private void DeleteBoundsClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count == 0)
            {
                MessageBox.Show("Незьзя удалить закрепления, пока модель пластины не разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.deleteBoundsControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            deleteBoundsControl = new DeleteBounds(this);
            this.Controls.Add(deleteBoundsControl);
            this.activeControl = deleteBoundsControl;
        
        }

        public void AddBoundsClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count == 0)
            {
                MessageBox.Show("Незьзя задать закрепления, пока модель пластины не разбита на КЭ!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addBoundsControl) this.disposeControl(activeControl);

            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

                addBoundsControl = new AddBoundsControl(this);
                this.Controls.Add(addBoundsControl);
                this.activeControl = addBoundsControl;
        }


        public void ForcesClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count == 0)
            //if (this.currentFullModel.FiniteElementModels[currentModel].FiniteElements.Count == 0)
            {
                MessageBox.Show("Незьзя задать нагрузки, пока модель пластины не разбита на КЭ!");
                return;
            }

            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated) this.disposeControl(activeControl);

            // проверяем, не открыт ли этот контрол
            if ((this.forcesForm != null && this.forcesForm.IsDisposed) || this.forcesForm == null)
            {
                forcesForm = new Forces(this);
                forcesForm.Show();
                activeForm = forcesForm;
            }
        }
        public void DeleteForcesClick()
        {
            if (this.currentFullModel.FiniteElementModels.Count == 0)
            //if (this.currentFullModel.FiniteElementModels[currentModel].FiniteElements.Count == 0)
            {
                MessageBox.Show("Незьзя удалять нагрузки, пока модель пластины не разбита на КЭ!");
                return;
            }

            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated) this.disposeControl(activeControl);

            // проверяем, не открыт ли этот контрол

            deleteforces = new DeleteForces(this);
            this.Controls.Add(deleteforces);
            this.activeControl = deleteforces;


        }
        public void OptimisationClick()
        {
            if (currentFullModel.FiniteElementModels.Count == 0)
            {
                MessageBox.Show("Нет ни одной сетки для оптимизации!");
                return;
            }
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addBoundsControl) this.disposeControl(activeControl);

            StdOptForm = new StdOptimisation(this);
            StdOptForm.Show();
            activeForm = StdOptForm;
        }

        public void RuppertClick()
        {
            if (currentFullModel.FiniteElementModels.Count == 0)
            {
                MessageBox.Show("Нет ни одной сетки для оптимизации!");
                return;
            }
            ruppertForm = new RuppertForm(this);
            ruppertForm.Show();
            activeForm = ruppertForm;
        }

        public void FrontalMethodClick()
        {
            if (activeControl != null && activeControl.IsHandleCreated) this.disposeControl(activeControl);
            modalForms = new Form[6] { divideIntoFEForm, forcesForm, addMaterialsForm, viewMaterialsForm, setMaterialsForm, exportForm };
            foreach (Form form in modalForms) if (form != null) form.Close();
            prepTree.SelectedNode = prepTree.Nodes[0];
            MethodForm FMform = new MethodForm(this, currentFullModel);
            FMform.ModelCreated += new Action<MyFiniteElementModel>(ModelCreated);
            FMform.Cancel += new Action(FMform_Cancel);
            MdiParent.AddOwnedForm(FMform);
            FMform.MdiParent = MdiParent;
            FMform.WindowState = FormWindowState.Maximized;
            modelsCount = currentFullModel.FiniteElementModels.Count;
            FMform.Show();
            this.Hide();
        }

        void FMform_Cancel()
        {
            this.Show();
            if (!prepTree.Nodes[0].IsExpanded) prepTree.Nodes[0].Expand();
            if (currentFullModel.FiniteElementModels.Count > 0)
            {
                currentFullModel.currentGridName = currentFullModel.FiniteElementModels.Last().ModelName;
                showOnlyFE.Checked = true;
            }
            else
            {
                showOnlyGeometry.Checked = true;
            }
            CenterModel();
            ReDrawAll(true);
        }


        public void ModelCreated(MyFiniteElementModel model)
        {
            if (model.type == MyFiniteElementModel.GridType.FrontalMethod)
            {
                this.Show();
                if (!prepTree.Nodes[0].IsExpanded) prepTree.Nodes[0].Expand();
            }
            while (currentFullModel.FiniteElementModels.FindAll(m => m.ModelName == model.ModelName).Count > 1)
                model.ModelName += "_";
            model.Id = currentFullModel.IdCandidate;

            currentFullModel.FiniteElementModels.Add(model);
            currentFullModel.currentGridName = model.ModelName;
            MakeListOfGrids();
            this.showOnlyFE.Checked = true;
            CenterModel();
            ReDrawAll();
            model.restoreArraysForOldMethods(currentFullModel.geometryModel);
        }

        public void SigmaExportClick()
        {
            if (this.currentFullModel.geometryModel.Areas.Count == 0)
            {
                MessageBox.Show("Нельзя экспортировать модель в CAE Sigma, не разбив ее на зоны!");
                return;
            }
            exportForm = new ExportForm(this);
            exportForm.ShowDialog();
        }

        public void ImportClick()
        {
            string SfmFileName = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetDirectoryName(this.FullProjectFileName);
            openFileDialog.Filter = "Sigma Geometry Files (*.sfm)|*.sfm|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SfmFileName = openFileDialog.FileName;
            }
            if (SfmFileName != null)
            {
                ReadSFMFile(SfmFileName);
                CenterModel();
                ReDrawAll();
            }
        }

        public void disposeControl(UserControl control)
        {
            if (control == this.editPointsControl && this.currentFullModel.geometryModel.editedPoint != null)
            {
                this.currentFullModel.geometryModel.editedPoint = null;
                ReDrawAll(true);
            }
            control.Dispose();
        }

        public void AddGridVariant(string name)
        {
            this.gridVariants.Nodes[0].Nodes.Add(name);
            this.gridVariants.Nodes[0].LastNode.Name = name;
            this.gridVariants.Nodes[0].Expand();
            this.currentFullModel.currentGridName = name;
            this.currentGridNameBox.Text = name;
            this.numOfGrids++;
        }

        public void PrepareForRuppert()
        {
            int currentModel = this.GetCurrentModelIndex();
            int i;

            if (File.Exists(Path.GetDirectoryName(this.FullProjectFileName) + "\\grid.Ralg")) File.Delete(Path.GetDirectoryName(this.FullProjectFileName) + "\\grid.Ralg");

            // создадим файл griddm.inout

            StringBuilder sb = new StringBuilder();
            StreamWriter sw = new StreamWriter(Path.GetDirectoryName(this.FullProjectFileName) + "\\griddm.inout");
            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NP; i++)
            {
                sb.AppendLine(this.currentFullModel.FiniteElementModels[currentModel].INOUT[i].ToString());
            }
            sw.Write(sb.ToString());
            sw.Dispose();

            // создадим бинарные файлы RESULT1.BIN и RESUL2.BIN

            byte boundtype;
            double xnode, ynode, wEnd, r1, r2;
            FileStream F1, F2;
            Int16 wEnd2 = -1;
            Int16 temp;
            wEnd = -1;


            F1 = new FileStream(Path.GetDirectoryName(this.FullProjectFileName) + "\\RESULT1.BIN", FileMode.Create, FileAccess.ReadWrite);
            F2 = new FileStream(Path.GetDirectoryName(this.FullProjectFileName) + "\\RESULT2.BIN", FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter writer1 = new BinaryWriter(F1);
            BinaryWriter writer2 = new BinaryWriter(F2);

            // result 1
            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NP; i++)
            {
                xnode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (i - 1) + 1];
                ynode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (i - 1) + 2];

                writer1.Write(xnode);
                writer1.Write(ynode);
                // 9*8*2 = 144
            }


            writer1.Write(wEnd);
            // 144 + 8 = 152;


            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NB; i++)
            {
                xnode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (this.currentFullModel.FiniteElementModels[currentModel].NBC[i] - 1) + 1];
                ynode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (this.currentFullModel.FiniteElementModels[currentModel].NBC[i] - 1) + 2];
                writer1.Write(xnode);
                writer1.Write(ynode);
                boundtype = (byte)this.currentFullModel.FiniteElementModels[currentModel].NFIX[i];
                writer1.Write(boundtype);

                // 3* 2 * 8 + 3* 1 = 51

            }
            writer1.Write(wEnd);

            // 51 + 8 + 152 = 211;


            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NP; i++)
            {
                if (this.currentFullModel.FiniteElementModels[currentModel].NLD != 0)
                {
                    r1 = this.currentFullModel.FiniteElementModels[currentModel].R[(i - 1) * this.currentFullModel.FiniteElementModels[currentModel].NDF + 1];
                    r2 = this.currentFullModel.FiniteElementModels[currentModel].R[(i - 1) * this.currentFullModel.FiniteElementModels[currentModel].NDF + 2];
                }
                else
                {
                    r1 = 0;
                    r2 = 0;
                }
                if (r1 == 0 && r2 == 0) continue;
                xnode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (i - 1) + 1];
                ynode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (i - 1) + 2];
                writer1.Write(xnode);
                writer1.Write(ynode);
                writer1.Write(r1);
                writer1.Write(r2);
                // 3*8*4 = 96
            }
            writer1.Write(wEnd);

            // 211 + 96 + 8 = 315

            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NP; i++)
            {
                xnode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (i - 1) + 1];
                ynode = this.currentFullModel.FiniteElementModels[currentModel].CORD[2 * (i - 1) + 2];
                writer1.Write(xnode);
                writer1.Write(ynode);
                // 9*2*8 = 144
            }
            writer1.Write(wEnd);
            // 315 + 144 + 8 = 467

            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NP; i++)
            {
                if (this.currentFullModel.FiniteElementModels[currentModel].NLD != 0)
                {
                    r1 = this.currentFullModel.FiniteElementModels[currentModel].R[(i - 1) * this.currentFullModel.FiniteElementModels[currentModel].NDF + 1];
                    r2 = this.currentFullModel.FiniteElementModels[currentModel].R[(i - 1) * this.currentFullModel.FiniteElementModels[currentModel].NDF + 2];
                }
                else
                {
                    r1 = 0;
                    r2 = 0;
                }
                writer1.Write(r1);
                writer1.Write(r2);
                // 9*2 *8 = 144;
            }
            writer1.Write(wEnd);

            // 144 + 8 + 467 = 619; // гы, хоть размер правльно расчитал..  

            // result 2
            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NE; i++)
            {
                temp = (Int16)this.currentFullModel.FiniteElementModels[currentModel].NOP[this.currentFullModel.FiniteElementModels[currentModel].NCN * (i - 1) + 1];
                writer2.Write(temp);
                temp = (Int16)this.currentFullModel.FiniteElementModels[currentModel].NOP[this.currentFullModel.FiniteElementModels[currentModel].NCN * (i - 1) + 2];
                writer2.Write(temp);
                temp = (Int16)this.currentFullModel.FiniteElementModels[currentModel].NOP[this.currentFullModel.FiniteElementModels[currentModel].NCN * (i - 1) + 3];
                writer2.Write(temp);
            }
            writer2.Write(wEnd2);

            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NE; i++)
            {
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
            }
            writer2.Write(wEnd);

            for (i = 1; i <= 21; i++)
            {
                writer2.Write(this.currentFullModel.FiniteElementModels[currentModel].ORT[i]);
            }
            writer2.Write(wEnd);

            for (i = 1; i <= this.currentFullModel.FiniteElementModels[currentModel].NE; i++)
            {
                temp = (Int16)this.currentFullModel.FiniteElementModels[currentModel].IMAT[i];
                writer2.Write(temp);
            }
            writer2.Write(wEnd);

            F1.Dispose();
            F2.Dispose();


        }

        public void SaveModel(string fileName)
        {
            string mod = Path.ChangeExtension(fileName, "prp");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(mod, FileMode.Create);
            formatter.Serialize(file, currentFullModel);
            file.Close();
            UndoManager.updateLastSaved();
        }

        public MemoryStream getModelStream()
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            try
            {
                formatter.Serialize(stream, currentFullModel);
            }
            catch { }
            return stream;
        }

        public void writeModelFromStream(MemoryStream stream)
        {
            FullModel model = null;
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                IFormatter formatter = new BinaryFormatter();
                model = (FullModel)formatter.Deserialize(stream);
            }
            catch { }
            currentFullModel = model;
            this.UpdateObjectNumberInControl();
            this.MakeListOfGrids();
            readSettings();
        }

        private void readSettings()
        {
            this.showArcs.Checked = currentFullModel.settings.ShowArcs;
            this.showOnlyAreas.Checked = currentFullModel.settings.ShowAreas;
            this.showBounds.Checked = currentFullModel.settings.ShowBounds;
            this.showCircles.Checked = currentFullModel.settings.ShowCircles;
 //           this.showFE.Checked = currentFullModel.settings.ShowFE;
            this.showForces.Checked = currentFullModel.settings.ShowForces;
            this.showForceValue.Checked = currentFullModel.settings.ShowForceValue;
            this.showGrid.Checked = currentFullModel.settings.ShowGrid;
            this.showLines.Checked = currentFullModel.settings.ShowLines;
            this.showPoints.Checked = currentFullModel.settings.ShowPoints;
            this.currentGridNameBox.Text = currentFullModel.currentGridName;
 //           this.showFENumbers.Checked = currentFullModel.settings.ShowFENumbers;
 //           this.showFENodes.Checked = currentFullModel.settings.ShowFENodes;
 //           this.showFEMaterials.Checked = currentFullModel.settings.ShowFEMaterials;
            this.showOnlyAreas.Checked = currentFullModel.settings.ShowOnlyAreas;
            this.showOnlyGeometry.Checked = currentFullModel.settings.ShowOnlyGeometry;
            this.showOnlyFE.Checked = currentFullModel.settings.ShowOnlyFE;
        }

        public bool modelChanged()
        {
            return UndoManager.modelDiffersFromSaved();
        }

        public void LoadModel(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            StreamReader file = new StreamReader(Path.ChangeExtension(fileName, "prp"));
            currentFullModel = (FullModel)formatter.Deserialize(file.BaseStream);
            file.Close();
            readSettings();
            UndoManager.updateLastSaved();
            MakeListOfGrids();
            CenterModel();
            ReDrawAll();
        }

        private void showOnlyGeometry_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowOnlyGeometry = this.showOnlyGeometry.Checked;
            this.showPoints.CheckState = this.showOnlyGeometry.CheckState;
            this.showLines.CheckState = this.showOnlyGeometry.CheckState;
            this.showArcs.CheckState = this.showOnlyGeometry.CheckState;
            this.showCircles.CheckState = this.showOnlyGeometry.CheckState;
            ReDrawAll(true);
        }

        private void showOnlyAreas_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowOnlyAreas = this.showOnlyAreas.Checked;
 //           this.showAreas.Checked = this.showOnlyAreas.Checked;
            ReDrawAll(true);
        }

        private void showOnlyFE_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowOnlyFE = this.showOnlyFE.Checked;
            this.showBounds.CheckState = this.showOnlyFE.CheckState;
            this.showForces.Checked = this.showOnlyFE.Checked;
            this.showForceValue.Checked = this.showOnlyFE.Checked;
            this.showFENumbers.Enabled = this.showOnlyFE.Checked;
            this.showFENodes.Enabled = this.showOnlyFE.Checked;
            this.showFEMaterials.Enabled = this.showOnlyFE.Checked;
            ReDrawAll(true);
        }

        private void showGrid_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowGrid = this.showGrid.Checked;
            ReDrawAll(true);
        }

        private void showPoints_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowPoints = this.showPoints.Checked;
            ReDrawAll(true);
        }

        private void showLines_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowLines = this.showLines.Checked;
            ReDrawAll(true);
        }

        private void showCircles_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowCircles = this.showCircles.Checked;
            ReDrawAll(true);
        }

        private void showArcs_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowArcs = this.showArcs.Checked;
            ReDrawAll(true);
        }

 /*       private void showAreas_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowAreas = this.showAreas.Checked;
            ReDrawAll(true);
        }
*/
 /*       private void showFE_CheckedChanged(object sender, EventArgs e)
        {
            this.showFENumbers.Enabled = this.showFE.Checked;
            this.showFENodes.Enabled = this.showFE.Checked;
            this.showFEMaterials.Enabled = this.showFE.Checked;
            this.currentFullModel.settings.ShowFE = this.showFE.Checked;
            ReDrawAll(true);
        }
        */
        private void showFENumbers_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowFENumbers = this.showFENumbers.Checked;
            ReDrawAll(true);
        }

        private void showFENodes_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowFENodes = this.showFENodes.Checked;
            ReDrawAll(true);

        }

        private void showFEMaterials_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowFEMaterials = this.showFEMaterials.Checked;
            ReDrawAll(true);
        }

        private void showBounds_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowBounds = this.showBounds.Checked;
            ReDrawAll(true);
        }

        private void showForces_CheckedChanged(object sender, EventArgs e)
        {
            this.showForceValue.Enabled = this.showForces.Checked;
            this.currentFullModel.settings.ShowForces = this.showForces.Checked;
            ReDrawAll(true);
        }

        private void showForceValue_CheckedChanged(object sender, EventArgs e)
        {
            this.currentFullModel.settings.ShowForceValue = this.showForceValue.Checked;
            ReDrawAll(true);
        }

        private void forceScale_ValueChanged(object sender, EventArgs e)
        {
            int val = forceScale.Value;
            int k;
            if (val > 25)
            {
                k = (val - 25 + 1);
                PixelPerNewton = startPixelPerNewton * k;
            }
            else
            {
                k = (25 - val + 1);
                PixelPerNewton = startPixelPerNewton / k;
            }
            ReDrawAll(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (forceScale.Value == forceScale.Maximum) return;
            forceScale.Value++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (forceScale.Value == forceScale.Minimum) return;
            forceScale.Value--;
        }

        private void gridVariants_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.gridVariants.SelectedNode == null) return;
            if (this.gridVariants.SelectedNode.Text == "Варианты сетки КЭ:" || this.gridVariants.SelectedNode.Text == "Результаты Рапперта:") return;
            this.currentFullModel.currentGridName = this.gridVariants.SelectedNode.Text;
            this.currentGridNameBox.Text = this.currentFullModel.currentGridName;
//            this.showFE.Checked = true;
            ReDrawAll();
        }

        public int GetCurrentModelIndex()
        {
            MyFiniteElementModel model = currentFullModel.FiniteElementModels.Find(p => p.ModelName == currentFullModel.currentGridName);
            return (model == null) ? 0 : currentFullModel.FiniteElementModels.IndexOf(model);
        }

        public int GetTreeNodeIndex(string name) // перебирает не все узлы дерева, а только определенную ветку
        {
            foreach (TreeNode treeNode in this.gridVariants.Nodes[0].Nodes)
            {
                if (treeNode.Text == name)
                {
                    return treeNode.Index;
                }
            }
            return 0;
        }

        private void centerModelButton_Click(object sender, EventArgs e)
        {
            this.CenterModel();
            ReDrawAll(true);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (mouseAboveArea)
            {
                if (e.Delta > 0) scalePlus();
                else scaleMinus();
            }
        }

        private void scale_ValueChanged(object sender, EventArgs e)
        {
            ChangeScale();
        }

        private void scalePlus()
        {
            if (scale.Value == this.scale.Maximum) return;
            scale.Value++;
        }

        private void scaleMinus()
        {
            if (scale.Value == this.scale.Minimum) return;
            scale.Value--;
        }

        private void prepTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            onSelected();
        }

        private void prepTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (prepTree.SelectedNode == null) return;
            if (prepTree.GetNodeAt(e.X, e.Y).IsSelected == true)
            {
                onSelected();
            }
        }

        private void AnalyzeGrid()
        {
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.editNodesControl) this.disposeControl(activeControl);
            // проверяем, не открыта ли форма нагрузок
            if ((this.forcesForm != null && this.forcesForm.IsHandleCreated)) return;

            if (currentFullModel.FiniteElementModels.Count == 0)
            {
                MessageBox.Show("Нет ни одной сетки для анализа!");
                return;
            }

            // проверяем, не открыт ли этот контрол
            if ((gridAnalysisControl != null && gridAnalysisControl.IsDisposed) || gridAnalysisControl == null)
            {
                gridAnalysisControl = new GridAnalysis(this);
                Controls.Add(gridAnalysisControl);
                this.showOnlyFE.Checked = true;
                this.activeControl = gridAnalysisControl;
                ReDrawAll(true);
            }
        }

        private void onSelected()
        {
            int currentModel = this.GetCurrentModelIndex();

            if (activeControl == addAreasControl || activeControl == addStraightLineControl || activeControl == addArcsControl)
            {
                clearSelection();
                ReDrawAll(true);
            }
            switch (prepTree.SelectedNode.Name)
            {
                case "Regularization":
                    if (currentFullModel.FiniteElementModels.Count == 0)
                    {
                        MessageBox.Show("Нет ни одной сетки для оптимизации!");
                        return;
                    }
                    RegulizeForm = new Regularization(this);
                    RegulizeForm.Show();
                    activeForm = StdOptForm;
                    break;
                case "nodeAnalysis":
                    this.AnalyzeGrid();
                    break;
                case "AddPoints":
                    this.AddPointsClick();
                    break;
                case "EditPoints":
                    this.EditPointsClick();
                    break;
                case "EditNodes":
                    this.EditNodesClick();
                    break;
                case "DeletePoints":
                    this.DeletePointsClick();
                    break;
                case "AddStraightLines":
                    this.AddStraightLinesClick();
                    break;
                case "DeleteLines":
                    this.DeleteLinesClick();
                    break;
                case "DeleteCircles":
                    this.DeleteCirclesClick();
                    break;
                case "AddCircles":
                    this.AddCirclesClick();
                    break;
                case "DelaneyTriangulate":
                    this.DelauneyTriangulateClick();
                    break;
                case "AddArcs":
                    this.AddArcsClick();
                    break;
                case "AddAreas":
                    this.AddAreasClick();
                    break;
                case "DeleteAreas":
                    this.DeleteAreasClick();
                    break;
                case "DeleteAllAreas":
                    this.DeleteAllAreasClick();
                    break;
                case "standartGrid":
                    this.CreateGridClick(MyFiniteElementModel.GridType.Normal);
                    break;
                case "FrontalMethod":
                    this.CreateGridClick(MyFiniteElementModel.GridType.FrontalMethod);
                    break;
                case "AddMaterials":
                    this.AddMaterialsClick();
                    break;
                case "EditMaterials":
                    this.EditMaterialsClick();
                    break;
                case "SetMaterials":
                    this.SetMaterialsClick();
                    break;
                case "AddBounds":
                    this.AddBoundsClick();
                    break;
                case "DeleteBounds":
                    this.DeleteBoundsClick();
                    break;
                case "Forces":
                    this.ForcesClick();
                    break;
                case "DeleteForces":
                    this.DeleteForcesClick();
                    break;
                case "standartOpt":
                    this.OptimisationClick();
                    break;
                case "RuppertOpt":
                    this.RuppertClick();
                    break;
                case "SigmaExport":
                    this.SigmaExportClick();
                    break;
                case "Import":
                    this.ImportClick();
                    break;

            }
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            this.scaleField.Text = "1:1";
            this.scale.Value = middleValueOfScale;
            this.MakeListOfGrids();
            this.prepTree.Nodes[0].Expand();
        }

        private void DelauneyTriangulateClick()
        {
            // закрываем все другие контолы                    
            if (this.activeControl != null && this.activeControl.IsHandleCreated && this.activeControl != this.addBoundsControl) this.disposeControl(activeControl);

            delauneyTriangulateForm = new Delauney(this);
            delauneyTriangulateForm.Show();
        }

        private void ProjectForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > constructionArea.Left && e.Y > constructionArea.Top &&
                e.X < constructionArea.Left + constructionArea.Width && e.Y < constructionArea.Top + constructionArea.Height)
                mouseAboveArea = true;
            else
                mouseAboveArea = false;
        }

        private void ProjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modelChanged())
            {
                switch (MessageBox.Show("Сохранить изменения в проекте " + FullProjectFileName + " ?", "Закрытие", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        SaveModel(FullProjectFileName);
                        return;
                    case DialogResult.No:
                        return;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
        }

        private void ProjectForm_Paint(object sender, PaintEventArgs e)
        {
            constructionArea_Resize(this, new EventArgs());
        }

        private void ProjectForm_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control is GridAnalysis)
            {
                activeControl = null;
                ReDrawAll(true);
            }
        }

        private void gridVariants_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridVariants.SelectedNode = gridVariants.GetNodeAt(e.X, e.Y);
                TreeNode node = gridVariants.SelectedNode;
                if (node != null && node.Nodes.Count == 0 && node.Parent != null)
                    cmsGrid.Show(gridVariants, e.X, e.Y);
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            string modelName = gridVariants.SelectedNode.Text;
            MyFiniteElementModel model = currentFullModel.FiniteElementModels.Find(m => m.ModelName == modelName);
            int idx = currentFullModel.FiniteElementModels.IndexOf(model);
            currentFullModel.FiniteElementModels.RemoveAt(idx);
            if (idx > 0) currentFullModel.currentGridName = currentFullModel.FiniteElementModels[idx - 1].ModelName;
            else if (idx == 0 && currentFullModel.FiniteElementModels.Count > 0) currentFullModel.currentGridName = currentFullModel.FiniteElementModels[0].ModelName;
            else currentFullModel.currentGridName = string.Empty;
            MakeListOfGrids();
            ReDrawAll();
        }

        private void ProjectForm_Deactivate(object sender, EventArgs e)
        {
            // Создаем контрол на который кидаем текущее изображения с opengl-контрола
            viewPosition = visualizer.GetCenter();
            viewScale = visualizer.scale;
            backupBox = new PictureBox();
            backupBox.Location = constructionArea.Location;
            backupBox.Size = constructionArea.Size;
            backupBox.Image = visualizer.GetImage();
            backupBox.Visible = backupBox.Enabled = true;
            Controls.Add(backupBox);
            backupBox.BringToFront();
            // сам opengl-контрол скрываем. Делаем всё это, т.к. у Tао то ли
            // проблемы с поддержкой MDI, то ли я плохо читал матчасть :)
            constructionArea.Enabled = constructionArea.Visible = false;
            visualizer = null;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void ProjectForm_Activated(object sender, EventArgs e)
        {
            if (backupBox != null)
            {
                Controls.Remove(backupBox);
                backupBox = null;
            }
            visualizer = new Visualizer(constructionArea);
            constructionArea.Enabled = constructionArea.Visible = true;

            visualizer.MoveTo(viewPosition.X, viewPosition.Y);
            visualizer.SetScale(viewScale);

            ReDrawAll(true);
        }
    }
}