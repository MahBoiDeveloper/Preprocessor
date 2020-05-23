using System;
using System.Collections.Generic;

using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyMaterial
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private double elasticModulus;
        public double ElasticModulus
        {
            get { return elasticModulus; }
            set { elasticModulus = value; }
        }

        private double poissonsRatio;
        public double PoissonsRatio
        {
            get { return poissonsRatio; }
            set { poissonsRatio = value; }
        }

        private double tension;
        public double Tension
        {
            get { return tension; }
            set { tension = value; }
        }

        private double tension2;
        public double Tension2
        {
            get { return tension2; }
            set { tension2 = value; }
        }

        private double thickness;
        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public void UpdateMaterial(string name, double em, double pr, double t, double t2, double th)
        {       
            this.Name = name;
            this.ElasticModulus = em;
            this.PoissonsRatio = pr;
            this.Tension = t;
            this.Tension2 = t2;
            this.Thickness = th;
        }

        public MyMaterial(int id, string name, double em, double pr, double t, double t2, double th)
        {
            this.Id = id+1;
            this.Name = name;
            this.ElasticModulus = em;
            this.PoissonsRatio = pr;
            this.Tension = t;
            this.Tension2 = t2;
            this.Thickness = th;
        }
    }
}
