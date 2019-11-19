// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Reliability", 
    "CA2007:Consider calling ConfigureAwait on the awaited task", 
    Justification = "ConfigureAwait not neccessary as ASP.NET core does not have a synchronisation context")]

[assembly: SuppressMessage(
    "Globalization", 
    "CA1303:Do not pass literals as localized parameters", 
    Justification = "OK for log messages as these are for internal consumption",
    Scope = "Module",
    MessageId = "Microsoft.Extensions.Logging.ILogger`1<System.Object>")]

[assembly: SuppressMessage(
    "Design", 
    "CA1054:Uri parameters should not be strings", 
    Justification = "Common practice in ASP.NET core for route handlers")]

[assembly: SuppressMessage(
    "Design", 
    "CA1056:Uri properties should not be strings",
    Justification = "Common practice in ASP.NET core Razor pages")]
