using System;

namespace OnlineShop.Core.Responses
{
    public record JwtModel(string Token, DateTime Expiration);
}