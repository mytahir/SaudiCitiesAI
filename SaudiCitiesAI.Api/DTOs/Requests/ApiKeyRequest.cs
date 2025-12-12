using System.ComponentModel.DataAnnotations;

public class ApiKeyRequest
{
    [Required]
    public string ApiKey { get; set; }
}
