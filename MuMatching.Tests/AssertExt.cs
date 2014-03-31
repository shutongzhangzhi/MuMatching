using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MuMatching.Tests {
    class AssertExt {
        internal static void Equal<TKey,TValue>(IDictionary<TKey, TValue> a, IDictionary<TKey, TValue> b) {
            Assert.Equal(a.Count, b.Count);
           
     
        }
    }
}
