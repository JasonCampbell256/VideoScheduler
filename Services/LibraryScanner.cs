using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FFMpegCore;
using VideoScheduler.Data;
using VideoScheduler.Data.Models;

namespace VideoScheduler.Services
{
    public class LibraryScanner
    {
        private static readonly string[] VideoExtensions = { 
            ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".mpg", ".mpeg", ".m4v", 
            ".flv", ".f4v", ".webm", ".ogv", ".ogg", ".divx", ".xvid", 
            ".3gp", ".3g2", ".asf", ".rm", ".rmvb", ".vob", ".ts", ".m2ts", 
            ".mts", ".dv", ".ogm", ".mxf"
        };

        private readonly bool _ffprobeAvailable;

        public LibraryScanner()
        {
            var ffprobeName = OperatingSystem.IsWindows() ? "ffprobe.exe" : "ffprobe";
            var localPath = Path.Combine(AppContext.BaseDirectory, ffprobeName);

            if (File.Exists(localPath))
            {
                // Bundled binary next to the executable
                GlobalFFOptions.Configure(opts => opts.BinaryFolder = AppContext.BaseDirectory);
                _ffprobeAvailable = true;
            }
            else
            {
                // Try system PATH
                _ffprobeAvailable = IsFfprobeOnPath(ffprobeName);
            }
        }

        private static bool IsFfprobeOnPath(string ffprobeName)
        {
            try
            {
                var proc = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ffprobeName,
                    Arguments = "-version",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                proc?.WaitForExit(2000);
                return proc?.ExitCode == 0;
            }
            catch { return false; }
        }

        public async Task<(LibraryData Data, List<string> Logs)> ScanLibraryAsync(string rootPath, IProgress<(string Message, int Percent)>? progress = null, HashSet<string>? existingFilePaths = null)
        {
            return await Task.Run(() =>
            {
                var data = new LibraryData();
                var logs = new List<string>();

                if (!Directory.Exists(rootPath))
                {
                    logs.Add($"Error: Directory '{rootPath}' does not exist.");
                    return (data, logs);
                }

                bool skipExisting = existingFilePaths != null && existingFilePaths.Count > 0;
                if (skipExisting)
                    logs.Add("Skip existing files: ON — files already in the library will be ignored.");

                logs.Add($"Scanning root: {rootPath}");
                progress?.Report(("Counting files...", 0));

                // Pre-count files for progress bar
                int totalFiles = 0;
                int processedFiles = 0;
                
                try 
                {
                    if (Directory.Exists(Path.Combine(rootPath, "Shows")))
                        totalFiles += Directory.GetFiles(Path.Combine(rootPath, "Shows"), "*.*", SearchOption.AllDirectories).Count(f => VideoExtensions.Contains(Path.GetExtension(f).ToLower()));
                } catch {}
                try 
                {
                    if (Directory.Exists(Path.Combine(rootPath, "Movies")))
                        totalFiles += Directory.GetFiles(Path.Combine(rootPath, "Movies"), "*.*", SearchOption.AllDirectories).Count(f => VideoExtensions.Contains(Path.GetExtension(f).ToLower()));
                } catch {}
                try 
                {
                    if (Directory.Exists(Path.Combine(rootPath, "Bumpers")))
                        totalFiles += Directory.GetFiles(Path.Combine(rootPath, "Bumpers"), "*.*", SearchOption.AllDirectories).Count(f => VideoExtensions.Contains(Path.GetExtension(f).ToLower()));
                } catch {}
                try 
                {
                    if (Directory.Exists(Path.Combine(rootPath, "Commercials")))
                        totalFiles += Directory.GetFiles(Path.Combine(rootPath, "Commercials"), "*.*", SearchOption.AllDirectories).Count(f => VideoExtensions.Contains(Path.GetExtension(f).ToLower()));
                } catch {}

                if (totalFiles == 0) totalFiles = 1; // Avoid divide by zero

                void ReportProgress(string msg)
                {
                    processedFiles++;
                    int percent = (int)((double)processedFiles / totalFiles * 100);
                    if (percent > 100) percent = 100;
                    progress?.Report((msg, percent));
                }

                // 1. Scan Shows
                var showsPath = Path.Combine(rootPath, "Shows");
                if (Directory.Exists(showsPath))
                {
                    logs.Add($"Found 'Shows' folder. Scanning...");
                    data.Shows = ScanShows(showsPath, logs, ReportProgress, existingFilePaths);
                }
                else
                {
                    logs.Add($"Skipped: 'Shows' folder not found in {rootPath}");
                }

                // 2. Scan Movies
                var moviesPath = Path.Combine(rootPath, "Movies");
                if (Directory.Exists(moviesPath))
                {
                    logs.Add($"Found 'Movies' folder. Scanning...");
                    data.Movies = ScanMovies(moviesPath, logs, ReportProgress, existingFilePaths);
                }
                else
                {
                    logs.Add($"Skipped: 'Movies' folder not found in {rootPath}");
                }

                // 3. Scan Bumpers
                var bumpersPath = Path.Combine(rootPath, "Bumpers");
                if (Directory.Exists(bumpersPath))
                {
                    logs.Add($"Found 'Bumpers' folder.");
                    data.Bumpers = ScanAssets(bumpersPath, ContentType.Bumper, logs, ReportProgress, existingFilePaths);
                }

                // 4. Scan Commercials
                var adsPath = Path.Combine(rootPath, "Commercials");
                if (Directory.Exists(adsPath))
                {
                    logs.Add($"Found 'Commercials' folder.");
                    data.Commercials = ScanAssets(adsPath, ContentType.Commercial, logs, ReportProgress, existingFilePaths);
                }

                progress?.Report(("Scan Complete", 100));
                return (data, logs);
            });
        }

        private int GetDuration(string filePath)
        {
            if (_ffprobeAvailable)
            {
                try
                {
                    var info = FFProbe.Analyse(filePath);
                    return (int)info.Duration.TotalSeconds;
                }
                catch { }
            }

            // Fallback: TagLib# (slower for large files — moov atom may be at end of file)
            try
            {
                using var tfile = TagLib.File.Create(filePath);
                return (int)tfile.Properties.Duration.TotalSeconds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading duration for {filePath}: {ex.Message}");
                return 0;
            }
        }

        private List<Show> ScanShows(string showsRoot, List<string> logs, Action<string> onFileProcessed, HashSet<string>? existingFilePaths = null)
        {
            var shows = new List<Show>();
            var showDirs = Directory.GetDirectories(showsRoot);

            foreach (var showDir in showDirs)
            {
                var showName = Path.GetFileName(showDir);
                // Handle "Show Name (Year)" format
                var yearMatch = Regex.Match(showName, @"^(.*?)\s*\((\d{4})\)$");
                if (yearMatch.Success)
                {
                    showName = yearMatch.Groups[1].Value.Trim();
                }

                logs.Add($"Found Show: {showName}");
                var showId = GenerateId(showName);

                var show = new Show
                {
                    Id = showId,
                    Title = showName,
                    DefaultDurationSec = 1800,
                    Episodes = new List<Episode>()
                };

                // Recursive scan for video files within the show folder
                var files = Directory.GetFiles(showDir, "*.*", SearchOption.AllDirectories)
                    .Where(f => VideoExtensions.Contains(Path.GetExtension(f).ToLower()));

                foreach (var file in files)
                {
                    if (existingFilePaths != null && existingFilePaths.Contains(file))
                    {
                        onFileProcessed?.Invoke($"Skipping (already in library): {Path.GetFileNameWithoutExtension(file)}");
                        continue;
                    }
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    onFileProcessed?.Invoke($"Scanning {showName} - {fileName}");

                    // Try to parse Season and Episode
                    // Strategies:
                    // 1. S01E01, S1E1
                    // 2. 1x01
                    // 3. Season 1 Episode 1
                    // 4. Fallback: Look at parent folder name for Season
                    
                    int seasonNum = 1;
                    int epNum = 0;
                    bool parsed = false;

                    // Regex Strategy 1: S01E01
                    // Use word boundaries to avoid matching inside hashes/ids, but allow "Show.S01E01" (. is boundary)
                    // Removed trailing \b to allow for suffixes like "a", "v2", etc. (e.g. S04E03a)
                    var sXeX = Regex.Match(fileName, @"\bS(\d+)E(\d+)", RegexOptions.IgnoreCase);
                    if (sXeX.Success)
                    {
                        seasonNum = int.Parse(sXeX.Groups[1].Value);
                        epNum = int.Parse(sXeX.Groups[2].Value);
                        parsed = true;
                    }

                    // Regex Strategy 2: 1x01
                    if (!parsed)
                    {
                        // Strict word boundaries are crucial here to avoid matching things like "S04M01X01"
                        var xMatch = Regex.Match(fileName, @"\b(\d+)x(\d+)\b", RegexOptions.IgnoreCase);
                        if (xMatch.Success)
                        {
                            seasonNum = int.Parse(xMatch.Groups[1].Value);
                            epNum = int.Parse(xMatch.Groups[2].Value);
                            parsed = true;
                        }
                    }

                    // Fallback: Check parent folder for "Season X" or "S04"
                    if (!parsed)
                    {
                        var fileDir = Path.GetDirectoryName(file);
                        // Only check parent folder if it's a subfolder of the show root
                        // This prevents "Shows/30 Rock/01.mp4" from being parsed as Season 30
                        if (fileDir != null && !fileDir.Equals(showDir, StringComparison.OrdinalIgnoreCase))
                        {
                            var parentDir = Path.GetFileName(fileDir);
                            if (!string.IsNullOrEmpty(parentDir))
                            {
                                var seasonMatch = Regex.Match(parentDir, @"(Season|S)\s*(\d+)", RegexOptions.IgnoreCase);
                                if (seasonMatch.Success)
                                {
                                    seasonNum = int.Parse(seasonMatch.Groups[2].Value);
                                    // Try to find just a number in the filename
                                    var epNumMatch = Regex.Match(fileName, @"(\d+)");
                                    if (epNumMatch.Success)
                                    {
                                        epNum = int.Parse(epNumMatch.Groups[1].Value);
                                        parsed = true;
                                    }
                                }
                                else if (Regex.IsMatch(parentDir, @"(Specials|Extras|Xtras)", RegexOptions.IgnoreCase))
                                {
                                    seasonNum = 0;
                                    // Try to find just a number in the filename
                                    var epNumMatch = Regex.Match(fileName, @"(\d+)");
                                    if (epNumMatch.Success)
                                    {
                                        epNum = int.Parse(epNumMatch.Groups[1].Value);
                                        parsed = true;
                                    }
                                }
                                else 
                                {
                                    var leadingNumMatch = Regex.Match(parentDir, @"^(\d+)(\D|$)");
                                    if (leadingNumMatch.Success)
                                    {
                                        seasonNum = int.Parse(leadingNumMatch.Groups[1].Value);
                                        var epNumMatch = Regex.Match(fileName, @"(\d+)");
                                        if (epNumMatch.Success)
                                        {
                                            epNum = int.Parse(epNumMatch.Groups[1].Value);
                                            parsed = true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // If still not parsed, maybe it's a flat folder and just has episode numbers?
                    // Or maybe it's a special/movie? Skip for now if we can't determine episode.
                    if (!parsed && epNum == 0)
                    {
                         // Last ditch: just find the first number
                         var anyNum = Regex.Match(fileName, @"(\d+)");
                         if (anyNum.Success) epNum = int.Parse(anyNum.Groups[1].Value);
                    }

                    var epId = $"{showId}-s{seasonNum}-e{epNum}";
                    var duration = GetDuration(file);

                    // Check for duplicates in this scan (e.g. multi-version)
                    if (!show.Episodes.Any(e => e.Season == seasonNum && e.Number == epNum))
                    {
                        show.Episodes.Add(new Episode
                        {
                            Id = epId,
                            ShowId = showId,
                            Title = fileName, // Use filename as title for now
                            Season = seasonNum,
                            Number = epNum,
                            Type = ContentType.Show,
                            FilePath = file,
                            DurationSec = duration
                        });
                    }
                }

                // Sort episodes
                show.Episodes = show.Episodes.OrderBy(e => e.Season).ThenBy(e => e.Number).ToList();
                shows.Add(show);
            }

            return shows;
        }

        private List<Movie> ScanMovies(string root, List<string> logs, Action<string> onFileProcessed, HashSet<string>? existingFilePaths = null)
        {
            var movies = new List<Movie>();
            // Recursive scan
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories)
                .Where(f => VideoExtensions.Contains(Path.GetExtension(f).ToLower()));

            foreach (var file in files)
            {
                if (existingFilePaths != null && existingFilePaths.Contains(file))
                {
                    onFileProcessed?.Invoke($"Skipping (already in library): {Path.GetFileNameWithoutExtension(file)}");
                    continue;
                }
                var name = Path.GetFileNameWithoutExtension(file);
                onFileProcessed?.Invoke($"Scanning {name}");
                var id = GenerateId(name);
                var duration = GetDuration(file);

                movies.Add(new Movie
                {
                    Id = id,
                    Title = name,
                    DurationSec = duration,
                    FilePath = file,
                    Tags = new List<string>()
                });
            }
            logs.Add($"Scanned {root}: Found {movies.Count} movies");

            return movies;
        }

        private List<Asset> ScanAssets(string root, ContentType type, List<string> logs, Action<string> onFileProcessed, HashSet<string>? existingFilePaths = null)
        {
            var assets = new List<Asset>();
            // Recursive scan
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories)
                .Where(f => VideoExtensions.Contains(Path.GetExtension(f).ToLower()));

            foreach (var file in files)
            {
                if (existingFilePaths != null && existingFilePaths.Contains(file))
                {
                    onFileProcessed?.Invoke($"Skipping (already in library): {Path.GetFileNameWithoutExtension(file)}");
                    continue;
                }
                var name = Path.GetFileNameWithoutExtension(file);
                onFileProcessed?.Invoke($"Scanning {name}");
                var id = GenerateId(name);
                var duration = GetDuration(file);

                assets.Add(new Asset
                {
                    Id = id,
                    Title = name,
                    Type = type,
                    DurationSec = duration,
                    FilePath = file,
                    Tags = new List<string>()
                });
            }
            logs.Add($"Scanned {root}: Found {assets.Count} items");

            return assets;
        }

        private string GenerateId(string text)
        {
            // Simple slugify
            return Regex.Replace(text.ToLower(), @"[^a-z0-9]+", "-").Trim('-');
        }
    }
}
