﻿using System.Threading.Tasks;
using GitTrends.Mobile.Shared;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;

namespace GitTrends.UITests
{
    [TestFixture(Platform.iOS, UserType.Neither)]
    [TestFixture(Platform.Android, UserType.Neither)]
    class SettingsTests : BaseTest
    {
        public SettingsTests(Platform platform, UserType userType) : base(platform, userType)
        {
        }

        public override async Task BeforeEachTest()
        {
            await base.BeforeEachTest().ConfigureAwait(false);

            RepositoryPage.DeclineGitHubUserNotFoundPopup();
            RepositoryPage.TapSettingsButton();

            await SettingsPage.WaitForPageToLoad().ConfigureAwait(false);
        }

        [Test]
        public async Task EnsureCreatedByLabelOpensBrowser()
        {
            //Arrange

            //Act
            SettingsPage.TapCreatedByLabel();

            //Assert
            if (App is iOSApp)
            {
                SettingsPage.WaitForBrowserToOpen();
                Assert.IsTrue(SettingsPage.IsBrowserOpen);
            }
            else
            {
                await Task.Delay(2000).ConfigureAwait(false);
                App.Screenshot("Browswer Opened");
            }
        }

        [Test]
        public async Task VerifyChartSettingsOptions()
        {
            //Arrange

            //Assert
            Assert.AreEqual(TrendsChartOption.All, SettingsPage.CurrentTrendsChartOption);

            //Act
            await SettingsPage.SetTrendsChartOption(TrendsChartOption.JustUniques).ConfigureAwait(false);

            //Assert
            Assert.AreEqual(TrendsChartOption.JustUniques, SettingsPage.CurrentTrendsChartOption);

            //Act
            await SettingsPage.SetTrendsChartOption(TrendsChartOption.NoUniques);

            //Assert
            Assert.AreEqual(TrendsChartOption.NoUniques, SettingsPage.CurrentTrendsChartOption);

            //Act
            await SettingsPage.SetTrendsChartOption(TrendsChartOption.All).ConfigureAwait(false);

            //Assert
            Assert.AreEqual(TrendsChartOption.All, SettingsPage.CurrentTrendsChartOption);
        }
    }
}
