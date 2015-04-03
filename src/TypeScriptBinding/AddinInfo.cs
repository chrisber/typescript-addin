using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly:Addin ("TypeScript", 
	Namespace = "MonoDevelop",
	Version = MonoDevelop.BuildInfo.Version,
	Category = "Language bindings")]

[assembly:AddinName ("TypeScript Language Binding")]
[assembly:AddinDescription ("Adds TypeScript support.")]

[assembly:AddinDependency ("Core", MonoDevelop.BuildInfo.Version)]
[assembly:AddinDependency ("Ide", MonoDevelop.BuildInfo.Version)]
[assembly:AddinDependency ("Refactoring", MonoDevelop.BuildInfo.Version)]
[assembly:AddinDependency ("SourceEditor2", MonoDevelop.BuildInfo.Version)]

