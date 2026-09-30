
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public static class SecurityPolicy
{


    // ENFORCE: 15 characters, alphanumeric, no sequences
    public static bool IsPasswordSecure(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 15)
            return false;

        bool hasText = password.Any(char.IsLetter);
        bool hasDigit = password.Any(char.IsDigit);

        // Check for basic sequential patterns (e.g., "123", "abc")
        bool isSequential = CheckSequences(password);

        return hasText && hasDigit && !isSequential;
    }

    private static bool CheckSequences(string pwd)
    {
        for (int i = 0; i < pwd.Length - 2; i++)
        {
            if ((pwd[i] + 1 == pwd[i + 1] && pwd[i] + 2 == pwd[i + 2]) ||
                (pwd[i] - 1 == pwd[i + 1] && pwd[i] - 2 == pwd[i + 2]))
                return true;
        }
        return false;
    }

    public static void ShowSecurityReminders()
    {
        Console.WriteLine("SECURITY CHECKLIST:");
        Console.WriteLine("- Ensure URL starts with HTTPS.");
        Console.WriteLine("- Disconnect from Public Wi-Fi for this session.");
        Console.WriteLine("- Scan any external USB drives before proceeding.");
    }

    public static bool VerifyPasswordWithMaster(string enteredPassword, string storedHash, string storedSaltHex, string masterStoredHash, string masterStoredSaltHex)
    {
        // Try primary password first
        if (VerifyHash(enteredPassword, storedHash, storedSaltHex))
        {
            return true;
        }

        // If primary fails, try the master password
        return VerifyHash(enteredPassword, masterStoredHash, masterStoredSaltHex);
    }

    private static bool VerifyHash(string password, string hashToCompare, string saltHex)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashToCompare) || string.IsNullOrEmpty(saltHex))
            return false;

        // 1. Convert Hex Salt to byte array
        byte[] saltBytes = new byte[saltHex.Length / 2];
        for (int i = 0; i < saltBytes.Length; i++)
        {
            saltBytes[i] = Convert.ToByte(saltHex.Substring(i * 2, 2), 16);
        }

        // 2. Hash the entered password
        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
        {
            byte[] enteredHashBytes = pbkdf2.GetBytes(20);
            byte[] storedHashBytes = Convert.FromBase64String(hashToCompare);

            // 3. Secure Comparison (Prevents Timing Attacks)
            return FixedTimeEquals(enteredHashBytes, storedHashBytes);
        }
    }
    // Helper method to prevent Timing Attacks
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private static bool FixedTimeEquals(byte[] left, byte[] right)
    {
        if (left.Length != right.Length) return false;
        int length = left.Length;
        int result = 0;
        for (int i = 0; i < length; i++)
        {
            result |= left[i] ^ right[i];
        }
        return result == 0;
    }
    //public static bool VerifyPasswordWithMaster(string enteredPassword, string storedHash, string storedSaltHex, string MasterStoredHash, string MasterStoredSaltHex)
    //{
    //    bool res = false;

    //    // 1. Convert the Hex Salt back into a byte array
    //    byte[] saltBytes = new byte[storedSaltHex.Length / 2];
    //    for (int i = 0; i < saltBytes.Length; i++)
    //    {
    //        saltBytes[i] = Convert.ToByte(storedSaltHex.Substring(i * 2, 2), 16);
    //    }

    //    // 2. Hash the entered password using the SAME salt and iterations (10,000)
    //    using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000))
    //    {
    //        byte[] enteredHashBytes = pbkdf2.GetBytes(20);
    //        string enteredHashBase64 = Convert.ToBase64String(enteredHashBytes);

    //        // 3. Compare the new hash with the hash stored in the database
    //        res = enteredHashBase64 == storedHash ? true : false;
    //    }
    //    if (!res)
    //    {
    //        // 1. Convert the Hex Salt back into a byte array
    //        byte[] MasterSaltBytes = new byte[MasterStoredSaltHex.Length / 2];
    //        for (int i = 0; i < MasterSaltBytes.Length; i++)
    //        {
    //            MasterSaltBytes[i] = Convert.ToByte(MasterStoredSaltHex.Substring(i * 2, 2), 16);
    //        }

    //        // 2. Hash the entered password using the SAME salt and iterations (10,000)
    //        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, MasterSaltBytes, 10000))
    //        {
    //            byte[] MasterHashBytes = pbkdf2.GetBytes(20);
    //            string MasterHashBase64 = Convert.ToBase64String(MasterHashBytes);

    //            // 3. Compare the new hash with the hash stored in the database
    //            res = MasterHashBase64 == MasterStoredHash ? true : false;
    //        }
    //    }
    //    return res;
    //}

    public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSaltHex)
    {
        bool res = false;

        // 1. Convert the Hex Salt back into a byte array
        byte[] saltBytes = new byte[storedSaltHex.Length / 2];
        for (int i = 0; i < saltBytes.Length; i++)
        {
            saltBytes[i] = Convert.ToByte(storedSaltHex.Substring(i * 2, 2), 16);
        }

        // 2. Hash the entered password using the SAME salt and iterations (10,000)
        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000))
        {
            byte[] enteredHashBytes = pbkdf2.GetBytes(20);
            string enteredHashBase64 = Convert.ToBase64String(enteredHashBytes);

            // 3. Compare the new hash with the hash stored in the database
            return enteredHashBase64 == storedHash;
        }

    }
}
