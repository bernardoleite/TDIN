using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class Table
    {

        public int Id;
        public enum State { Waiting, Done, Closed }
    }
}