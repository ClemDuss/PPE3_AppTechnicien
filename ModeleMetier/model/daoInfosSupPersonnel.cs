using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.model
{
    public class daoInfosSupPersonnel
    {
        private dbal _myDbal;
        private string _tableDB = "InfosSupPersonnel";

        public daoInfosSupPersonnel(dbal theDbal)
        {
            _myDbal = theDbal;
        }
    }
}
