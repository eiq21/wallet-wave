using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.CreateWallet;
using Wallet.Application.Features.Deposit;
using Wallet.Application.Features.GetBalance;
using Wallet.Application.Features.Transfer;

namespace Wallet.API.Controllers.Wallets
{
    [ApiController]
    [Authorize]
    [Route("api/wallets")]
    public class WalletController : ControllerBase
    {
        private readonly ISender _sender;
        public WalletController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand command,
        CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{walletId}/deposit")]
        public async Task<IActionResult> Deposit(Guid walletId, [FromBody] decimal amount)
        {
            var command = new DepositMoneyCommand(walletId, amount);
            var result = await _sender.Send(command);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Error);
        }

        [HttpPost("{walletId}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid walletId, [FromBody] decimal amount)
        {
            var command = new WithdrawMoneyCommand(walletId, amount);
            var result = await _sender.Send(command);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Error);
        }

        [HttpPost("{walletId}/transfer")]
        public async Task<IActionResult> Transfer(Guid walletId, [FromBody] TransferRequest request)
        {
            var command = new TransferMoneyCommand(walletId, request.RecipientWalletId, request.Amount);
            var result = await _sender.Send(command);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Error);
        }

        [HttpGet("{walletId}/balance")]
        public async Task<IActionResult> GetBalance(Guid walletId)
        {
            var query = new GetWalletBalanceQuery(walletId);
            var result = await _sender.Send(query);
            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}