using Blasphemous.ModdingAPI;
using System;
using System.Collections.Generic;

namespace Blasphemous.AtriumOfAtonement.Levels;

/// <summary>
/// Base class for IModifiers with properties 
/// in the format of `propertyName = propertyValue`
/// </summary>
public abstract class ModifierWithProperties
{
    protected Dictionary<string, string> _properties;

    /// <summary>
    /// Dictionary that contains all the valid arguments for each possible property. 
    /// Key is the property name. 
    /// Value is a function that returns a bool indicating whether the argument is valid. 
    /// It should be declared values in the Apply() method of the IModifier object. 
    /// </summary>
    protected Dictionary<string, Func<string, bool>> _validPropertyArguments;

    /// <summary>
    /// Dictionary that contains all the default arguments for each possible property. 
    /// Key is the property name. 
    /// Value is the default value for that property. 
    /// If the property doesn't apply default value, the property is not included in this dictionary
    /// or has default value specified as `null`. 
    /// It should be declared values in the Apply() method of the IModifier object. 
    /// </summary>
    protected Dictionary<string, string> _defaultPropertyArguments;

    /// <summary>
    /// Unzip properties in the format of `propertyName = propertyValue` 
    /// into a Dictionary
    /// </summary>
    public Dictionary<string, string> UnzipProperties(string[] properties)
    {
        Dictionary<string, string> result = new();

        foreach (string argument in properties)
        {
            int sepIndex = argument.IndexOf('=');
            string argumentName = argument.Substring(0, sepIndex).Trim().ToLower();
            string argumentValue = argument.Substring(sepIndex + 1).Trim().ToLower();
            result.Add(argumentName, argumentValue);
        }

        // check property validity for each argument that can be specified,
        // fill in missing or invalid properties with default value
        foreach (string argName in _validPropertyArguments.Keys)
        {
            if (result.TryGetValue(argName, out string argValue))
            {
                if (!_validPropertyArguments[argName](argValue))
                {
                    ModLog.Error($"property {argName} encountered unknown argument value {argValue}, " +
                        $"skipped registering object!");
                    return null;
                }
            }
            else
            {
                if (_defaultPropertyArguments.ContainsKey(argName)
                    && _defaultPropertyArguments[argName] != null)
                {
                    // set to default parameter
                    argValue = _defaultPropertyArguments[argName];
                    result.Add(argName, argValue);
                }
                else
                {
                    ModLog.Error($"property {argName} is not specified, " +
                        $"skipped registering object!");
                    return null;
                }
            }
        }

        return result;
    }
}
