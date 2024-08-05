// Global using directives

global using System.Text;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Modules.Auth.Api.Extensions;
global using Modules.Auth.Api.Middlewares;
global using Modules.Auth.Application.Extensions;
global using Modules.Auth.Application.Features.Auth.GetUserList;
global using Modules.Auth.Application.Features.Auth.Login;
global using Modules.Auth.Application.Features.Auth.Register;
global using Modules.Auth.Application.Services;
global using Modules.Auth.Application.Services.Jwt;
global using Modules.Auth.Application.Services.ValidatorServices;
global using Modules.Auth.Domain.Interfaces;
global using Modules.Auth.Infrastructure.Db;
global using Shard.Infrastructure;
global using Shared.Domain.Enums;
global using Shared.Domain.Resources;
global using Shared.DTOs.Features;
global using Shared.DTOs.Features.Auth;