using System;
using System.Collections.Generic;
using System.IO;

namespace Lyrico.Common
{
    public static class Constants
    {
        public const string RedirectUri = "io.devmikan.lyrico://callback";
        public const string RedirectScheme = "io.devmikan.lyrico";
        public const string DatabaseFilename = "LyricsCache.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
