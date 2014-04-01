using MuMatching.WuManber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MuMatching.Tests.WuManber {
    public class WuManberInternalStateBuilderTest {

        public WuManberInternalStateBuilderTest() {
        }

        [Fact]
        public void Build_Test() {

            // Arrange
            var patterns = new string[] { "abcdef", "decabf", "eecab", "abbde" };
            var builder = new WuManberInternalStateBuilder(patterns);
            

            // Act
            builder.Initialize();
            var state = builder.Build();

            // Assert
            Assert.Equal(5, state.LengthInfo.MinPatternLength);
            Assert.Equal(2, state.LengthInfo.BlockLength);
            Assert.Equal(2, state.LengthInfo.PrefixLength);
            AssertShiftTable(state.ShiftTable);
            AssertPrefixTable(state);
        }

        private void AssertShiftTable(Dictionary<Substring, int> shiftTable) {
            var expected = new Dictionary<string, int>{ 
                {"bc",2}, {"cd",1},{"ec",2}, 
                {"ca",1}, {"ee",3},{"bb",2}};
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

            // de*ab
            Assert.Contains("decabf", abPrefixTable[Substring.Create("de")]);

            // ee*ab
            Assert.Contains("eecab", abPrefixTable[Substring.Create("ee")]);

            // ab*de
            Assert.Contains("abcdef", dePrefixTable[Substring.Create("ab")]);

            // ab*de
            Assert.Contains("abbde", dePrefixTable[Substring.Create("ab")]);

        }



        [Fact]
        public void Constructor_Of_MinPatternLength_Test() {
            
            // Arrange
            var minPatternLength = 1;
            var builder = new WuManberInternalStateBuilder(minPatternLength);

            // Act
            builder.Initialize();
            var state = builder.Build();

            // Assert
            Assert.Equal(minPatternLength, state.LengthInfo.MinPatternLength);
        }

        [Fact]
        public void Constructor_Of_Patterns_Test() {

            // Arrange
            var patterns = new string[] { "abcde", "bcbde", "adcabe" };
            var builder = new WuManberInternalStateBuilder(patterns);

            // Act
            builder.Initialize();
            var state = builder.Build();

            // Assert
            Assert.Equal(5, state.LengthInfo.MinPatternLength);

        }


        [Fact]
        public void AddPatterns_When_Pattern_Length_Less_Than_MinPatternLength_Test() {
            // Arrage
            var builder = new WuManberInternalStateBuilder(5);

            // Act/Assert
            builder.Initialize();
            Assert.Throws<ArgumentException>(
                () => builder.AddPatterns(new[] {"abc"}));
        }
    }
}
