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
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class IEnumerableExtensionsTests
	{
		[Test]
		public void IsNotNullOrEmpty_Should_Return_True_When_Passed_A_Non_Empty_List() {
			// Arrange
			var Products = new List<string> {"DummyValue"};
			// Act

			// Assert
			Products.IsNotNullOrEmpty().Should().BeTrue();
		}

		[Test]
		public void IsNotNullOrEmpty_Should_Return_False_When_Passed_An_Empty_List() {
			// Arrange
			var Products = new List<string>();
			// Act

			// Assert
			Products.IsNotNullOrEmpty().Should().BeFalse();
		}

		[Test]
		public void IsNotNullOrEmpty_Should_Return_False_When_Passed_A_Null_List() {
			// Arrange
			List<string> Products = null;
			// Act

			// Assert
			Products.IsNotNullOrEmpty().Should().BeFalse();
		}
	}
}