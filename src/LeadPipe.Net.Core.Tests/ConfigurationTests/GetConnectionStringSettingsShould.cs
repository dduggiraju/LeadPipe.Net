// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) Lead Pipe Software. All rights reserved.
// Licensed under the MIT License. Please see the LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------------------------

using LeadPipe.Net.Configuration;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Configuration;

namespace LeadPipe.Net.Tests.ConfigurationTests
{
    /// <summary>
    /// GetConnectionStringSettings tests.
    /// </summary>
    [TestFixture]
    public class GetConnectionStringSettingsShould
    {
        [OneTimeSetUp]
        public void Init()
        {
            var _config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.test.json")
             .Build();

            var asppSettings = _config.GetSection("appConfig").GetChildren();
            var connectionStrings = _config.GetSection("ConnectionStrings").GetChildren();
            foreach (var setting in asppSettings)
            {
                System.Configuration.ConfigurationManager.AppSettings.Set(setting.Key, setting.Value);
            }

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection csSection = config.ConnectionStrings;
            foreach (var connectionString in connectionStrings)
            {
                var connectionStringSetting = new ConnectionStringSettings(connectionString.Key, connectionString.Value);
                csSection.ConnectionStrings.Add(connectionStringSetting);
                config.Save(ConfigurationSaveMode.Modified);
            }
        }
        /// <summary>
        /// Tests to ensure that we can retrieve a connection string setting a context value exists and is not supplied.
        /// </summary>
        [Test]
        public void ReturnValueGivenContextSettingExistsAndContextIsNotSupplied()
        {
            // Arrange

            // Act
            var settings = ConfigurationService.GetConnectionStringSettings("TestConnection");

            // Assert
            Assert.That(settings != null);
        }

        /// <summary>
        /// Tests to ensure that we can retrieve a connection string setting a context value exists and is supplied.
        /// </summary>
        [Test]
        public void ReturnValueGivenContextSettingExistsAndContextSupplied()
        {
            // Arrange

            // Act
            var settings = ConfigurationService.GetConnectionStringSettings("11-UnitTest", "TestConnection");

            // Assert
            Assert.That(settings != null);
        }
    }
}