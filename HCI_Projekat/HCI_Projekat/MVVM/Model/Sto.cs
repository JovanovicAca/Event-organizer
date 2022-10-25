using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.Model
{
    class Sto
    {
        public int _br_stola;
        public int _br_mesta;
        public List<string> _imena;
        public Sto(int br_stola, int br_mesta, List<string> imena)
        {
            _br_stola = br_stola;
            _br_mesta = br_mesta;
            _imena = imena;
        }

        public string returnStringFile()
        {

            string ljudi = "";
            foreach (string covek in _imena)
            {
                ljudi += covek + ",";
            }

            ljudi = ljudi.Remove(ljudi.Length - 1);

            return _br_stola.ToString() + ";" + _br_mesta.ToString() + ";" + ljudi; 
        }
    }
}
