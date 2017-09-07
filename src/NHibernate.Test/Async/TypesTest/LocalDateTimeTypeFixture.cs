﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using NUnit.Framework;

namespace NHibernate.Test.TypesTest
{
	using System.Threading.Tasks;
	/// <summary>
	/// The Unit Tests for the UtcDateTimeType.
	/// </summary>
	[TestFixture]
	public class LocalDateTimeTypeFixtureAsync : TypeFixtureBase
	{
		protected override string TypeName
		{
			get { return "DateTime"; }
		}

		[Test]
		public async Task ReadWriteAsync()
		{
			DateTime val = DateTime.UtcNow;
			DateTime expected = new DateTime(val.Year, val.Month, val.Day, val.Hour, val.Minute, val.Second, DateTimeKind.Local);

			DateTimeClass basic = new DateTimeClass();
			basic.Id = 1;
			basic.LocalDateTimeValue = val;

			ISession s = OpenSession();
			await (s.SaveAsync(basic));
			await (s.FlushAsync());
			s.Close();

			s = OpenSession();
			basic = (DateTimeClass) await (s.LoadAsync(typeof (DateTimeClass), 1));

			Assert.AreEqual(DateTimeKind.Local, basic.LocalDateTimeValue.Value.Kind);
			Assert.AreEqual(expected, basic.LocalDateTimeValue.Value);

			await (s.DeleteAsync(basic));
			await (s.FlushAsync());
			s.Close();
		}
	}
}