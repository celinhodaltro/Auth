using Auth.Application.Commands;
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
        public LoginHandler() { }

        public Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
