var signature = ls.getSignatureHelpItems(host.fileName, host.position);
host.updateSignatureAtPosition(JSON.stringify({result: signature}));