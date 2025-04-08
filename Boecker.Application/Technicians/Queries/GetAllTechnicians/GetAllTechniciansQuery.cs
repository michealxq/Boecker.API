
using Boecker.Application.Technicians.Dtos;
using MediatR;

namespace Boecker.Application.Technicians.Queries.GetAllTechnicians;

public class GetAllTechniciansQuery : IRequest<List<TechnicianDto>> { }
