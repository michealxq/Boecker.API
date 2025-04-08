
using Boecker.Domain.Entities;

namespace Boecker.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
