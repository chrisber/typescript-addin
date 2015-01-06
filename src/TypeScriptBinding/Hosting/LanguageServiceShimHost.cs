// 
// LanguageServiceShimHost.cs
// 
// Author:
//   Matt Ward <ward.matt@gmail.com>
// 
// Copyright (C) 2013 Matthew Ward
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.TypeScriptBinding;
using ICSharpCode.TypeScriptBinding.Hosting;
using MonoDevelop.Core;
using Newtonsoft.Json;
using V8.Net;

namespace TypeScriptHosting
{
    /// <summary>
    /// TODO: Idea Remove service (service.ts 869) functions from host.
    /// 
    /// getSyntacticDiagnostics(fileName: string): Diagnostic[];
    /// getSemanticDiagnostics(fileName: string): Diagnostic[];
    /// getCompilerOptionsDiagnostics(): Diagnostic[];
    /// 
    /// Use a Managed Javascript function (V8.Net.V8ManagedObject) implementations
    /// for callbacks from the service to managed
    /// RunMemberCompletionScript(){
    ///  execute()
    ///  return updateCompletionInfoAtCurrentPosition
    /// }
    /// this should speed up the process and seperate host from service
    /// </summary>
    [ScriptObject("LanguageServiceShimHost", security: ScriptMemberSecurity.Permanent)]
	public class LanguageServiceShimHost : ILanguageServiceShimHost
	{
		Dictionary<string, Script> scripts = new Dictionary<string, Script>();
		ILogger logger;
		string defaultLibScriptFileName;
		CompilerOptions compilerSettings = new CompilerOptions();
		
		public LanguageServiceShimHost(ILogger logger)
		{
			this.logger = logger;
            SemanticDiagnosticsResult = new SemanticDiagnosticsResult ();
            SyntaticDiagnosticsResult = new SyntaticDiagnosticsResult ();
		}
		
		internal void AddDefaultLibScript(FilePath fileName, string text)
		{
			defaultLibScriptFileName = fileName;
			AddFile(fileName, text);
		}
		
		internal bool AddFile(FilePath fileName, string text)
		{
			string lowercaseFileName = fileName.FullPath;
			if (!scripts.ContainsKey(lowercaseFileName)) {
				scripts.Add(lowercaseFileName, new Script(lowercaseFileName, text));
                return true;
			}
            return false;
		}
		
        [ScriptMember (inScriptName: "FindScript", security: ScriptMemberSecurity.Permanent)]
		Script FindScript(FilePath fileName)
		{
			string matchFileName = fileName;
			Script script = null;
			if (scripts.TryGetValue(matchFileName, out script)) {
				return script;
			}
			return null;
		}
		
		internal void UpdateFile(FilePath fileName, string text)
		{
			scripts[fileName.FullPath].Update(text);
		}
		
        [ScriptMember (inScriptName: "position", security: ScriptMemberSecurity.Permanent)]
		public int position { get; set; }
        [ScriptMember (inScriptName: "fileName", security: ScriptMemberSecurity.Permanent)]
		public string fileName { get; set; }
        [ScriptMember (inScriptName: "isMemberCompletion", security: ScriptMemberSecurity.Permanent)]
		public bool isMemberCompletion { get; set; }
        [ScriptMember (inScriptName: "completionEntry", security: ScriptMemberSecurity.Permanent)]
		public string completionEntry { get; set; }
		

        internal CompletionResult CompletionResult { get; private set; }


        [ScriptMember (inScriptName: "updateCompletionInfoAtCurrentPosition", security: ScriptMemberSecurity.Permanent)]
		public void updateCompletionInfoAtCurrentPosition(string completionInfo)
        { 
			LogDebug(completionInfo);
			CompletionResult = JsonConvert.DeserializeObject<CompletionResult>(completionInfo);

		}
		
		
        [ScriptMember (inScriptName: "updateCompletionEntryDetailsAtCurrentPosition", security: ScriptMemberSecurity.Permanent)]
		public void updateCompletionEntryDetailsAtCurrentPosition(string completionEntryDetails)
		{
			LogDebug(completionEntryDetails);
			CompletionEntryDetailsResult = JsonConvert.DeserializeObject<CompletionEntryDetailsResult>(completionEntryDetails);
		}
		
		internal CompletionEntryDetailsResult CompletionEntryDetailsResult { get; private set; }
		
        [ScriptMember (inScriptName: "updateSignatureAtPosition", security: ScriptMemberSecurity.Permanent)]
		public void updateSignatureAtPosition(string signature)
		{
			LogDebug(signature);
			SignatureResult = JsonConvert.DeserializeObject<SignatureResult>(signature);
		}
		
		internal SignatureResult SignatureResult { get; private set; }
		
        [ScriptMember (inScriptName: "updateReferencesAtPosition", security: ScriptMemberSecurity.Permanent)]
		public void updateReferencesAtPosition(string references)
		{
			LogDebug(references);
			ReferencesResult = JsonConvert.DeserializeObject<ReferencesResult>(references);
		}
		
		internal ReferencesResult ReferencesResult { get; private set; }
		
        [ScriptMember (inScriptName: "updateDefinitionAtPosition", security: ScriptMemberSecurity.Permanent)]
		public void updateDefinitionAtPosition(string definition)
		{
			LogDebug(definition);
			DefinitionResult = JsonConvert.DeserializeObject<DefinitionResult>(definition);
		}
		
		internal DefinitionResult DefinitionResult { get; private set; }
		
        [ScriptMember (inScriptName: "updateLexicalStructure", security: ScriptMemberSecurity.Permanent)]
		public void updateLexicalStructure(string structure)
		{

            LogDebug(structure.ToString());
                LexicalStructure = JsonConvert.DeserializeObject<List<NavigationBarItem>>(structure);
		}
		
        internal List<NavigationBarItem>  LexicalStructure { get; private set; }
		
		//public void updateOutliningRegions(string regions)
		//{
		//	LogDebug(regions);
		//	OutlingRegions = new NavigationInfo(regions);
		//}
		
		//internal NavigationInfo OutlingRegions { get; private set; }
		
        [ScriptMember (inScriptName: "information", security: ScriptMemberSecurity.Permanent)]
		public bool information()
		{
			return logger.information();
		}
		
        [ScriptMember (inScriptName: "debug", security: ScriptMemberSecurity.Permanent)]
		public bool debug()
		{
			return logger.debug();
		}
		
        [ScriptMember (inScriptName: "warning", security: ScriptMemberSecurity.Permanent)]
		public bool warning()
		{
			return logger.warning();
		}
		
        [ScriptMember (inScriptName: "error", security: ScriptMemberSecurity.Permanent)]
		public bool error()
		{
			return logger.error();
		}
		
        [ScriptMember (inScriptName: "fatal", security: ScriptMemberSecurity.Permanent)]
		public bool fatal()
		{
			return logger.fatal();
		}
		
        [ScriptMember (inScriptName: "log", security: ScriptMemberSecurity.Permanent)]
		public void log(string s)
		{
            System.Diagnostics.Debugger.Log(0, null, s + "\n");
			logger.log(s);
		}
		
        [ScriptMember (inScriptName: "LogDebug", security: ScriptMemberSecurity.Permanent)]
		void LogDebug(string format, params object[] args)
		{
			LogDebug(String.Format(format, args));
		}
		
        [ScriptMember (inScriptName: "LogDebug", security: ScriptMemberSecurity.Permanent)]
		void LogDebug(string s)
		{
			if (debug()) {
				log(s);
			}
		}
		
        [ScriptMember (inScriptName: "getCompilationSettings", security: ScriptMemberSecurity.Permanent)]
        public CompilerOptions getCompilationSettings()
		{
            LogDebug("Host.getCompilationSettings " + JsonConvert.SerializeObject(compilerSettings));
            return compilerSettings;
		}
		
		internal void UpdateCompilerSettings(ITypeScriptOptions options)
		{
			compilerSettings = new CompilerOptions(options);
		}
		
        [ScriptMember (inScriptName: "getScriptVersion", security: ScriptMemberSecurity.Permanent)]
		public string getScriptVersion(string fileName)
		{
			LogDebug("Host.getScriptVersion: " + fileName);
            return scripts[fileName].Version.ToString();
		}
		
		internal void UpdateFileName(FilePath fileName)
		{
			this.fileName = fileName;
		}
		
		internal void RemoveFile(FilePath fileName)
		{
			scripts.Remove(fileName.ToLower());
		}
		
		internal IEnumerable<string> GetFileNames()
		{
			return scripts.Keys.AsEnumerable();
		}
		
        [ScriptMember (inScriptName: "getScriptSnapshot", security: ScriptMemberSecurity.Permanent)]
		public IScriptSnapshotShim getScriptSnapshot(string fileName)
		{
			log("Host.getScriptSnapshot: " + fileName);
			Script script = scripts[fileName];
			return new ScriptSnapshotShim(logger, script);
		}
		
        [ScriptMember (inScriptName: "getScriptIsOpen", security: ScriptMemberSecurity.Permanent)]
		public bool getScriptIsOpen(string fileName)
		{
			log("Host.getScriptIsOpen: " + fileName);
			if (defaultLibScriptFileName.Equals(new FilePath(fileName))) {
				return false;
			}
			return true;
		}
		
        [ScriptMember (inScriptName: "getDiagnosticsObject", security: ScriptMemberSecurity.Permanent)]
		public ILanguageServicesDiagnostics getDiagnosticsObject()
		{
			log("Host.getDiagnosticsObject");
			return new LanguageServicesDiagnostics(logger);
		}
		
        [ScriptMember (inScriptName: "getScriptFileNames", security: ScriptMemberSecurity.Permanent)]
		public string[] getScriptFileNames()
		{
			log("Host.getScriptFileNames");
            return scripts.Keys.ToArray ();
		}
		
        [ScriptMember (inScriptName: "getScriptByteOrderMark", security: ScriptMemberSecurity.Permanent)]
		public ByteOrderMark getScriptByteOrderMark(string fileName)
		{
			log("Host.getScriptByteOrderMark: " + fileName);
			return ByteOrderMark.None;
		}
		
        [ScriptMember (inScriptName: "resolveRelativePath", security: ScriptMemberSecurity.Permanent)]
		public string resolveRelativePath(string path, string directory)
		{
			log("Host.resolveRelativePath: " + fileName);
			
			if (System.IO.Path.IsPathRooted(path) || String.IsNullOrEmpty(directory)) {
				return path;
			}
			return System.IO.Path.Combine(path, directory);
		}
		
        [ScriptMember (inScriptName: "fileExists", security: ScriptMemberSecurity.Permanent)]
		public bool fileExists(string path)
		{
			log("Host.fileExists: " + path);
			return File.Exists(path);
		}
		
        [ScriptMember (inScriptName: "directoryExists", security: ScriptMemberSecurity.Permanent)]
		public bool directoryExists(string path)
		{
			log("Host.directoryExists: " + path);
			return Directory.Exists(path);
		}
		
        [ScriptMember (inScriptName: "getParentDirectory", security: ScriptMemberSecurity.Permanent)]
		public string getParentDirectory(string path)
		{
			log("Host.getParentDirectory: " + path);
			return Path.GetDirectoryName(path);
		}
		
        [ScriptMember (inScriptName: "getLocalizedDiagnosticMessages", security: ScriptMemberSecurity.Permanent)]
		public string getLocalizedDiagnosticMessages()
		{
			log("Host.getLocalizedDiagnosticMessages");
			return null;
		}
		
        [ScriptMember (inScriptName: "ResolvePath", security: ScriptMemberSecurity.Permanent)]
		public string ResolvePath(string path)
		{
			log("ResolvePath: '" + path + "'");
			return path;
		}
        [ScriptMember (inScriptName: "updateCompilerResult", security: ScriptMemberSecurity.Permanent)]
		public void updateCompilerResult(string result)
		{
			log(result);
			CompilerResult = JsonConvert.DeserializeObject<CompilerResult>(result);
		}
		
		internal CompilerResult CompilerResult { get; private set; }
		
        [ScriptMember (inScriptName: "updateSemanticDiagnosticsResult", security: ScriptMemberSecurity.Permanent)]
		public void updateSemanticDiagnosticsResult(string json)
		{

			log(json);
            try{
                var result = JsonConvert.DeserializeObject<DiagnosticsResult>(json);
                var semantic = result.semantic.Where(x => x != null).ToArray();
                var syntactic = result.syntactic.Where(x => x != null).ToArray();
                SemanticDiagnosticsResult.result = new Diagnostic[semantic.Length + syntactic.Length]; 
                Array.Copy(semantic,SemanticDiagnosticsResult.result,semantic.Length);
                Array.Copy(syntactic, 0, SemanticDiagnosticsResult.result, semantic.Length, syntactic.Length);

            }catch(Exception ex){
                MonoLog (ex.ToString());
            }
//            SemanticDiagnosticsResult.result = res;
//            SemanticDiagnosticsResult = JsonConvert.DeserializeObject<SemanticDiagnosticsResult>(result);
		}

        internal SemanticDiagnosticsResult SemanticDiagnosticsResult { get; private set; }



        [ScriptMember (inScriptName: "updateSyntacticDiagnosticsResult", security: ScriptMemberSecurity.Permanent)]
        public void updateSyntacticDiagnosticsResult(string result)
        {
            log(result);
//            SyntaticDiagnosticsResult = JsonConvert.DeserializeObject<SyntaticDiagnosticsResult>(result);
        }

        internal SyntaticDiagnosticsResult SyntaticDiagnosticsResult { get; private set; }
	

        [ScriptMember (inScriptName: "getCancellationToken", security: ScriptMemberSecurity.Permanent)]
        public bool getCancellationToken (){
            return false;
        }

        [ScriptMember (inScriptName: "getCurrentDirectory", security: ScriptMemberSecurity.Permanent)]
        public string getCurrentDirectory()
        {
            log("Host.getCurrentDirectory");
            return "";
        }

        [ScriptMember (inScriptName: "getDefaultLibFilename", security: ScriptMemberSecurity.Permanent)]
        public string getDefaultLibFilename()
        {
            return defaultLibScriptFileName;
        }

        [ScriptMember (inScriptName: "MonoLog", security: ScriptMemberSecurity.Permanent)]
        public void MonoLog(string s)
        {
            System.Diagnostics.Debugger.Log(0, null,"MonoLog()-->" + s + "\n");
            logger.log(s);
        }
	}
}
