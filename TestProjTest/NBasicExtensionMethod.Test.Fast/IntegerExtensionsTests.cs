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
	using System;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class IntegerExtensionsTests
	{
		[Test]
		public void Millisecond_Should_Retrun_Timespan_Object_With_One_Millisecond_Set() {
			// Arrange
			var iValue = 1;
			// Act
			var ts = iValue.Millisecond();
			// Assert
			ts.TotalMilliseconds.Equals(1).Should().BeTrue();
		}

		[Test]
		[TestCase(45)]
		[TestCase(-45)]
		public void Milliseconds_Should_Return_Timespan_Object_With_Same_Number_Of_Milliseconds_Set_As_Passsed_In(int iValue) {
			// Arrange

			// Act
			var ts = iValue.Milliseconds();
			// Assert
			ts.TotalMilliseconds.Equals(iValue).Should().BeTrue();
		}

		[Test]
		public void Second_Should_Retrun_Timespan_Object_With_One_Second_Set() {
			// Arrange
			var iValue = 1;
			// Act
			var ts = iValue.Second();
			// Assert
			ts.TotalSeconds.Equals(1).Should().BeTrue();
		}

		[Test]
		[TestCase(45)]
		[TestCase(-45)]
		public void Seconds_Should_Return_Timespan_Object_With_Same_Number_Of_Seconds_Set_As_Passsed_In(int iValue) {
			// Arrange

			// Act
			var ts = iValue.Seconds();
			// Assert
			ts.TotalSeconds.Equals(iValue).Should().BeTrue();
		}

		[Test]
		public void Minute_Should_Retrun_Timespan_Object_With_One_Minute_Set() {
			// Arrange
			var iValue = 1;
			// Act
			var ts = iValue.Minute();
			// Assert
			ts.TotalMinutes.Equals(1).Should().BeTrue();
		}

		[Test]
		[TestCase(45)]
		[TestCase(-45)]
		public void Minute_Should_Return_Timespan_Object_With_Same_Number_Of_Minutes_Set_As_Passsed_In(int iValue) {
			// Arrange

			// Act
			var ts = iValue.Minutes();
			// Assert
			ts.TotalMinutes.Equals(iValue).Should().BeTrue();
		}

		[Test]
		public void Hour_Should_Retrun_Timespan_Object_With_One_Hour_Set() {
			// Arrange
			var iValue = 1;
			// Act
			var ts = iValue.Hour();
			// Assert
			ts.TotalHours.Equals(1).Should().BeTrue();
		}

		[Test]
		[TestCase(45)]
		[TestCase(-45)]
		public void Hour_Should_Return_Timespan_Object_With_Same_Number_Of_Hours_Set_As_Passsed_In(int iValue) {
			// Arrange

			// Act
			var ts = iValue.Hours();
			// Assert
			ts.TotalHours.Equals(iValue).Should().BeTrue();
		}

		[Test]
		public void Day_Should_Retrun_Timespan_Object_With_One_Day_Set() {
			// Arrange
			var iValue = 1;
			// Act
			var ts = iValue.Day();
			// Assert
			ts.TotalDays.Equals(1).Should().BeTrue();
		}

		[Test]
		[TestCase(45)]
		[TestCase(-45)]
		public void Days_Should_Return_Timespan_Object_With_Same_Number_Of_Days_Set_As_Passsed_In(int iValue) {
			// Arrange

			// Act
			var ts = iValue.Days();
			// Assert
			ts.TotalDays.Equals(iValue).Should().BeTrue();
		}

		[Test]
		[TestCase(1, 365.2425)]
		[TestCase(2, 730.485)]
		[TestCase(4, 1460.97)]
		[TestCase(6, 2191.455)]
		[TestCase(10, 3652.4249999999997)]
		[TestCase(20, 7304.8499999999995)]
		[TestCase(100, 36524.25)]
		public void Years_Should_Return_TimeSpan_Object_With_TotalDays_Set_As_Expected(int iValue, double iExpectedReturnValue) {
			// Arrange

			// Act
			var ts = iValue.Years();
			// Assert
			ts.TotalDays.Equals(iExpectedReturnValue).Should().BeTrue();
		}

		[Test]
		[TestCase(2, 1, 3)]
		[TestCase(20, 1, 30)]
		[TestCase(3425, 3000, 30000)]
		public void IsBetween_Should_Return_True_When_Value_Is_Between_Lower_And_Upper_Boundaries(int iInputValue, int iLowerBoundary, int iUpperBoundary) {
			// Arrange

			// Act

			// Assert
			iInputValue.IsBetween(iLowerBoundary, iUpperBoundary).Should().BeTrue();
		}

		[Test]
		[TestCase(1, 1, 3)]
		[TestCase(3, 1, 3)]
		public void IsBetween_Should_Return_False_When_Value_Is_Equal_To_Lower_Or_Upper_Boundary(int iInputValue, int iLowerBoundary, int iUpperBoundary) {
			// Arrange

			// Act

			// Assert
			iInputValue.IsBetween(iLowerBoundary, iUpperBoundary).Should().BeFalse();
		}

		[Test]
		[TestCase(-1, 1, 3)]
		[TestCase(4, 1, 3)]
		public void IsBetween_Should_Return_False_When_Value_Is_Less_Or_Grater_Than_Lower_Or_Upper_Boundary(int iInputValue, int iLowerBoundary, int iUpperBoundary) {
			// Arrange

			// Act

			// Assert
			iInputValue.IsBetween(iLowerBoundary, iUpperBoundary).Should().BeFalse();
		}

		[Test]
		[TestCase(2, 1, 3)]
		[TestCase(20, 1, 30)]
		[TestCase(3425, 3000, 30000)]
		public void IsBetweenInclusive_Should_Return_True_When_Value_Is_Between_Lower_And_Upper_Boundaries(int iInputValue, int iLowerBoundary, int iUpperBoundary) {
			// Arrange

			// Act

			// Assert
			iInputValue.IsBetweenInclusive(iLowerBoundary, iUpperBoundary).Should().BeTrue();
		}

		[Test]
		[TestCase(1, 1, 3)]
		[TestCase(3, 1, 3)]
		public void IsBetweenInclusive_Should_Return_True_When_Value_Is_Equal_To_Lower_Or_Upper_Boundary(int iInputValue, int iLowerBoundary, int iUpperBoundary) {
			// Arrange

			// Act

			// Assert
			iInputValue.IsBetweenInclusive(iLowerBoundary, iUpperBoundary).Should().BeTrue();
		}

		[Test]
		[TestCase(-1, 1, 3)]
		[TestCase(4, 1, 3)]
		public void IsBetweenInclusive_Should_Return_False_When_Value_Is_Less_Or_Grater_Than_Lower_Or_Upper_Boundary(int iInputValue,
		                                                                                                             int iLowerBoundary,
		                                                                                                             int iUpperBoundary) {
			// Arrange

			// Act

			// Assert
			iInputValue.IsBetweenInclusive(iLowerBoundary, iUpperBoundary).Should().BeFalse();
		}

		[Test]
		[TestCase(2, 1)]
		[TestCase(3842, 1000)]
		public void IsGreaterThan_Should_Return_True_When_Value_Is_Grater_Than_Boundary(int iValue, int iBoundary) {
			// Arrange

			// Act

			// Assert
			iValue.IsGreaterThan(iBoundary).Should().BeTrue();
		}

		[Test]
		[TestCase(-1, 1)]
		[TestCase(999, 1000)]
		public void IsGreaterThan_Should_Return_False_When_Value_Is_Less_Than_Boundary(int iValue, int iBoundary) {
			// Arrange

			// Act

			// Assert
			iValue.IsGreaterThan(iBoundary).Should().BeFalse();
		}

		[Test]
		[TestCase(2, 1)]
		[TestCase(3842, 1000)]
		public void IsLessThan_Should_Return_False_When_Value_Is_Grater_Than_Boundary(int iValue, int iBoundary) {
			// Arrange

			// Act

			// Assert
			iValue.IsLessThan(iBoundary).Should().BeFalse();
		}

		[Test]
		[TestCase(-1, 1)]
		[TestCase(999, 1000)]
		public void IsLessThan_Should_Return_True_When_Value_Is_Less_Than_Boundary(int iValue, int iBoundary) {
			// Arrange

			// Act

			// Assert
			iValue.IsLessThan(iBoundary).Should().BeTrue();
		}

		[Test]
		[TestCase(0)]
		[TestCase(2)]
		[TestCase(1024)]
		public void IsPositive_Should_Return_True_When_Value_Is_A_Positive_Value(int iValue) {
			// Arrange

			// Act

			// Assert
			iValue.IsPositive().Should().BeTrue();
		}

		[Test]
		[TestCase(-1)]
		[TestCase(-2)]
		[TestCase(-1024)]
		public void IsPositive_Should_Return_False_When_Value_Is_Not_A_Positive_Value(int iValue) {
			// Arrange

			// Act

			// Assert
			iValue.IsPositive().Should().BeFalse();
		}
	}
}