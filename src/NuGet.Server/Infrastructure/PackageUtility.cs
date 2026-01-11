// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 
using System;
using System.Configuration;
using System.Web.Hosting;

namespace NuGet.Server.Infrastructure
{
    public class PackageUtility
    {
        private static readonly Lazy<string> _packagePhysicalPath = new Lazy<string>(ResolvePackagePhysicalPath);
        private static readonly Lazy<string> _packageReletivePath = new Lazy<string>(ResolvePackageReletivePath);

        public static string PackagePhysicalPath
        {
            get
            {
                return _packagePhysicalPath.Value;
            }
        }

        public static string PackageReletivePath
        {
            get
            {
                return _packageReletivePath.Value;
            }
        }

        private static string ResolvePackagePhysicalPath()
        {
            // The packagesPath could be an absolute path (rooted and use as is)
            // or a virtual path (and use as a virtual path)
            var path = ConfigurationManager.AppSettings["packagesPath"];

            if (String.IsNullOrEmpty(PackageReletivePath))
            {
                // Default path
                return HostingEnvironment.MapPath("~/Packages");
            }

            if (PackageReletivePath.StartsWith("~/"))
            {
                return HostingEnvironment.MapPath(PackageReletivePath);
            }

            return PackageReletivePath;
        }

        private static string ResolvePackageReletivePath()
        {
            // The packagesPath could be an absolute path (rooted and use as is)
            // or a virtual path (and use as a virtual path)
            var path = ConfigurationManager.AppSettings["packagesPath"];

            if (String.IsNullOrEmpty(path))
            {
                // Default path
                return "~/Packages";
            }

            return path;
        }
    }
}
