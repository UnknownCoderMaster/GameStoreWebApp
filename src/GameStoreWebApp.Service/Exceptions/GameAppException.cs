using System;

namespace GameStoreWebApp.Service.Exceptions;

public class GameAppException : Exception
{
    public int Code { get; set; }
    public GameAppException(int code, string message) : base(message)
    {
        Code = code;
    }
}
