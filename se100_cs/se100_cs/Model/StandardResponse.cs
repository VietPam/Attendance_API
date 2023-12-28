namespace se100_cs.Model
{
    public class StandardResponse
    {
        public string code { get; set; } = "";
        public string message { get; set; } = "";
        public Object? data { get; set; } = new object();

        public StandardResponse(string code , Object? data, string message= null)
        {
            this.code = code;
            this.data = data;
            this.message = message ?? GetDefaultMessageStatusCode(code);
        }

        private string GetDefaultMessageStatusCode(string code)
        {
            return code switch
            {
                "MS001" => "Your account has been created successfully. Please verify your email!",
                "ER001" => "Bad Network Connection!",
                _ => "Undefined status code."
            };
        }
    }
}

