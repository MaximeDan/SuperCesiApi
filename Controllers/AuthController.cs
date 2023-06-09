﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperCesiApi.Data;

namespace SuperCesiApi.Controllers;

/// <summary>
/// API controller for authentication-related operations.
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager instance for managing users.</param>
    /// <param name="signInManager">The sign-in manager instance for handling user sign-in.</param>
    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="model">The registration model.</param>
    /// <returns>Ok if registration is successful, BadRequest otherwise.</returns>
    [HttpPost("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email};

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Set the email confirmed status to true
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            // Registration successful
            return Ok();
        }

        // Registration failed
        return BadRequest(result.Errors);
    }

    /// <summary>
    /// Authenticate and log in a user.
    /// </summary>
    /// <param name="model">The login model.</param>
    /// <returns>Ok if login is successful, Unauthorized otherwise.</returns>
    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // Login successful
            return Ok();
        }

        // Login failed
        return Unauthorized();
    }

    /// <summary>
    /// Log out the current user.
    /// </summary>
    /// <returns>Ok if logout is successful.</returns>
    [HttpPost("logout")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        // Logout successful
        return Ok();
    }
}