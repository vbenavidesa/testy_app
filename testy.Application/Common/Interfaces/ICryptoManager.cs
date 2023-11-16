namespace testy.Application.Common.Interfaces
{
    public interface ICryptoManager
    {
        string EncryptText(string PlainText);
        string DecryptText(string EncryptedText);
    }
}
