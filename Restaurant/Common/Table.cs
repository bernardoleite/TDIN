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
        public State StateProperty { get; set; }

        public Table(int id)
        {
            this.Id = id;
            this.StateProperty = State.Closed;
        }
    }
}