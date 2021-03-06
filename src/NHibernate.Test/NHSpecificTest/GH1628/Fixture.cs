﻿using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.GH1628
{
	[TestFixture]
	public class Fixture : BugTestCase
	{
		protected override void OnSetUp()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var e1 = new Entity
				{
					Id = 1,
					Name = "Bob",
					ALongText = "Bob's very long text"
				};
				session.Save(e1);

				var e2 = new Entity
				{
					Id = 2,
					Name = "Sally",
					ALongText = "Sally's very long text"
				};
				session.Save(e2);

				transaction.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				// The HQL delete does all the job inside the database without loading the entities, but it does
				// not handle delete order for avoiding violating constraints if any. Use
				// session.Delete("from System.Object");
				// instead if in need of having NHbernate ordering the deletes, but this will cause
				// loading the entities in the session.
				session.CreateQuery("delete from System.Object").ExecuteUpdate();

				transaction.Commit();
			}
		}

		[Test]
		public void ShouldNotThrowStackOverflowException()
		{
			using (var session = OpenSession())
			using (session.BeginTransaction())
			{
				IEntity result = session.Get<Entity>(2);
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Thing, Is.Null);
			}
		}
	}
}
