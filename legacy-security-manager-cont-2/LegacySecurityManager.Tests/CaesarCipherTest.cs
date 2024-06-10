using NUnit.Framework;

namespace LegacySecurityManager.Tests
{
    public class CaesarCipherTest
    {
        [TestCase("","")]
        [TestCase("a","x")]
        [TestCase("b","y")]
        [TestCase("t","q")]
        [TestCase("U","r")]
        [TestCase("1","1")]
        [TestCase("*","*")]
        [TestCase("aa","xx")]
        [TestCase("THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG", "qeb nrfzh yoltk clu grjmp lsbo qeb ixwv ald")]
        public void encrypt_string(string input, string output)
        {
            Cipher cipher = new CaesarCipher(23);

            var encryption = cipher.Encrypt(input);

            Assert.That(encryption, Is.EqualTo(output));
        }
    }

    public class CaesarCipher : Cipher
    {
        private readonly int _shift;

        public CaesarCipher(int shift)
        {
            _shift = shift;
        }

        public string Encrypt(string str)
        {
            var stringEncrypted = "";

            foreach (var character in str)
            {
                stringEncrypted += CipherOneCharacter(character.ToString());
            }

            return stringEncrypted;
        }

        private string CipherOneCharacter(string character)
        {
            const string characters = "abcdefghijklmnopqrstuvwxyz";
            var lowerChar = character.ToLower();

            if (!characters.Contains(lowerChar))
            {
                return character;
            }

            var position = (characters.IndexOf(lowerChar) + _shift) % characters.Length;
            return characters[position].ToString();
        }
    }
}
