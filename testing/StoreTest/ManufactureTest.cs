using Store;
using Xunit;
namespace StoreTest
{
    public class ManufactureTest
    {
        [Fact]
        public void IsNumberPhone_CorrectNumber_Variant1_True()
        {
            string number = "+7917-362-09-02";
            var x = new Maker(1, "", number, "", "", "");

            Assert.Equal("+7917-362-09-02", x.NumberPhone);
        }

        [Fact]
        public void IsNumberPhone_CorrectNumber_Variant2_True()
        {
            string number = "8917-362-09-02";

            var x = new Maker(1, "", number, "", "", "");

            Assert.Equal("8917-362-09-02", x.NumberPhone);
        }

        [Fact]
        public void IsNumberPhone_NoCorrectNumber_False()
        {
            string number = "12412352424232r-qwf324sd-09-02";

            var x = new Maker(1, "", number, "", "", "");

            Assert.Equal("", x.NumberPhone);
        }

        [Fact]
        public void IsEmail_CorrectEmail_True()
        {
            string email = "gusakseva8@gmail.com";

            var x = new Maker(1, "", "", email, "", "");

            Assert.Equal("gusakseva8@gmail.com", x.Email);
        }

        [Fact]
        public void IsEmail_NoCorrectEmail_True()
        {
            string email = "gusadfgssakseva8@asdfgddsfdfgmail.c2321324om";

            var x = new Maker(1, "", "", email, "", "");

            Assert.Equal("", x.Email);
        }
    }
}
