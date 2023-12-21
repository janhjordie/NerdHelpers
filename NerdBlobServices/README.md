Contains services for Azure Blobs

Required in AppSettings.json or on Azure WebApp settings

``` json 
{
    "NerdAzureBlob" : {
        "ConnectionString" : "xxx",
        "ContainerName" : "xxx"
    }
}
```

ServiceCollection methods

``` c#
public static IServiceCollection AddNerdAzureBlobServices(this IServiceCollection collection, IConfiguration configuration)
```