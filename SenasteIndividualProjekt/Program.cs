using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers(); // Map controllers to handle incoming HTTP requests

app.Run();

// Configure services
var services = app.Services;
services.AddControllers(); // Add MVC services

app.Run();

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
        [HttpGet("encrypt")]
        public IActionResult Encrypt(string text, int shift)
        {
            if (string.IsNullOrEmpty(text))
                return BadRequest("Text is required.");

            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char encryptedChar = (char)(c + shift);
                    if (char.IsUpper(c) && encryptedChar > 'Z' || char.IsLower(c) && encryptedChar > 'z')
                        encryptedChar = (char)(c - (26 - shift));
                    encryptedText.Append(encryptedChar);
                }
                else
                {
                    encryptedText.Append(c);
                }
            }

            return Ok(encryptedText.ToString());
        }

        [HttpGet("decrypt")]
        public IActionResult Decrypt(string encryptedText, int shift)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return BadRequest("Encrypted text is required.");

            StringBuilder decryptedText = new StringBuilder();

            foreach (char c in encryptedText)
            {
                if (char.IsLetter(c))
                {
                    char decryptedChar = (char)(c - shift);
                    if (char.IsUpper(c) && decryptedChar < 'A' || char.IsLower(c) && decryptedChar < 'a')
                        decryptedChar = (char)(c + (26 - shift));
                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    decryptedText.Append(c);
                }
            }

            return Ok(decryptedText.ToString());
        }
    }
}
