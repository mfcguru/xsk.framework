using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using X.Api.Entities;
using X.Api.Source.Domain.BusinessRules;

namespace X.Api.Source.Domain.UsesCases.Command
{
    public class EditStateCommand : IRequest
    {
        public Guid Id { get; }
        public EditStateDto Dto { get; }

        public EditStateCommand(Guid id, EditStateDto dto)
        {
            Id = id;
            Dto = dto;
        }

        private class RequestHandler : IRequestHandler<EditStateCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(EditStateCommand request, CancellationToken cancellationToken)
            {
                var entity = context.States.Find(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException();
                }

                context.States.Update(mapper.Map(request.Dto, entity));
                
                await context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
