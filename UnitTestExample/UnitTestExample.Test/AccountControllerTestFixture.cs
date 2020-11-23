using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]

        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var actualResult = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [
            Test,
            TestCase("Abcdefgh", false),
            TestCase("ABCDEFG1", false),
            TestCase("abcdefg1", false),
            TestCase("Abcdef1", false),
            TestCase("Abcdefgh1", true)

        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidatePassword(password);

            Assert.AreEqual(expectedResult, actualResult);

        }

        [
            Test,
            TestCase("csaba@huzian.hu","Abcd1234"),
            TestCase("huzian@csaba.hu","aBcDeF12")
        ]

        public void TestRegisterHappyPath(string email, string password)
        {
            var accountrController = new AccountController();

            var actualResult = accountrController.Register(email, password);

            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreEqual(Guid.Empty, actualResult.ID);
        }


        [
            Test,
            TestCase("csaba@huzian.hu","éáóab12334"),
            TestCase("csaba@huzian.hu","Ábcs1234"),
            TestCase("csaba@huzian.hu","avdsvdsfgds"),
            TestCase("csaba@huzian.hu", "abcdABCD"),            
        ]

        public void TestRegisterValidateException(string email, string password)
        {
            //Arrange
            var accountController = new AccountController();

            //Act + (Assert)
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {

                Assert.IsInstanceOf<ValidationException>(ex);
            }



        }


    }
}
