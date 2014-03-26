using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MuMatching.WuManber
{
    internal sealed class WMInternalStateBuilder
    {
        private const int DEFAULT_BLOCK_LENGTH = 2;
        


        public void AddPatterns(IEnumerable<string> patterns) {
            Debug.Assert(patterns != null);

            foreach (var pattern in patterns) {
                if (!String.IsNullOrEmpty(pattern)) {
                    AddPattern(pattern);
                }
            }
        }

        private void AddPattern(string pattern) {
          
        }


        internal int MinPatternLength { get; set; }


    }
}
