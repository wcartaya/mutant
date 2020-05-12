using System.Collections.Generic;

namespace Test.Util.Stub
{
    public class stringArrayStub
    {
        private IList<string[]> _data;
        
        public stringArrayStub()
        {
            _data = new List<string[]>();
            _data.Add( new string[] { "ATGGGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" });
            _data.Add( new string[] { "ATGCGA", "CAGTTC", "TTGAGT", "AGAAGG", "ACCCTA", "TCACTG" });
        }
        public string[] GetOnPosition(int position)
        {
            return _data[position];
        }
    }
}
