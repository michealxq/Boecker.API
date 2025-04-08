
using Boecker.Application.Users.Dtos;
using MediatR;

namespace Boecker.Application.Users.Queries;

public class GetCurrentUserQuery : IRequest<CurrentUserDto> { }
