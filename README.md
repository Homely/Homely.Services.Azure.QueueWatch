<div>
    <p align="center">
    <img src="https://imgur.com/9E8hN79.png" alt="Homely - Homely.Services.Azure.QueueWatch" />
    </p>
</div>

# Homely.Services.Azure.QueueWatch

## Why
Azure Monitor does not currently allow alerting on queue size. See [here](https://docs.microsoft.com/en-us/answers/questions/343880/alerts-on-azure-storage-queue.html) for more info.

## How
This app runs periodically, polling the queues of your choice and notifying you (currently supported: webhook) when the queue size is over a particular threshold.

## Getting started
### Azure Function
- Setup a new Azure Function App
- Ensure the value of `FUNCTIONS_EXTENSION_VERSION` is set to `~3` in the function configuration

### Configuration
- Update the `appSettings.Development.json` and `appSettings.Production.json` with the queues you would like to watch
- Keep secret values out of the config, and in user secrets (local) or environment variables / azure configuration (production)

### Deployment	
Normally.. 

![alt text](https://damianbrady.com.au/content/images/2018/01/friends-sticker.png)

...

But..in this case since it'll be deployed on your environments, we'll let you deploy it the way you want :blush:

## Contributing

Discussions and pull requests are encouraged :) Please ask all general questions in this repo or pick a specialized repo for specific, targetted issues. We also have a [contributing](https://github.com/Homely/Homely/blob/master/CONTRIBUTING.md) document which goes into detail about how to do this.

## Code of Conduct
Yep, we also have a [code of conduct](https://github.com/Homely/Homely/blob/master/CODE_OF_CONDUCT.md) which applies to all repositories in the (GitHub) Homely organisation.

## Feedback
Yep, refer to the [contributing page](https://github.com/Homely/Homely/blob/master/CONTRIBUTING.md) about how best to give feedback - either good or needs-improvement :)