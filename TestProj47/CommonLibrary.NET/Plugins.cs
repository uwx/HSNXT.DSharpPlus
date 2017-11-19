﻿using System;
using System.Collections.Generic;
using System.Linq;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Plugins;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Parsing
{    
    /// <summary>
    /// Stores all the combinators.
    /// </summary>
    public class RegisteredPlugins 
    {
        // e.g.
        // [ "monday"   ] = [ DayPlugin ]
        // [ "$IdToken" ] = [ LinqPlugin, ]
        private readonly IDictionary<string, List<ILangPlugin>> _sysStmtPlugins = new Dictionary<string, List<ILangPlugin>>();
        private readonly IDictionary<string, List<ILangPlugin>> _expPlugins     = new Dictionary<string, List<ILangPlugin>>();
        private readonly IDictionary<string, List<ILangPlugin>> _tokPlugins     = new Dictionary<string, List<ILangPlugin>>();
        private readonly IDictionary<string, List<ILangPlugin>> _lexPlugins     = new Dictionary<string, List<ILangPlugin>>();
        private readonly IDictionary<string, List<ILangPlugin>> _postfixPlugins = new Dictionary<string, List<ILangPlugin>>();
        private readonly IDictionary<string, List<ILangPlugin>> _stmtPlugins    = new Dictionary<string, List<ILangPlugin>>();
        private readonly IDictionary<string, List<ILangPlugin>> _expStmtPlugins = new Dictionary<string, List<ILangPlugin>>();
        private readonly IDictionary<string, ISetupPlugin>      _setupPlugins   = new Dictionary<string, ISetupPlugin>();
        

        private int _totalExpPlugins;
        private bool _hasLiteralCombinators;
        private bool _hasTokenPlugins;
        private bool _hasPostfixPlugins;

        private IExprPlugin _lastMatchedExp;
        private ITokenPlugin _lastMatchedTok;
        private ILexPlugin _lastMatchedLex;
        private IExprPlugin _lastMatchedSysStmt;
        private IExprPlugin _lastMatchedExtStmt;
        private readonly IDictionary<string, ILangPlugin> _sysMap;
        private readonly IDictionary<string, ILangPlugin> _extMap;
        private readonly IDictionary<string, object> _pluginSettings;
        private IList<IDisposable> _disposablePlugins;


        /// <summary>
        /// Initialize plugins.
        /// </summary>
        public RegisteredPlugins()
        {
            _sysMap = new Dictionary<string, ILangPlugin>();
            _extMap = new Dictionary<string, ILangPlugin>();
            _setupPlugins = new Dictionary<string, ISetupPlugin>();
            _pluginSettings = new Dictionary<string, object>();
        }


        /// <summary>
        /// Initialize
        /// </summary>
        public void Init()
        {
            // System plugins ( basic features - loops, if etc )
            _sysMap["Break"]           =  new BreakPlugin();
            _sysMap["Continue"]        =  new ContinuePlugin();
            _sysMap["For"]             =  new ForLoopPlugin();
            _sysMap["FuncDeclare"]     =  new FuncDeclarePlugin();
            _sysMap["If"]              =  new IfPlugin();
            _sysMap["New"]             =  new NewPlugin();
            _sysMap["Return"]          =  new ReturnPlugin();
            _sysMap["Throw"]           =  new ThrowPlugin();
            _sysMap["TryCatch"]        =  new TryCatchPlugin();
            _sysMap["TypeOf"]          =  new TypeOfPlugin();
            _sysMap["While"]           =  new WhilePlugin();
            _sysMap["Var"]             =  new VarPlugin();

            // Custom plugins - extended functionality.
            _extMap["Aggregate"]       =  new AggregatePlugin();
            _extMap["Alias"]           =  new AliasPlugin();
            _extMap["AndOr"]		   =  new AndOrPlugin();
            _extMap["Bool"]      	   =  new BoolPlugin();
            _extMap["Compare"]		   =  new ComparePlugin();
            _extMap["ConstCaps"]       =  new ConstCapsPlugin(); 
            _extMap["Date"]      	   =  new DatePlugin();
            _extMap["DateNumber"]      =  new DateNumberPlugin();
            _extMap["DateTimeCombiner"] = new DateTimeCombinerPlugin();
            _extMap["Day"]      	   =  new DayPlugin();
            _extMap["Def"]      	   =  new DefPlugin();
            _extMap["Enable"]      	   =  new EnablePlugin();
            _extMap["Email"]      	   =  new EmailPlugin();
            _extMap["Fail"]            =  new FailPlugin();
            _extMap["FileExt"]         =  new FileExtPlugin();
            _extMap["FluentFunc"]      =  new FluentFuncPlugin();
            _extMap["FluentMember"]    =  new FluentMemberPlugin();
            _extMap["FuncWildCard"]    =  new FuncWildCardPlugin();
            _extMap["HashComment"]     =  new HashCommentPlugin();
            _extMap["Holiday"]         =  new HolidayPlugin();
            _extMap["Linq"]      	   =  new LinqPlugin();
            _extMap["Log"]             =  new LogPlugin();
            _extMap["MachineInfo"]     =  new MachineInfoPlugin();
            _extMap["Marker"]      	   =  new MarkerPlugin();
            _extMap["MarkerLex"]       =  new MarkerLexPlugin();
            _extMap["Money"]      	   =  new MoneyPlugin();
            _extMap["Percent"]         =  new PercentPlugin();
            _extMap["Print"]      	   =  new PrintPlugin();
            _extMap["PrintExpression"] =  new PrintExpressionPlugin();
            _extMap["Records"]         =  new RecordsPlugin();
            _extMap["Repeat"]          =  new RepeatPlugin();
            _extMap["Round"]      	   =  new RoundPlugin();
            _extMap["Run"]      	   =  new RunPlugin();
            _extMap["Set"]      	   =  new SetPlugin();
            _extMap["Sort"]      	   =  new SortPlugin();
            _extMap["Step"]            =  new StepPlugin();
            _extMap["StringLiteral"]   =  new StringLiteralPlugin();
            _extMap["Suffix"]      	   =  new SuffixPlugin();
            _extMap["Swap"]      	   =  new SwapPlugin();
            _extMap["Time"]      	   =  new TimePlugin();
            _extMap["TypeOperations"]  =  new TypeOperationsPlugin();
            _extMap["Units"]      	   =  new UnitsPlugin();
            _extMap["Uri"]      	   =  new UriPlugin();
            _extMap["VariablePath"]    =  new VariablePathPlugin();
            _extMap["Version"]         =  new VersionPlugin();
            _extMap["Words"]           =  new WordsPlugin();
            _extMap["WordsInterpret"]  =  new WordsInterpretPlugin();
            /*
            var info = "";
            foreach(var pair in _extMap)
                if(pair.Value is IExprPlugin)
                {
                    var tokens = ((IExprPlugin)pair.Value).StartTokens;
                    info += pair.Value.GetType().Name + " - ";
                    foreach (var token in tokens)
                    {
                        //if (token.Contains("$"))
                        info += token + ", ";
                    }
                    info += Environment.NewLine;
                }
                else if (pair.Value is ILexPlugin)
                {
                    var tokens = ((ILexPlugin)pair.Value).Tokens;
                    info += pair.Value.GetType().Name + " - ";
                    foreach (var token in tokens)
                    {
                        //if (token.Contains("$"))
                        info += token + ", ";
                    }
                    info += Environment.NewLine;
                }
            Console.WriteLine(info);
            */
        }


        /// <summary>
        /// The total number of expression plugins.
        /// </summary>
        public int TotalExpressions => _totalExpPlugins;


        /// <summary>
        /// The total number of combinators.
        /// </summary>
        public int TotalLexical => _lexPlugins.Count;


        /// <summary>
        /// The total number of token based plugins.
        /// </summary>
        public int TotalTokens => _lexPlugins.Count;


        /// <summary>
        /// The total number of statement based plugins.
        /// </summary>
        public int TotalStmts => _stmtPlugins.Count;


        /// <summary>
        /// Whether there exist combinators that handle generic literls aside from IdTokens.
        /// </summary>
        public bool HasLiteralTokenPlugins => _hasLiteralCombinators;


        /// <summary>
        /// Whether there exist token based plugins.
        /// </summary>
        public bool HasTokenBasedPlugins => _hasTokenPlugins;


        /// <summary>
        /// Whether or not there are any postfix plugins.
        /// </summary>
        public bool HasPostfixPlugins => _hasPostfixPlugins;


        /// <summary>
        /// Last matched expression plugin.
        /// </summary>
        public IExprPlugin LastMatchedExpressionPlugin => _lastMatchedExp;


        /// <summary>
        /// Last matched token plugin.
        /// </summary>
        public ITokenPlugin LastMatchedTokenPlugin => _lastMatchedTok;


        /// <summary>
        /// Last matched lex plugin.
        /// </summary>
        public ILexPlugin LastMatchedLexPlugin => _lastMatchedLex;


        /// <summary>
        /// Last matched sys stmt plugin.
        /// </summary>
        public IExprPlugin LastMatchedSysStmtPlugin => _lastMatchedSysStmt;


        /// <summary>
        /// Last matched sys stmt plugin.
        /// </summary>
        public IExprPlugin LastMatchedExtStmtPlugin => _lastMatchedExtStmt;


        /// <summary>
        /// Register all plugins within the commonlibrary
        /// </summary>
        public void RegisterAll()
        {
            // NOTE: Not calling RegisterSystem and RegisterCustom here
            // because each will have to sort the plugins by their precedence.
            // There is no needed to sort twice when they are in a single list.
            var plugins = new List<ILangPlugin>();
            foreach (var pair in _sysMap) plugins.Add(pair.Value);
            foreach (var pair in _extMap) plugins.Add(pair.Value);
            Register(plugins.ToArray());
            Register(new FileIOPlugin());
        }


        /// <summary>
        /// Registers the system plugins.
        /// </summary>
        public void RegisterAllSystem()
        {
            RegisterExtensionsByNames(_sysMap.Keys, _sysMap);
        }


        /// <summary>
        /// Register all of the custom extensions
        /// </summary>
        public void RegisterAllCustom()
        {
            RegisterExtensionsByNames(_extMap.Keys, _extMap);
        }


        /// <summary>
        /// Register all of the custom extensions
        /// </summary>
        public void RegisterCustomByType(Type pluginType)
        {
            var name = pluginType.Name.Replace("Plugin", "");
            var plugin = _extMap.ContainsKey(name) 
                                 ? _extMap[name]
                                 : (ILangPlugin)CreatePluginInstance(pluginType);
            Register(plugin);            
        }


        /// <summary>
        /// Register a specific set of custom extensions
        /// </summary>
        /// <param name="pluginKeys"></param>
        public void RegisterCustomSubSet(ICollection<string> pluginKeys)
        {
            RegisterExtensionsByNames(pluginKeys, _extMap);
        }



        /// <summary>
        /// Registers a setup plugin.
        /// </summary>
        /// <param name="plugin"></param>
        public void Register(ISetupPlugin plugin)
        {
            _setupPlugins[plugin.Id] = plugin;
        }


        /// <summary>
        /// Register the list of plugins.
        /// </summary>
        /// <param name="plugins">The plugins to register</param>
        public void Register(ILangPlugin[] plugins)
        {
            var sorted = plugins.ToList();
            sorted = SortPlugins(sorted);
            foreach (var plugin in sorted)
                Register(plugin, false);
        }


        /// <summary>
        /// Registers a custom function callback.
        /// </summary>
        /// <param name="pluginToRegister">The function</param>
        /// <param name="sort">Whether or not to sort the plugin by precedence after adding it to the system.</param>
        public void Register(ILangPlugin pluginToRegister, bool sort = true)
        {
            if (pluginToRegister is IDisposable)
            {
                if (_disposablePlugins == null)
                    _disposablePlugins = new List<IDisposable>();
                _disposablePlugins.Add(pluginToRegister as IDisposable);
            }
            if (pluginToRegister is IExprPlugin) RegisterExprPlugin(pluginToRegister as IExprPlugin, sort);
            else if (pluginToRegister is ITokenPlugin) RegisterTokenPlugin(pluginToRegister as ITokenPlugin, sort);
            else if (pluginToRegister is ILexPlugin) RegisterLexPlugin(pluginToRegister as ILexPlugin, sort);
        }

        /*
        /// <summary>
        /// Register a system plugin.
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="sort">Whether or not to sort the plugin by precedence after adding it to the system.</param>
        public void RegisterStmtPlugin(IStmtPlugin plugin, bool sort)
        {
            var map = plugin.IsSystemLevel ? _sysStmtPlugins : _stmtPlugins;
            RegisterPlugin(map, plugin, plugin.StartTokens, sort);
        }
        */

        /// <summary>
        /// Register an expression plugin.
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="sort">Whether or not to sort the plugin by precedence after adding it to the system.</param>
        public void RegisterExprPlugin(IExprPlugin plugin, bool sort)
        {
            _totalExpPlugins++;

            var tokens = plugin.StartTokens;
            // Case 1: Postfix plugin - single start token
            if (tokens.Length == 1 && tokens[0] == "$Suffix")
            {
                AddPlugin(_postfixPlugins, plugin, "$Suffix", sort);
                _hasPostfixPlugins = true;
            }
            else
            {
                // Case 2: Expression plugin
                foreach (var token in tokens)
                {
                    AddPlugin(_expPlugins, plugin, token, sort);
                    if (plugin.IsSystemLevel)
                        AddPlugin(_sysStmtPlugins, plugin, token, sort);

                    else if(plugin.IsStatement)
                        AddPlugin(_expStmtPlugins, plugin, token, sort);

                    if (token == "$NumericLiteralToken")
                        _hasLiteralCombinators = true;
                }
            }
        }


        /// <summary>
        /// Register token plugin.
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="sort">Whether or not to sort the plugin by precedence after adding it to the system.</param>
        public void RegisterTokenPlugin(ITokenPlugin plugin, bool sort)
        {
            RegisterPlugin(_tokPlugins, plugin, plugin.Tokens, sort);
            _hasTokenPlugins = true;
        }


        /// <summary>
        /// Registers a lex plugin.
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="sort">Whether or not to sort the plugin by precedence after adding it to the system.</param>
        public void RegisterLexPlugin(ILexPlugin plugin, bool sort)
        {
            RegisterPlugin(_lexPlugins, plugin, plugin.Tokens, sort);
        }


        /// <summary>
        /// Iterate through all the combinators
        /// </summary>
        /// <param name="callback"></param>
        public void ForEach<T>(Action<T> callback) where T: class
        {
            if (typeof(T) == typeof(ITokenPlugin))
            {
                CallBack(_tokPlugins, callback);
            }
            else if (typeof(T) == typeof(ILexPlugin))
            {
                CallBack(_lexPlugins, callback);
            }
            else if (typeof(T) == typeof(IExprPlugin))
            {
                CallBack(_expPlugins, callback);
                CallBack(_expStmtPlugins, callback);
                CallBack(_postfixPlugins, callback);
                CallBack(_stmtPlugins, callback);
                CallBack(_sysStmtPlugins, callback);
            }
        }


        /// <summary>
        /// Executes all setup plugins.
        /// </summary>
        /// <param name="ctx"></param>
        public void ExecuteSetupPlugins(Context ctx)
        {
            if (_setupPlugins == null || _setupPlugins.Count == 0) return;

            foreach (var pair in _setupPlugins)
                pair.Value.Setup(ctx);
        }


        /// <summary>
        /// Disposes of all the plugins.
        /// </summary>
        public void Dispose()
        {
            if (_disposablePlugins == null || _disposablePlugins.Count == 0)
                return;

            foreach (var plugin in _disposablePlugins)
            {
                try { plugin.Dispose(); } catch (Exception) { }
            }
        }


        /// <summary>
        /// Get settigns for a plugin.
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        public T GetSettings<T>(string pluginId) where T: class
        {
            if (!_pluginSettings.ContainsKey(pluginId))
                return default;

            var o = _pluginSettings[pluginId];
            T settings = default;
            if (o != null)
                settings = o as T;
            return settings;
        }


        /// <summary>
        /// Set settigns for a plugin.
        /// </summary>
        /// <param name="pluginId"></param>
        /// <param name="settings">The settings object for the plugins.</param>
        /// <returns></returns>
        public void SetSettings(string pluginId, object settings)
        {
            _pluginSettings[pluginId] = settings;
        }


        /// <summary>
        /// Whether or not there is a expression plugin that can handle the token supplied.
        /// </summary>
        /// <param name="token">The token to check against combinators.</param>
        /// <returns></returns>
        public bool CanHandleExp(Token token)
        {
            var plugin = GetExp(token);
            _lastMatchedExp = plugin;
            if (plugin == null) return false;
            return true;
        }


        /// <summary>
        /// Whether or not there is a statement plugin that can handle the token supplied.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CanHandleStmt(Token token)
        {
            var plugin = GetStmt(token);           
            if (plugin == null) return false;
            _lastMatchedExtStmt = plugin;
            return true;            
        }


        /// <summary>
        /// Whether or not there is a statement plugin that can handle the token supplied.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CanHandleSysStmt(Token token)
        {
            _lastMatchedSysStmt = null;
            var plugin = GetSysStmt(token);
            if (plugin == null) return false;
            _lastMatchedSysStmt = plugin;
            return true;
        }
                


        /// <summary>
        /// Whether or not there is a lex plugin that can handle the token supplied.
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <returns></returns>
        public bool CanHandleLex(Token token)
        {
            _lastMatchedLex = null;
            var plugin = GetLex(token);            
            if (plugin == null) return false;
            _lastMatchedLex = plugin;
            return true;
        }


        
        /// <summary>
        /// Whether or not there is a lex plugin that can handle the token supplied.
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <param name="isCurrentToken">Whether or not the token supplied is the current token.</param>
        /// <returns></returns>
        public bool CanHandleTok(Token token, bool isCurrentToken)
        {
            var tokenPos = isCurrentToken ? 0 : 1;
            var plugin = GetTok(token, tokenPos);
            _lastMatchedTok = plugin;
            if (plugin == null) return false;
            return true;
        }
        

        /// <summary>
        /// Whether or not there is an expression based plugin for the token supplied.
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <returns></returns>
        public bool ContainsExp(Token token)
        {
            var plugin = GetExp(token);
            return plugin != null;
        }


        /// <summary>
        /// Whether or not there is an statment based plugin for the token supplied.
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <returns></returns>
        public bool ContainsStmt(Token token)
        {
            var plugin = GetStmt(token);
            return plugin != null;
        }


        /// <summary>
        /// Whether or not there is a token based plugin for the token supplied.
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <param name="tokenPos">The position of the token in relation to the current token. 
        /// e.g. 0 indicates it's the current token, 1 indicates its the next token</param>
        /// <returns></returns>
        public bool ContainsTok(Token token, int tokenPos)
        {
            var plugin = GetTok(token, tokenPos);
            return plugin != null;
        }


        /// <summary>
        /// Whether or not there is a lex based plugin for the token supplied.
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <returns></returns>
        public bool ContainsLex(Token token)
        {
            var plugin = GetLex(token);
            return plugin != null;
        }


        /// <summary>
        /// Whether or not the function call supplied is a custom function callback that is 
        /// outside of the script.
        /// </summary>
        /// <param name="name">Name of the function</param>
        /// <returns></returns>
        public bool ContainsExp(string name)
        {
            if (_expPlugins.ContainsKey(name)) return true;
            if (_expPlugins.ContainsKey(name.ToLower())) return true;
            return false;
        }


        /// <summary>
        /// Whether or not there is a statement plugin with the supplied name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsStmt(string name)
        {
            if (_stmtPlugins.ContainsKey(name))
                return true;
            
            if (_expStmtPlugins.ContainsKey(name))
                return true;

            return false;
        }


        /// <summary>
        /// Get the postfix plugin.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IExprPlugin GetPostFix(Token token)
        {
            return GetPlugin(_postfixPlugins, token, "$Suffix") as IExprPlugin;
        }


        /// <summary>
        /// Gets the statement based plugin associated with the token supplied.
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns></returns>
        public IExprPlugin GetSysStmt(Token token)
        {
            return GetPlugin(_sysStmtPlugins, token) as IExprPlugin;
        }
        
        
        /// <summary>
        /// Get the expression based plugin associated with the token supplied
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <returns></returns>
        public IExprPlugin GetExp(Token token)
        {
            return GetPlugin(_expPlugins, token) as IExprPlugin;
        }


        /// <summary>
        /// Get the token based plugin associated with the token supplied
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <param name="tokenPos">The position of the token in relation to the current token.</param>
        /// <returns></returns>
        public ITokenPlugin GetTok(Token token, int tokenPos)
        {
            return GetPlugin(_tokPlugins, token, null, tokenPos) as ITokenPlugin;
        }

        
        /// <summary>
        /// Get the lex based plugin associated with the token supplied
        /// </summary>
        /// <param name="token">The token to check against plugins.</param>
        /// <returns></returns>
        public ILexPlugin GetLex(Token token)
        {
            return GetPlugin(_lexPlugins, token) as ILexPlugin;
        }


        /// <summary>
        /// Gets the statement based plugin associated with the token supplied.
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns></returns>
        public IExprPlugin GetStmt(Token token)
        {
            //string name = token.Text;            
            //if (_expStmtPlugins.ContainsKey(name))
            //{
            //    return GetPlugin(_expStmtPlugins, token) as IExprBasePlugin;
            //}
            //if (_stmtPlugins.ContainsKey(name))
            //{
            //    return GetPlugin(_stmtPlugins, token) as IExprBasePlugin;
            //}
            var plugin1 = GetPlugin(_expStmtPlugins, token) as IExprPlugin;
            var pluginToReturn = plugin1;
            var plugin2 = GetPlugin(_stmtPlugins, token) as IExprPlugin;
            if (plugin1 == null) return plugin2;
            if (plugin1 != null && plugin2 != null)
                pluginToReturn = plugin1.Precedence < plugin2.Precedence ? plugin1 : plugin2;
            return pluginToReturn;
        }


        private void RegisterExtensionsByNames(ICollection<string> pluginKeys, IDictionary<string, ILangPlugin> map)
        {
            var plugins = new ILangPlugin[pluginKeys.Count];
            var ndx = 0;
            foreach (var key in pluginKeys)
            {
                if (!map.ContainsKey(key))
                    throw new ArgumentException("Unknown plugin : " + key);

                plugins[ndx] = map[key];
                ndx++;
            }
            Register(plugins);
        }  


        private void RegisterPlugin(IDictionary<string, List<ILangPlugin>> map, ILangPlugin plugin, string[] tokens, bool sort)
        {
            if (tokens.Length > 0)
            {
                foreach (var token in tokens)
                    AddPlugin(map, plugin, token, sort);
            }
        }


        /// <summary>
        /// Gets a plugin that matches the token supplied.
        /// </summary>
        /// <param name="map">The map of plugins to match the tokens against</param>
        /// <param name="token">The token to match a plugin against</param>
        /// <param name="key">Optional key to use to look for plugin instead of using the token.Text property</param>
        /// <param name="tokenPos">The position of the token supplied. e.g. 0 indicates it's the current token. 1 indicates its the next token</param>
        /// <returns></returns>
        private ILangPlugin GetPlugin(IDictionary<string, List<ILangPlugin>> map, Token token, string key = null, int tokenPos = 0)
        {
            // Check for end token.
            if (token == Tokens.EndToken) return null;
            if (map == null || map.Count == 0) return null;

            var name = key == null ? token.Text : key;
            var isCurrentToken = tokenPos == 0;
            List<ILangPlugin> plugins = null;
            if (name == null)
                return null;

            if (map.ContainsKey(name))
            {
                plugins = map[name];
            }
            else
            {
                var kind = "IdToken";
                if (token.Kind == TokenKind.Ident) kind = "IdToken";
                else if (token.Kind == TokenKind.LiteralDate) kind = "DateToken";
                else if (token.Kind == TokenKind.LiteralNumber) kind = "NumberToken";
                else kind = token.GetType().Name;

                name = "$" + kind;
                if (map.ContainsKey(name))
                    plugins = map[name];
            }
            if (plugins != null)
            {
                // Either a specific word like "select" or a general IdToken.                
                ILangPlugin matchedPlugin = null;
                foreach (var plugin in plugins)
                {
                    var isTokenTypePlugin = plugin is ITokenPlugin;
                    if (isTokenTypePlugin)
                    {
                        if (((ITokenPlugin)plugin).CanHandle(token, isCurrentToken))
                        {
                            matchedPlugin = plugin;
                            break;
                        }
                    }
                    else if (plugin.CanHandle(token))
                    {
                        matchedPlugin = plugin;
                        break;
                    }
                }
                return matchedPlugin;
            }
            return null;
        }


        private void CallBack<T>(IDictionary<string, List<ILangPlugin>> map, Action<T> callback)
        {
            foreach (var pair in map)
            {
                foreach (var plugin in pair.Value)
                    callback((T)plugin);
            }
        }


        private void AddPlugin(IDictionary<string, List<ILangPlugin>> map, ILangPlugin plugin, string token, bool sort)
        {
            List<ILangPlugin> list;
            if (!map.ContainsKey(token))
            {
                list = new List<ILangPlugin>();
                map[token] = list;
            }
            else
                list = map[token];

            list.Add(plugin);

            if (sort)
            {
                SortPlugins(list);
            }
        }


        private List<ILangPlugin> SortPlugins(List<ILangPlugin> plugins)
        {
            var ordered = plugins.OrderBy(plugin => plugin.Precedence).ToList();
            return ordered;
        }


        private object CreatePluginInstance(Type pluginType)
        {
            var instance = Activator.CreateInstance(pluginType);
            return instance;
        }
    }
}
