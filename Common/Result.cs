using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public AppError? Error { get; }

        private Result(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        private Result(AppError error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(AppError error) => new(error);

        public static implicit operator Result<T>(AppError error) => Failure(error);

    }
}