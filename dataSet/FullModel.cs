using System;
using System.Collections.Generic;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class FullModel
    {
        public MyGeometryModel geometryModel;
        public MySettings settings;
        public MyMaterials materials;
        public List<MyFiniteElementModel> FiniteElementModels;
        public List<DensityPoint> densityPoints = new List<DensityPoint>();
        public string currentGridName = "";

        public int IdCandidate {
            get { return FiniteElementModels.Count + 1; }                 
        }

        public FullModel()
        {
            this.geometryModel = new MyGeometryModel();
            this.settings = new MySettings();
            this.materials = new MyMaterials();
            FiniteElementModels = new List<MyFiniteElementModel>();
        }
    }
}
