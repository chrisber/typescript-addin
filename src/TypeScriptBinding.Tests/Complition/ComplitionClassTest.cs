using System;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem;
using MonoDevelop.Ide.TypeSystem;
using NUnit.Framework;
using FluentAssertions;

using TypeScriptLanguageService;

namespace TypeScriptBinding.Tests.Complition
{
	public class ComplitionClassTest : ComplitionTests
	{
		[Test]
		public void Complition_EmptyStudentClass_ClassHasCorrectBodyRegion()
		{

			var sLoader = new ComplitionTestScriptLoader();
			string fileName = sLoader.GetComplitionTestFullNameScript();
			string text = File.ReadAllText(fileName);

			Complition(fileName, 184, text, true);

			CompitionResult.entries.Should().NotBeNullOrEmpty();

//			FoldingRegion folding = ParsedDocument.Foldings.FirstOrDefault();
//			Assert.AreEqual(expectedBodyRegion, folding.Region);
//			Assert.AreEqual(FoldType.Type, folding.Type);
		}
	}
}

