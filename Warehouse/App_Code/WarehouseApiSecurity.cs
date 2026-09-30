using System;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Services.Protocols;

public static class WarehouseApiSecurity
{
    public static string GetRequiredSetting(string name)
    {
        string value = ConfigurationManager.AppSettings[name];
        if (String.IsNullOrWhiteSpace(value))
        {
            throw new ConfigurationErrorsException("Required appSetting '" + name + "' is not configured.");
        }

        return value;
    }

    public static void RequireCredential(string suppliedCredential, string settingName)
    {
        string expectedCredential;
        try
        {
            expectedCredential = GetRequiredSetting(settingName);
        }
        catch (ConfigurationErrorsException)
        {
            throw new SoapException("Service authentication is not configured. Set appSetting '" + settingName + "'.", SoapException.ServerFaultCode);
        }

        if (String.IsNullOrEmpty(suppliedCredential) ||
            !String.Equals(suppliedCredential, expectedCredential, StringComparison.Ordinal))
        {
            throw new SoapException("Authentication failed.", SoapException.ClientFaultCode);
        }
    }

    public static void RequireApiKey()
    {
        string expectedKey;
        try
        {
            expectedKey = GetRequiredSetting("WarehouseApiKey");
        }
        catch (ConfigurationErrorsException)
        {
            throw new SoapException("Service authentication is not configured. Set appSetting 'WarehouseApiKey'.", SoapException.ServerFaultCode);
        }

        HttpContext context = HttpContext.Current;
        string suppliedKey = context == null ? null : context.Request.Headers["X-Warehouse-Api-Key"];
        if (String.IsNullOrEmpty(suppliedKey) ||
            !String.Equals(suppliedKey, expectedKey, StringComparison.Ordinal))
        {
            throw new SoapException("Authentication failed.", SoapException.ClientFaultCode);
        }
    }

    public static void RequireAuthenticatedSession(HttpSessionState session)
    {
        if (session == null ||
            (String.IsNullOrWhiteSpace(Convert.ToString(session["UserID"])) &&
             String.IsNullOrWhiteSpace(Convert.ToString(session["Username"]))))
        {
            throw new SoapException("Authentication is required.", SoapException.ClientFaultCode);
        }
    }
}
