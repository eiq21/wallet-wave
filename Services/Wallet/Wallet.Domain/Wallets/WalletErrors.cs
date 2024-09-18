using Wallet.Domain.Abstractions;

namespace Wallet.Domain.Wallets;
public static class WalletErrors
{
       public static Error NotFound = new(
              "Wallet.NotFound",
              "The wallet with the specified identifier was not found");

       public static Error UserNotFound = new(
                  "Wallet.UserNotFound",
                  "The User with the specified identifier was not found");

       public static Error WalletSenderNotFound = new(
              "Wallet.NotFound", "Sender wallet not found.");

       public static Error WalletRecipientNotFound = new(
              "Wallet.NotFound", "Destination wallet not found.");
}
