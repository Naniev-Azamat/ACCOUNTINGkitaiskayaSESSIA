namespace kitaiskayaSESSIA.Services
{
    public class AuthOptions
    {
        public const string SectionName = "Auth";

        public string EmployeePassword { get; set; } = "1234";

        public string DirectorName { get; set; } = "директор";

        public string DirectorDisplayName { get; set; } = "Директор";

        public string DirectorPassword { get; set; } = "4321";
    }
}
