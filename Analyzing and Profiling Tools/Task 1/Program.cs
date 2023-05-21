using System.Security.Cryptography;

internal class Program
{
    private static int saltLengthLimit = 32;
    private static int  iterate = 10000;


    private static void Main(string[] args)
    {
        var password = "blabla";
        byte[] salt = GetSalt();
        var result = GeneratePasswordHashUsingSalt(password, salt);
        Console.WriteLine(result);
        Console.ReadLine();
    }


    public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
    {
        // Rfc2898DeriveBytes class defaults to using the SHA1 algorithm
        using var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);

        // Aes is a symmetric encryption algorithm 
        Aes encAlg = Aes.Create();
        encAlg.Key = pbkdf2.GetBytes(16);

        // encryption
        using MemoryStream encryptionStream = new();
        CryptoStream encrypt = new(encryptionStream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] utfD1 = new System.Text.UTF8Encoding(false).GetBytes(passwordText);
        encrypt.Write(utfD1, 0, utfD1.Length);
        encrypt.FlushFinalBlock();
        encrypt.Close();
        byte[] edata1 = encryptionStream.ToArray();
        pbkdf2.Reset();

        var passwordHash = Convert.ToBase64String(edata1);
        return passwordHash;
    }

    public static byte[] GetSalt()
    {
        byte[] salt = new byte[saltLengthLimit];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

}