﻿namespace RealEase.API.Requests
{
    public class NewVisitRequest
    {
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

    }
}
