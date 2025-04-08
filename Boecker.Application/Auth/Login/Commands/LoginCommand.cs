
using MediatR;

namespace Boecker.Application.Auth.Login.Commands;

public record LoginCommand(string Email, string Password) : IRequest<string>;
