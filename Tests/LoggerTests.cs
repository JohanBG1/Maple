using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace Tests
{
    public abstract class LoggerTests
    {
        protected readonly ITestOutputHelper Output;

        public LoggerTests(ITestOutputHelper output)
        {
            this.Output = output;
        }
    }
}
