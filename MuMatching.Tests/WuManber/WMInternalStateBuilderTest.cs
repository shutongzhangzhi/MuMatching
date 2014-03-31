using MuMatching.WuManber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MuMatching.Tests.WuManber {
    public class WMInternalStateBuilderTest {
        private WMInternalStateBuilder _builder;

        public WMInternalStateBuilderTest() {
            _builder = new WMInternalStateBuilder();
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
        }

        private void AssertShiftTable(Dictionary<Substring, int> shiftTable) {
            var expected = new Dictionary<string, int>{ 
             {"ab",0}, {"bc",2}, {"cd",1}, 
             {"de",0}, {"cb",2}, {"bd",1},
             {"ad",3}, {"dc",2}, {"ca",1}};

            var actual = shiftTable.Select(
                kv => new KeyValuePair<string, int>(kv.Key.ToString(), kv.Value));

            Assert.Equal(expected, actual);

        }
    }
}
