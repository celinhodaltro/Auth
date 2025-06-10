using MediatR;

namespace Auth.Application.Commands;

public class LoginCommand : IRequest<bool>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

