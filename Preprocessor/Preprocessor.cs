using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PreprocessorLib;
using FrontalMethod;

namespace Preprocessor
{
    public partial class Preprocessor : Form
    {
        private string args_filename;
        
        public Preprocessor()
        {
            InitializeComponent();
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripButton.Enabled = false;            
        }

        public Preprocessor(string[] args)
        {
            if (args.Length != 0)
            {
                InitializeComponent();
                args_filename = args[0];
            }
            else
            {
                InitializeComponent();
                this.saveAsToolStripMenuItem.Enabled = false;
                this.saveToolStripMenuItem.Enabled = false;
                this.saveToolStripButton.Enabled = false;
            }
        }

        public void ChildClosed(object sender, FormClosedEventArgs e)
        {
            if (MdiChildren.Length == 0)
            {
                this.saveAsToolStripMenuItem.Enabled = false;
                this.saveToolStripMenuItem.Enabled = false;
                this.saveToolStripButton.Enabled = false;
            }
        }

        private string loadConfiguration()
        {
            string currentExeDir = Application.StartupPath + "\\preprocessor.conf";
            FileInfo configFileInfo = new FileInfo(currentExeDir);
            string conf = "";
            if (!configFileInfo.Exists)
            {
                FileStream tmp = File.Create(currentExeDir);
                tmp.Close();
            }
            else
            {
                StreamReader sr = new StreamReader(currentExeDir);
                conf = sr.ReadLine();
                sr.Close();
            }
            return conf;
        }

        private void saveConfiguration(string conf)
        {
            string currentExeDir = Application.StartupPath + "\\preprocessor.conf";
            FileInfo configFileInfo = new FileInfo(currentExeDir);
            if (!configFileInfo.Exists)
                File.Create(currentExeDir);
            StreamWriter sw = new StreamWriter(currentExeDir);
            sw.WriteLine(conf);
            sw.Close();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string PrevFullFileName = loadConfiguration();
            string prevPath;
            if (File.Exists(PrevFullFileName))
            {
                prevPath = Path.GetFullPath(PrevFullFileName);
                saveFileDialog.InitialDirectory = prevPath;
            }
            else saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.FileName = "geom_model";
            saveFileDialog.Filter = "Preprocessor Files (*.prp)|*.prp|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectForm childForm = new ProjectForm();
                childForm.MdiParent = this;
                childForm.FormClosed += new FormClosedEventHandler(ChildClosed);
                childForm.WindowState = FormWindowState.Maximized;             
                
                string FileName = saveFileDialog.FileName;
                if (MdiChildren.Any(p => p.Text == FileName))
                    MdiChildren.First(p => p.Text == FileName).Close();

                Save(FileName);
            }
        }

        public void Save(string FileName)
        {
            string PrevFileName = loadConfiguration();
            ProjectForm childForm = new ProjectForm();
            childForm.MdiParent = this;
            childForm.FormClosed += new FormClosedEventHandler(ChildClosed);
            childForm.Text = FileName;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
            this.ActiveMdiChild.Text = FileName;
            ((ProjectForm)this.ActiveMdiChild).SaveModel(FileName);
            ((ProjectForm)this.ActiveMdiChild).FullProjectFileName = FileName;

            PrevFileName = FileName;
            saveConfiguration(PrevFileName);

            this.saveAsToolStripMenuItem.Enabled = true;
            this.saveToolStripMenuItem.Enabled = true;
            this.saveToolStripButton.Enabled = true;
        }

        public void AddStarToProjectName() // функция добавляет звездочку к имени проекта - признак того, что проект изменен.
        {
            this.ActiveMdiChild.Text += "*";
        }

        private void OpenFile(object sender, EventArgs e)
        {
            Open();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }
        
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ProjectForm form in MdiChildren)
                form.Close();
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();  
        }

        private void Save()
        {
            this.ActiveMdiChild.Text = ((ProjectForm)this.ActiveMdiChild).FullProjectFileName;
            ((ProjectForm)this.ActiveMdiChild).SaveModel(((ProjectForm)this.ActiveMdiChild).FullProjectFileName);
        }

        private void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            string PrevFileName = loadConfiguration();
            string prevPath;
            if (File.Exists(PrevFileName))
            {
                prevPath = Path.GetFullPath(PrevFileName);
                saveFileDialog.InitialDirectory = prevPath;
            }
            else saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            saveFileDialog.Filter = "Preprocessor Files (*.prp)|*.prp|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                if (MdiChildren.Any(p => p.Text == FileName))
                {
                    MdiChildren.First(p => p.Text == FileName).Close();
                }
                SaveAs(FileName);
            }
        }

        private void SaveAs(string FileName)
        {
            //windowsMenu.DropDownItems.Find(ActiveMdiChild.Text,true)[0].Text = FileName;
            for (int i = 0; i < windowsMenu.DropDownItems.Count; i++)
                if (windowsMenu.DropDownItems[i].Text.IndexOf(ActiveMdiChild.Text) != -1)
                {
                    windowsMenu.DropDownItems[i].Text = FileName;
                    break;
                }

            ProjectForm tempForm = (ProjectForm)ActiveMdiChild;
            tempForm.Text = FileName;
            tempForm.FullProjectFileName = this.ActiveMdiChild.Text;
            tempForm.SaveModel(FileName);
            tempForm.ReDrawAll();
            
            saveConfiguration(FileName);
        }

        private void Open(string filename)
        {
            ProjectForm childForm = new ProjectForm();
            childForm.MdiParent = this;
            childForm.FormClosed += new FormClosedEventHandler(ChildClosed);
            childForm.Text = filename;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
            ((ProjectForm)this.ActiveMdiChild).LoadModel(filename);
            ((ProjectForm)this.ActiveMdiChild).FullProjectFileName = filename;
            saveConfiguration(filename);
            this.saveAsToolStripMenuItem.Enabled = true;
            this.saveToolStripMenuItem.Enabled = true;
            this.saveToolStripButton.Enabled = true;
        }

        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string PrevFileName = loadConfiguration();
            string prevPath;
            if (File.Exists(PrevFileName))
            {
                prevPath = Path.GetFullPath(PrevFileName);
                openFileDialog.InitialDirectory = prevPath;
            }
            else openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            openFileDialog.Filter = "Preprocessor Files (*.prp)|*.prp|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                if (MdiChildren.Length == 0) 
                    Open(FileName);
                else
                {
                    if (MdiChildren.Any(p => p.Text == FileName)) 
                        MdiChildren.First(p => p.Text == FileName).Activate();
                    else 
                        Open(FileName);
                }
            }
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {            
            Save();
        }

        private void openLast_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string prevFileName = loadConfiguration();

            if (File.Exists(prevFileName))
            {
                string FileName = prevFileName;
                if (MdiChildren.Length == 0)
                    Open(FileName);
                else
                {
                    if (MdiChildren.Any(p => p.Text == FileName))
                        MdiChildren.First(p => p.Text == FileName).Activate();
                    else
                        Open(FileName);
                }

            }
            else
            {
                MessageBox.Show("Файл с последним проектом не найден!");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).Undo();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).Redo();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).ImportClick();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).SigmaExportClick();
        }

        private void PointButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).AddPointsClick();
        }

        private void StraightLineButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).AddStraightLinesClick();
        }

        private void ArcButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).AddArcsClick();
        }

        private void CircleButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).AddCirclesClick();
        }

        private void AreaButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).AddAreasClick();
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).CreateGridClick();
        }

        private void BoundsButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).AddBoundsClick();
        }

        private void ForceButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).ForcesClick();
        }

        private void MaterialButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).SetMaterialsClick();
        }

        private void RuppertButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).RuppertClick();
        }

        private void FrontalMethodButton_Click(object sender, EventArgs e)
        {
            if ((ProjectForm)this.ActiveMdiChild == null) return;
            ((ProjectForm)this.ActiveMdiChild).FrontalMethodClick();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild.GetType() != typeof(FrontalMethod.MethodForm))
            {
                if ((ProjectForm)this.ActiveMdiChild == null) return;
                ((ProjectForm)this.ActiveMdiChild).Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild.GetType() != typeof(FrontalMethod.MethodForm))
            {
                if ((ProjectForm)this.ActiveMdiChild == null) return;
                ((ProjectForm)this.ActiveMdiChild).Redo();
            }
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, "help.chm");        
        }

        private void Preprocessor_Shown(object sender, EventArgs e)
        {
            if (args_filename != null)
            {
               Open(args_filename);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Препроцессор к CAE Sigma. "
                + "Версия: 2.0"
                + "\r\n"
                + "\r\n"
                + "Разработчики:"
                + "\r\n"
                + "Николай Комашенко"
                + "\r\n"
                + "Арсений Панфилов"
                + "\r\n"
                + "Сергей Попов"
                + "\r\n"
                + "Андрей Якимов","О программе",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Preprocessor_MdiChildActivate(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                Type childType = this.ActiveMdiChild.GetType();
                if (childType.Name == "MethodForm") this.toolStrip.Visible = false;
                else this.toolStrip.Visible = true;
            }
        }
     }
}
