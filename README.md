# Homely.Services.Azure.QueueWatch

## Why
Azure Monitor does not currently allow alerting on queue size. See [here](https://docs.microsoft.com/en-us/answers/questions/343880/alerts-on-azure-storage-queue.html) for more info.

## How
This app runs periodically, polling the queues of your choice and notifying you when the queue size is over a particular threshold.

## Getting started
1. Setup a new Azure Function App
2. Ensure the value of `FUNCTIONS_EXTENSION_VERSION` is set to `~3` in the function configuration
3. Update the `appSettings.Development.json` and `appSettings.Production.json` with the queues you would like to watch (keeping secret values in your user secrets)
4. Override settings in the portal where necessary