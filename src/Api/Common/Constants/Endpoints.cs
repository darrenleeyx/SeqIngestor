namespace Api.Common.Constants;

public class Endpoints
{
    internal const string ApiBase = "api";

    internal static class Seq
    {
        private const string Base = $"{ApiBase}/seq";

        internal const string Ingest = $"{Base}/ingest";
    }
}

