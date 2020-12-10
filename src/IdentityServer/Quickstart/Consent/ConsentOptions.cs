﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityServer.Quickstart.Consent
{
    public class ConsentOptions
    {
        public static bool EnableOfflineAccess { get; } = true;

        public const string OfflineAccessDisplayName = "Offline Access";

        public const string OfflineAccessDescription =
            "Access to your applications and resources, even when you are offline";

        public const string MustChooseOneErrorMessage =
            "You must pick at least one permission";

        public const string InvalidSelectionErrorMessage =
            "Invalid selection";
    }
}
