namespace Toothy.Application.DTOs.Auth
{
    public class RefreshTokenRequestDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
