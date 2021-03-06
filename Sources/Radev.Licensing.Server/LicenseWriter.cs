﻿//
// Copyright (c) 2016 Repetti Adriano.
//
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.IO;
using System.Text;
using Radev.Licensing.Client;

namespace Radev.Licensing.Server
{
    /// <summary>
    /// Factory class to create convert a <see cref="License"/> to a secure string representation.
    /// </summary>
    public static class LicenseWriter
    {
        /// <summary>
        /// Convert license to a string.
        /// </summary>
        /// <typeparam name="TLicense">Type of the license.</typeparam>
        /// <typeparam name="TConverter">Converter used to transform the license into text.</typeparam>
        /// <param name="license">License to be converted.</param>
        /// <returns>A string with the full content of the given license.</returns>
        public static string ToString<TLicense, TConverter>(TLicense license)
            where TLicense : License
            where TConverter : ILicenseTextConverter<TLicense>, new()
        {
            return ServerSecureCommunication.EncodeMessageFromServer(new TConverter().ToString(license));
        }

        /// <summary>
        /// Convert license to a string.
        /// </summary>
        /// <param name="license">License to be converted.</param>
        /// <returns>A string with the full content of the given license.</returns>
        public static string ToString(License license)
        {
            return ToString<License, IniLicenseTextConverter<License>>(license);
        }

        /// <summary>
        /// Write license into a UTF-8 text file.
        /// </summary>
        /// <typeparam name="TLicense">Type of the license.</typeparam>
        /// <typeparam name="TConverter">Converter used to transform the license into text.</typeparam>
        /// <param name="path">Full path of the UTF-8 text file that will contain the license. If existing it will be overwritten.</param>
        /// <param name="license">License to be converted.</param>
        public static void ToFile<TLicense, TConverter>(string path, TLicense license)
            where TLicense : License
            where TConverter : ILicenseTextConverter<TLicense>, new()
        {
            File.WriteAllText(path, ToString<TLicense, TConverter>(license), Encoding.UTF8);
        }

        /// <summary>
        /// Write license into a UTF-8 text file.
        /// </summary>
        /// <param name="path">Full path of the UTF-8 text file that will contain the license. If existing it will be overwritten.</param>
        /// <param name="license">License to be converted.</param>
        public static void ToFile(string path, License license)
        {
            ToFile<License, IniLicenseTextConverter<License>>(path, license);
        }
    }
}
