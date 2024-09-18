/// <summary>
/// This Extention class helps in set regular expression and validation for form data
/// </summary>
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class ValidationExtention
{
	/// <summary>
    /// Regular expression, which is used to validate an E-Mail address.
    /// </summary>
    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" +
        @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
        @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
        @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    /// <summary>
    /// Checks whether the given Email-Parameter is a valid E-Mail address.
    /// </summary>
    /// <param name="email">Parameter-string that contains an E-Mail address.</param>
    /// <returns>True, wenn Parameter-string is not null and contains a valid E-Mail address;
    /// otherwise false.</returns>
    public static bool IsEmail (this string email) {
        if (email != null) return Regex.IsMatch (email, MatchEmailPattern);
        else return false;
    }

    //____________

    public static bool ValidateEmail (string email) {
        Regex regexEmail = new Regex (@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        Match emailMatch = regexEmail.Match (email);
        if (emailMatch.Success) {
            return true;
        } else {
            return false;
        }
    }

    public static bool ValidateUsername (this string _username) {
        Regex username = new Regex ("^[a-zA-Z0-9 #!@_]+$");
        Match usernameMatch = username.Match (_username);
        if (usernameMatch.Success) {
            return true;
        } else {
            return false;
        }
    }
    public static bool ValidatePassword (this string _password) {
        Regex password = new Regex ("^[a-zA-Z0-9]+$");
        Match passwordMatch = password.Match (_password);
        if (passwordMatch.Success) {
            return true;
        } else {
            return false;
        }
    }
    public static bool ValidateAge (this string _age) {
        Regex regexAge = new Regex ("[0-9]");
        Match ageMatch = regexAge.Match (_age);
        if (ageMatch.Success) {
            return true;
        } else {
            return false;
        }
    }
}
