using Store;
using Xunit;

namespace StoreTest
{
    public class MakerTest
    {
        [Fact]
        public void IsCellPhone_WithNull_ReturnFalse()
        {
            bool actual = Maker.IsNumberPhone(null, out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithNonsense_ReturnFalse()
        {
            bool actual = Maker.IsNumberPhone("213124321209429213", out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithSpace_ReturnFalse()
        {
            bool actual = Maker.IsNumberPhone("          ", out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithVoidString_ReturnFalse()
        {
            bool actual = Maker.IsNumberPhone("", out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithNoCorrectForInternationalRule_ReturnFalse()
        {
            bool actual = Maker.IsNumberPhone("89173620902", out string form);

            Assert.False(actual);
        }

        [Fact]  
        public void IsCellPhone_WithCorectNumber_ReturnTrue()
        {
            bool actual = Maker.IsNumberPhone("+79876543210", out string form);

            Assert.True(actual);
        }



        // checking Email 

        [Fact]
        public void IsEmail_WithNull_ReturnFalse()
        {
            bool actual = Maker.IsEmail(null);

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithVoidString_ReturnFalse()
        {
            bool actual = Maker.IsEmail("");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithNonsense_ReturnFalse()
        {
            bool actual = Maker.IsEmail("asfigoasnaewiodsjnadgs");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithNotFullCorrectVale_ReturnFalse()
        {
            bool actual = Maker.IsEmail("gusakseva8@gmail");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithoutSymbolDog_ReturnFalse()
        {
            bool actual = Maker.IsEmail("gusakseva8gmail.com");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithCorrectValue_ReturnTrue()
        {
            bool actual = Maker.IsEmail("gusakseva8@gmail.com");

            Assert.True(actual);
        }
    }
}
