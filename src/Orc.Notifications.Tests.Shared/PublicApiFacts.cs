// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublicApiFacts.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications.Tests
{
    using ApiApprover;
    using NUnit.Framework;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test]
        public void Orc_Notifications_HasNoBreakingChanges()
        {
            var assembly = typeof(NotificationService).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }
    }
}