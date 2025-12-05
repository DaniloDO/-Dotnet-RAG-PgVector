using System;

namespace AiIntegratedApp.SeedData;

public static class SeedIngest
{
    public static readonly string[] ContextBase =
    {
    "Bitcoin price fluctuated between $82,605 and $94,900 in early December 2025.",
    "Bitcoin experienced a 23% drop in November 2025, its worst monthly performance since 2022.",
    "ETF outflows totaled $903 million in November 2025, contributing to market pressure.",
    "Analysts observe increasing correlation between Bitcoin and U.S. tech stocks.",
    "JPMorgan forecasts a long-term Bitcoin price target of $240,000.",
    "BitMEX co-founder Arthur Hayes maintains a $250,000 Bitcoin price target for 2025.",
    "Peter Brandt warns of potential Bitcoin price drops to $81,000 or $58,000.",
    "Over $7.5 billion in Bitcoin whale deposits to Binance in 30 days signal possible sell pressure.",
    "Michael Saylor projects Bitcoin could reach $21 million per BTC within two decades.",
    "Bitcoin's 24-hour trading volume increased by 14% in early December 2025, indicating market activity.",
    "MACD turned positive at +787, signaling potential bullish momentum recovery.",
    "Key resistance levels are at $94,200, $95,000, and $98,700 as of early December 2025.",
    "Support levels are identified at $91,500 and $90,000.",
    "Bitcoin market cap dropped over $1 trillion from its peak of $4.3 trillion in late 2025.",
    "S&P Global downgraded Tether’s rating, citing higher-risk reserves and disclosure gaps.",
    "Coinbase Global shares fell 4.8% amid broader crypto market declines.",
    "CME Bitcoin futures showed shrinking premium, indicating reduced bullish sentiment.",
    "Historical data shows Bitcoin averages a 9.7% gain in December, ranking third in performance.",
    "Long-term forecasts suggest Bitcoin could reach $200,000–$300,000 by 2027.",
    "Bitcoin adoption as 'digital gold' is expected to grow in remittances and inflation hedging."
    };

    public static readonly string[] TopicContextBase =
    {
    """
    Bitcoin Price Trends:
    - Price fluctuated between $82,000 and $94,900 in early December 2025.
    - 23% drop in November 2025, worst since 2022.
    - Market cap dropped over $1 trillion from the peak.
    - Key support levels: $91,500 and $90,000.
    """,

    """
    Market Sentiment and Predictions:
    - JPMorgan long-term target: $240,000.
    - Arthur Hayes maintains $250,000 target.
    - Saylor forecasts 21 million per BTC within two decades.
    - Historical averages show 9.7% gain in December.
    """,

    """
    Institutional Signals:
    - ETF outflows totaled $903M in November 2025.
    - Increasing correlation with U.S. tech stocks.
    - Whale deposits to Binance reached $7.5B in 30 days.
    - CME futures premium shrinking indicates reduced sentiment.
    """
    };

}

