using Finance.Application.Services;
using Finance.Domain;
using Finance.Infrastructure;
using Finance.Shared.ApplicationFiles;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Finance.Application.Commands.RigesterUser;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly IStorageService _storageService;

    public RegisterCommandHandler(UserManager<User> userManager,
        IStorageService storageService)
    {
        _userManager = userManager;
        _storageService = storageService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid().ToString();

        var storedFile = await _storageService.Upload(request.Image, true, "ProfileImages", userId);
        var fileDto = new FileDto
        {
            Url = string.Join(",", storedFile.Path)
        };

        var user = new User(userId,
            request.FirstName,
            request.LastName,
            request.Email,
            fileDto);

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new ApplicationException($"User creation failed: {errors}");
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
        if (!roleResult.Succeeded)
        {
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            throw new ApplicationException($"Role assignment failed: {errors}");
        }

        return user.Id;
    }
}
