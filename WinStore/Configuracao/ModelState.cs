﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStore.Configuracao
{
    public static class ModelState
    {
        public static List<string> ErrorMessages = new List<string>();

        public static bool IsValid<T>(T model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, validationContext, results, true))
            {
                return true;
            }
            else
            {
                ErrorMessages = results.Select(x => x.ErrorMessage).ToList();
                return false;
            }
        }
    }
}
