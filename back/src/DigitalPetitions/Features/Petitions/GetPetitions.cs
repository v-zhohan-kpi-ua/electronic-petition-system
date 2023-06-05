using AutoMapper;
using Azure;
using DigitalPetitions.Common.Models;
using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Features.Petitions.Dto;
using DigitalPetitions.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Text.Json.Serialization;

namespace DigitalPetitions.Features.Petitions
{
    public class GetPetitionsQuery : IRequest<PaginationResponse<PetitionDto>>
    {

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public PetitionStatus[] Status { get; set; } = new PetitionStatus[] { PetitionStatus.Signing };

        public GetPetitionsOrder Order { get; set; } = GetPetitionsOrder.Id;

        public string Search { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string Role { get; set; } = string.Empty;
    }

    public class GetPetitionsValidator : AbstractValidator<GetPetitionsQuery>
    {
        public GetPetitionsValidator()
        {
            RuleFor(v => v.Page)
                .GreaterThanOrEqualTo(1);

            RuleFor(v => v.PageSize)
                .GreaterThanOrEqualTo(0);
        }
    }

    internal sealed class GetPetitionsHandler : IRequestHandler<GetPetitionsQuery, PaginationResponse<PetitionDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPetitionsHandler(ApplicationDbContext context, IMapper mapper) 
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<PetitionDto>> Handle(GetPetitionsQuery request, CancellationToken cancellationToken)
        {
            int itemsToSkip = (request.Page - 1) * request.PageSize;

            IQueryable<Petition> query = _dbContext.Petitions
                .Include(p => p.Signs)
                .Include(p => p.Answer)
                .Include(p => p.Creator)
                .Include(p => p.ModerationResult);

            query = query
                .Where(p => request.Status.Contains(p.Status));

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string[] queryWords = request.Search.Split(" ");

                query = query
                    .Search(p => p.Title, p => p.Body)
                    .Containing(queryWords);
            }

            switch (request.Order)
            {
                case GetPetitionsOrder.Id:
                    query = query
                       .OrderBy(p => p.Id);
                    break;
                case GetPetitionsOrder.Popularity:
                    query = query
                        .OrderByDescending(p => p.Signs.Count);
                    break;
                case GetPetitionsOrder.OldStatus:
                    query = query
                        .OrderBy(p => p.StatusChangedAt);
                    break;
                default:
                    goto case GetPetitionsOrder.Id;
            }

            var totalItems = await query.CountAsync();

            query = query
                .Skip(itemsToSkip)
                .Take(request.PageSize);

            var petitions = await query.ToListAsync();

            var mapped = _mapper.Map<List<PetitionDto>>(
                    petitions,
                    options => options.Items["UserRole"] = request.Role);

            return new PaginationResponse<PetitionDto>(mapped, totalItems, request.Page, request.PageSize);
        }
    }

    public enum GetPetitionsOrder
    {
        Id = 0,
        Popularity = 1,
        OldStatus = 2
    }
}
