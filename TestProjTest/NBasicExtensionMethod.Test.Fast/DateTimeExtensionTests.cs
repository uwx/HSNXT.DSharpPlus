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
	public class DateTimeExtensionTests
	{
		private DateTime _mInvalidDateTimeLessThanMinDate = new DateTime(1899, 1, 1, 14, 30, 00);
		private DateTime _mInvalidDateTimeGraterThanMaxDate = new DateTime(2101, 1, 1, 14, 30, 00);
		private DateTime _mValidDateTime = new DateTime(2000, 1, 1, 14, 30, 00);

		[Test]
		public void IsValid_Should_Return_True_When_Date_Is_Grater_Than_MinDate_And_Less_Than_Max_Date() {
			// Arrange

			// Act & Assert
			_mValidDateTime.IsValid().Should().BeTrue();
		}

		[Test]
		public void IsValid_Should_Return_False_When_Date_Is_Less_Than_MinDate_And_Less_Than_Max_Date() {
			// Arrange

			// Act & Assert
			_mInvalidDateTimeLessThanMinDate.IsValid().Should().BeFalse();
		}

		[Test]
		public void IsValid_Should_Return_False_When_Date_Is_Grater_Than_MinDate_And_Grater_Than_Max_Date() {
			// Arrange

			// Act

			// Assert
			_mInvalidDateTimeGraterThanMaxDate.IsValid().Should().BeFalse();
		}

		/*
         * 
         * The following IsInFuture tests are not written in the past way as they rely on the system date and time.
         * At the time of writting I haven't worked out a why around this.
         * 
         */

		[Test]
		public void IsInFuture_Should_Return_True_When_Date_Is_Grater_Than_Now() {
			// Arrange
			var dtFutureDate = DateTime.Now.AddDays(2);
			// Act

			// Assert
			dtFutureDate.IsInFuture().Should().BeTrue();
		}

		[Test]
		public void IsInFuture_Should_Return_False_When_Date_Is_Less_Than_Now() {
			// Arrange
			var dtValue = DateTime.Now.AddDays(-1);
			// Act

			// Assert
			dtValue.IsInFuture().Should().BeFalse();
		}

		[Test]
		public void IsInFuture_Should_Return_False_When_Date_Equals_Now() {
			// Arrange
			var dtValue = DateTime.Now;
			// Act

			// Assert
			dtValue.IsInFuture().Should().BeFalse();
		}

		[Test]
		public void IsOlderThan_Should_Return_True_When_DateA_Is_Older_Than_DateB() {
			// Arrange
			var dtDateA = new DateTime(2000, 1, 1, 14, 30, 00);
			var dtDateB = new DateTime(2010, 1, 1, 14, 30, 00);
			// Act

			// Assert
			dtDateA.IsOlderThan(dtDateB).Should().BeTrue();
		}

		[Test]
		public void IsOlderThan_Should_Return_False_When_DateA_Is_Later_Than_DateB() {
			// Arrange
			var dtDateA = new DateTime(2011, 1, 1, 14, 30, 00);
			var dtDateB = new DateTime(2010, 1, 1, 14, 30, 00);
			// Act

			// Assert
			dtDateA.IsOlderThan(dtDateB).Should().BeFalse();
		}

		[Test]
		public void IsOlderThan_Should_Return_False_When_DateA_And_DateB_Are_The_Same() {
			// Arrange
			var dtDateA = new DateTime(2000, 1, 1, 14, 30, 00);
			var dtDateB = new DateTime(2000, 1, 1, 14, 30, 00);
			// Act

			// Assert
			dtDateA.IsOlderThan(dtDateB).Should().BeFalse();
		}

		[Test]
		public void IsLaterThan_Should_Return_True_When_DateA_Is_Later_Than_DateB() {
			// Arrange
			var dtDateA = new DateTime(2011, 1, 1, 14, 30, 00);
			var dtDateB = new DateTime(2010, 1, 1, 14, 30, 00);
			// Act

			// Assert
			dtDateA.IsLaterThan(dtDateB).Should().BeTrue();
		}

		[Test]
		public void IsLaterThan_Should_Return_False_When_DateA_Is_Sooner_Than_DateB() {
			// Arrange
			var dtDateA = new DateTime(2000, 1, 1, 14, 30, 00);
			var dtDateB = new DateTime(2010, 1, 1, 14, 30, 00);
			// Act

			// Assert
			dtDateA.IsLaterThan(dtDateB).Should().BeFalse();
		}

		[Test]
		public void IsLaterThan_Should_Return_False_When_DateA_And_DateB_Are_The_Same() {
			// Arrange
			var dtDateA = new DateTime(2000, 1, 1, 14, 30, 00);
			var dtDateB = new DateTime(2000, 1, 1, 14, 30, 00);
			// Act

			// Assert
			dtDateA.IsLaterThan(dtDateB).Should().BeFalse();
		}
	}
}