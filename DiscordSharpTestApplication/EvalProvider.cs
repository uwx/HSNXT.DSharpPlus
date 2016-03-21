﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

public static class EvalProvider
{
    public static Func<T, TResult> CreateEvalMethod<T, TResult>(string code, string[] usingStatements = null, string[] assemblies = null)
    {
        Type returnType = typeof(TResult);
        Type inputType = typeof(T);

        var includeUsings = new HashSet<string>(new[] { "System" });
        includeUsings.Add(returnType.Namespace);
        includeUsings.Add(inputType.Namespace);
        if (usingStatements != null)
            foreach (var usingStatement in usingStatements)
                includeUsings.Add(usingStatement);

        using (CSharpCodeProvider compiler = 
            new CSharpCodeProvider())
        {
            var name = "F" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            var includeAssemblies = new HashSet<string>(new[] { "System.dll" });
            if (assemblies != null)
                foreach (var assembly in assemblies)
                    includeAssemblies.Add(assembly);

            var parameters = new CompilerParameters(includeAssemblies.ToArray())
            {
                GenerateInMemory = true,
                //CompilerOptions = "/langversion:6"
            };

            string source = string.Format(@"
{0}
namespace {1}
{{
	public static class EvalClass
	{{
        static DiscordClient discordClient;
		static bool SubwayTime = true;
		public static {2} Eval({3} arg)
		{{
            discordClient = arg;
			{4}
		}}
	}}
}}", GetUsing(includeUsings), name, returnType.Name, inputType.Name, code);

            var compilerResult = compiler.CompileAssemblyFromSource(parameters, source);
            errors = compilerResult.Errors;
            var compiledAssembly = compilerResult.CompiledAssembly;
            var type = compiledAssembly.GetType(string.Format("{0}.EvalClass", name));
            var method = type.GetMethod("Eval");
            return (Func<T, TResult>)Delegate.CreateDelegate(typeof(Func<T, TResult>), method);
        }
    }
    public static CompilerErrorCollection errors;
    private static string GetUsing(HashSet<string> usingStatements)
    {
        StringBuilder result = new StringBuilder();
        foreach (string usingStatement in usingStatements)
        {
            result.AppendLine(string.Format("using {0};", usingStatement));
        }
        return result.ToString();
    }
}
