using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Errors;
public class FileSaveError : Error
{
    public FileSaveError(string fileName, string message) : base($"Failed to save the file '{fileName}' with error: {message}.")
    {
    }
}