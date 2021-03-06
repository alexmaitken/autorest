﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AutoRest.CSharp.LoadBalanced.Json.Tests
{
    [TestFixture]
    public class DateCustomConverterTest
    {
        [Test, TestCaseSource(nameof(GetSerializationTestCases))]
        public void DateSerializationTest(TestObject testObject, string expectedJson)
        {
            Assert.AreEqual(JsonConvert.SerializeObject(testObject), expectedJson);
        }

        [Test, TestCaseSource(nameof(GetSerializationTestCases))]
        public void DateDeserializationTest(TestObject testObject, string jsonText)
        {
            var deserializedObject = JsonConvert.DeserializeObject<TestObject>(jsonText);
            Assert.AreEqual(testObject.MyDate, deserializedObject.MyDate);
            Assert.AreEqual(testObject.MyDate2, deserializedObject.MyDate2);
        }


        public static IEnumerable<object[]> GetSerializationTestCases()
        {
            yield return new object[]
                         {
                             new TestObject { MyDate = new DateTime(2030, 1, 23, 2, 3, 12) },
                             "{\"my\":\"1/23/2030 2:03:12 AM\",\"my2\":null}"
                         };

            yield return new object[]
                         {
                             new TestObject { MyDate2 = new DateTime(2030, 1, 23, 2, 3, 12) },
                             "{\"my\":\"1/1/0001 12:00:00 AM\",\"my2\":\"1/23/2030 2:03:12 AM\"}"
                         };
        }

        public class TestObject
        {
            [JsonProperty("my"), JsonConverter(typeof(DateTimeStringConverter))]
            public DateTime MyDate { get; set; }

            [JsonProperty("my2"), JsonConverter(typeof(DateTimeStringConverter))]
            public DateTime? MyDate2 { get; set; }
        }
    }
}
