using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MuMatching.WuManber
{
    public sealed class WMStringMatcher : StringMatcher
    {
        internal void Initialize()
        {
            
        }


        #region Override Members

        protected override IEnumerable<StringMatchHit> ExecuteCore(TextReader source)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<StringMatchHit> ExecuteCore(string source, int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
