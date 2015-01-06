var definition = ls.getDefinitionAtPosition(host.fileName, host.position);
host.updateDefinitionAtPosition(JSON.stringify({result: definition}));