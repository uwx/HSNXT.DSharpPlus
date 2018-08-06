using Newtonsoft.Json;

namespace HSNXT.Test.VLCRPC
{
    public class VLCStatus
    {
        [JsonProperty("apiversion")]
        public long ApiVersion { get; set; }

        [JsonProperty("audiodelay")]
        public long AudioDelay { get; set; }

        [JsonProperty("audiofilters")]
        public Audiofilters AudioFilters { get; set; }

        [JsonProperty("currentplid")]
        public long CurrentPlayingId { get; set; }

        [JsonProperty("equalizer")]
        public object[] Equalizer { get; set; }

        [JsonProperty("fullscreen")]
        public long Fullscreen { get; set; }

        [JsonProperty("information")]
        public Information Information { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("loop")]
        public bool Loop { get; set; }

        [JsonProperty("position")]
        public double Position { get; set; }

        [JsonProperty("random")]
        public bool Random { get; set; }

        [JsonProperty("rate")]
        public long Rate { get; set; }

        [JsonProperty("repeat")]
        public bool Repeat { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("subtitledelay")]
        public long SubtitleDelay { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("videoeffects")]
        public Videoeffects VideoEffects { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }
    }

    public class Videoeffects
    {
        [JsonProperty("brightness")]
        public long Brightness { get; set; }

        [JsonProperty("contrast")]
        public long Contrast { get; set; }

        [JsonProperty("gamma")]
        public long Gamma { get; set; }

        [JsonProperty("hue")]
        public long Hue { get; set; }

        [JsonProperty("saturation")]
        public long Saturation { get; set; }
    }

    public class Stats
    {
        [JsonProperty("averagedemuxbitrate")]
        public long AverageDemuxBitrate { get; set; }

        [JsonProperty("averageinputbitrate")]
        public long AverageInputBitrate { get; set; }

        [JsonProperty("decodedaudio")]
        public long DecodedAudio { get; set; }

        [JsonProperty("decodedvideo")]
        public long DecodedVideo { get; set; }

        [JsonProperty("demuxbitrate")]
        public long DemuxBitrate { get; set; }

        [JsonProperty("demuxcorrupted")]
        public long DemuxCorrupted { get; set; }

        [JsonProperty("demuxdiscontinuity")]
        public long DemuxDiscontinuity { get; set; }

        [JsonProperty("demuxreadbytes")]
        public long DemuxReadBytes { get; set; }

        [JsonProperty("demuxreadpackets")]
        public long DemuxReadPackets { get; set; }

        [JsonProperty("displayedpictures")]
        public long DisplayedPictures { get; set; }

        [JsonProperty("inputbitrate")]
        public long InputBitrate { get; set; }

        [JsonProperty("lostabuffers")]
        public long LostABuffers { get; set; }

        [JsonProperty("lostpictures")]
        public long LostPictures { get; set; }

        [JsonProperty("playedabuffers")]
        public long PlayedABuffers { get; set; }

        [JsonProperty("readbytes")]
        public long ReadBytes { get; set; }

        [JsonProperty("readpackets")]
        public long ReadPackets { get; set; }

        [JsonProperty("sendbitrate")]
        public long SendBitrate { get; set; }

        [JsonProperty("sentbytes")]
        public long SentBytes { get; set; }

        [JsonProperty("sentpackets")]
        public long SentPackets { get; set; }
    }

    public class Information
    {
        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("chapter")]
        public long Chapter { get; set; }

        [JsonProperty("chapters")]
        public object[] Chapters { get; set; }

        [JsonProperty("title")]
        public long Title { get; set; }

        [JsonProperty("titles")]
        public object[] Titles { get; set; }
    }

    public class Category
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("Stream 0")]
        public Stream0 Stream0 { get; set; }
    }

    public class Stream0
    {
        [JsonProperty("Bitrate")]
        public string Bitrate { get; set; }

        [JsonProperty("Bits_per_sample")]
        public string BitsPerSample { get; set; }

        [JsonProperty("Channels")]
        public string Channels { get; set; }

        [JsonProperty("Codec")]
        public string Codec { get; set; }

        [JsonProperty("Sample_rate")]
        public string SampleRate { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }

    public class Meta
    {
        [JsonProperty("ALBUM ARTIST")]
        public string AlbumArtist { get; set; }

        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("artwork_url")]
        public string ArtworkUrl { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("YEAR")]
        public string Year { get; set; }
    }

    public class Audiofilters
    {
        [JsonProperty("filter_0")]
        public string Filter0 { get; set; }
    }

}