using System;

namespace Common
{
    [Serializable]
    public class Table
    {

        public int Id;
        public enum State { Waiting, Done, Closed }
    }
}