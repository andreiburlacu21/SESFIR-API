﻿using Dapper.Contrib.Extensions;

namespace SESFIR.DataAccess.Data.Domains
{
    [Table("table_Reviews")]
    public sealed class Review
    {
        [ExplicitKey]
        public int ReviewId { get; set; }
        public int AccountId { get; set; }
        public int LocationId { get; set; }
        public int Grade { get; set; }
        public string? Description { get; set; }
        public string? Date { get; set; }
    }
}
