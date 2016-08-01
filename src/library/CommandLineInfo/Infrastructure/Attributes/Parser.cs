/*
using CommandLine.Infrastructure;
using CommandLine.Parsing;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	public sealed class Parser : IDisposable
	{
		public const int DefaultExitCodeFail = 1;

		private static readonly Parser DefaultParser = new Parser(true);

		private readonly ParserSettings _settings;

		private bool _disposed;

		public static Parser Default
		{
			get
			{
				return Parser.DefaultParser;
			}
		}

		public ParserSettings Settings
		{
			get
			{
				return this._settings;
			}
		}

		public Parser()
		{
			this._settings = new ParserSettings
			{
				Consumed = true
			};
		}

		[Obsolete("Use constructor that accepts Action<ParserSettings>.")]
		public Parser(ParserSettings settings)
		{
			Assumes.NotNull<ParserSettings>(settings, "settings", "The command line parser settings instance cannot be null.");
			if (settings.Consumed)
			{
				throw new InvalidOperationException("The command line parserSettings instance cannnot be used more than once.");
			}
			this._settings = settings;
			this._settings.Consumed = true;
		}

		public Parser(Action<ParserSettings> configuration)
		{
			Assumes.NotNull<Action<ParserSettings>>(configuration, "configuration", "The command line parser settings delegate cannot be null.");
			this._settings = new ParserSettings();
			configuration(this.Settings);
			this._settings.Consumed = true;
		}

		private Parser(bool singleton) : this(delegate(ParserSettings with)
		{
			with.CaseSensitive = false;
			with.MutuallyExclusive = false;
			with.HelpWriter = Console.Error;
			with.ParsingCulture = CultureInfo.InvariantCulture;
		})
		{
		}

		~Parser()
		{
			this.Dispose(false);
		}

		public bool ParseArguments(string[] args, object options)
		{
			Assumes.NotNull<string[]>(args, "args", "The arguments string array cannot be null.");
			Assumes.NotNull<object>(options, "options", "The target options instance cannot be null.");
			return this.DoParseArguments(args, options);
		}

		public bool ParseArguments(string[] args, object options, Action<string, object> onVerbCommand)
		{
			Assumes.NotNull<string[]>(args, "args", "The arguments string array cannot be null.");
			Assumes.NotNull<object>(options, "options", "The target options instance cannot be null.");
			Assumes.NotNull<object>(options, "onVerbCommand", "Delegate executed to capture verb command instance reference cannot be null.");
			object verbInstance = null;
			bool result = this.DoParseArgumentsVerbs(args, options, ref verbInstance);
			onVerbCommand(args.FirstOrDefault<string>() ?? string.Empty, result ? verbInstance : null);
			return result;
		}

		public bool ParseArgumentsStrict(string[] args, object options, Action onFail = null)
		{
			Assumes.NotNull<string[]>(args, "args", "The arguments string array cannot be null.");
			Assumes.NotNull<object>(options, "options", "The target options instance cannot be null.");
			bool result;
			if (!this.DoParseArguments(args, options))
			{
				this.InvokeAutoBuildIfNeeded(options);
				if (onFail == null)
				{
					Environment.Exit(1);
				}
				else
				{
					onFail();
				}
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public bool ParseArgumentsStrict(string[] args, object options, Action<string, object> onVerbCommand, Action onFail = null)
		{
			Assumes.NotNull<string[]>(args, "args", "The arguments string array cannot be null.");
			Assumes.NotNull<object>(options, "options", "The target options instance cannot be null.");
			Assumes.NotNull<object>(options, "onVerbCommand", "Delegate executed to capture verb command instance reference cannot be null.");
			object verbInstance = null;
			bool result;
			if (!this.DoParseArgumentsVerbs(args, options, ref verbInstance))
			{
				onVerbCommand(args.FirstOrDefault<string>() ?? string.Empty, null);
				this.InvokeAutoBuildIfNeeded(options);
				if (onFail == null)
				{
					Environment.Exit(1);
				}
				else
				{
					onFail();
				}
				result = false;
			}
			else
			{
				onVerbCommand(args.FirstOrDefault<string>() ?? string.Empty, verbInstance);
				result = true;
			}
			return result;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		internal static object InternalGetVerbOptionsInstanceByName(string verb, object target, out bool found)
		{
			found = false;
			object result;
			if (string.IsNullOrEmpty(verb))
			{
				result = target;
			}
			else
			{
				Pair<PropertyInfo, VerbOptionAttribute> pair = ReflectionHelper.RetrieveOptionProperty<VerbOptionAttribute>(target, verb);
				found = (pair != null);
				result = (found ? pair.Left.GetValue(target, null) : target);
			}
			return result;
		}

		private static void SetParserStateIfNeeded(object options, IEnumerable<ParsingError> errors)
		{
			if (options.CanReceiveParserState())
			{
				PropertyInfo property = ReflectionHelper.RetrievePropertyList<ParserStateAttribute>(options)[0].Left;
				object parserState = property.GetValue(options, null);
				if (parserState != null)
				{
					if (!(parserState is IParserState))
					{
						throw new InvalidOperationException("Cannot apply ParserStateAttribute to a property that does not implement IParserState or is not accessible.");
					}
					if (!(parserState is ParserState))
					{
						throw new InvalidOperationException("ParserState instance cannot be supplied.");
					}
				}
				else
				{
					try
					{
						property.SetValue(options, new ParserState(), null);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException("Cannot apply ParserStateAttribute to a property that does not implement IParserState or is not accessible.", ex);
					}
				}
				IParserState state = (IParserState)property.GetValue(options, null);
				foreach (ParsingError error in errors)
				{
					state.Errors.Add(error);
				}
			}
		}

		private static StringComparison GetStringComparison(ParserSettings settings)
		{
			return settings.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
		}

		private bool DoParseArguments(string[] args, object options)
		{
			Pair<MethodInfo, HelpOptionAttribute> pair = ReflectionHelper.RetrieveMethod<HelpOptionAttribute>(options);
			TextWriter helpWriter = this._settings.HelpWriter;
			bool result;
			if (pair != null && helpWriter != null)
			{
				if (this.ParseHelp(args, pair.Right) || !this.DoParseArgumentsCore(args, options))
				{
					string helpText;
					HelpOptionAttribute.InvokeMethod(options, pair, out helpText);
					helpWriter.Write(helpText);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				result = this.DoParseArgumentsCore(args, options);
			}
			return result;
		}

		private bool DoParseArgumentsCore(string[] args, object options)
		{
			bool hadError = false;
			OptionMap optionMap = OptionMap.Create(options, this._settings);
			optionMap.SetDefaults();
			ValueMapper valueMapper = new ValueMapper(options, this._settings.ParsingCulture);
			StringArrayEnumerator arguments = new StringArrayEnumerator(args);
			while (arguments.MoveNext())
			{
				string argument = arguments.Current;
				if (!string.IsNullOrEmpty(argument))
				{
					ArgumentParser parser = ArgumentParser.Create(argument, this._settings.IgnoreUnknownArguments);
					if (parser != null)
					{
						PresentParserState result = parser.Parse(arguments, optionMap, options);
						if ((ushort)(result & PresentParserState.Failure) == 2)
						{
							Parser.SetParserStateIfNeeded(options, parser.PostParsingState);
							hadError = true;
						}
						else if ((ushort)(result & PresentParserState.MoveOnNextElement) == 4)
						{
							arguments.MoveNext();
						}
					}
					else if (valueMapper.CanReceiveValues)
					{
						if (!valueMapper.MapValueItem(argument))
						{
							hadError = true;
						}
					}
				}
			}
			hadError |= !optionMap.EnforceRules();
			return !hadError;
		}

		private bool DoParseArgumentsVerbs(string[] args, object options, ref object verbInstance)
		{
			IList<Pair<PropertyInfo, VerbOptionAttribute>> verbs = ReflectionHelper.RetrievePropertyList<VerbOptionAttribute>(options);
			Pair<MethodInfo, HelpVerbOptionAttribute> helpInfo = ReflectionHelper.RetrieveMethod<HelpVerbOptionAttribute>(options);
			bool result;
			if (args.Length == 0)
			{
				if (helpInfo != null || this._settings.HelpWriter != null)
				{
					this.DisplayHelpVerbText(options, helpInfo, null);
				}
				result = false;
			}
			else
			{
				OptionMap optionMap = OptionMap.Create(options, verbs, this._settings);
				if (this.TryParseHelpVerb(args, options, helpInfo, optionMap))
				{
					result = false;
				}
				else
				{
					OptionInfo verbOption = optionMap[args.First<string>()];
					if (verbOption == null)
					{
						if (helpInfo != null)
						{
							this.DisplayHelpVerbText(options, helpInfo, null);
						}
						result = false;
					}
					else
					{
						verbInstance = verbOption.GetValue(options);
						if (verbInstance == null)
						{
							verbInstance = verbOption.CreateInstance(options);
						}
						bool verbResult = this.DoParseArgumentsCore(args.Skip(1).ToArray<string>(), verbInstance);
						if (!verbResult && helpInfo != null)
						{
							this.DisplayHelpVerbText(options, helpInfo, args.First<string>());
						}
						result = verbResult;
					}
				}
			}
			return result;
		}

		private bool ParseHelp(string[] args, HelpOptionAttribute helpOption)
		{
			bool caseSensitive = this._settings.CaseSensitive;
			bool result;
			for (int i = 0; i < args.Length; i++)
			{
				string arg = args[i];
				char? shortName = helpOption.ShortName;
				if ((shortName.HasValue ? new int?((int)shortName.GetValueOrDefault()) : null).HasValue)
				{
					if (ArgumentParser.CompareShort(arg, helpOption.ShortName, caseSensitive))
					{
						result = true;
						return result;
					}
				}
				if (!string.IsNullOrEmpty(helpOption.LongName))
				{
					if (ArgumentParser.CompareLong(arg, helpOption.LongName, caseSensitive))
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		private bool TryParseHelpVerb(string[] args, object options, Pair<MethodInfo, HelpVerbOptionAttribute> helpInfo, OptionMap optionMap)
		{
			TextWriter helpWriter = this._settings.HelpWriter;
			bool result;
			if (helpInfo != null && helpWriter != null)
			{
				if (string.Compare(args[0], helpInfo.Right.LongName, Parser.GetStringComparison(this._settings)) == 0)
				{
					string verb = args.FirstOrDefault<string>();
					if (verb != null)
					{
						OptionInfo verbOption = optionMap[verb];
						if (verbOption != null)
						{
							if (verbOption.GetValue(options) == null)
							{
								verbOption.CreateInstance(options);
							}
						}
					}
					this.DisplayHelpVerbText(options, helpInfo, verb);
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		private void DisplayHelpVerbText(object options, Pair<MethodInfo, HelpVerbOptionAttribute> helpInfo, string verb)
		{
			string helpText;
			if (verb == null)
			{
				HelpVerbOptionAttribute.InvokeMethod(options, helpInfo, null, out helpText);
			}
			else
			{
				HelpVerbOptionAttribute.InvokeMethod(options, helpInfo, verb, out helpText);
			}
			if (this._settings.HelpWriter != null)
			{
				this._settings.HelpWriter.Write(helpText);
			}
		}

		private void InvokeAutoBuildIfNeeded(object options)
		{
			if (this._settings.HelpWriter != null && !options.HasHelp() && !options.HasVerbHelp())
			{
				this._settings.HelpWriter.Write(HelpText.AutoBuild(options, delegate(HelpText current)
				{
					HelpText.DefaultParsingErrorsHandler(options, current);
				}, options.HasVerbs()));
			}
		}

		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					if (this._settings != null)
					{
						this._settings.Dispose();
					}
					this._disposed = true;
				}
			}
		}
	}
}
*/