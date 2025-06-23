﻿using MenShop_Assignment.Datas;
using MenShop_Assignment.DTOs;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.Momo;

namespace MenShop_Assignment.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<PaymentViewModel> AddPaymentToOrderAsync(string orderId, CreatePaymentDTO dto);
        //Task<(OrderResponseDto Order, MomoCreatePaymentResponseModel Payment)> CreateOrderAndPayAsync(CreateOrderDto dto);
        Task<PaymentViewModel> AddCodPaymentAsync(string orderId);

    }
}
