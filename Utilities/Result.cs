using System.Security.AccessControl;

namespace Adaca_Challenge.Utilities
{
    public record Result
    {
        public string Decision { get; set; }
        public ValidationResult[] ValidationResults { get; set; }
    }

    public class ResultDecision
    {
        public const string Qualified = "Qualified";
        public const string Unqualified = "Unqualified";
        public const string Unknown = "Unknown";

    }
}
