using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Portality.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Portality.Tests.Extensions
{
    [TestClass]
    public class RegularExtensionsTests
    {
        [TestMethod]
        public void ToUrlParameterTest()
        {
            // Create "INPUT" => "OUTPUT"
            var values = new Dictionary<string, string>();
            values.Add("This Is Simple", "This-Is-Simple");                 // should remove spaces and enter replacement character
            values.Add("So~@#$-%^*(){}Simple", "So-Simple");                // should replace all clutter
            values.Add("Could+It&Be-That_Good", "Could+It&Be-That_Good");   // should replace nothing

            foreach (KeyValuePair<string, string> pair in values)
            {
                var target = pair.Key.ToUrlParameter();
                Assert.IsTrue(target == pair.Value, String.Format("Expected: [{0}], Actual: [{1}]", pair.Value, target));
            }

        }
    }
}
