﻿namespace RealEase.Infrastructure.Exceptions
{
    public class PaymentException : Exception
    {
        public PaymentException(string message) : base(message) { }
    }
}
