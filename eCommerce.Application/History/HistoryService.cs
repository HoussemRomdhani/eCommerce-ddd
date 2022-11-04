using AutoMapper;
using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;

namespace eCommerce.Application.History
{
    public class HistoryService : IHistoryService
    {
        IDomainEventRepository domainEventRepository;
        private readonly IMapper _mapper;

        public HistoryService(IDomainEventRepository domainEventRepository, IMapper mapper)
        {
            this.domainEventRepository = domainEventRepository ?? throw new ArgumentNullException(nameof(domainEventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public HistoryDto GetHistory()
        {
            IEnumerable<DomainEventRecord> events = domainEventRepository.FindAll();

            HistoryDto history = new HistoryDto
            {
                Events = _mapper.Map<IEnumerable<DomainEventRecord>, List<EventDto>>(events)
            };

            return history;
        }
    }
}
