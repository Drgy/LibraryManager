// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using ThreadHelper = Microsoft.VisualStudio.Shell.ThreadHelper;

namespace Microsoft.Web.LibraryManager.Build
{
    internal class Logger : Contracts.ILogger
    {
        private Logger()
        {
        }

        public static Logger Instance { get; } = new Logger();
        public ICollection<string> Messages { get; } = new List<string>();
        public ICollection<string> Errors { get; } = new List<string>();

        public void Clear()
        {
            Messages.Clear();
            Errors.Clear();
        }

        public void Log(string message, Contracts.LogLevel level)
        {
            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                switch (level)
                {
                    case Contracts.LogLevel.Error:
                        Errors.Add(message);
                        break;
                    case Contracts.LogLevel.Operation:
                    case Contracts.LogLevel.Task:
                    case Contracts.LogLevel.Status:
                        Messages.Add(message);
                        break;
                }
            });
        }
    }
}
