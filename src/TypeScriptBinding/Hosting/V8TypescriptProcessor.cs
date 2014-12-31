using System;
using V8.Net;
using TypeScriptHosting;
using ICSharpCode.TypeScriptBinding.Hosting;
using MonoDevelop.Core;
using System.IO;
using System.Diagnostics;

namespace ICSharpCode.TypeScriptBinding
{
    public class V8TypescriptProcessor
    {
        static V8Engine v8Engine; 
        static IScriptLoader _scriptLoader;
        static LanguageServiceShimHost v8Host;

        static Handle serviceScriptHandle;
        static Handle mainScriptHandle;
        static Handle memberCompletionScriptHandle;
        static Handle completionDetailsScriptHandle;
        static Handle functionSignatureScriptHandle;
        static Handle findReferencesScriptHandle;
        static Handle definitionScriptHandle;
        static Handle navigationScriptHandle;
        static Handle languageServicesCompileScriptHandle;
        static Handle semanticDiagnosticsScriptHandle;





        public V8TypescriptProcessor()
            : this(new ScriptLoader(), new LanguageServiceLogger())
        {
        }

        public V8TypescriptProcessor (IScriptLoader scriptLoader, ILogger logger)
        {
            try {
                v8Engine = new V8Engine();
                _scriptLoader = scriptLoader;
                 v8Host = new LanguageServiceShimHost(logger);
                v8Host.AddDefaultLibScript(new FilePath(_scriptLoader.LibScriptFileName), _scriptLoader.GetLibScript());

                v8Engine.RegisterType<Script>(null, recursive: true);
                v8Engine.RegisterType<ScriptSnapshotShim>(null, recursive: true);
                v8Engine.RegisterType<LanguageServiceShimHost>(null, recursive: true);
                v8Engine.GlobalObject.SetProperty("host", v8Host);

                serviceScriptHandle = v8Engine.Compile(_scriptLoader.GetTypeScriptServicesScript(),"Monodevelop.ScriptServicesScript");
                mainScriptHandle = v8Engine.Compile(_scriptLoader.GetMainScript(),"Monodevelop.MainScript");
                memberCompletionScriptHandle = v8Engine.Compile(_scriptLoader.GetMemberCompletionScript(),"Monodevelop.MemberCompletionScript");
                completionDetailsScriptHandle = v8Engine.Compile(_scriptLoader.GetCompletionDetailsScript(),"Monodevelop.CompletionDetailsScript");
                functionSignatureScriptHandle = v8Engine.Compile(_scriptLoader.GetFunctionSignatureScript(),"Monodevelop.FunctionSignatureScript");
                findReferencesScriptHandle = v8Engine.Compile(_scriptLoader.GetFindReferencesScript(),"Monodevelop.FindReferencesScript");
                definitionScriptHandle = v8Engine.Compile(_scriptLoader.GetDefinitionScript(),"Monodevelop.DefinitionScript");
                navigationScriptHandle = v8Engine.Compile(_scriptLoader.GetNavigationScript(),"Monodevelop.NavigationScriptHandle");
                languageServicesCompileScriptHandle = v8Engine.Compile(_scriptLoader.GetLanguageServicesCompileScript(),"Monodevelop.LanguageServicesCompileScrip");
                semanticDiagnosticsScriptHandle = v8Engine.Compile(_scriptLoader.GetSemanticDiagnosticsScript(),"Monodevelop.SemanticDiagnosticsScript");



            var result = v8Engine.Execute (serviceScriptHandle, "V8.NET");
            System.Diagnostics.Debug.WriteLine (result);
            }catch(Exception ex){
                log (ex.ToString());
            }

        }

        public static void RunMainScript(){
            var result = v8Engine.Execute (mainScriptHandle);
            log (result);
        }

        public static void RunMemberCompletionScript(){
            try {
            var result = v8Engine.Execute (memberCompletionScriptHandle);
                log (result);
            }catch (Exception e){
                log (e.ToString ());
            }
        }

        public static void RunCompletionDetailsScript(){
            var result = v8Engine.Execute (completionDetailsScriptHandle);
            log (result);
        }

        public static void RunFunctionSignatureScript(){
            var result = v8Engine.Execute (functionSignatureScriptHandle);
            log (result);
        }

        public static void RunFindReferencesScript(){
            var result = v8Engine.Execute (findReferencesScriptHandle);
            log (result);
        }

        public static void RunDefinitionScript(){
            var result = v8Engine.Execute (definitionScriptHandle);
            log (result);
        }


        public static void RunNavigationScript(){
            var result = v8Engine.Execute (navigationScriptHandle);
            log (result);
        }

        public static void RunLanguageServicesCompileScript(){
            var result = v8Engine.Execute (languageServicesCompileScriptHandle);
            log (result);

        }

        public static void RunSemanticDiagnosticsScript(){
            var result = v8Engine.Execute (semanticDiagnosticsScriptHandle);
            log (result);
        }
            
        public static LanguageServiceShimHost host(){
            return v8Host;
        }
        public static  void log(string result){
            var mth = new StackTrace().GetFrame(1).GetMethod();
            System.Diagnostics.Debugger.Log(0, null, mth.ReflectedType.Name +"."+ mth.Name +"()-->"+ result + "\n");
        }


    }
}

