using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress(ErrorMessage = "Email is not valid!")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; } = string.Empty;
}