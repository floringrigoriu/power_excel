using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphCaller;
using System.Diagnostics;

namespace GraphCallerTest
{
    [TestClass]
    public class AdAccountTest
    {
        // This is an old invalidate valude , but should serve as an warning no leakage of sensitive and sensible data
        public static readonly string testAdAccount = "CAACZBwbJIzL0BAEnYg5GBIGUO4d8j4TZC9ZCb7dDd8VfosR6FpzTLjXJBZBfTTrbEeIvaZB9jS8TzDMgDwGkxKrG3P4wEGydCaWDyaN3JsYt7TLtExPWo0mN5B1YEd4nkGDB1LWJDWV1msBGfFXjOQzRNPxUmPZCYerK9LNijabl9MyZA6IngAw96SheytMyNXAGlgrME4BZCwZDZD";
            //do update with the real deal - access token
        public const long florinAdAccount = 10151318637546538;

        public TestContext TestContext
        {
            get;
            set;
        }


       [TestMethod]
        public void GetAdAccount()
        {
            // call
            var account = AdAccount.getAdAccount(testAdAccount, florinAdAccount);
            // assert
            Assert.AreEqual(florinAdAccount, account.AccontID);
            Assert.IsTrue(account.DailySpend > 0);
        }

        [TestMethod]
        public void GetAdGroup()
        {
            // call
            var adgroup = AdGroups.getAdGroup(testAdAccount, florinAdAccount);
            // assert
            //Assert.AreEqual(florinAdAccount, adgroup.AdgroupAccontID);
        }

        [TestMethod]
        public void GetAdAccountNOWCF()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            // call
            var account = AdAccount.getAdAccount(testAdAccount, florinAdAccount);
            
            // test result
            Assert.AreEqual(florinAdAccount, account.AccontID);
            Assert.IsTrue(account.DailySpend > 0);

            // test time
            stopwatch.Stop();
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 1000,
                "Expect fast exececution, faster than 1s, instead {0} ms",
                stopwatch.ElapsedMilliseconds);
            this.TestContext.WriteLine("test completed in {0} ms.", stopwatch.ElapsedMilliseconds);
        }
        
    }
}
