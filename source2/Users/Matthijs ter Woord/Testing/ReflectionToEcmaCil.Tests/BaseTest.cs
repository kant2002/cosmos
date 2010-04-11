﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EcmaCil;
using System.IO;
using System.Xml;

namespace ReflectionToEcmaCil.Tests
{
    public class BaseTest
    {
        protected void AssertCompilationSame(string refName, Type baseTYpe)
        {
            var xReader = new Reader();
            var xResult = xReader.Execute(baseTYpe.Assembly.Location);
            string xActualOutput;
            using (var xStringWriter = new StringWriter())
            {
                using (var xXmlOut = XmlWriter.Create(xStringWriter))
                {
                    Dump.DumpTypes(xResult, xXmlOut);
                    xXmlOut.Flush();
                    xStringWriter.Flush();
                    xActualOutput = xStringWriter.ToString();
                }
            }

            var xExpectedOutput = ReadAllTextFromStream(typeof(BaseTest).Assembly.GetManifestResourceStream(typeof(BaseTest).Namespace + "." + refName + ".xml"));
            Assert.AreEqual(xExpectedOutput, xActualOutput);
        }

        private static string ReadAllTextFromStream(Stream aStream)
        {
            using (var xStreamReader = new StreamReader(aStream))
            {
                return xStreamReader.ReadToEnd();
            }
        }
    }
}