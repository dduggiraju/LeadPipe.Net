﻿// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) Lead Pipe Software. All rights reserved.
// Licensed under the MIT License. Please see the LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------------------------

using LeadPipe.Net.Commands;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeadPipe.Net.Tests.CommandTests
{
    /// <summary>
    /// A command that writes to the debug console for unit testing.
    /// </summary>
    public class DebugWriteWithValidationCommand : ICommand<UnitType>, IValidatableObject
    {
        /// <summary>
        /// Gets or sets the text to write.
        /// </summary>
        /// <value>The text to write.</value>
        public string TextToWrite { get; set; }

        /// <summary>
        /// Validates the specified validation context.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation results.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return new ValidationResult("You must supply a value for TextToWrite!");
        }
    }
}