using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayPal.Sdk.Checkout.Payments;

/// <summary>
/// The breakdown of the refund.
/// </summary>
public class PaymentMerchantPayableBreakdown
{
    /// <summary>
    /// The currency and amount for a financial transaction, such as a balance or payment due.
    /// </summary>
    [JsonPropertyName("gross_amount")]
    public PaymentMoney GrossAmount { get; set; } = null!;

    /// <summary>
    /// The currency and amount for a financial transaction, such as a balance or payment due.
    /// </summary>
    [JsonPropertyName("net_amount")]
    public PaymentMoney NetAmount { get; set; } = null!;

    /// <summary>
    /// An array of breakdown values for the net amount. Returned when the currency of the refund is different from the currency of the PayPal account where the merchant holds their funds.
    /// </summary>
    [JsonPropertyName("net_amount_breakdown")]
    public ICollection<PaymentNetAmountBreakdownItem> NetAmountBreakdown { get; set; } = null!;

    /// <summary>
    /// The currency and amount for a financial transaction, such as a balance or payment due.
    /// </summary>
    [JsonPropertyName("paypal_fee")]
    public PaymentMoney PaypalFee { get; set; } = null!;

    /// <summary>
    /// An array of platform or partner fees, commissions, or brokerage fees for the refund.
    /// </summary>
    [JsonPropertyName("platform_fees")]
    public ICollection<PlatformFee> PlatformFees { get; set; } = null!;

    /// <summary>
    /// The currency and amount for a financial transaction, such as a balance or payment due.
    /// </summary>
    [JsonPropertyName("total_refunded_amount")]
    public PaymentMoney TotalRefundedAmount { get; set; } = null!;
}
