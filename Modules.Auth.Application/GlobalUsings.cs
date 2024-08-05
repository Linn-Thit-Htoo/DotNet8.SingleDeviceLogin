// Global using directives

global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using FluentValidation;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;
global using Modules.Auth.Application.Services.Jwt;
global using Modules.Auth.Application.Services.ValidatorServices;
global using Modules.Auth.Domain.Interfaces;
global using Modules.Auth.Infrastructure.Db;
global using Modules.Auth.Infrastructure.Mapper;
global using Shard.Infrastructure;
global using Shared.Domain.Enums;
global using Shared.DTOs.Features;
global using Shared.DTOs.Features.Auth;