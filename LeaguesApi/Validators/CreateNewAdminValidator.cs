using AutoMapper;
using FluentValidation;
using LeaguesApi.Data;
using LeaguesApi.Dtos.Responses;
using LeaguesApi.Dtos.Requests;
using LeaguesApi.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
        RuleFor(x => x.Email)
            .MustAsync(async (email, _)
                => EmailHelper.ValidateEmail(email)) 
            .WithMessage("Email is not valid");
            
    }
}