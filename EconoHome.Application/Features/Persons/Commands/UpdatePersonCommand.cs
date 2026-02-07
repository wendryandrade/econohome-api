using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoHome.Application.Features.Persons.Commands
{
    public record UpdatePersonCommand(Guid Id, string Name, int Age) : IRequest<Unit>;
}
