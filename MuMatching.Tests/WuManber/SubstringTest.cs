using MuMatching.WuManber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace MuMatching.Tests.WuManber {
    public class SubstringTest {


        [Theory]
        [InlineData("abcdefg", 0, 3, "bcdabcd", 3, 3, true)]
        [InlineData("abcdefg", 0, 0, "bcdabcd", 3, 0, true)]
        [InlineData("ABCdefg", 0, 3, "bcdabcd", 3, 3, false)]
        [InlineData("abcdefg", 0, 4, "abcdefg", 0, 3, false)]
        public void Equals_Test(
            string target1, int startIndex1, int length1,
            string target2, int startIndex2, int length2,
            bool result) {

            // Arrange
            var sub1 = Substring.Create(target1, startIndex1, length1);
            var sub2 = Substring.Create(target2, startIndex2, length2);

            // Act
            var result1 = sub1.Equals(sub2);
            var result2 = sub2.Equals(sub1);

            // Assert
            Assert.Equal(result, result1);
            Assert.Equal(result, result2);
        }

        [Theory]
        [InlineData("abcdefg", 0, 3, "bcdabcd", 3, 3, true)]
        [InlineData("abcdefg", 0, 0, "bcdabcd", 3, 0, true)]
        [InlineData("ABCdefg", 0, 3, "bcdabcd", 3, 3, false)]
        [InlineData("abcdefg", 0, 4, "abcdefg", 0, 3, false)]
        public void GetHashCode_Test(
            string target1, int startIndex1, int length1,
            string target2, int startIndex2, int length2,
            bool equal) {

            // Arrange
            var sub1 = Substring.Create(target1, startIndex1, length1);
            var sub2 = Substring.Create(target2, startIndex2, length2);

            // Act
            var result1 = sub1.GetHashCode();
            var result2 = sub2.GetHashCode();

            // Assert
            if (equal) {
                Assert.Equal(result1, result2);
            }
            else {
                Assert.NotEqual(result1, result2);
            }
        }

        [Theory]
        [InlineData("abcdefg", 0, 3, "abc")]
        [InlineData("abcdefg", 0, 0, "")]
        [InlineData("abcdefg", 0, 7, "abcdefg")]
        public void ToString_Test(
            string target, int startIndex, int length, string subStr) {

            // Arrange
            var sub = Substring.Create(target, startIndex, length);

            // Act
            var result = sub.ToString();

            // Assert
            Assert.Equal(subStr, result);
        }

    }
}
