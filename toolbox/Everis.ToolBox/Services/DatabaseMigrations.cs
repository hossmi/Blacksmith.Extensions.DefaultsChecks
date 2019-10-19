using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Everis.ToolBox.Services
{
    public class DatabaseMigrations<TConnection>
        where TConnection : class, IDbConnection, new()
    {
        private const string VERSION = "version";
        private const string MODE = "mode";
        private const string DESCRIPTION = "description";

        private static readonly Regex migrationFileRegex =
            new Regex($@"^migration.(?<{VERSION}>\d+).(?<{MODE}>upgrade|downgrade|data).?(?<{DESCRIPTION}>[\w\d\ ',]*).sql$", RegexOptions.IgnoreCase);

        private readonly string connectionString;
        private readonly string scriptsPath;
        private readonly IEnumerable<PrvMigration> migrations;

        public DatabaseMigrations(string connectionString, string scriptsPath)
        {
            Asserts.stringIsNotEmpty(connectionString);
            Asserts.stringIsNotEmpty(scriptsPath);

            this.connectionString = connectionString;
            this.scriptsPath = scriptsPath;

            this.migrations = loadMigrations(scriptsPath);
        }

        public void reset()
        {
            IEnumerable<PrvMigration> downMigrations;
            IEnumerable<PrvMigration> upMigrations;
            DB<TConnection> db;

            db = new DB<TConnection>(this.connectionString);

            downMigrations = this.migrations
                .Where(m => m.Kind == Kind.Downgrade)
                .OrderByDescending(migrationStep => migrationStep.Version);

            foreach (PrvMigration migration in downMigrations)
            {
                Debug.Print($"Downgrading database to version {migration.Version} {migration.Description}...");
                executeEvenWithErrors(db, migration);
            }

            upMigrations = this.migrations
                .Where(m => m.Kind != Kind.Downgrade)
                .OrderBy(migrationStep => migrationStep.Version)
                .ThenBy(migrationStep => migrationStep.Kind);

            foreach (PrvMigration migration in upMigrations)
            {
                string verb;

                verb = migration.Kind == Kind.Upgrade ? "Upgrading database to version" : "Appling data update";
                Debug.Print($"{verb} {migration.Version} {migration.Description}...");
                db.write(migration.Statements);
            }
        }

        private static void executeEvenWithErrors(DB<TConnection> db, PrvMigration migration)
        {
            IEnumerable<string> statements = migration
                .Statements
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string statement in statements)
            {
                try
                {
                    db.write(statement);
                }
                catch (Exception ex)
                {
                    Debug.Print($@"Error downgrading to version {migration.Version}: {ex.Message}");
                }
            }
        }

        private static IEnumerable<PrvMigration> loadMigrations(string scriptsPath)
        {
            DirectoryInfo directory;
            IEnumerable<PrvMigration> migrations;
            ISet<string> uniqueMigrations;

            directory = new DirectoryInfo(scriptsPath);

            migrations = directory
                .EnumerateFiles("*.sql", SearchOption.TopDirectoryOnly)
                .Select(tryComposeMigration);

            uniqueMigrations = new SortedSet<string>();

            foreach (PrvMigration migration in migrations)
            {
                string key;

                key = $"{migration.Kind}{migration.Version}";
                Asserts.isFalse(uniqueMigrations.Contains(key));

                uniqueMigrations.Add(key);
                yield return migration;
            }
        }

        private static PrvMigration tryComposeMigration(FileInfo file)
        {
            Match match = migrationFileRegex.Match(file.Name);

            Asserts.isTrue(match.Success);

            string description, statements;
            long version;
            Kind direction;

            version = long.Parse(match.Groups[VERSION].Value);
            direction = (Kind)Enum.Parse(typeof(Kind), match.Groups[MODE].Value, true);
            description = match.Groups[DESCRIPTION].Value ?? string.Empty;
            statements = File.ReadAllText(file.FullName);

            return new PrvMigration(version, direction, description, statements);
        }

        private static bool isNotNull(PrvMigration migration)
        {
            return migration != null;
        }

        private enum Kind
        {
            Downgrade,
            Upgrade,
            Data,
        }

        private class PrvMigration
        {
            public PrvMigration(long version, Kind direction, string description, string statements)
            {
                this.Version = version;
                this.Kind = direction;
                this.Description = description;
                this.Statements = statements;
            }

            public long Version { get; }
            public string Statements { get; }
            public Kind Kind { get; }
            public string Description { get; }
        }
    }
}
