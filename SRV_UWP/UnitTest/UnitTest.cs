
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SRV_UWP.models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLoginName()
        {
            User user = new User();
            Assert.AreEqual(false, user.Login("000","srv"));
        }

        [TestMethod]
        public void TestLogPassword()
        {
            User user = new User();
            Assert.AreEqual(false, user.Login("000724247", "aaa"));
        }

        [TestMethod]
        public void TestGetStudentById()
        {
            Student student = new Student();
            Assert.AreEqual("Mary", student.GetStudentById("000724247").FirstName);
        }

        [TestMethod]
        public void TestSelectQualification()
        {
            User user = new User();
            Qualification qualification = new Qualification();
            qualification = qualification.GetQualification("C3_IDM15");
            Assert.AreEqual(null, user.SelectQualification(qualification));
        }


        [TestMethod]
        public void TestIsLecturerNameLogin()
        {
            Lecturer lecturer = new Lecturer();
            Assert.AreEqual(true, lecturer.IsLecturerNameLogIn("Trevor.Learey"));
        }

    }
}
