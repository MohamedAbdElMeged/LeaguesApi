using AutoMapper;
using FluentValidation;
using LeaguesApi.Data;
using LeaguesApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi.Validators;

public class CreateNewAdminValidator : AbstractValidator<CreateNewAdminRequest>
{
    private readonly ApplicationDbContext _context;

    public CreateNewAdminValidator(ApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.Email)
            .MustAsync(async (email, _) =>
                !await _context.Admins.AnyAsync(a => a.Email == email))
            .WithMessage("Email is already taken");
    }
}