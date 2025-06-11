using Auth.Application.Commands;
using Auth.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, bool>
    {
        public AuthRepository _repo { get; set; }
        public LoginHandler() { }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await _repo.ValidateUserAsync(request.Username!, request.Password!)
                .ContinueWith(task =>
                {
                    if (task.Result)
                    {
                        return true;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Invalid credentials");
                    } 
                });

            return false;
        }
    }
}
