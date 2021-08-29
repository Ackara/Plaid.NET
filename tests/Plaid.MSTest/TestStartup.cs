using AutoBogus;
using AutoBogus.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: ApprovalTests.Namers.UseApprovalSubdirectory("approved-results")]
[assembly: ApprovalTests.Reporters.UseReporter(typeof(ApprovalTests.Reporters.DiffReporter), typeof(ApprovalTests.Reporters.ClipboardReporter))]

namespace Acklann.Plaid
{
	[TestClass]
	public class TestStartup
	{
		[AssemblyInitialize]
		public static void Initialize(TestContext _)
		{
			AutoFaker.Configure((builder) =>
			{
				builder.WithConventions((convention) =>
				{
				});
			});
		}

		[AssemblyCleanup]
		public static void Cleanup()
		{
			ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
		}
	}
}
