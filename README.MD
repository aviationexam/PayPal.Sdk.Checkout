﻿# REST API SDK for Dotnet V2

[![Build Status](https://github.com/aviationexam/PayPal.Sdk.Checkout/actions/workflows/build.yml/badge.svg?branch=develop)](https://github.com/aviationexam/PayPal.Sdk.Checkout/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/PayPal.Sdk.Checkout.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PayPal.Sdk.Checkout/)
[![MyGet](https://img.shields.io/myget/paypal-sdk-checkout/vpre/PayPal.Sdk.Checkout?label=MyGet)](https://www.myget.org/feed/paypal-sdk-checkout/package/nuget/PayPal.Sdk.Checkout)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Faviationexam%2Fpaypal-sdk-checkout%2Fshield%2FPayPal.Sdk.Checkout%2Flatest&label=PayPal.Sdk.Checkout)](https://f.feedz.io/aviationexam/paypal-sdk-checkout/packages/PayPal.Sdk.Checkout/latest/download)


![Home Image](homepage.jpg)

__Welcome to PayPal Dotnet SDK__. This repository contains PayPal's Dotnet SDK and samples for [v2/checkout/orders](https://developer.paypal.com/docs/api/orders/v2/) and [v2/payments](https://developer.paypal.com/docs/api/payments/v2/) APIs.

This is a part of the next major PayPal SDK. It includes a simplified interface to only provide simple model objects and blueprints for HTTP calls. This repo currently contains functionality for PayPal Checkout APIs which includes [Orders V2](https://developer.paypal.com/docs/api/orders/v2/) and [Payments V2](https://developer.paypal.com/docs/api/payments/v2/).

Please refer to the [PayPal Checkout Integration Guide](https://developer.paypal.com/docs/checkout/) for more information. Also refer to [Setup your SDK](https://developer.paypal.com/docs/checkout/reference/server-integration/setup-sdk/) for additional information about setting up the SDK's.

## Prerequisites

.NET Core 3.1, .NET 5.0, or .Net Standard 2.1

An environment which supports TLS 1.2 (see the TLS-update site for more information)

## Usage

### Setting up credentials
Get client ID and client secret by going to https://developer.paypal.com/developer/applications and generating a REST API app. Get <b>Client ID</b> and <b>Secret</b> from there.

## Examples

## Configure library

```c#
void Configure(IServiceCollection serviceCollection)
{
    serviceCollection.AddPayPalCheckout(c => configuration.GetSection("MyAppSettingSection").Bind(c));
}
```

### Creating an Order
This will create an order and print order id for the created order

```c#
public async static Task<Order> CreateOrder(IPayPalHttpClient payPalHttpClient, AccessToken accessToken)
{
    // Construct a request object and set desired parameters
    // Here, OrdersCreateRequest() creates a POST request to /v2/checkout/orders
    var order = new OrderRequest
    {
        CheckoutPaymentIntent = EOrderIntent.Capture,
        PurchaseUnits = new PurchaseUnitRequest[]
        {
            new()
            {
                ReferenceId = "test_ref_id1",
                AmountWithBreakdown = new AmountWithBreakdown
                {
                    CurrencyCode = "USD",
                    Value = "100.00"
                }
            }
        },
        ApplicationContext = new ApplicationContext
        {
            ReturnUrl = "https://www.example.com",
            CancelUrl = "https://www.example.com"
        },
    };

    var response = await payPalHttpClient.CreateOrderRawAsync(accessToken, request =>
    {
        request.SetPreferReturn(EPreferReturn.Representation);
        request.SetRequestBody(order);
    });

    var statusCode = response.ResponseStatusCode;
    var result = response.ResponseBody;
    Console.WriteLine("Status: {0}", result.Status);
    Console.WriteLine("Order Id: {0}", result.Id);
    Console.WriteLine("Intent: {0}", result.Intent);
    Console.WriteLine("Links:");
    foreach (LinkDescription link in result.Links)
    {
         Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
    }
    return response.ResponseBody;
}
```

### Capturing an Order
Before capturing an order, order should be approved by the buyer using the approve link in create order response
```c#
public async static Task<Order> CaptureOrder(this IPayPalHttpClient payPalHttpClient, AccessToken accessToken, string orderId)
{
    var response = await payPalHttpClient.CaptureOrderAsync(accessToken, orderId,
        request => {
            request.SetRequestBody(new OrderActionRequest());
        }
    );

    var statusCode = response.ResponseStatusCode;
    var result = response.ResponseBody;
    Console.WriteLine("Status: {0}", result.Status);
    Console.WriteLine("Capture Id: {0}", result.Id);
    return response;
}
```
## Running tests

To run integration tests using your client id and secret, run the `test`
```shell
PAYPAL_ClientId=YOUR_SANDBOX_CLIENT_ID PAYPAL_ClientSecret=YOUR_SANDBOX_CLIENT_SECRET dotnet test
```

You may use the client id and secret above for demonstration purposes.


## Samples

You can start off by trying out [creating and capturing an order](/samples/PayPal.Sdk.Checkout.Samples/CaptureIntentExamples/RunAllCaptureIntentFlow.cs).

To try out different samples for both create and authorize intent head to [this link](/samples/PayPal.Sdk.Checkout.Samples).

Note: Update the `tests/PayPal.Sdk.Checkout.Test/user.appsettings.json5` or the `sample/PayPal.Sdk.Checkout.Samples/user.appsettings.json5` with your sandbox client credentials.


## License
Code released under [SDK LICENSE](LICENSE)
