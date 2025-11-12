using FluentValidation;
using LeaguesApi.Data;
using LeaguesApi.Dtos.Requests;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Validators;

public class CreateNewLeagueValidator : AbstractValidator<CreateLeagueRequest>
{
    private readonly ApplicationDbContext _context;

    public CreateNewLeagueValidator(ApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.Name)
            .MustAsync(async (name, _) =>
                !await _context.Leagues.AnyAsync(l => l.Name.ToLower() == name.ToLower()))
            .WithMessage("Name is already taken");
    }
}