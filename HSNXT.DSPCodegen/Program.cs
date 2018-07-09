using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DSharpPlus;

namespace HSNXT.DSPCodegen
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");
            var methods = typeof(DiscordClient).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var properties = typeof(DiscordClient).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var events = typeof(DiscordClient).GetEvents(BindingFlags.Public | BindingFlags.Instance);
            
            var lines = new List<string>();
            var tokens = new List<string>();
            foreach (var m in methods)
            {
                tokens.Clear();
                
                if (ProcessMethod(m, tokens)) continue;

                lines.Add(string.Join(' ', tokens));                
                Console.WriteLine(string.Join(' ', tokens));
            }

            foreach (var p in properties)
            {
                tokens.Clear();
                
                ProcessProperty(tokens, p);

                tokens.Add("}");
                
                lines.Add(string.Join(' ', tokens));                
                Console.WriteLine(string.Join(' ', tokens));
            }

            foreach (var e in events)
            {
                tokens.Clear();
                
                tokens.Add("public event");
                
                AddTypeAndGeneric(tokens, e.EventHandlerType);
                
                tokens.Add(e.Name);
                
                tokens.Add("{ add => _client.");
                tokens.Add(e.Name);
                tokens.Add("+= value;");
                
                tokens.Add("remove => _client.");
                tokens.Add(e.Name);
                tokens.Add("-= value; }");
                
                lines.Add(string.Join(' ', tokens));                
                Console.WriteLine(string.Join(' ', tokens));
            }
        }

        private static void ProcessProperty(ICollection<string> tokens, PropertyInfo p)
        {
            tokens.Add("public");

            AddTypeAndGeneric(tokens, p.PropertyType);

            tokens.Add(p.Name);

            tokens.Add("{");

            if (p.CanRead && (p.GetGetMethod()?.IsPublic ?? false))
            {
                tokens.Add("get => _client.");
                tokens.Add(p.Name);
                tokens.Add(";");
            }

            if (p.CanWrite && (p.GetSetMethod()?.IsPublic ?? false))
            {
                tokens.Add("set => _client.");
                tokens.Add(p.Name);
                tokens.Add("= value;");
            }
        }

        private static bool ProcessMethod(MethodInfo m, ICollection<string> tokens)
        {
            if (m.Name.StartsWith("get_")) return true;
            if (m.Name.StartsWith("set_")) return true;
            if (m.Name.StartsWith("add_")) return true;
            if (m.Name.StartsWith("remove_")) return true;
            if (m.DeclaringType == typeof(object) && !m.IsVirtual) return true;

            tokens.Add("public");

            if (m.GetBaseDefinition() != m && m.GetBaseDefinition().DeclaringType != typeof(BaseDiscordClient))
            {
                tokens.Add("override");
            }

            if (m.Attributes.HasFlag(MethodAttributes.Final))
            {
                tokens.Add("sealed");
            }

            AddTypeAndGeneric(tokens, m.ReturnType);

            tokens.Add(m.Name);

            var methodGenerics = m.GetGenericArguments(); // TODO add co/contravariance
            AddMethodGenerics(methodGenerics, tokens);

            tokens.Add("(");

            var first = true;
            foreach (var p in m.GetParameters())
            {
                if (!first)
                {
                    tokens.Add(",");
                }

                first = false;

                if (p.IsOut)
                {
                    tokens.Add("out");
                }

                if (p.CustomAttributes.Any(e => e.AttributeType == typeof(ParamArrayAttribute)))
                {
                    tokens.Add("params");
                }

                AddTypeAndGeneric(tokens, p.ParameterType);

                tokens.Add(p.Name);

                if (p.HasDefaultValue)
                {
                    tokens.Add("=");
                    tokens.Add(p.DefaultValue?.ToString() ?? (p.ParameterType.IsValueType ? "default" : "null"));
                }
            }

            tokens.Add(")");

            if (methodGenerics.Length != 0)
            {
                foreach (var g in methodGenerics)
                {
                    var constraints = g.GetGenericParameterConstraints();

                    if (constraints.Length == 0) continue;

                    first = true;
                    foreach (var constraint in constraints)
                    {
                        tokens.Add(!first ? "," : ("where " + g.Name + " :"));
                        first = false;

                        AddTypeAndGeneric(tokens, constraint);
                    }

                    var sConstraints = g.GenericParameterAttributes &
                                       GenericParameterAttributes.SpecialConstraintMask;

                    // ReSharper disable once InvertIf
                    if (sConstraints == GenericParameterAttributes.None)
                    {
                        //No special constraints.
                    }
                    else
                    {
                        if (GenericParameterAttributes.None != (sConstraints &
                                                                GenericParameterAttributes.DefaultConstructorConstraint))
                        {
                            tokens.Add(!first ? "," : ("where " + g.Name + " :"));
                            first = false;

                            tokens.Add("struct");
                        }

                        if (GenericParameterAttributes.None != (sConstraints &
                                                                GenericParameterAttributes.ReferenceTypeConstraint))
                        {
                            tokens.Add(!first ? "," : ("where " + g.Name + " :"));
                            first = false;

                            tokens.Add("class");
                        }

                        if (GenericParameterAttributes.None != (sConstraints &
                                                                GenericParameterAttributes.NotNullableValueTypeConstraint))
                        {
                            tokens.Add(!first ? "," : ("where " + g.Name + " :"));
                            first = false;

                            tokens.Add("new()");
                        }
                    }
                }
            }

            tokens.Add("=> _client.");

            tokens.Add(m.Name);

            AddMethodGenerics(methodGenerics, tokens);

            tokens.Add("(");

            first = true;
            foreach (var p in m.GetParameters())
            {
                if (!first)
                {
                    tokens.Add(",");
                }

                first = false;

                tokens.Add(p.Name);
            }

            tokens.Add(");");
            return false;
        }

        private static void AddMethodGenerics(IReadOnlyCollection<Type> methodGenerics, ICollection<string> tokens)
        {
            if (methodGenerics.Count == 0) return;
            
            tokens.Add("<");
            foreach (var g in methodGenerics)
            {
                AddTypeAndGeneric(tokens, g);
            }

            tokens.Add(">");
        }

        private static void AddGenericTokens(ICollection<string> tokens, Type t)
        {
            if (t.GenericTypeArguments.Length == 0) return;

            var first = true;
            tokens.Add("<");
            foreach (var g in t.GenericTypeArguments)
            {
                if (!first)
                {
                    tokens.Add(",");
                }
                first = false;
                
                AddTypeAndGeneric(tokens, g);
            }

            tokens.Add(">");
        }

        private static void AddTypeAndGeneric(ICollection<string> tokens, Type g)
        {
            tokens.Add(GName(g));
            AddGenericTokens(tokens, g);
        }

        private static string GName(MemberInfo g)
        {
            if (g.Name == "Void") return "void";
            
            var idx = g.Name.IndexOf('`');
            return idx != -1 
                ? g.Name.Substring(0, idx) 
                : g.Name;
        }
    }
}