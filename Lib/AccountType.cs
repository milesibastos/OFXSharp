using System.ComponentModel;

namespace OfxSharpLib
{
    public enum AccountType
    {
        [Description("Bank Account")]
        Bank,
        [Description("Credit Card")]
        Cc,
        [Description("Accounts Payable")]
        Ap,
        [Description("Accounts Recievable")]
        Ar,
        Na,
    }
}