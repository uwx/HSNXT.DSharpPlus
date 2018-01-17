// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NBasicExtensionMethod.
// 
// NBasicExtensionMethod is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NBasicExtensionMethod is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NBasicExtensionMethod. If not, see <http://www.gnu.org/licenses/>.

namespace HSNXT.Test.Fast
{
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class BooleanExtensionTests
	{
		[Test]
		public void And_Should_Return_True_When_Both_Values_Are_True() {
			// Arrange
			var bValueA = true;
			var bValueB = true;

			// Act & Assert
			bValueA.And(bValueB).Should().Be(true);
		}

		[Test]
		[TestCase(true, false)]
		[TestCase(false, true)]
		public void And_Should_Return_False_When_One_Value_Is_False(bool bValueA, bool bValueB) {
			// Arrange

			// Act & Assert
			bValueA.And(bValueB).Should().Be(false);
		}

		[Test]
		public void Or_Should_Return_False_When_Both_Values_Are_False() {
			// Arrange
			var bValueA = false;
			var bValueB = false;

			// Act & Assert
			bValueA.Or(bValueB).Should().Be(false);
		}

		[Test]
		[TestCase(false, true)]
		[TestCase(true, false)]
		[TestCase(true, true)]
		public void Or_Should_Return_True_When_One_Value_Is_True(bool bValueA, bool bValueB) {
			// Arrange

			// Act & Assert
			bValueA.Or(bValueB).Should().Be(true);
		}
	}
}