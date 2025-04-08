﻿
using MediatR;

namespace Boecker.Application.Technicians.Commands.CreateTechnician;

public class CreateTechnicianCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
}
