using IdSharp.Tagging.ID3v1;
using IdSharp.Tagging.ID3v2;
using IdSharp.Tagging.ID3v2.Frames;
using MusicLibraryManager.Models;
using System.IO;

namespace MusicLibraryManager.Services
{
    internal class MusicMetadataService
    {
        public static MusicMetadata LoadMetadata(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new MusicMetadata();
            }

            if (ID3v2Tag.DoesTagExist(filePath))
            {
                return LoadID3v2Metadata(filePath);
            }
            else if(ID3v1Tag.DoesTagExist(filePath))
            {
                return LoadID3v1Metadata(filePath);
            }

            return new MusicMetadata();
        }

        private static MusicMetadata LoadID3v1Metadata(string filePath)
        {
            ID3v1Tag tag = new(filePath);

            MusicMetadata musicMetadata = new()
            {
                Title = tag.Title,
                Artist = tag.Artist,
                Album = tag.Album,
                AlbumArtist = tag.Artist,
                TrackNumber = tag.TrackNumber.ToString(),
                DiscNumber = "",
                Year = tag.Year,
                Genre = tag.GenreIndex.ToString(),
                LengthMilliseconds = null
            };

            MusicComment comment = new()
            {
                Value = tag.Comment,
                LanguageCode = "eng",
                Description = ""
            };
            musicMetadata.Comments.Add(comment);

            MusicLyrics lyrics = new()
            {
                Text = "",
                LanguageCode = "",
                ContentDescriptor = ""
            };
            musicMetadata.Lyrics.Add(lyrics);

            return musicMetadata;
        }

        private static MusicMetadata LoadID3v2Metadata(string filePath)
        {
            ID3v2Tag tag = new(filePath);

            MusicMetadata musicMetadata = new()
            {
                Title = tag.Title,
                Artist = tag.Artist,
                Album = tag.Album,
                AlbumArtist = tag.AlbumArtist,
                TrackNumber = tag.TrackNumber,
                DiscNumber = tag.DiscNumber,
                Year = tag.Year,
                Genre = tag.Genre,
                LengthMilliseconds = tag.LengthMilliseconds
            };

            foreach (IComments comment in tag.CommentsList)
            {
                MusicComment musicComment = new()
                {
                    Value = comment.Value,
                    LanguageCode = comment.LanguageCode,
                    Description = comment.Description
                };
                musicMetadata.Comments.Add(musicComment);
            }

            foreach (IUnsynchronizedText lyric in tag.UnsynchronizedLyricsList)
            {
                MusicLyrics musicLyrics = new()
                {
                    Text = lyric.Text,
                    LanguageCode = lyric.LanguageCode,
                    ContentDescriptor = lyric.ContentDescriptor
                };
                musicMetadata.Lyrics.Add(musicLyrics);
            }

            return musicMetadata;
        }

        public static void UpdateMetadata(MusicMetadata metadata, string filePath)
        {
            // Load the existing ID3v2 tag from the file (preserve existing data that are not avaiable in the MusicMetadata object)
            ID3v2Tag tagV2 = new(filePath);
            // Update the tag with the new values available in the MusicMetadata object
            tagV2.Title = metadata.Title;
            tagV2.Artist = metadata.Artist;
            tagV2.Album = metadata.Album;
            tagV2.AlbumArtist = metadata.AlbumArtist;
            tagV2.TrackNumber = metadata.TrackNumber;
            tagV2.DiscNumber = metadata.DiscNumber;
            tagV2.Year = metadata.Year;
            tagV2.Genre = metadata.Genre;

            tagV2.CommentsList.Clear();
            foreach (MusicComment musicComment in metadata.Comments)
            {
                IComments comment = tagV2.CommentsList.AddNew();
                comment.Value = musicComment.Value;
                comment.LanguageCode = musicComment.LanguageCode;
                comment.Description = musicComment.Description;
            }

            tagV2.UnsynchronizedLyricsList.Clear();
            foreach (MusicLyrics musicLyrics in metadata.Lyrics)
            {
                IUnsynchronizedText lyric = tagV2.UnsynchronizedLyricsList.AddNew();
                lyric.Text = musicLyrics.Text;
                lyric.LanguageCode = musicLyrics.LanguageCode;
                lyric.ContentDescriptor = musicLyrics.ContentDescriptor;
            }

            tagV2.Save(filePath);
        }
    }
}
