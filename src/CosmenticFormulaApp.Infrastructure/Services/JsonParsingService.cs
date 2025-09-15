using CosmenticFormulaApp.Application.Common.Interfaces;
using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Input;
using CosmenticFormulaApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Infrastructure.Services
{
    public class JsonParsingService : IJsonParsingService
    {
        private readonly ILogger<JsonParsingService> _logger;

        public JsonParsingService(ILogger<JsonParsingService> logger)
        {
            _logger = logger;
        }

        public async Task<Result<FormulaDto>> ParseFormulaAsync(string jsonContent)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jsonContent))
                    return Result<FormulaDto>.Failure("JSON content cannot be empty");

                var validationResult = ValidateJsonStructure(jsonContent);
                if (!validationResult.IsSuccess)
                    return Result<FormulaDto>.Failure(validationResult.Message);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var formulaDto = JsonSerializer.Deserialize<FormulaDto>(jsonContent, options);

                if (formulaDto == null)
                    return Result<FormulaDto>.Failure("Failed to deserialize JSON content");

                var businessValidation = ValidateFormulaDto(formulaDto);
                if (!businessValidation.IsSuccess)
                    return Result<FormulaDto>.Failure(businessValidation.Errors);

                return Result<FormulaDto>.Success(formulaDto);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error");
                return Result<FormulaDto>.Failure($"Invalid JSON format: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error parsing JSON");
                return Result<FormulaDto>.Failure("An unexpected error occurred while parsing the file");
            }
        }

        public async Task<Result<List<FormulaDto>>> ParseMultipleFormulasAsync(List<string> jsonContents)
        {
            var formulas = new List<FormulaDto>();
            var errors = new List<string>();

            foreach (var jsonContent in jsonContents)
            {
                var result = await ParseFormulaAsync(jsonContent);
                if (result.IsSuccess)
                {
                    formulas.Add(result.Data);
                }
                else
                {
                    errors.Add(result.Error);
                }
            }

            return errors.Any()
                ? Result<List<FormulaDto>>.Failure(errors)
                : Result<List<FormulaDto>>.Success(formulas);
        }

        public ValidationResult ValidateJsonStructure(string jsonContent)
        {
            try
            {
                var document = JsonDocument.Parse(jsonContent);
                var root = document.RootElement;

                var errors = new List<string>();

                if (!root.TryGetProperty("name", out _))
                    errors.Add("Missing required field: name");

                if (!root.TryGetProperty("weight", out _))
                    errors.Add("Missing required field: weight");

                if (!root.TryGetProperty("rawMaterials", out var rawMaterialsElement) || rawMaterialsElement.ValueKind != JsonValueKind.Array)
                    errors.Add("Missing or invalid field: rawMaterials (must be an array)");

                return errors.Any()
                    ? ValidationResult.Failure(errors)
                    : ValidationResult.Success();
            }
            catch (JsonException)
            {
                return ValidationResult.Failure("Invalid JSON format");
            }
        }

        private ValidationResult ValidateFormulaDto(FormulaDto formulaDto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(formulaDto.Name))
                errors.Add("Formula name cannot be empty");

            if (formulaDto.Weight <= 0)
                errors.Add("Formula weight must be positive");

            if (!formulaDto.RawMaterials.Any())
                errors.Add("Formula must contain at least one raw material");

            foreach (var rawMaterial in formulaDto.RawMaterials)
            {
                if (string.IsNullOrWhiteSpace(rawMaterial.Name))
                    errors.Add($"Raw material name cannot be empty");

                if (rawMaterial.Percentage < 0 || rawMaterial.Percentage > 100)
                    errors.Add($"Raw material '{rawMaterial.Name}' percentage must be between 0 and 100");

                if (rawMaterial.Price.Amount <= 0)
                    errors.Add($"Raw material '{rawMaterial.Name}' price must be positive");
            }

            var totalPercentage = formulaDto.RawMaterials.Sum(rm => rm.Percentage);
            if (totalPercentage < 95 || totalPercentage > 105)
                errors.Add($"Total raw material percentages ({totalPercentage:F1}%) should be approximately 100% (95-105% tolerance)");

            return errors.Any()
                ? ValidationResult.Failure(errors)
                : ValidationResult.Success();
        }
    }
}
