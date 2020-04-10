## Application Insights

* Perfomance management and monitoring
* Diagnoses


Install SDK in application

Funnels

User Flows

Impact

Retention

Handle Transient faults , appropriate strategy for retry operation.  

CosmosDb DocumentClient class will automatically retry failed attempts. You can set maximum wait time.
```C#
DocumentClient client = new DocumentClient(new Uri(endpoint),authkey);
var options =client.ConnectionPolicy.RetryOptions;
options.MaxRetryAttemptsOnThrottledRequests = 5;

```