global using System.Net;
global using System.Text.Json;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Metadata;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
global using Microsoft.AspNetCore.Mvc;
//global using Microsoft.AspNetCore.Mvc.Filters;
global using TogetherAPI.Data;
global using TogetherAPI.Models.Interfaces;
global using TogetherAPI.Models.Repositories;
global using TogetherAPI.Models.Entities;
global using TogetherAPI.Models.Validations;
global using TogetherAPI.Filters.ActionFilters;
global using TogetherAPI.Filters.ExceptionFilters;


namespace TogetherAPI;
