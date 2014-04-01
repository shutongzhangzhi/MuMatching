using MuMatching.WuManber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MuMatching.Tests.WuManber {
    public class WuManberInternalStateBuilderTest {
        private WuManberInternalStateBuilder _builder;

        public WuManberInternalStateBuilderTest() {
            _builder = new WuManberInternalStateBuilder();
        }

        [Fact]
        public void Build_Test() {

            // Arrange
            var patterns = new string[] { "abcde", "bcbde", "adcabe" };
            _builder.Initialize(5);

            // Act
            _builder.AddPatterns(patterns);
            var state = _builder.Build();

            // Assert
            Assert.Equal(5, state.MinPatternLength);
            Assert.Equal(2, state.BlockLength);
            Assert.Equal(2, state.PrefixLength);
            AssertShiftTable(state.ShiftTable);
            AssertPrefixTable(state);
        }

        private void AssertShiftTable(Dictionary<Substring, int> shiftTable) {
            var expected = new Dictionary<string, int>{ 
              {"bc",2}, {"cd",1}, {"cb",2}, 
              {"bd",1}, {"ad",3}, {"dc",2}, {"ca",1}};
            //{"ab",0},{"de",0} 
            foreach (var block in expected) {
                Assert.Contains(new KeyValuePair<Substring, int>(
                    Substring.Create(block.Key), block.Value),
                    shiftTable);
            }
        }

        private void AssertPrefixTable(WuManberInternalState state) {

           var abPrefixTable = state.GetPrefixTable(state.ShiftTable[Substring.Create("ab")]);
           var dePrefixTable = state.GetPrefixTable(state.ShiftTable[Substring.Create("de")]);

            // ad*abe
            Assert.Contains("adcabe", abPrefixTable[Substring.Create("ad")]);

            // ab*de
            Assert.Contains("abcde", dePrefixTable[Substring.Create("ab")]);

            // bc*de
            Assert.Contains("bcbde", dePrefixTable[Substring.Create("bc")]);

        }

    }
}
