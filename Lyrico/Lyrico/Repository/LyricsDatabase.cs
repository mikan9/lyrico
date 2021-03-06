﻿using Lyrico.Common;
using Lyrico.Extensions;
using Lyrico.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lyrico.Repository
{
    public class LyricsDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public LyricsDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Lyrics).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Lyrics)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public async Task DropTable()
        {
            if (Database.TableMappings.Any(m => m.MappedType.Name == typeof(Lyrics).Name))
            {
                await Database.DropTableAsync<Lyrics>();
            }
        }

        public Task<List<Lyrics>> GetAllLyricsAsync()
        {
            return Database.Table<Lyrics>().ToListAsync();
        }

        public Task<Lyrics> GetLyricsAsync(string artist, string title)
        {
            return Database.Table<Lyrics>().Where(i => i.Artist == artist && i.Title == title).FirstOrDefaultAsync();
        }

        public async Task<int> SaveLyricsAsync(Lyrics entry)
        {
            if (entry.ID != 0) return Database.UpdateAsync(entry).Result;
            else
            {
                var lyrics = await GetLyricsAsync(entry.Artist, entry.Title);
                if (lyrics == null)
                    return Database.InsertAsync(entry).Result;
                else return -1;
            }
        }

        public Task<int> DeleteLyricsAsync(Lyrics entry)
        {
            return Database.DeleteAsync(entry);
        }
    }
}
