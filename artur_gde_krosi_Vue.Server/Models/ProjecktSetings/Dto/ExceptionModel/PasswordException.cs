using Microsoft.AspNetCore.Identity;
using System;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings.Dto.ExceptionModel
{
    public class PasswordException : Exception
    {
        public IEnumerable<IdentityError> errors {  get; set; }
        public PasswordException() : base() { }
        public PasswordException(string message,
            IEnumerable<IdentityError> errors) : base(message)
        {
            this.errors = errors;
        }
        public PasswordException(string message, Exception innerException) : base(message, innerException) { }
    }
}
