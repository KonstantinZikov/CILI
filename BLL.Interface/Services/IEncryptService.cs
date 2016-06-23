namespace BLL.Interface.Services
{
    public interface IEncryptService
    {
        byte[] Encrypt(int data);
        int Decrypt(byte[] data);
    }
}
