using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AES
{
	//private static string AES_Key = "hjhuy6789oijnhgtfrdesaw345678wnhu87yht5rfvc=";//abcdefgh01234567
	//private static string AES_IV = "hjhuy6789oijnhgtfrdesaw345678jndu87yht5rfvc=";
	private static string AES_Key="YXNramRuMjNjODd0Mm5jMiwzOTByWzJ1a2MwOTJ1NHQ=";
	private static string AES_IV="aGR5ZGp3ZWZpaDIzNGYyMzRtMnUzOTQ4MjMsdTRjMjk=";

	public static string Encrypt(string message)
	{	
		var aes = new RijndaelManaged();
		aes.KeySize = 256;
		aes.BlockSize = 256;
		//aes.Padding = PaddingMode.PKCS7;
		aes.Key = Convert.FromBase64String(AES_Key);
		aes.IV =Convert.FromBase64String(AES_IV);
		var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
		byte[] xBuff = null;
		
		using (var ms = new MemoryStream())
		{
			using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
			{
				byte[] xXml = Encoding.UTF8.GetBytes(message);
				cs.Write(xXml, 0, xXml.Length);
			}
			xBuff = ms.ToArray();
		}
		string Output = Convert.ToBase64String(xBuff);
		return Output;
	}
	
	public static string Decrypt(string message )	
	{	

		RijndaelManaged aes = new RijndaelManaged();
		aes.KeySize = 256;
		aes.BlockSize = 256;
		aes.Mode = CipherMode.CBC;
		//aes.Padding = PaddingMode.PKCS7;
		aes.Key = Convert.FromBase64String(AES_Key);
		aes.IV =Convert.FromBase64String(AES_IV);
		var decrypt = aes.CreateDecryptor();
		byte[] xBuff = null;
		
		using (var ms = new MemoryStream())
		{
			using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
			{
				byte[] xXml = Convert.FromBase64String(message);
				cs.Write(xXml, 0, xXml.Length);
			}
			xBuff = ms.ToArray();
		}
		
		string Output = Encoding.UTF8.GetString(xBuff);
		return Output;
	}

	public static string Encrypt_SK(string message )
	{	
		string key = "";
		string iv = "";

		key = AES_Key;
		iv = AES_IV;


		var aes = new RijndaelManaged();
		aes.KeySize = 256;
		aes.BlockSize = 256;
		//aes.Padding = PaddingMode.PKCS7;
		aes.Key = Convert.FromBase64String(key);
		aes.IV = Convert.FromBase64String(iv);
		var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
		byte[] xBuff = null;
		
		using (var ms = new MemoryStream())
		{
			using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
			{
				byte[] xXml = Encoding.UTF8.GetBytes(message);
				cs.Write(xXml, 0, xXml.Length);
			}
			xBuff = ms.ToArray();
		}
		string Output = Convert.ToBase64String(xBuff);
		return Output;
	}
	
	public static string Decrypt_SK(string message)	
	{	
		string key = "";
		string iv = "";

		key = AES_Key;
		iv = AES_IV;

		RijndaelManaged aes = new RijndaelManaged();
		aes.KeySize = 256;
		aes.BlockSize = 256;
		aes.Mode = CipherMode.CBC;
		//aes.Padding = PaddingMode.PKCS7;
		aes.Key = Convert.FromBase64String(key);
		aes.IV = Convert.FromBase64String(iv);
		var decrypt = aes.CreateDecryptor();
		byte[] xBuff = null;
		
		using (var ms = new MemoryStream())
		{
			using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
			{
				byte[] xXml = Convert.FromBase64String(message);
				cs.Write(xXml, 0, xXml.Length);
			}
			xBuff = ms.ToArray();
		}
		
		string Output = Encoding.UTF8.GetString(xBuff);
		return Output;
	}
}
