function conv (item){
var cache = [];
	var diagnostics = JSON.stringify(item, function(key, value) {
	    if (typeof value === 'object' && value !== null) {
	        if (cache.indexOf(value) !== -1) {
	            // Circular reference found, discard key
	            return;
	        }
	        // Store value in our collection
	        cache.push(value);
	    }
	    return value;
	});
	cache = null;
	return diagnostics;
}
var syntactic = ls.getSyntacticDiagnostics(host.fileName);
var semantic = ls.getSemanticDiagnostics(host.fileName);
var jsons = { 
	"syntactic": syntactic,
	"semantic": semantic,
}

host.updateSemanticDiagnosticsResult(conv(jsons));










