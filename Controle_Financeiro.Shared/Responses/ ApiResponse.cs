using Controle_Financeiro.Shared.Responses;

namespace Controle_Financeiro.Shared.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public T? Data { get; set; }


    public ApiResponse(
        bool success,
        string message,
        T? data = default)
    {
        Success = success;
        Message = message;
        Data = data;
    }


    public static ApiResponse<T> Ok(
        T data,
        string message = "Operação realizada com sucesso.")
    {
        return new ApiResponse<T>(
            true,
            message,
            data
        );
    }


    public static ApiResponse<T> Error(
        string message)
    {
        return new ApiResponse<T>(
            false,
            message
        );
    }
}