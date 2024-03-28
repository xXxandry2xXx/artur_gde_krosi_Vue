﻿using System.Text.Json;

namespace artur_gde_krosi_Vue.Server.Dto
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
