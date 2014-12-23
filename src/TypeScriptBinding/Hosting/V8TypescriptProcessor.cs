using System;
using V8.Net;
using TypeScriptHosting;
using ICSharpCode.TypeScriptBinding.Hosting;
using MonoDevelop.Core;
using System.IO;

namespace ICSharpCode.TypeScriptBinding
{
    public class V8TypescriptProcessor
    {
        V8Engine v8Engine; 
        IScriptLoader scriptLoader;

        Handle serviceScriptHandle;
        Handle mainScriptHandle;
        Handle memberCompletionScriptHandle;
        Handle completionDetailsScriptHandle;
        Handle functionSignatureScriptHandle;
        Handle findReferencesScriptHandle;
        Handle definitionScriptHandle;
        Handle navigationScriptHandle;
        Handle languageServicesCompileScriptHandle;
        Handle semanticDiagnosticsScriptHandle;



        public LanguageServiceShimHost Host {
            get;
            set;
        }

        public V8TypescriptProcessor()
            : this(new ScriptLoader(), new LanguageServiceLogger())
        {
        }

        public V8TypescriptProcessor (IScriptLoader scriptLoader, ILogger logger)
        {
            try {
            v8Engine = new V8Engine();
                string tscJs = File.ReadAllText("/Build/Typescript/typescript-addin/src/TypeScriptBinding/bin/Debug/tsc.js");
                string tscJs1 = File.ReadAllText("/Build/Typescript/typescript-addin/src/TypeScriptBinding/bin/Debug/tsc.js");
                string tscJs2 = File.ReadAllText("/Build/Typescript/typescript-addin/src/TypeScriptBinding/bin/Debug/tsc.js");
                string tscJs3 = File.ReadAllText("/Build/Typescript/typescript-addin/src/TypeScriptBinding/bin/Debug/tsc.js");

                this.scriptLoader = scriptLoader;
            Host = new LanguageServiceShimHost(logger);
            Host.AddDefaultLibScript(new FilePath(scriptLoader.LibScriptFileName), scriptLoader.GetLibScript());

            v8Engine.RegisterType<LanguageServiceShimHost>(null, recursive: true);
            v8Engine.GlobalObject.SetProperty("host", Host);

            serviceScriptHandle = v8Engine.Compile(scriptLoader.GetTypeScriptServicesScript(),"Monodevelop.ScriptServicesScript");
            mainScriptHandle = v8Engine.Compile(scriptLoader.GetMainScript(),"Monodevelop.MainScript");
            memberCompletionScriptHandle = v8Engine.Compile(scriptLoader.GetMemberCompletionScript(),"Monodevelop.MemberCompletionScript");
            completionDetailsScriptHandle = v8Engine.Compile(scriptLoader.GetCompletionDetailsScript(),"Monodevelop.CompletionDetailsScript");
            functionSignatureScriptHandle = v8Engine.Compile(scriptLoader.GetFunctionSignatureScript(),"Monodevelop.FunctionSignatureScript");
            findReferencesScriptHandle = v8Engine.Compile(scriptLoader.GetFindReferencesScript(),"Monodevelop.FindReferencesScript");
            definitionScriptHandle = v8Engine.Compile(scriptLoader.GetDefinitionScript(),"Monodevelop.DefinitionScript");
            navigationScriptHandle = v8Engine.Compile(scriptLoader.GetNavigationScript(),"Monodevelop.NavigationScriptHandle");
            languageServicesCompileScriptHandle = v8Engine.Compile(scriptLoader.GetLanguageServicesCompileScript(),"Monodevelop.LanguageServicesCompileScrip");
            semanticDiagnosticsScriptHandle = v8Engine.Compile(scriptLoader.GetSemanticDiagnosticsScript(),"Monodevelop.SemanticDiagnosticsScript");



            var result = v8Engine.Execute (serviceScriptHandle, "V8.NET");
            System.Diagnostics.Debug.WriteLine (result);
            }catch(Exception ex){
                log (ex.ToString());
            }

        }

        public void RunMainScript(){
            var result = v8Engine.Execute (mainScriptHandle);
            log (result);
        }

        public void RunMemberCompletionScript(){
            var result = v8Engine.Execute (memberCompletionScriptHandle);
            log (result);
        }

        public void RunCompletionDetailsScript(){
            var result = v8Engine.Execute (completionDetailsScriptHandle);
            log (result);
        }

        public void RunFunctionSignatureScript(){
            var result = v8Engine.Execute (functionSignatureScriptHandle);
            log (result);
        }

        public void RunFindReferencesScript(){
            var result = v8Engine.Execute (findReferencesScriptHandle);
            log (result);
        }

        public void RunDefinitionScript(){
            var result = v8Engine.Execute (definitionScriptHandle);
            log (result);
        }


        public void RunNavigationScript(){
            var result = v8Engine.Execute (navigationScriptHandle);
            log (result);
        }

        public void RunLanguageServicesCompileScript(){
            var result = v8Engine.Execute (languageServicesCompileScriptHandle);
            log (result);
        }

        public void RunSemanticDiagnosticsScript(){
            var result = v8Engine.Execute (semanticDiagnosticsScriptHandle);
            log (result);
        }

        public void log(string result){
            System.Diagnostics.Debugger.Log(0, null, result);


        }


    }
}

