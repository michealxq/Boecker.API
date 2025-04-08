
using MediatR;

namespace Boecker.Application.Auth.Register.Command;

public record RegisterCommand(string Email, string Password, string Role) : IRequest<string>;
