﻿using System.Threading.Tasks;
using Meziantou.Analyzer.Rules;
using TestHelper;
using Xunit;

namespace Meziantou.Analyzer.Test.Rules;

public sealed class ReturnTaskFromResultInsteadOfReturningNullAnalyzerTests
{
    private static ProjectBuilder CreateProjectBuilder()
    {
        return new ProjectBuilder()
            .WithAnalyzer<ReturnTaskFromResultInsteadOfReturningNullAnalyzer>();
    }

    [Fact]
    public async Task Method()
    {
        const string SourceCode = @"using System.Threading.Tasks;
class Test
{
    Task A() { [||]return null; }
    Task B() => [||]null;
    Task C() { [||]return ((Test)null)?.A(); }
    async Task<object> Valid() { return null; }
}";
        await CreateProjectBuilder()
              .WithSourceCode(SourceCode)
              .ValidateAsync();
    }

    [Fact]
    public async Task LocalFunction()
    {
        const string SourceCode = @"using System.Threading.Tasks;
class Test
{
    void A()
    {
        Task<object> Valid1() { return Task.FromResult<object>(null); }
        async Task<object> Valid2() { return null; }
        Task A() { [||]return null; }
        Task<object> B() { [||]return null; }
        Task<object> C() => [||]null;
        object       D() => null;
    }
}";

        await CreateProjectBuilder()
              .WithSourceCode(SourceCode)
              .ValidateAsync();
    }

    [Fact]
    public async Task LambdaExpression()
    {
        const string SourceCode = @"using System.Threading.Tasks;
class Test
{
    void A()
    {
        System.Func<Task>         a = () => [||]null;
        System.Func<Task<object>> b = () => [||]null;
        System.Func<Task<object>> c = () => { [||]return null; };
        System.Func<Task>         valid1 = async () => { };
        System.Func<Task<object>> valid2 = async () => null;
        System.Func<object>       valid3 = () => null;
        System.Func<Task<object>> valid4 = async () => { return null; };
    }
}";

        await CreateProjectBuilder()
              .WithSourceCode(SourceCode)
              .ValidateAsync();
    }

    [Fact]
    public async Task AnonymousMethods()
    {
        const string SourceCode = @"using System.Threading.Tasks;
class Test
{
    void A()
    {
        System.Func<Task> a = delegate () { [||]return null; };
        System.Func<Task<object>> b = delegate () { [||]return null; };
        System.Func<Task> c = async delegate () { };
        System.Func<Task<object>> d = async delegate () { return null; };
        System.Func<object> e = delegate () { return null; };
        System.Action f = () => { };
    }
}";

        await CreateProjectBuilder()
              .WithSourceCode(SourceCode)
              .ValidateAsync();
    }

    [Fact]
    public async Task AsyncLambdaInTask()
    {
        const string SourceCode = @"using System.Threading.Tasks;
class Test
{
    Task A()
    {
        
        System.Func<Task<object>> valid4 = async () => { return null; };
        return Task.CompletedTask;
    }
}";

        await CreateProjectBuilder()
              .WithSourceCode(SourceCode)
              .ValidateAsync();
    }

}
