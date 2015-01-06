var items = ls.getCompletionEntryDetails(host.fileName, host.position, host.completionEntry);
host.updateCompletionEntryDetailsAtCurrentPosition(JSON.stringify({result: items}));