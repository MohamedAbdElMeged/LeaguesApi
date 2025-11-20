using FluentValidation;
using LeaguesApi.Data;
using LeaguesApi.Dtos.Requests;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Validators;

public class CreateNewSeasonValidator : AbstractValidator<CreateSeasonRequest>
{
    private readonly ApplicationDbContext _context;

    public CreateNewSeasonValidator(ApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x)
            .MustAsync(async (x, _) =>
                !await _context.Seasons.AnyAsync(s => s.Year == x.Year && s.LeagueId == x.LeagueId)
            ).WithMessage("Season Already Added");

        RuleFor(x => x.LeagueId)
            .MustAsync(async (leagueId, _) =>
                await _context.Leagues.AnyAsync(l => l.Id == leagueId))
            .WithMessage("League Not Found");
    }
}