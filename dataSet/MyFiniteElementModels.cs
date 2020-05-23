using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyFiniteElementModels
    {
        public List<MyFiniteElementModel> FiniteElementModels;
        public int IdCandidate; // id будущей модели

        public MyFiniteElementModels()
        {
            this.IdCandidate = 1;
            FiniteElementModels = new List<MyFiniteElementModel>();
        }
    }
}
