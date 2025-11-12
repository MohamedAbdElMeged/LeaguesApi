using FluentValidation;
using LeaguesApi.Data;
using LeaguesApi.Dtos.Responses;
using LeaguesApi.Dtos.Requests;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Validators;

public class CreateNewSubscriberValidator : AbstractValidator<CreateNewSubscriberRequest>
{
    public readonly ApplicationDbContext _context;

    public CreateNewSubscriberValidator(ApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.Name)
            .MustAsync(async (name, _) =>
                !await _context.Subscribers.AnyAsync(s => s.Name == name))
            .WithMessage("Name is already taken");
    }
}