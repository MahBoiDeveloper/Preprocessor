using System;
using System.Collections.Generic;

using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MySettings
    {
        // настройки галочек
        private bool showAxis;
        public bool ShowAxis
        {
            get { return showAxis; }
            set { showAxis = value; }
        }

        private bool showGrid;
        public bool ShowGrid
        {
            get { return showGrid; }
            set { showGrid = value; }
        }

        private bool showPoints;
        public bool ShowPoints
        {
            get { return showPoints; }
            set { showPoints = value; }
        }

        private bool showLines;
        public bool ShowLines
        {
            get { return showLines; }
            set { showLines = value; }
        }

        private bool showCircles;
        public bool ShowCircles
        {
            get { return showCircles; }
            set { showCircles = value; }
        }

        private bool showArcs;
        public bool ShowArcs
        {
            get { return showArcs; }
            set { showArcs = value; }
        }

        private bool showAreas;
        public bool ShowAreas
        {
            get { return showAreas; }
            set { showAreas = value; }
        }


        private bool showBounds;
        public bool ShowBounds
        {
            get { return showBounds; }
            set { showBounds = value; }
        }

        private bool showForces;
        public bool ShowForces
        {
            get { return showForces; }
            set { showForces = value; }
        }

        private bool showForceValue;
        public bool ShowForceValue
        {
            get { return showForceValue; }
            set { showForceValue = value; }
        }

        private string currentGridName;
        public string CurrentGridName
        {
            get { return currentGridName; }
            set { currentGridName = value; }
        }

        // шаг сетки
        private double gridPeriod;
        public double GridPeriod
        {
            get { return gridPeriod; }
            set { gridPeriod = value; }
        }

        private bool bindCursorToGrid; // привязка курсора к сетке
        public bool BindCursorToGrid
        {
            get { return bindCursorToGrid; }
            set { bindCursorToGrid = value; }
        }

        private bool showFENumbers;
        public bool ShowFENumbers
        {
            get { return showFENumbers; }
            set { showFENumbers = value; }
        }

        private bool showFENodes;
        public bool ShowFENodes
        {
            get { return showFENodes; }
            set { showFENodes = value; }
        }

        private bool showFEMaterials;
        public bool ShowFEMaterials
        {
            get { return showFEMaterials; }
            set { showFEMaterials = value; }
        }

        private bool showOnlyAreas;
        public bool ShowOnlyAreas
        {
            get { return showOnlyAreas; }
            set { showOnlyAreas = value; }
        }

        private bool showOnlyGeometry;
        public bool ShowOnlyGeometry
        {
            get { return showOnlyGeometry; }
            set { showOnlyGeometry = value; }
        }

        private bool showOnlyFE;
        public bool ShowOnlyFE
        {
            get { return showOnlyFE; }
            set { showOnlyFE = value; }
        }

        public MySettings()
        {
            this.showArcs = true;
            this.showAreas = true;
            this.showAxis = true;
            this.showBounds = true;
            this.showCircles = true;
            this.showForces = true;
            this.showForceValue = true;
            this.showGrid = true;
            this.showLines = true;
            this.showPoints = true;
            this.showFENumbers = true;
            this.showFENodes = true;
            this.currentGridName = "";
            this.gridPeriod = 1.0;
            this.bindCursorToGrid = false;
            this.showFEMaterials = true;
            this.showOnlyAreas = true;
            this.showOnlyGeometry = true;
            this.showOnlyFE = true;
        }
    }
}
