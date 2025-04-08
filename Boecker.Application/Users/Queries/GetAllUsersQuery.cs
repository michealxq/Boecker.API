
using Boecker.Application.Users.Dtos;
using MediatR;

namespace Boecker.Application.Users.Queries;

public record GetAllUsersQuery() : IRequest<List<UserDto>>;
