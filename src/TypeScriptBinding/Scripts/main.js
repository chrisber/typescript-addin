//var ls = new TypeScript.Services.TypeScriptServicesFactory().createLanguageServiceShim(host);//ls.refresh(true);
var ls = ts.createLanguageService(host, ts.createDocumentRegistry())
