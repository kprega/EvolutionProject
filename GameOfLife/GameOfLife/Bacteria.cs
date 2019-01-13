using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Bacteria
    {
        public byte NeighbourRequiredToExist { get; set; }
        public byte DeathThreshold { get; set; }

        public Bacteria()
        {

        }

        private bool IsAlive()
        {
            throw new NotImplementedException();
        }
    }
}
