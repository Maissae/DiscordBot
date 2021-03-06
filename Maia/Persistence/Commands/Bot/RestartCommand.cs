﻿using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands.Bot
{
    class RestartCommand : BaseCommand, ICommand
    {
        public RestartCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IValidationHandler validationHandler, params string[] parameters) 
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new OwnerOnlyNoParametersValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
        }

        public async override Task ExecuteAsync()
        {
            if (CanExecute())
            {
                await _messageWriter.Send("Brb! Gonna bring new stuff!", Author, Channel);
                var dir = Environment.CurrentDirectory + @"/";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    Process.Start(dir + "Run.bat", "-n");
                else
                    Process.Start(dir + "Run.sh", "-n");
                Environment.Exit(0);
            }
            else
                await _messageWriter.Send("Invalid use of command or not authorized!", Author, Channel);
        }
    }
}
