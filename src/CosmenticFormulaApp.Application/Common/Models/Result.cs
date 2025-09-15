using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; init; }
        public string Error { get; init; } = string.Empty;
        public List<string> Errors { get; init; } = new();
        public static Result Success() => new() { IsSuccess = true };
        public static Result Failure(string error) => new() { IsSuccess = false, Error = error };
        public static Result Failure(List<string> errors) => new() { IsSuccess = false, Errors = errors };
    }
    public class Result<T> : Result
    {
        public T? Data { get; init; }
        public static Result<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public static new Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
        public static new Result<T> Failure(List<string> errors) => new() { IsSuccess = false, Errors = errors };
    }
}
