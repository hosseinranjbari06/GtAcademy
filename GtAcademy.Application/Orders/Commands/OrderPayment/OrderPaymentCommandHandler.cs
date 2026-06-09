using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Orders;
using GtAcademy.Domain.Wallets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Commands.OrderPayment
{
    public class OrderPaymentCommandHandler : IRequestHandler<OrderPaymentCommand, ErrorOr<Guid>>
    {
        private readonly IWalletService _walletService;

        private readonly IOrderService _orderService;

        private readonly IGenericService<Order> _genericOrderService;

        private readonly IGenericService<WalletIncome> _genericIncomeService;

        private readonly IGenericService<Wallet> _genericWalletService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IReferralService _referralService;

        private readonly IUserService _userService;

        public OrderPaymentCommandHandler(IWalletService walletService, IOrderService orderService, IGenericService<Order> genericOrderService, IUnitOfWork unitOfWork, IReferralService referralService, IUserService userService, IGenericService<WalletIncome> genericIncomeService, IGenericService<Wallet> genericWalletService)
        {
            _walletService = walletService;
            _orderService = orderService;
            _genericOrderService = genericOrderService;
            _unitOfWork = unitOfWork;
            _referralService = referralService;
            _userService = userService;
            _genericIncomeService = genericIncomeService;
            _genericWalletService = genericWalletService;
        }

        public async Task<ErrorOr<Guid>> Handle(OrderPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetUserCurrentOrderIncludeItems(request.UserId);
            if (order == null) return Error.NotFound();

            var walletId = await _walletService.GetWalletIdByUserId(request.UserId);
            if (walletId == null) return Error.NotFound();

            int walletBalance = await _walletService.GetWalletBalance(walletId.Value);

            if (order.TotalAmount > walletBalance) return Error.Validation("All", "موجودی کیف پول شما کافی نمیباشد");

            var wallet = await _genericWalletService.GetByIdAsync(walletId!);
            wallet!.WalletBalance -= order.TotalAmount;

            order.IsPaid = true;
            order.PaymentDate = DateTime.Now;

            _genericWalletService.Update(wallet);
            _genericOrderService.Update(order);

            var userReferrerId = await _referralService.GetUsersReferrerId(request.UserId);

            if (userReferrerId != null)
            {
                var referralWalletId = await _walletService.GetWalletIdByUserId((Guid)userReferrerId);
                float rewardPercent = await _referralService.GetRewardPercent();

                int incomeAmount = (int)Math.Floor((order.TotalAmount * rewardPercent) / 100);

                var referralWallet = await _genericWalletService.GetByIdAsync(referralWalletId!);
                referralWallet!.WalletBalance += incomeAmount;

                var referralReward = new WalletIncome()
                {
                    WalletId = (Guid)referralWalletId!,
                    IncomeDate = DateTime.Now,
                    Amount = incomeAmount,
                    Description = "پاداش انجام تراکنش توسط " + await _userService.GetUserNameById(request.UserId),
                    IsPaid = true,
                    IsReferralReward = true,
                    ReferredId = userReferrerId,
                    Wallet = referralWallet
                };

                await _genericIncomeService.AddAsync(referralReward);
                _genericWalletService.Update(referralWallet);
            }

            await _unitOfWork.CommitAsync();

            return order.OrderId;
        }
    }
}
