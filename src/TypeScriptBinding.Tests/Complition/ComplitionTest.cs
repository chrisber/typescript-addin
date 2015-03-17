using System;
using ICSharpCode.TypeScriptBinding;
using TypeScriptLanguageService;


namespace TypeScriptBinding.Tests.Complition
{
	public abstract class ComplitionTests
	{
		public CompletionInfo CompitionResult { get; set; }

		public void Complition(string filename, int offset, string text, bool ismemberCompletion)
		{
			var contextFactory = new FakeTypeScriptContextFactory();
//			var parser = new TypeScriptParser(contextFactory);
			var context = contextFactory.CreateContext();
			context.AddFile(filename, text);
			CompitionResult = context.GetCompletionItems(
					filename,
					offset,
					text,
					ismemberCompletion
				);
		}
	}
}

