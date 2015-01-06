var emitResult = ls.getEmitOutput(host.fileName);
host.updateCompilerResult(JSON.stringify({result: emitResult}));